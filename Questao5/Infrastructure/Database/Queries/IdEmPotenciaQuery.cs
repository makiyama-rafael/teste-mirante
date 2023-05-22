namespace Questao5.Infrastructure.Database.Queries;

public class IdEmPotenciaQuery
{
    public static string Incluir => @"INSERT INTO idempotencia (chave_idempotencia, requisicao, resultado)
                                      VALUES(:IdEmPotência, :Requisicao, :Resultado);";
}
