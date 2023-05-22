using Questao2.Services.Responses;
using System.Net.Http.Json;

namespace Questao2.Services;

public class FootballMatchesService
{
    static HttpClient client = new HttpClient();
    public static FootballMatchesResponse GetFootballMatches(string url)
    {
        FootballMatchesResponse? footballMatchesResponse = null;
        HttpResponseMessage response = client.GetAsync(url).Result;
        if (response.IsSuccessStatusCode)
        {
            footballMatchesResponse = response.Content.ReadFromJsonAsync<FootballMatchesResponse>().Result;
        }
        return footballMatchesResponse;
    }
}
