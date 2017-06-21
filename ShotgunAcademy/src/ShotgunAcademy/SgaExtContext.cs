using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShotgunAcademy
{
    public class SgaExtContext
    {
        public string ConnectionString { get; set; }

        public SgaExtContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public List<RumbleList> GetAllRumblePlayers()
        {
            var list = new List<RumbleList>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from rumbleList", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new RumbleList()
                        {
                            MembershipId = reader["membershipid"].ToString(),
                            PlayerName = reader["playerName"].ToString(),
                        });
                    }
                }
            }
            return list;
        }

        public bool InsertPlayerInfo(RumbleList rumbleList)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("insert into rumbleList(membershipid, playerName, platform) values (?membershipid,?playerName,?platform);", conn);

                cmd.Parameters.AddWithValue("?playerName", rumbleList.PlayerName);
                cmd.Parameters.AddWithValue("?platform", rumbleList.Platform);
                cmd.Parameters.AddWithValue("?membershipid", rumbleList.MembershipId);

                cmd.ExecuteNonQuery();
            }

            return true;
        }

        public bool InsertPlayerInfo2(RumbleList rumbleList)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("insert into trialsList(membershipid, playerName, platform) values (?membershipid,?playerName,?platform);", conn);

                cmd.Parameters.AddWithValue("?playerName", rumbleList.PlayerName);
                cmd.Parameters.AddWithValue("?platform", rumbleList.Platform);
                cmd.Parameters.AddWithValue("?membershipid", rumbleList.MembershipId);

                cmd.ExecuteNonQuery();
            }

            return true;
        }
    }
}
