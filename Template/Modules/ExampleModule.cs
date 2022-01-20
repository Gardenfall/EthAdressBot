using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using Discord.Commands;
using Microsoft.Extensions.Logging;

namespace Template.Modules
{
    public class ExampleModule : ModuleBase<SocketCommandContext>
    {
        private readonly ILogger<ExampleModule> _logger;

        public ExampleModule(ILogger<ExampleModule> logger)
            => _logger = logger;

        [Command("ping")]
        public async Task PingAsync()
        {
            await ReplyAsync("Pong!");
            _logger.LogInformation($"{Context.User.Username} executed the ping command!");
        }

        [Command("echo")]
        public async Task EchoAsync([Remainder] string text)
        {
            await ReplyAsync(text);
            _logger.LogInformation($"{Context.User.Username} executed the echo command!");
        }

        [Command("math")]
        public async Task MathAsync([Remainder] string math)
        {
            var dt = new DataTable();
            var result = dt.Compute(math, null);
            
            await ReplyAsync($"Result: {result}");
            _logger.LogInformation($"{Context.User.Username} executed the math command!");
        }

        [Command("adress")]
        public async Task EthAdress(string pMessage) {
            var userName = Context.User.Username;
            var userDisc = Context.User.Discriminator;
            var userTag = userName + "#" + userDisc;
            var ethAdress = pMessage;

            using (StreamWriter sw = File.AppendText("EthAdressBot.txt")) { 
                sw.WriteLine(userTag + " : " + ethAdress);
            }

            using (StreamReader sr = File.OpenText("EthAdressBot.txt")) {
                string s = "";
                while((s = sr.ReadLine()) != null) {
                    Console.WriteLine(s);
                }
            }

            await Context.Channel.SendMessageAsync(userTag + " : " + ethAdress + " have been added to our database !");
        }
    }
}