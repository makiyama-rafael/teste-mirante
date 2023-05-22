using Newtonsoft.Json;
using Questao2.Services;
using Questao2.Services.Responses;

namespace Questao2;

public class Program
{
    public static void Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    public static int getTotalScoredGoals(string team, int year)
    {
        int totalGoals = 0;
        for (int i = 1; i <= 2; i++)
        {
            var footballMatchesResponse = new FootballMatchesResponse();
            int j = 1;
            do
            {
                string url = $"https://jsonmock.hackerrank.com/api/football_matches?year={year}&team{i}={team.Replace(" ", "%20")}&page={j}";
                footballMatchesResponse = FootballMatchesService.GetFootballMatches(url);
                totalGoals += i == 1 ? footballMatchesResponse.data.Sum(c => Convert.ToInt32(c.team1goals)) : footballMatchesResponse.data.Sum(c => Convert.ToInt32(c.team2goals));

                j++;
            }
            while (footballMatchesResponse.total_pages >= j);
        }

        return totalGoals;
    }

}