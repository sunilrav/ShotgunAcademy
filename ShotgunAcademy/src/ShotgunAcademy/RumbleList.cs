using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShotgunAcademy
{
    public class RumbleList
    {
        private SgaExtContext context;

        public string MembershipId { get; set; }

        public string PlayerName { get; set; }

        public string Platform { get; set; }
    }
}
