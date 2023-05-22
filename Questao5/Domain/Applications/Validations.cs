using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.Repositories;
using System.ComponentModel.DataAnnotations;

namespace Questao5.Domain.Applications;

public interface IValidations
{
    public void ValidarMovimento(MovimentaContaEntity movimento);
    public void ValidarConta(string idContaCorrente);
}

public class Validations : IValidations
{

    private readonly IContaCorrenteRepository _contaCorrenteRepository;

    public Validations(IContaCorrenteRepository contaCorrenteRepository)
    {
        _contaCorrenteRepository = contaCorrenteRepository;
    }

    public void ValidarMovimento(MovimentaContaEntity movimento)
    {
        char[] tipoMovimentoValidos = { 'C', 'D' };
        if (!tipoMovimentoValidos.Contains(movimento.TipoMovimento))
            throw new ValidationException("INVALID_TYPE");

        ValidarConta(movimento.IdContaCorrente);
    }

    public void ValidarConta(string idContaCorrente)
    {
        var contaCorrente = _contaCorrenteRepository.BuscarContaCorrentePorId(idContaCorrente);
        if (contaCorrente == null)
            throw new ValidationException("INVALID_ACCOUNT");

        if (contaCorrente.Ativo == 0)
            throw new ValidationException("INACTIVE_ACCOUNT");
    }
}
