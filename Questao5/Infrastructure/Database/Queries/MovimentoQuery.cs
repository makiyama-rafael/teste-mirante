namespace Questao5.Infrastructure.Database.Queries;

public class MovimentoQuery
{
    public static string Incluir => @"INSERT INTO movimento (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor)
                                      VALUES(:IdMovimento, :IdContaCorrente, :DataMovimento, :TipoMovimento, :Valor);";
}
