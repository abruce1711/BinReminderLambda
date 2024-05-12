using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using BinReminder.Interfaces;
using BinReminder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BinReminder.Services
{
    internal class DynamoService : IDynamoService
    {
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var tableName = Environment.GetEnvironmentVariable("BIN_REMINDER_TABLE_NAME");
            var client = new AmazonDynamoDBClient();
            List<string> userProps = typeof(User).GetProperties().Select(p => p.Name).ToList();


            ScanResponse dbResponse = new ScanResponse();
            var response = new List<User>();
            try
            {
                dbResponse = await client.ScanAsync(tableName, userProps);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            if (dbResponse.HttpStatusCode == HttpStatusCode.OK)
            {
                foreach (var dbItem in dbResponse.Items)
                {
                    User user = new User()
                    {
                        FirstName = dbItem.GetValueOrDefault("FirstName").S ?? string.Empty,
                        LastName = dbItem.GetValueOrDefault("LastName").S ?? string.Empty,
                        HouseNumber = dbItem.GetValueOrDefault("HouseNumber").S ?? string.Empty,
                        PostCode = dbItem.GetValueOrDefault("PostCode").S ?? string.Empty,
                        MobileNumber = dbItem.GetValueOrDefault("MobileNumber").S ?? string.Empty
                    };

                    response.Add(user);
                }

                return response;
            }

            return response;
        }
    }
}
