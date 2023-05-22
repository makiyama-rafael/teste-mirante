using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Newtonsoft.Json;
using Questao5.Domain.Applications;
using Questao5.Domain.Entities;
using Questao5.Domain.Profiles;
using Questao5.Infrastructure.Database.DTOs;
using Questao5.Infrastructure.Database.Repositories;
using Questao5.Infrastructure.Sqlite;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.IO;
using System.Net;
using static System.Net.WebRequestMethods;

namespace Questao5.Infrastructure.Services.Controllers;

[ApiController]
[Route("[controller]")]
public class ContaCorrenteController : ControllerBase
{
    private readonly ILogger<ContaCorrenteController> _logger;
    private readonly DatabaseConfig _databaseConfig;
    private readonly IContaCorrenteRepository _contaCorrenteRepository;
    private readonly IMovimentoRepository _movimentoRepository;
    private readonly IIdEmPotenciaRepository _idEmPotenciaRepository;
    private readonly IValidations _validations;

    private IMapper _mapper;
    SqliteConnection _conexao;

    public ContaCorrenteController(ILogger<ContaCorrenteController> logger, DatabaseConfig databaseConfig
                                   , IContaCorrenteRepository contaCorrenteRepository, IMovimentoRepository movimentoRepository
                                   , IIdEmPotenciaRepository idEmPotenciaRepository, IValidations validations)
    {
        _logger = logger;
        _databaseConfig = databaseConfig;
        _contaCorrenteRepository = contaCorrenteRepository;
        _movimentoRepository = movimentoRepository;
        _conexao = new SqliteConnection(_databaseConfig.Name);
        _idEmPotenciaRepository = idEmPotenciaRepository;
        _validations = validations;

        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MovimentaContaProfile>();
        });

        _mapper = configuration.CreateMapper();
    }

    /// <summary>Adiciona um movimento de crédito ou débito para o cliente</summary>                                  
    /// <param name="IdEmPotência">ID da request</param>
    /// <param name="IdContaCorrente">ID da conta do cliente</param>
    /// <param name="TipoMovimento">Tipo de movimento (C = Crédito, D = Débito)</param>
    /// <param name="Valor"> Valor do movimento (maior que 0)</param>
    /// <returns>IActionResult</returns>                                                         
    /// <response code="201">Caso o movimento seja realizado com sucesso</response>  
    /// <response code="400">Caso haja algum problema de validação</response>
    /// <response code="500">Caso haja algum outro problema</response>
    [HttpPost]
    [Route("api/[controller]/Movimentar/")]
    public IActionResult MovimentaConta(MovimentaContaDTO movimentaContaDTO)
    {
        try
        {
            var movimento = _mapper.Map<MovimentaContaDTO, MovimentaContaEntity>(movimentaContaDTO);
            ConfigurarConexao();
            _validations.ValidarMovimento(movimento);

            var transacao = _conexao.BeginTransaction();
            try
            {
                _movimentoRepository.Incluir(movimento);
                _idEmPotenciaRepository.Incluir(new IdEmPotenciaEntity(movimentaContaDTO.IdEmPotência, JsonConvert.SerializeObject(movimentaContaDTO), JsonConvert.SerializeObject(new { IdMovimento = movimento.IdMovimento })));
                transacao.Commit();
            }
            catch (SqliteException sqex)
            {
                transacao.Rollback();
                if (sqex.Message.Contains("UNIQUE"))
                    throw;
                else
                    throw new Exception();
            }
            catch (Exception ex)
            {
                transacao.Rollback();
                throw;
            }

            return CreatedAtAction(nameof(MovimentaConta), new { idMovimento = movimento.IdMovimento });
        }
        catch (SqliteException sqex)
        {
            _logger.LogError(sqex.Message);
            return BadRequest("idEmPotência duplicado");
        }
        catch (ValidationException vex)
        {
            _logger.LogError(vex.Message);
            return BadRequest(vex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest("System temporarily unavailable");
        }
        finally
        {
            FecharConexao();
        }
    }

    /// <summary>Retorna os dados do cliente, sumarizando seu saldo </summary>                                  
    /// <param name="idContaCorrente"> id da conta do cliente</param>
    /// <returns>IActionResult</returns>                                                         
    /// <response code="200">Caso inserção seja feita com sucesso</response>
    /// <response code="400">Caso haja algum problema de validação</response>
    /// <response code="500">Caso haja algum outro problema</response>
    [HttpGet]
    [Route("api/[controller]/Saldo/")]
    public IActionResult SaldoContaCorrente([FromQuery] string idContaCorrente)
    {
        try
        {
            ConfigurarConexao();
            _validations.ValidarConta(idContaCorrente);
            var contaCorrente = _contaCorrenteRepository.BuscarSaldo(idContaCorrente);

            return Ok(new
            {
                NumeroConta = contaCorrente.Numero,
                NomeTitular = contaCorrente.Nome,
                DataConsulta = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                SaldoAtual = contaCorrente.Saldo.ToString("N2")
            });
        }
        catch (ValidationException vex)
        {
            _logger.LogError(vex.Message);
            return BadRequest(vex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(500);
        }
        finally
        {
            FecharConexao();
        }
    }

    private void ConfigurarConexao()
    {
        _conexao.Open();
        _contaCorrenteRepository.SetConnection(_conexao);
        _movimentoRepository.SetConnection(_conexao);
        _idEmPotenciaRepository.SetConnection(_conexao);
    }

    private void FecharConexao()
    {
        _conexao.Close();
        _conexao.Dispose();
    }
}