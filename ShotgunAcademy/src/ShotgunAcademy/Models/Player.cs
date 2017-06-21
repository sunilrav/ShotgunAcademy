using System.Collections.Generic;

namespace ShotgunAcademy.Models
{
    public class Player
    {
        public string GamerTag { get; set; }
        public string BungiePlayerId { get; set; }
        public double RumbleElo { get; set; }
        public double TrailsElo { get; set; }
        public int Rank { get; set; }
        public string Platform { get; set; }

        public int ThisMonthRumbleElo { get; set; }
        public int ThisMonthTrialsElo { get; set; }
        public int LastMonthRumbleElo { get; set; }
        public int LastMonthTrialsElo { get; set; }        
        public long CampusMartiusTime { get; set; }
        public string CampusMartiusTimeDisplay { get; internal set; }

        public long InfiniteDecentTime { get; set; }
        public string InfiniteDecentTimeDisplay { get; set; }

        public long HaakonPrincipleTime { get; internal set; }
        public string HaakonPrincipleTimeDisplay { get; internal set; }

        public long ShiningSandsTime { get; internal set; }
        public string ShiningSandsTimeDisplay { get; internal set; }


        public int RumbleLadderNo { get; set; }
        public int TrialsLadderNo { get; set; }        
        public int CMLadderNo { get; set; }
        public int IDLadderNo { get; set; }
        public int HPLadderNo { get; set; }
        public int SSLadderNo { get; set; }


        public double RumbleRank { get; set; }
        public double TrailsRank { get; set; }


    }
}
