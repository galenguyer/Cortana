﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace Cortana.Modules
{
    public class ServerModule : ModuleBase
    {
        [Command("listChannels")]
        [Summary("List all channels in the guild")]
        [Remarks("server")]
        public async Task ListChannels(ulong id = 0)
        {
            IGuild guild;
            if (id == 0) guild = Context.Guild;
            else
            {
                try
                {
                    guild = Context.Client.GetGuildAsync(id).Result;
                }
                catch (Exception e)
                {
                    await ReplyAsync($"That doesn't appear to be a valid guild ID!");
                    return;
                }
            }
            string name = guild.Name;
            string textChannels = "";
            foreach (var c in guild.GetTextChannelsAsync().Result.OrderBy(c => c.Position))
            {
                textChannels += $"**{c.Position}:** {c.Name}\n";
            }
            string voiceChannels = "";
            foreach (var c in guild.GetVoiceChannelsAsync().Result.OrderBy(c => c.Position))
            {
                voiceChannels += $"**{c.Position}:** {c.Name}\n";
            }
            if ((await Context.Guild.GetCurrentUserAsync()).GetPermissions(Context.Channel as IGuildChannel).EmbedLinks)
            {
                var em = new EmbedBuilder();
                em.WithTitle($"Channels for {name}");
                em.AddField(new EmbedFieldBuilder().WithName("Text Channels").WithValue(textChannels));
                em.AddField(new EmbedFieldBuilder().WithName("Voice Channels").WithValue(voiceChannels));
                em.WithCurrentTimestamp();
                await ReplyAsync("", embed: em.Build());
            }
            else
            {
                string msg = "";
                msg += $"Channels for `{name}`\n";
                msg += $"__**Text Channels**__\n";
                msg += textChannels;
                msg += $"__**Voice Channels**__\n";
                msg += voiceChannels;
                await ReplyAsync(msg);
            }
        }
        [Command("listTextChannels")]
        [Summary("List all *text* channels in the guild")]
        [Remarks("server")]
        public async Task ListTextChannels(ulong id = 0)
        {
            IGuild guild;
            if (id == 0) guild = Context.Guild;
            else
            {
                try
                {
                    guild = Context.Client.GetGuildAsync(id).Result;
                }
                catch (Exception e)
                {
                    await ReplyAsync($"That doesn't appear to be a valid guild ID!");
                    return;
                }
            }
            string name = guild.Name;
            string textChannels = "";
            foreach (var c in guild.GetTextChannelsAsync().Result.OrderBy(c => c.Position))
            {
                textChannels += $"**{c.Position}:** {c.Name}\n";
            }
            if ((await Context.Guild.GetCurrentUserAsync()).GetPermissions(Context.Channel as IGuildChannel).EmbedLinks)
            {
                var em = new EmbedBuilder();
                em.WithTitle($"Channels for {name}");
                em.WithDescription(textChannels);
                em.WithCurrentTimestamp();
                await ReplyAsync("", embed: em.Build());
            }
            else
            {
                string msg = "";
                msg += $"Text channels for `{name}`\n";
                msg += textChannels;
                await ReplyAsync(msg);
            }
        }
        [Command("listVoiceChannels")]
        [Summary("List all *voice* channels in the guild")]
        [Remarks("server")]
        public async Task ListVoiceChannels(ulong id = 0)
        {
            IGuild guild;
            if (id == 0) guild = Context.Guild;
            else
            {
                try
                {
                    guild = Context.Client.GetGuildAsync(id).Result;
                }
                catch (Exception e)
                {
                    await ReplyAsync($"That doesn't appear to be a valid guild ID!");
                    return;
                }
            }
            string name = guild.Name;
            string voiceChannels = "";
            foreach (var c in guild.GetVoiceChannelsAsync().Result.OrderBy(c => c.Position))
            {
                voiceChannels += $"**{c.Position}:** {c.Name}\n";
            }
            if ((await Context.Guild.GetCurrentUserAsync()).GetPermissions(Context.Channel as IGuildChannel).EmbedLinks)
            {
                var em = new EmbedBuilder();
                em.WithTitle($"Voice channels for {name}");
                em.WithDescription(voiceChannels);
                em.WithCurrentTimestamp();
                await ReplyAsync("", embed: em.Build());
            }
            else
            {
                string msg = "";
                msg += $"Voice channels for `{name}`\n";
                msg += $"__**Voice Channels**__\n";
                msg += voiceChannels;
                await ReplyAsync(msg);
            }
        }
    }
}