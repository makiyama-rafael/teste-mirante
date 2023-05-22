namespace Questao5.Infrastructure.Database.Queries;

internal class ContaCorrenteQuery
{
    public static string Buscar => @"SELECT 
                                           numero, 
                                           nome, 
                                           ativo 
                                   FROM contacorrente 
                                   WHERE idcontacorrente = :idContaCorrente;";

    public static string BuscarSaldo => @"SELECT 
                                                c.numero, 
                                                c.nome,
                                                COALESCE(SUM(CASE m.tipomovimento
                                                    WHEN 'C'
                                                        THEN m.valor
                                                    ELSE -(m.valor)
                                                END), 0) saldo 
                                          FROM contacorrente as c
                                          LEFT JOIN movimento as m on m.idcontacorrente = c.idcontacorrente
                                          WHERE c.idcontacorrente = :idContaCorrente
                                          GROUP BY c.numero, c.nome;";
}
