using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace DiscoBot.Modules
{
    public class Cat : ModuleBase<SocketCommandContext>
    {
        [Command("cat")]
        public async Task CatASync()
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle("Woosh")
                .WithColor(Color.Green)
                .WithDescription("Here it is, enjoy!")
                .WithImageUrl("http://www.catster.com/wp-content/uploads/2017/08/A-fluffy-cat-looking-funny-surprised-or-concerned.jpg");
            await ReplyAsync("", false, builder.Build());
                

          
            
        }

    }
}
