using Questao1;
using Xunit.Abstractions;

namespace ExercicioTestes;

public class Questao1_Testes
{
    public ITestOutputHelper _saidaConsoleTeste;
    ContaBancaria _conta;

    public Questao1_Testes(ITestOutputHelper SaidaConsoleTeste)
    {
        this._saidaConsoleTeste = SaidaConsoleTeste;
        this._conta = new ContaBancaria(0001, "Teste", 0);
    }

    [Fact]
    public void Saque100ComSaldoInicial100()
    {
        _conta = new ContaBancaria(0001, "Teste", 100);
        _conta.Saque(100);

        Assert.Equal(-3.5, _conta.Saldo);
    }

    [Fact]
    public void SaqueNegativo()
    {
        _conta.Deposito(100);

        Assert.Equal(100, _conta.Saldo);
    }

    [Fact]
    public void Deposito100()
    {
        _conta.Deposito(100);

        Assert.Equal(100, _conta.Saldo);
    }
    [Fact]
    public void DepositoNegativo()
    {
        _conta.Deposito(-100);

        Assert.Equal(0, _conta.Saldo);
    }

    [Fact]
    public void Case1Milton()
    {
        _conta = new ContaBancaria(5447, "Milton Gonçalves", 350);

        _conta.Deposito(200);
        _conta.Saque(199);

        Assert.Equal(347.50, _conta.Saldo);
    }

    [Fact]
    public void Case2Elza()
    {
        _conta = new ContaBancaria(5139, "Elza Soares", 0);

        _conta.Deposito(300);
        _conta.Saque(298);

        Assert.Equal(-1.50, _conta.Saldo);
    }

    [Theory]
    [InlineData(5447, "Milton Gonçalves", 's', 350)]
    [InlineData(5139, "Elza Soares", 'n', 0)]
    public void TextoSaida(int numero, string titular, char temDepositoInicial, double valorDepositoInicial)
    {
        if (temDepositoInicial == 's' || temDepositoInicial == 'S')
            _conta = new ContaBancaria(numero, titular, valorDepositoInicial);
        else
            _conta = new ContaBancaria(numero, titular);

        _saidaConsoleTeste.WriteLine("Dados da conta:");
        _saidaConsoleTeste.WriteLine(_conta.ToString());

        Assert.Equal($"Conta {_conta.NumeroConta}, Titular: {_conta.NomeTitular}, Saldo: $ {_conta.Saldo.ToString("N2")}", _conta.ToString());
    }
}