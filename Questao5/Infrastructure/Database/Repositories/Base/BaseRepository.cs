using Microsoft.Data.Sqlite;
using System.Data;
using System.Security.Cryptography.X509Certificates;

namespace Questao5.Infrastructure.Database.Repositories.Base;

public interface IBaseRepository
{
    void SetConnection(SqliteConnection connection);
}
public class BaseRepository
{
    public IDbConnection Connection { get; private set; }
    public void SetConnection(SqliteConnection connection) => Connection = connection;
}
