using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace HomeTask
{
    class Program
    {
        static void Main(string[] args)
        {
            string insertPlayer = "Insert into Player values (@Name, @Number, @TeamId)";
            string insertTeam = "Insert into Team values (@Id, @Name)";

            string selectPlayer = "select * from Team";
            string selectTeam = "select * from people";
            var providerName = ConfigurationManager.ConnectionStrings["DapperConnection"].ProviderName;
            var providerFactory = DbProviderFactories.GetFactory(providerName);
            using (var connection = providerFactory.CreateConnection())
            {
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["DapperConnection"].ConnectionString;
                var player = new Player
                {
                  
                    Name = "Jehnsen Ackles",
                    Number = 10,
                    TeamId = 1

                };

                var team = new Team
                {
                    Id = 1,
                    Name = "Winners"                  
                };

                var result = connection.Execute(insertPlayer, player);
                var result1 = connection.Execute(insertTeam, team);

                /* connection.Execute(insertSql, new
                 {
                     person.Id,
                     person.FullName,
                     person.PhoneNumber,
                     person.Email });
                }*/

                var people1 = connection.Query<Team>(selectPlayer);
                if (result != 1) Console.WriteLine("Error!");
                else
                {
                    var people = connection.Query<Player>(selectPlayer);
                }
                Console.ReadLine();
            }
        }
    }
}
