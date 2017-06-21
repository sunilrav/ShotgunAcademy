using System.Collections.Generic;

namespace ShotgunAcademy.Models.SearchDestinyPlayer
{
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
}
