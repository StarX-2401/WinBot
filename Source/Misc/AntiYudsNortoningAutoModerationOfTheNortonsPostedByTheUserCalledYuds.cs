using System;
using System.IO;
using System.Timers;
using System.Threading.Tasks;
using System.Collections.Generic;

using Newtonsoft.Json;

using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;


namespace WinBot.Misc
{
    public class AntiYudsNortoningAutoModerationOfTheNortonsPostedByTheUserCalledYuds
    {
        public const ulong yudsID = 469275318079848459;

        public static void Init()
        {
            Bot.client.MessageCreated += MessageCreated;
            Bot.client.MessageReactionAdded += MessageReactionAdded;
        }

        public static async Task MessageCreated (DiscordClient client, MessageCreateEventArgs e)
        {
            if(e.Author.Id != yudsID)
                return;
            if(e.Message.Content.ToLower().Contains("<:norton") || e.Message.Content.ToLower().Contains("<:oldnorton")
            || e.Message.Content.ToLower().Contains("<:srsly"))
                await e.Message.CreateReactionAsync(DiscordEmoji.FromName(client, ":yuds:"));
        }

        public static async Task MessageReactionAdded(DiscordClient client, MessageReactionAddEventArgs e)
        {
            if(e.User.Id != yudsID)
                return;
            if(e.Emoji.Name.ToLower().Contains("norton")) {
                await e.Message.DeleteReactionAsync(e.Emoji, e.User, "Yuds");
                await Task.Delay(250);
                await e.Message.CreateReactionAsync(DiscordEmoji.FromName(client, ":kek:"));
            }
        }
    }    
}