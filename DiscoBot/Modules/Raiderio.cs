using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DiscoBot.Modules
{
    public class Raiderio : ModuleBase<SocketCommandContext>
    {

        private List<string> myList = new List<string>();

        //Prints player statistics "!m+ playername playerserver-playerserver"
        [Command("m+")]
        public async Task RaiderASync([Remainder] string args)
        {
            string[] split_args = args.Split(" ");
            string raiderio_url = "https://raider.io/api/v1/characters/profile?region=eu&realm=" + split_args[1] + "&name=" + split_args[0] + "&fields=mythic_plus_scores";
            
            await GetRequest(raiderio_url);
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle(myList[0])
                            .WithColor(Color.Green)
                            .AddInlineField("Class", myList[1])
                            .AddInlineField("Role", myList[2])
                            .AddInlineField("Score", myList[3]);
            await ReplyAsync("", false, builder.Build());      
        }
        


        //Fetch json from server and parse valuable info
        private async Task GetRequest(string url)
        {
            using (HttpClient raiderioclient = new HttpClient())
            {
                using (HttpResponseMessage response = await raiderioclient.GetAsync(url))
                {
                    using (HttpContent content = response.Content)
                    {
                        string mycontent = await content.ReadAsStringAsync();
                        string[] split_content = mycontent.Split(",");

                        
                        //get name
                        string[] name = split_content[0].Split(":");
                        myList.Add(name[1].Trim('"'));
                        
                        //get class
                        string[] playerclass = split_content[2].Split(":");
                        myList.Add(playerclass[1].Trim('"'));
                        //get role
                        string[] role = split_content[4].Split(":");
                        myList.Add(role[1].Trim('"'));
                        //get score
                        string[] score = split_content[13].Split(":");
                        myList.Add(score[2].Trim('"'));                                                                        
                    }
                }
            }

        }
    }    
}
