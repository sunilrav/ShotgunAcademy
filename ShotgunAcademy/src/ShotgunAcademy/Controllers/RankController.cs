using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShotgunAcademy.Models.SearchDestinyPlayer;

namespace ShotgunAcademy.Controllers
{
    public class RankController : Controller
    {
		static HttpClient client;

		public IActionResult Index()
        {
            return View();
        }

		//private async static Task<string> GetBungiePlayerId(string gamerTag, string platform)
		//{
		//	var searchDestinyPlayerResults = new SearchDestinyPlayerResults();

		//	client.DefaultRequestHeaders.Clear();
		//	client.DefaultRequestHeaders.Add("X-API-KEY", "de7e5188e3af4dd3b5bf481bd7f8f198");
		//	var response = await client.GetAsync($"http://www.bungie.net/Platform/Destiny/SearchDestinyPlayer/{platform}/{gamerTag}/");
		//	if (response.IsSuccessStatusCode)
		//	{
		//		var json = await response.Content.ReadAsStringAsync();
		//		searchDestinyPlayerResults = JsonConvert.DeserializeObject<SearchDestinyPlayerResults>(json);
		//		if (searchDestinyPlayerResults.Response.Count > 0)
		//			return searchDestinyPlayerResults.Response[0].membershipId;
		//		else
		//			return "BAD: " + gamerTag;
		//	}
		//	else
		//	{
		//		return "BAD: " + gamerTag;
		//	}
		//}
	}
}
