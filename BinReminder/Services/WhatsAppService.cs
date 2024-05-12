using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using BinReminder.Interfaces;
using BinReminder.Models.BinModels;
using BinReminder.Models.WhatsAppModels;
using BinReminder.Extensions;

namespace BinReminder.Services
{
    public class WhatsAppService : IWhatsAppService
    {
        private readonly HttpClient _client = new();

        public void SendWhatsApp(List<Bin> bins, string to)
        {
            HttpRequestMessage request = new(HttpMethod.Post, AppConfig.WHATSAPP_API_URL);

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Add("Authorization", "Bearer " + AppConfig.WHATSAPP_AUTH_TOKEN);
            var requestBody = new WhatsAppMessageRequestBody { To = to };

            string collectionDateMessage;
            var collectionDate = bins.First().Date;
            if (bins.First().IsBeingCollectedTomorrow)
            {
                requestBody.Template.Name = AppConfig.TOMORROW_TEMPLATE;
                collectionDateMessage = $"the {collectionDate.FullDateWithSuffix()}";
            }
            else
            {
                collectionDateMessage = collectionDate.FullDateWithDayAndSuffix();
            }

            List<string> binColours = bins.Select(b => b.Colour).ToList();
            var message = string.Empty;
            var binColourEmojis = string.Empty;

            switch (binColours.Count)
            {
                case 1:
                    // e.g. the *blue* bin
                    // the embolden extension just wraps the colour name in *
                    // which makes it bold in whatsapp
                    message = binColours.First().Embolden() + " bin";
                    binColourEmojis = GetUnicodeEmoji(binColours.First());
                    break;
                case 2:
                    // e.g the *green*, *brown*, *and blue* bins
                    binColours.ForEach(b => binColourEmojis += GetUnicodeEmoji(b));
                    message = binColours.First().Embolden() + " and " + binColours.Last().Embolden() + " bins";
                    break;
                case > 2:
                {
                    // e.g the *green*, *brown*, *and blue* bins
                    binColours.ForEach(b => binColourEmojis += GetUnicodeEmoji(b));
                    foreach (var binColour in binColours)
                    {
                        if (binColour != binColours.Last())
                            message += binColour.Embolden() + ", ";
                        else
                            message += "and " + binColour.Embolden();
                    }

                    message += " bins";
                    break;
                }
            }

            requestBody.Template.Components.Add(new WhatsAppMessageComponent
            {
                Type = "header",
                Parameters = new List<WhatsAppParameter>
                        {
                            new WhatsAppParameter
                            {
                                Text = binColourEmojis
                            }
                        }
            });

            requestBody.Template.Components.Add(new WhatsAppMessageComponent
            {
                Type = "body",
                Parameters = new List<WhatsAppParameter>
                        {
                            // which bin
                            new WhatsAppParameter
                            {
                                Text = message
                            },
                            // when
                            new WhatsAppParameter
                            {
                                Text = collectionDateMessage
                            }
                        }
            });

            var whatsappRequestBody = JsonConvert.SerializeObject(requestBody);
            request.Content = new StringContent(whatsappRequestBody, Encoding.UTF8, "application/json");

            _client.Send(request);
        }

        // used for error messages
        public void SendWhatsApp(string message, string to)
        {
            HttpRequestMessage request = new(HttpMethod.Post, AppConfig.WHATSAPP_API_URL);

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Add("Authorization", "Bearer " + AppConfig.WHATSAPP_AUTH_TOKEN);
            var requestBody = new WhatsAppMessageRequestBody { To = to };
            requestBody.Template.Name = AppConfig.ERROR_TEMPLATE;
            
            requestBody.Template.Components.Add(new WhatsAppMessageComponent
            {
                Type = "body",
                Parameters = new List<WhatsAppParameter>
                        {
                            // message param
                            new WhatsAppParameter
                            {
                                Text = message
                            }
                        }
            });

            var whatsappRequestBody = JsonConvert.SerializeObject(requestBody);
            request.Content = new StringContent(whatsappRequestBody, Encoding.UTF8, "application/json");

            _client.Send(request);
        }

        private static string GetUnicodeEmoji(string colour)
        {
            return colour.ToLower() switch
            {
                "blue" => AppConfig.BLUE,
                "brown" => AppConfig.BROWN,
                "grey" => AppConfig.GREY,
                "green" => AppConfig.GREEN,
                _ => string.Empty
            };
        }
    }
}
