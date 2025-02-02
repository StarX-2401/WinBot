using System;
using System.IO;
using System.Timers;
using System.Threading.Tasks;
using System.Collections.Generic;

using Newtonsoft.Json;

using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;

using Serilog;

namespace WinBot.Misc
{
    public class AntiMichaelNortoningAutoModerationOfTheNortonsPostedByTheUserCalledMichael
    {
        public const ulong michaelID = 852513780734492702;

        public static void Init()
        {
            Bot.client.MessageCreated += MessageCreated;
            Bot.client.MessageReactionAdded += MessageReactionAdded;
        }

        public static async Task MessageCreated (DiscordClient client, MessageCreateEventArgs e)
        {
            if(e.Author.Id != michaelID)
                return;
            if(e.Message.Content.ToLower().Contains("<:norton") || e.Message.Content.ToLower().Contains("<:oldnorton")
            || e.Message.Content.ToLower().Contains("<:srsly") || e.Message.Content.ToLower().Contains("😐") 
            || e.Message.Content.ToLower().Contains("norton") || e.Message.Content.ToLower().Contains("😑") 
            || e.Message.Content.ToLower().Contains("😒") || e.Message.Content.ToLower().Contains("reallybro"))
                await e.Message.CreateReactionAsync(DiscordEmoji.FromName(client, ":mild_dissatisfaction:"));
        }

        public static async Task MessageReactionAdded(DiscordClient client, MessageReactionAddEventArgs e)
        {
            if(e.User.Id != michaelID)
                return;
            if(e.Emoji.Name.ToLower().Contains("norton") || e.Emoji.Name.ToLower().Contains("😐") || e.Emoji.Name.ToLower().Contains("😑")
            || e.Emoji.Name.ToLower().Contains("😒") || e.Emoji.Name.ToLower().Contains("reallybro")) {
                await e.Message.DeleteReactionAsync(e.Emoji, e.User, "Michael");
                await Task.Delay(250);
                await e.Message.CreateReactionAsync(DiscordEmoji.FromName(client, ":kek:"));
                Log.Information($"A norton reaction to {e.Message.JumpLink.ToString()} was deleted");
            }
        }
    }    
}