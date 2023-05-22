namespace Questao5.Domain.Entities;

public class MovimentaContaEntity
{
    public string IdMovimento { get; }
    public string IdContaCorrente { get; set; }
    public DateTime DataMovimento { get;}
    public char TipoMovimento { get; set; }
    public double Valor { get; set; }

    public MovimentaContaEntity()
    {
        this.IdMovimento = Guid.NewGuid().ToString();
        this.DataMovimento = DateTime.Now;
    }
}
