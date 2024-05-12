using Amazon.Lambda.Core;
using BinReminder.Interfaces;
using BinReminder.Models;
using BinReminder.Models.BinModels;
using BinReminder.Services;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace BinReminder;

public class Function
{
    private readonly IWhatsAppService _whatsAppService = new WhatsAppService();
    private readonly ICouncilService _councilService = new CouncilService();
    private readonly IDynamoService _dynamoService = new DynamoService();

    // This doesn't really need to return anything but it's good to leave here for now for testing
    public async Task FunctionHandler()
    {
        var appUserResponse = await _dynamoService.GetAllUsers();
        List<User> appUsers = appUserResponse.ToList();
        var mobileNumberList = appUsers.Select(u => u.MobileNumber);

        string authKey = await _councilService.GetAuthKey();

        if (string.IsNullOrEmpty(authKey))
        {
            foreach (var to in mobileNumberList)
                _whatsAppService.SendWhatsApp("Failed to get auth key", to);

            return;
        }

        foreach(var user in appUsers)
        {
            string uprn = await _councilService.GetPropertyUprn(authKey, user.PostCode, user.HouseNumber);

            if (string.IsNullOrEmpty(uprn))
            {
                _whatsAppService.SendWhatsApp($"Could not find bin information for post code {user.PostCode}" +
                    $" and house number {user.HouseNumber}. Please double check address is correct.", user.MobileNumber);
                return;
            }

            string requestBody = JsonConvert.SerializeObject(new BinCalendarRequestBody(uprn));

            var binsResponse = await _councilService.GetThisWeeksBins(authKey, requestBody);
            var thisWeeksBins = binsResponse.ToList();

            if (thisWeeksBins.Count == 0)
            {
                _whatsAppService.SendWhatsApp("Authorised with council but failed to get bins", user.MobileNumber);
                return;
            }
            // sends reminders only for bins being collected in the next two days
            else if (thisWeeksBins.First().IsBeingCollectedTomorrowOrTheNextDay)
            {
                _whatsAppService.SendWhatsApp(thisWeeksBins, user.MobileNumber);
            }
        }
    }
}






