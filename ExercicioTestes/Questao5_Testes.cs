using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.Repositories;
using Questao5.Infrastructure.Sqlite;
using System.IO;
using Xunit.Abstractions;

namespace ExercicioTestes;

public class Questao5_Testes
{
    public ITestOutputHelper _saidaConsoleTeste;
    SqliteConnection _conexao;

    public Questao5_Testes(ITestOutputHelper SaidaConsoleTeste)
    {
        this._saidaConsoleTeste = SaidaConsoleTeste;
        this._conexao = new SqliteConnection($"Data Source=C:\\sqlite3\\questao-5.db");
    }

    [Fact]
    public void IncluirMovimento()
    {
        bool inseriu = false;
        _conexao.Open();
        var repository = new MovimentoRepository();
        repository.SetConnection(_conexao);

        try
        {
            var transaction = _conexao.BeginTransaction();

            var movimento = new MovimentaContaEntity();
            movimento.IdContaCorrente = "D2E02051-7067-ED11-94C0-835DFA4A16C9";
            movimento.TipoMovimento = 'C';
            movimento.Valor = 0.01;

            _saidaConsoleTeste.WriteLine($"Id Movimento: {movimento.IdMovimento}");
            _saidaConsoleTeste.WriteLine($"Data Movimento: {movimento.DataMovimento.ToString("dd/MM/yyyy")}");
            _saidaConsoleTeste.WriteLine($"Id Conta Corrente: {movimento.IdContaCorrente}");
            _saidaConsoleTeste.WriteLine($"Tipo Movimento: {movimento.TipoMovimento.ToString()}");
            _saidaConsoleTeste.WriteLine($"Valor: {movimento.Valor.ToString("N2")}");

            repository.Incluir(movimento);
            inseriu = true;

            transaction.Rollback();
        }
        catch (Exception ex)
        {
            _saidaConsoleTeste.WriteLine(ex.Message);
        }

        FechaConexao();

        Assert.True(inseriu);
    }

    [Fact]
    public void IncluirIdEmPotencia()
    {
        bool inseriu = false;
        _conexao.Open();
        var repository = new IdEmPotenciaRepository();
        repository.SetConnection(_conexao);

        try
        {
            var transaction = _conexao.BeginTransaction();

            repository.Incluir(new IdEmPotenciaEntity("123", "", ""));
            inseriu = true;

            transaction.Rollback();
        }
        catch(Exception ex)
        {
            _saidaConsoleTeste.WriteLine(ex.Message);
        }

        FechaConexao();

        Assert.True(inseriu);
    }

    [Fact]
    public void BuscaSaldo()
    {
        _conexao.Open();
        var repository = new ContaCorrenteRepository();
        repository.SetConnection(_conexao);

        var retorno = repository.BuscarSaldo("D2E02051-7067-ED11-94C0-835DFA4A16C9");

        _saidaConsoleTeste.WriteLine($"Numero Conta: {retorno.Numero.ToString()}");
        _saidaConsoleTeste.WriteLine($"Nome Titular: {retorno.Nome}");
        _saidaConsoleTeste.WriteLine($"Ativo: {retorno.Ativo.ToString()}");
        _saidaConsoleTeste.WriteLine($"Saldo: {retorno.Saldo.ToString("N2")}");

        FechaConexao();

        Assert.Equal(0, retorno.Saldo);
    }

    [Fact]
    public void BuscarContaCorrentePorId()
    {
        _conexao.Open();
        var repository = new ContaCorrenteRepository();
        repository.SetConnection(_conexao);

        var retorno = repository.BuscarContaCorrentePorId("FA99D033-7067-ED11-96C6-7C5DFA4A16C9");

        _saidaConsoleTeste.WriteLine($"Numero Conta: {retorno.Numero.ToString()}");
        _saidaConsoleTeste.WriteLine($"Nome Titular: {retorno.Nome}");
        _saidaConsoleTeste.WriteLine($"Ativo: {retorno.Ativo.ToString()}");

        FechaConexao();

        Assert.Equal("Eva Woodward", retorno.Nome);
    }

    void FechaConexao()
    {
        _conexao.Close();
        _conexao.Dispose();
    }
}
