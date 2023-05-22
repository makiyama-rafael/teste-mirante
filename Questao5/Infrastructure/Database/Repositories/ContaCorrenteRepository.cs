using Dapper;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.Queries;
using Questao5.Infrastructure.Database.Repositories.Base;
using System.Data;

namespace Questao5.Infrastructure.Database.Repositories;

public interface IContaCorrenteRepository : IBaseRepository
{
    ContaCorrenteEntity BuscarContaCorrentePorId(string idContaCorrente);
    ContaCorrenteEntity BuscarSaldo(string idContaCorrente);
}
public class ContaCorrenteRepository : BaseRepository, IContaCorrenteRepository
{
    public ContaCorrenteEntity BuscarContaCorrentePorId(string idContaCorrente)
    {
        return Connection.QueryFirstOrDefault<ContaCorrenteEntity>(ContaCorrenteQuery.Buscar, new { idContaCorrente }, commandType : CommandType.Text);
    }

    public ContaCorrenteEntity BuscarSaldo(string idContaCorrente)
    {
        return Connection.QueryFirstOrDefault<ContaCorrenteEntity>(ContaCorrenteQuery.BuscarSaldo, new { idContaCorrente }, commandType: CommandType.Text);
    }
}
