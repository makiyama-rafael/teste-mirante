using Questao2;
using Xunit.Abstractions;

namespace ExercicioTestes;

public class Questao2_Testes
{
    public ITestOutputHelper _saidaConsoleTeste;
    string _url;

    public Questao2_Testes(ITestOutputHelper SaidaConsoleTeste)
    {
        this._saidaConsoleTeste = SaidaConsoleTeste;
        _url = $"https://jsonmock.hackerrank.com/api/football_matches";
    }

    [Fact]
    public void Case1ParisSaintGermain2013()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;

        int totalGoals = Program.getTotalScoredGoals(teamName, year);

        Assert.Equal(109, totalGoals);
    }

    [Fact]
    public void Case2Chelsea2014()
    {
        string teamName = "Chelsea";
        int year = 2014;

        int totalGoals = Program.getTotalScoredGoals(teamName, year);

        Assert.Equal(92, totalGoals);
    }

    [Fact]
    public void PaginacaoAPI()
    {
        _url += "?page=5";
        var resultado = Questao2.Services.FootballMatchesService.GetFootballMatches(_url);

        _saidaConsoleTeste.WriteLine($"Página: {resultado.page.ToString()}");
        Assert.Equal(5, resultado.page);
    }

    [Fact]
    public void FiltrosTime1EAnoAPI()
    {
        var teste = false;
        _url += "?year=2012&team1=Chelsea";
        var resultado = Questao2.Services.FootballMatchesService.GetFootballMatches(_url);

        if(resultado.data.All(c => c.team1 == "Chelsea") && resultado.data.All(c => c.year == 2012))
            teste = true;

        Assert.True(teste);
    }

    [Fact]
    public void ConexaoAPI()
    {
        var resultado = Questao2.Services.FootballMatchesService.GetFootballMatches(_url);

        Assert.NotNull(resultado);
    }
}
