using Dapper;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.Queries;
using Questao5.Infrastructure.Database.Repositories.Base;
using System.Data;

namespace Questao5.Infrastructure.Database.Repositories;

public interface IMovimentoRepository : IBaseRepository
{
    public void Incluir(MovimentaContaEntity movimentaContaEntity);
}

public class MovimentoRepository : BaseRepository, IMovimentoRepository
{
    public void Incluir(MovimentaContaEntity movimentaContaEntity)
    {
        Connection.Execute(MovimentoQuery.Incluir, new { 
            movimentaContaEntity.IdMovimento, 
            movimentaContaEntity.IdContaCorrente, 
            DataMovimento = movimentaContaEntity.DataMovimento.ToString("dd/MM/yyyy"), 
            movimentaContaEntity.TipoMovimento, 
            movimentaContaEntity.Valor 
        }, commandType : CommandType.Text);
    }
}
