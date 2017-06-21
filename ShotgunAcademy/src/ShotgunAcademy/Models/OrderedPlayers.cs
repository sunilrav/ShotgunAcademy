using System.Collections.Generic;

namespace ShotgunAcademy.Models
{
    public class OrderedPlayers
    {
        public List<Player> RumblePlayers { get; set; }
        public List<Player> TrialsPlayers { get; set; }

        public List<Player> SrlCMPlayers { get; set; }
        public List<Player> SrlIDPlayers { get; set; }
        public List<Player> SrlHPPlayers { get; set; }
        public List<Player> SrlSSPlayers { get; set; }

    }
}

