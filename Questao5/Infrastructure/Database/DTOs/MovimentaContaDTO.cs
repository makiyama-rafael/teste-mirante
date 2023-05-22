using System.ComponentModel.DataAnnotations;

namespace Questao5.Infrastructure.Database.DTOs;

public class MovimentaContaDTO
{
    public MovimentaContaDTO() { }
    public MovimentaContaDTO(string idEmPotência, string idContaCorrente, char tipoMovimento, double valor)
    {
        this.IdEmPotência = idEmPotência;
        this.IdContaCorrente = idContaCorrente;
        this.TipoMovimento = tipoMovimento;
        this.Valor = valor;
    }

    [Required]
    [MaxLength(37, ErrorMessage = "INVALID_ID_REQUEST")]
    public string IdEmPotência { get; set; }

    [Required]
    [MaxLength(37, ErrorMessage = "INVALID_ACCOUNT_ID")]
    public string IdContaCorrente { get; set; }

    [Required]
    public char TipoMovimento { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "INVALID_VALUE")]
    public double Valor { get; set; }
}
