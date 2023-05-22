using System.Globalization;

namespace Questao1;

public class ContaBancaria
{
    public ContaBancaria(int numeroConta, string nomeTitular)
    {
        this.NumeroConta = numeroConta;
        this.NomeTitular = nomeTitular;
    }

    public ContaBancaria(int numeroConta, string nomeTitular, double depositoInicial)
    {
        this.NumeroConta = numeroConta;
        this.NomeTitular = nomeTitular;

        this.Deposito(depositoInicial);
    }

    public int NumeroConta { get; private set; }
    public string NomeTitular { get; set; }
    public double Saldo { get; private set; }

    private double _tarifaSaque = 3.5;

    public void Deposito(double quantia)
    {
        if (quantia > 0)
            this.Saldo += quantia;
    }

    public void Saque(double quantia)
    {
        if(quantia > 0)
        {
            this.Saldo -= quantia;
            this.Saldo -= _tarifaSaque;
        }
    }

    public override string ToString()
    {
        return $"Conta {this.NumeroConta}, Titular: {this.NomeTitular}, Saldo: $ {this.Saldo.ToString("N2")}";
    }
}
