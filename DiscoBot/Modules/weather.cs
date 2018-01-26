using Discord;
using Discord.Commands;
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
                        Console.WriteLine(weatherinfo);
                        string[] split_content = weatherinfo.Split(",");
                        
                        //description
                        string[] description = split_content[4].Split(":");
                        cleaninglist.Add(description[1]);
                        //temperature
                        string[] temp = split_content[7].Split(":");
                        cleaninglist.Add(temp[2]);
                        //windspeed
                        string[] windspeed = split_content[13].Split(":");
                        cleaninglist.Add(windspeed[2]);
                        //winddirection
                        string[] winddirection = split_content[14].Split(":");
                        cleaninglist.Add(winddirection[1].Trim('}'));
                        


                    }

                }
            }
        }
        private List<string> cleaninglist = new List<string>();
    }
}
