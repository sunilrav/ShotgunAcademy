using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShotgunAcademy
{
    public class PlayerInfo
    {
        [Required]
        public string PlayerName { get; set; }

        public string Platform { get; set; }

        public string Role { get; set; }
    }
}
