using System;
using System.Threading.Tasks;

using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

using WinBot.Util;
using WinBot.Commands.Attributes;

namespace WinBot.Commands.Main
{
    public class WhoisCommand : BaseCommandModule
    {
        [Command("whois")]
        [Description("Gets basic info about a user")]
        [Usage("[user]")]
        [Category(Category.Main)]
        public async Task Whois(CommandContext Context, [RemainingText] DiscordUser user)
        {
            DiscordMember memberUser = null;
            if(user == null)
                user = Context.User;
            try {
            memberUser = await Context.Guild.GetMemberAsync(user.Id);
            }
            catch{}

            try {
                // Set up the embed
                DiscordEmbedBuilder Embed = new DiscordEmbedBuilder();
                Embed.WithColor(DiscordColor.Gold);   

                // Basic user info
                if (user.AvatarUrl != null) {
                    Embed.WithThumbnail(user.AvatarUrl);
                    Embed.WithAuthor(user.Username, null, user.AvatarUrl);
                }
                else {
                    Embed.WithThumbnail(user.DefaultAvatarUrl);
                    Embed.WithAuthor(user.Username, null, user.DefaultAvatarUrl);
                }
                Embed.AddField("**ID**", user.Id.ToString(), false);

                // Embed dates
                Embed.AddField("**Created On**", $"{MiscUtil.FormatDate(user.CreationTimestamp)} ({(int)DateTime.Now.Subtract(user.CreationTimestamp.DateTime).TotalDays} days ago)", true);
                if(memberUser != null)
                    Embed.AddField("**Joined On**", $"{MiscUtil.FormatDate(memberUser.JoinedAt.DateTime)} ({(int)DateTime.Now.Subtract(memberUser.JoinedAt.DateTime).TotalDays} days ago)", true);

                await Context.ReplyAsync("", Embed.Build());
            }
            catch (Exception ex) {
                await Context.ReplyAsync("Error: " + ex.Message + "\nStack Trace:" + ex.StackTrace);
            }
        }
    }
}