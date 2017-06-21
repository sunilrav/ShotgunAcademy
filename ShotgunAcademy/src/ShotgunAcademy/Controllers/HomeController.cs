using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using ShotgunAcademy.Models;
using Microsoft.Extensions.Caching.Memory;

namespace ShotgunAcademy.Controllers
{
    public class HomeController : Controller
    {
        static HttpClient client;
        public HomeController()
        {            
            client = new HttpClient();
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("about")]
        public IActionResult About()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Join()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Join(PlayerInfo playerInfo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    SgaExtContext context = HttpContext.RequestServices.GetService(typeof(SgaExtContext)) as SgaExtContext;

                    var rumbleList = new RumbleList
                    {
                        MembershipId = await GetBungiePlayerId(playerInfo.PlayerName, playerInfo.Platform),
                        PlayerName = playerInfo.PlayerName,
                        Platform = playerInfo.Platform
                    };
                    context.InsertPlayerInfo(rumbleList);
                    context.InsertPlayerInfo2(rumbleList);

                    return RedirectToAction("PlayerAdded");
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Sorry we could not add you to the database. Please contact an administrator.");
                    //ModelState.AddModelError(string.Empty, ex.ToString());
                    return View(playerInfo);
                }
            }

            return View(playerInfo);            
        }

        private async static Task<string> GetBungiePlayerId(string gamerTag, string platform)
        {
            var searchDestinyPlayerResults = new SearchDestinyPlayerResults();

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("X-API-KEY", "de7e5188e3af4dd3b5bf481bd7f8f198");
            var response = await client.GetAsync($"http://www.bungie.net/Platform/Destiny/SearchDestinyPlayer/{platform}/{gamerTag}/");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                searchDestinyPlayerResults = JsonConvert.DeserializeObject<SearchDestinyPlayerResults>(json);
                if (searchDestinyPlayerResults.Response.Count > 0)
                    return searchDestinyPlayerResults.Response[0].membershipId;
                else
                    throw new Exception("Could not get Id");
            }
            else
            {
                throw new Exception("Could not get Id");
            }
        }

        public class SearchDestinyPlayerResults
        {
            public List<Response> Response { get; set; }
            public int ErrorCode { get; set; }
            public int ThrottleSeconds { get; set; }
            public string ErrorStatus { get; set; }
            public string Message { get; set; }
        }

        public class Response
        {
            public string iconPath { get; set; }
            public int membershipType { get; set; }
            public string membershipId { get; set; }
            public string displayName { get; set; }
        }
     
        public IActionResult Error()
        {
            return View();
        }

        public IActionResult PlayerAdded()
        {
            return View();
        }

        //DEC2016=1480550400000
        //JAN2016=1483228800000
        //FEB2016=1485907200000

        //static long thisMonthEpoch = 1485907200000;
        //static long lastMonthEpoch = 1483228800000;

        //IMemoryCache _memoryCache;
        //public HomeController(IMemoryCache memoryCache)
        //{
        //    _memoryCache = memoryCache;
        //    client = new HttpClient();
        //}

        //[Route("leaderboard")]
        //public async Task<IActionResult> Leaderboard()
        //{
        //    OrderedPlayers orderedPlayers = await GetOrderedPlayers();

        //    return View(orderedPlayers);
        //}

        //[Route("rumbleleaderboard")]
        //public async Task<IActionResult> RumbleLeaderboard()
        //{
        //    OrderedPlayers orderedPlayers = await GetOrderedPlayersByRank();

        //    return View(orderedPlayers);
        //}

        //[Route("trialsleaderboard")]
        //public async Task<IActionResult> TrialsLeaderboard()
        //{
        //    OrderedPlayers orderedPlayers = await GetOrderedPlayers();

        //    return View(orderedPlayers);
        //}

        //[Route("command")]
        //public async Task<string> Command(string user, string channel, string format, string query, string bot)
        //{
        //    if (query == "rumbleladder")
        //    {
        //        return "This command has been discontinued. Please try these commands instead. !rumblegold !rumblesilver !rumblebronze";
        //    }

        //    if (query == "trialsladder")
        //    {
        //        return "This command has been discontinued. Please try these commands instead. !trialsgold !trialssilver !trialsbronze";
        //    }

        //    if (query == "rumblegold" || query == "rumblesilver" || query == "rumblebronze")
        //    {
        //        var orderedRumblePlayerString = "";

        //        var orderedPlayers = await GetOrderedPlayersByRank();

        //        var rumbleplayers = new List<Player>();
        //        if (query == "rumblegold")
        //        {
        //            rumbleplayers = orderedPlayers.RumblePlayers.Take(10).ToList();
        //        }                    
        //        else if (query == "rumblesilver")
        //        {
        //            rumbleplayers = orderedPlayers.RumblePlayers.Skip(10).Take(10).ToList();
        //        }
        //        else if (query == "rumblebronze")
        //        {
        //            rumbleplayers = orderedPlayers.RumblePlayers.Skip(20).Take(10).ToList();
        //        }

        //        foreach (var player in rumbleplayers)
        //        {
        //            orderedRumblePlayerString += $"```{player.RumbleLadderNo.ToString().PadRight(2)}. {player.GamerTag.ToString()} | {player.RumbleRank}```";
        //        }

        //        orderedRumblePlayerString += "<http://sgacademy.net/rumbleleaderboard>. (" + DateTime.Now + ")";

        //        return orderedRumblePlayerString;

        //    }

        //    if (query == "trialsgold" || query == "trialssilver" || query == "trialsbronze")
        //    {
        //        var orderedTrialsPlayerString = "";

        //        OrderedPlayers orderedPlayers = await GetOrderedPlayers();

        //        var trialsplayers = new List<Player>();
        //        if (query == "trialsgold")
        //        {
        //            trialsplayers = orderedPlayers.TrialsPlayers.Take(10).ToList();
        //        }
        //        else if (query == "trialssilver")
        //        {
        //            trialsplayers = orderedPlayers.TrialsPlayers.Skip(10).Take(10).ToList();
        //        }
        //        else if(query == "trialsbronze")
        //        {
        //            trialsplayers = orderedPlayers.TrialsPlayers.Skip(20).Take(10).ToList();
        //        }

        //        foreach (var player in trialsplayers)
        //        {
        //            orderedTrialsPlayerString += $"```{player.TrialsLadderNo.ToString().PadRight(2)}. {player.GamerTag.ToString()} | {player.TrailsElo}({(player.TrailsElo - player.ThisMonthTrialsElo).ToString("+0;-#")})```";
        //        }

        //        orderedTrialsPlayerString += "<http://sgacademy.net/trialsleaderboard>. (" + DateTime.Now + ")";

        //        return orderedTrialsPlayerString;

        //    }

        //    return user + ": ERROR";
        //}



        //private async Task<OrderedPlayers> GetOrderedPlayers()
        //{
        //    var orderedPlayerFromCache = new OrderedPlayers();
        //    if (!_memoryCache.TryGetValue("orderedplayers", out orderedPlayerFromCache))
        //    {
        //        var gameModeStatList = new List<GameModeStat>();
        //        var chartPoints = new List<ChartPoint>();
        //        var players = GetPlayers("1");
        //        var psplayers = GetPlayers("2");

        //        players.AddRange(psplayers);

        //        foreach (var player in players)
        //        {
        //            if (!String.IsNullOrEmpty(player.BungiePlayerId))
        //            {
        //                //var response = await client.GetAsync($"http://api.guardian.gg/elo/{player.BungiePlayerId}");
        //                //if (response.IsSuccessStatusCode)
        //                //{
        //                //    var json = await response.Content.ReadAsStringAsync();
        //                //    gameModeStatList = JsonConvert.DeserializeObject<List<GameModeStat>>(json);
        //                //    if (gameModeStatList.Count > 0)
        //                //    {
        //                //        player.RumbleElo = Math.Round(gameModeStatList.FirstOrDefault(x => x.Mode == 13).Elo);
        //                //        player.TrailsElo = Math.Round(gameModeStatList.FirstOrDefault(x => x.Mode == 14).Elo);
        //                //    }
        //                //}

        //                var response = await client.GetAsync($"http://api.guardian.gg/chart/elo/{player.BungiePlayerId}");
        //                if (response.IsSuccessStatusCode)
        //                {
        //                    var json = await response.Content.ReadAsStringAsync();
        //                    chartPoints = JsonConvert.DeserializeObject<List<ChartPoint>>(json);

        //                    var rumbleChartPoints = chartPoints.FindAll(x => x.Mode == 13).OrderBy(x => x.X).ToList();
        //                    if (rumbleChartPoints.Count > 0)
        //                        player.RumbleElo = rumbleChartPoints.Last().Y;

        //                    var thisMonthRumbleChartPoints = chartPoints.FindAll(x => x.Mode == 13 && x.X >= thisMonthEpoch).OrderBy(x => x.X).ToList();
        //                    if (thisMonthRumbleChartPoints.Count > 0)
        //                        player.ThisMonthRumbleElo = thisMonthRumbleChartPoints[0].Y;
        //                    else
        //                        player.ThisMonthRumbleElo = int.Parse(player.RumbleElo.ToString());

        //                    var lastMonthAgoRumbleChartPoints = chartPoints.FindAll(x => x.Mode == 13 && (x.X >= lastMonthEpoch && x.X < thisMonthEpoch)).OrderBy(x => x.X).ToList();
        //                    if (lastMonthAgoRumbleChartPoints.Count > 0)
        //                        player.LastMonthRumbleElo = lastMonthAgoRumbleChartPoints[0].Y;

        //                    var trialsChartPoints = chartPoints.FindAll(x => x.Mode == 14).OrderBy(x => x.X).ToList();
        //                    if (trialsChartPoints.Count > 0)
        //                        player.TrailsElo = trialsChartPoints.Last().Y;

        //                    var thisMonthTrialsChartPoints = chartPoints.FindAll(x => x.Mode == 14 && x.X >= thisMonthEpoch).OrderBy(x => x.X).ToList();
        //                    if (thisMonthTrialsChartPoints.Count > 0)
        //                        player.ThisMonthTrialsElo = thisMonthTrialsChartPoints[0].Y;
        //                    else
        //                        player.ThisMonthTrialsElo = int.Parse(player.TrailsElo.ToString());

        //                    var lastMonthAgoTrialsChartPoints = chartPoints.FindAll(x => x.Mode == 14 && (x.X >= lastMonthEpoch && x.X < thisMonthEpoch)).OrderBy(x => x.X).ToList();
        //                    if (lastMonthAgoTrialsChartPoints.Count > 0)
        //                        player.LastMonthTrialsElo = lastMonthAgoTrialsChartPoints[0].Y;
        //                }

        //            }
        //        }

        //        client.Dispose();

        //        var rumblePlayers = players.OrderByDescending(x => x.RumbleElo).ToList();
        //        var trialsPlayers = players.OrderByDescending(x => x.TrailsElo).ToList();

        //        var orderedPlayers = new OrderedPlayers
        //        {
        //            RumblePlayers = rumblePlayers,
        //            TrialsPlayers = trialsPlayers
        //        };

        //        var count1 = 1;
        //        orderedPlayers.RumblePlayers.ForEach(x => x.RumbleLadderNo = count1++);

        //        var count2 = 1;
        //        orderedPlayers.TrialsPlayers.ForEach(x => x.TrialsLadderNo = count2++);

        //        _memoryCache.Set("orderedplayers", orderedPlayers, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(60)));

        //        return orderedPlayers;
        //    }

        //    return orderedPlayerFromCache;
        //}        

        //private void RankPlayers(List<Player> players)
        //{
        //    foreach (var player in players)
        //    {
        //        if (player.RumbleElo < 1300)
        //            player.Rank = 0;

        //        if (player.RumbleElo > 1300)
        //            player.Rank = 1;

        //        if (player.RumbleElo  >= 1400 || player.TrailsElo >= 1500)
        //            player.Rank = 2;

        //        if (player.RumbleElo >= 1450 || player.TrailsElo >= 1550)
        //            player.Rank = 3;

        //        if (player.RumbleElo >= 1500 || player.TrailsElo >= 1700)
        //            player.Rank = 4;

        //        if (player.RumbleElo >= 1650 || player.TrailsElo >= 1800)
        //            player.Rank = 5;

        //    }
        //}

        //private double ConvertToUnixTimestamp(DateTime date)
        //{
        //    DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        //    TimeSpan diff = date.ToUniversalTime() - origin;
        //    return Math.Floor(diff.TotalSeconds);
        //}

        //private List<Player> GetPlayers(string platform)
        //{

        //    var gamertags = new List<Tuple<string,string>>();

        //    if (platform == "1")
        //        gamertags = Database.GetXboxGamerTags();
        //    else
        //        gamertags = Database.GetPSGamerTags();

        //    var players = new List<Player>();

        //    foreach(var gamertag in gamertags)
        //    {
        //        players.Add(new Player { GamerTag = gamertag.Item1, BungiePlayerId = gamertag.Item2, Platform = platform == "1" ? "XB" : "PS" });
        //    }

        //    return players;
        //}

        //private DateTime FromUnixTime(long unixTime)
        //{         
        //    var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        //    return epoch.AddSeconds(unixTime/1000);         
        //}

        [Route("contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [Route("sgapartners")]
        public IActionResult SgaPartners()
        {
            return View();
        }

        //[Route("leaderboardbyrank")]
        //public async Task<IActionResult> LeaderboardByRank()
        //{
        //    OrderedPlayers orderedPlayers = await GetOrderedPlayersByRank();

        //    return View(orderedPlayers);
        //}

        //private async Task<OrderedPlayers> GetOrderedPlayersByRank()
        //{
        //    OrderedPlayers orderedPlayerFromCache;
        //    if (!_memoryCache.TryGetValue("orderedplayersbyrank", out orderedPlayerFromCache))
        //    {
        //        var players = GetPlayers("1");
        //        var psplayers = GetPlayers("2");

        //        players.AddRange(psplayers);

        //        foreach (var player in players)
        //        {
        //            if (!String.IsNullOrEmpty(player.BungiePlayerId))
        //            {
        //                var response = await client.GetAsync($"http://api.guardian.gg/elo/{player.BungiePlayerId}");
        //                if (response.IsSuccessStatusCode)
        //                {
        //                    var json = await response.Content.ReadAsStringAsync();
        //                    var gameModeStatList = JsonConvert.DeserializeObject<List<GameModeStat>>(json);
        //                    if (gameModeStatList.Count <= 0) continue;
        //                    var rumbleRank = gameModeStatList.FirstOrDefault(x => x.Mode == 13);
        //                    if (rumbleRank != null)
        //                        player.RumbleRank = rumbleRank.Rank > 0 ? rumbleRank.Rank : 999999;
        //                    else
        //                        player.RumbleRank = 999999;


        //                    var trialsRank = gameModeStatList.FirstOrDefault(x => x.Mode == 14);
        //                    if (trialsRank != null)
        //                        player.TrailsRank = trialsRank.Rank > 0 ? trialsRank.Rank : 999999;
        //                    else
        //                        player.TrailsRank = 999999;
        //                }                        
        //            }
        //        }

        //        client.Dispose();

        //        var rumblePlayers = players.OrderBy(x => x.RumbleRank).ToList();
        //        var trialsPlayers = players.OrderBy(x => x.TrailsRank).ToList();

        //        var orderedPlayers = new OrderedPlayers
        //        {
        //            RumblePlayers = rumblePlayers,
        //            TrialsPlayers = trialsPlayers
        //        };

        //        var count1 = 1;
        //        orderedPlayers.RumblePlayers.ForEach(x => x.RumbleLadderNo = count1++);

        //        var count2 = 1;
        //        orderedPlayers.TrialsPlayers.ForEach(x => x.TrialsLadderNo = count2++);

        //        _memoryCache.Set("orderedplayersbyrank", orderedPlayers, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(60)));

        //        return orderedPlayers;
        //    }

        //    return orderedPlayerFromCache;
        //}

        //[Route("schedule")]
        //public IActionResult Schedule()
        //{
        //    return View();
        //}
    }
}
