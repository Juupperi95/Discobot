using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace DiscoBot.Modules
{
    public class Help : ModuleBase<SocketCommandContext>
    {
        [Command("help")]
        public async Task HelpAsync()
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle("Commands")
                .AddField("!help", "show user commands")
                .AddField("!cat", "nice cat picture")
                .AddField("!m+", "check someones m+ score")
                .AddField("!join", "tell bot to join your voice channel")
                .AddField("!leave", "tell bot to leave your voice channel")
                .AddField("!play", "bot plays song from youtube")
                .WithColor(Color.Green);
            await ReplyAsync("", false, builder.Build());
        }
    }
}
