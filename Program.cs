using DiscordRPC;
using DiscordRPC.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace Discord_RPC
{
   class Program
    {
        public static DiscordRpcClient client;
	
	static void Main(string[] args)
        {
	    /* Config */
	    dynamic Config = JObject.Parse(File.ReadAllText($"{AppDomain.CurrentDomain.BaseDirectory}\\Config.json"));
	    string clientid = Config["ID"]; 
	    string LargeImageKey = Config["LargeImageKey"]; string LargeImageText = Config["LargeImageText"];
	    string LabelText = Config["LabelText"]; string Url1 = Config["Link"];
	    string LabelText2 = Config["LabelText2"]; string Url2 = Config["Link2"];

	    /* Initialize RPC Connection */
	    client = new DiscordRpcClient(clientid) { Logger = new ConsoleLogger() { Level = LogLevel.Warning } }; //logging
	    client.OnReady += (sender, e) => { Console.WriteLine("Received Ready from user {0}", e.User.Username); }; //Username
	    client.OnPresenceUpdate += (sender, e) => { Console.WriteLine("Received Update! {0}", e.Presence); }; //Presence
	    client.Initialize();
	    
	    /* Updates Presence For Current Session */
            client.SetPresence(new RichPresence()
            {
	        Timestamps = Timestamps.Now, Assets = new Assets() { SmallImageKey = "https://c.tenor.com/TgKK6YKNkm0AAAAi/verified-verificado.gif", LargeImageKey = LargeImageKey, LargeImageText = LargeImageText },
		Buttons = new DiscordRPC.Button[] { new Button() { Label = LabelText, Url = Url1 }, new Button() { Label = LabelText2, Url = Url2 },
	    }}); Console.Read();
        }
    }
}
