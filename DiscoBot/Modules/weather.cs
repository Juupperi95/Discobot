using Discord;
using Discord.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DiscoBot.Modules
{
    public class Weather : ModuleBase<SocketCommandContext>
    {
        [Command("weather")]
        public async Task WeatherAsync([Remainder] string location)
        {
            string appid = System.IO.File.ReadAllText(@"weatherkey.txt");
            string weatherurl = "http://api.openweathermap.org/data/2.5/weather?q=" + location + "&units=metric&lang=fi&APPID=" + appid;
            await GetRequest(weatherurl);
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle("Weather " + location)
                            .WithColor(Color.Green)
                            .WithDescription(cleaninglist[0].Trim('"'))
                            .AddField("Lämpötila ", cleaninglist[1] + " °C")
                            .AddField("Tuuli ", cleaninglist[2] + " m/s, suunta " + cleaninglist[3]);
            await ReplyAsync("", false, builder.Build());


        }

        private async Task GetRequest(string url)
        {
            using (HttpClient weatherclient = new HttpClient())
            {
                using (HttpResponseMessage response = await weatherclient.GetAsync(url))
                {
                    using (HttpContent content = response.Content)
                    {
                        string weatherinfo = await content.ReadAsStringAsync();
                        var weatherjson = JsonConvert.DeserializeObject<dynamic>(weatherinfo);
                        string desc = weatherjson["weather"][0]["description"];
                        cleaninglist.Add(desc);
                        string temp = weatherjson["main"]["temp"];
                        cleaninglist.Add(temp);
                        string speed = weatherjson["wind"]["speed"];
                        cleaninglist.Add(speed);
                        string deg = weatherjson["wind"]["deg"];
                        cleaninglist.Add(deg);                                      
                    }

                }
            }
        }
        private List<string> cleaninglist = new List<string>();
    }
}
