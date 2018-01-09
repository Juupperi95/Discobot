using Discord;
using Discord.Audio;
using Discord.Commands;
using System.Diagnostics;
using System.Threading.Tasks;


namespace DiscoBot.Modules
{
    public class Play : ModuleBase<SocketCommandContext>
    {

        private static IAudioClient _audioclient;



        //Joins to users current voicechannel
        [Command("join", RunMode = RunMode.Async)]
        public async Task JoinChannel()
        {
            IVoiceChannel channel = (Context.User as IVoiceState).VoiceChannel;
            if (channel == null)
            {
                await ReplyAsync("Error: Couldn't find channel to join");
            }
            else
            {
                _audioclient = await channel.ConnectAsync();
            }
        }



        //Leaves current voicechannel
        [Command("leave", RunMode = RunMode.Async)]
        public async Task LeaveChannel()
        {
            IVoiceChannel channel = (Context.User as IVoiceState).VoiceChannel;
            await  _audioclient.StopAsync();
        }



        //Streams audio to current voicechannel
        [Command("play", RunMode = RunMode.Async)]
        public async Task PlaySong([Remainder] string url)
        {
            // Create FFmpeg using the previous example
            var ffmpeg = CreateStream(url);
            var output = ffmpeg.StandardOutput.BaseStream;
            var stream = _audioclient.CreatePCMStream(AudioApplication.Mixed);
            await output.CopyToAsync(stream);
            await stream.FlushAsync();
        }



        //Creates stream
        private Process CreateStream(string url)
        {
            var ffmpeg = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/C youtube-dl -o - {url} | ffmpeg -i pipe:0 -ac 2 -f s16le -ar 48000 pipe:1",
                UseShellExecute = false,
                RedirectStandardOutput = true,
            };
            return Process.Start(ffmpeg);
        }
    }                 
}

      