namespace Questao5.Domain.Entities;

public class IdEmPotenciaEntity
{
    public string IdEmPotência { get; }
    public string Requisicao { get; }
    public string Resultado { get; }

    public IdEmPotenciaEntity(string idEmPotência, string requisicao, string response)
    {
        this.IdEmPotência = idEmPotência;
        this.Requisicao = requisicao;
        this.Resultado = response;
    }
}
