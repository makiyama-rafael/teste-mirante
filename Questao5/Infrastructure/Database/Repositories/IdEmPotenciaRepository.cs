using Dapper;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.Queries;
using Questao5.Infrastructure.Database.Repositories.Base;
using System.Data;

namespace Questao5.Infrastructure.Database.Repositories;

public interface IIdEmPotenciaRepository : IBaseRepository
{
    public void Incluir(IdEmPotenciaEntity idEmPotenciaEntity);
}

public class IdEmPotenciaRepository : BaseRepository, IIdEmPotenciaRepository
{
    public void Incluir(IdEmPotenciaEntity idEmPotenciaEntity)
    {
        Connection.Execute(IdEmPotenciaQuery.Incluir, new { 
            idEmPotenciaEntity.IdEmPotência, 
            idEmPotenciaEntity.Requisicao, 
            idEmPotenciaEntity.Resultado 
        }, commandType: CommandType.Text);
    }
}
