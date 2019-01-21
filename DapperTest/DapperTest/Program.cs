using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Configuration;
using Dapper;
namespace DapperTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string insertSql = "Insert into people values (@fullName, @phoneNumber, @email)";
            string selectSql = "select * from people";
            var providerName = ConfigurationManager.ConnectionStrings["DapperConnection"].ProviderName;
            var providerFactory = DbProviderFactories.GetFactory(providerName);
            using (var connection = providerFactory.CreateConnection())
            {
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["DapperConnection"].ConnectionString;
                var person = new Person
                {
                   
                    FullName = "Ivan",
                    PhoneNumber = "+77018882628",
                    Email = "Ivan@gmail.com"
                };
                
              var result = connection.Execute(insertSql, person);
               
                /* connection.Execute(insertSql, new
                 {
                     person.Id,
                     person.FullName,
                     person.PhoneNumber,
                     person.Email });
                }*/

                if (result != 1) Console.WriteLine("Error!");
                else
                {
                    var people = connection.Query<Person>(selectSql);
                }
                Console.ReadLine();
            }
        }
    }
}
