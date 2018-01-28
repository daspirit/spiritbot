using System;
using System.Collections.Generic;
using System.Text;
using DSharpPlus;
using DSharpPlus.Entities;

namespace SpiritBot
{
    class BotUtility
    {

        public static void CurrentDiscordChannels(DiscordClient discord)
        {
            //current discord channels
            discord.MessageCreated += async e =>
            {

                if (e.Message.Content.ToLower().StartsWith("spirit channels"))
                {
                    //Check Guild channels
                    string responseString = "";
                    IReadOnlyList<DiscordChannel> guildChannels = e.Guild.Channels;

                    foreach (var item in guildChannels)
                    {
                        if (item.Name != "@everyone")
                        {
                            responseString += item.Name + " : " + item.Id + " | ";
                        }
                    }

                    await e.Message.RespondAsync("I found these channels..." + responseString);
                }

            };
        }
        public static void CurrentDiscordRoles(DiscordClient discord)
        {
            //current discord roles
            discord.MessageCreated += async e =>
            {

                if (e.Message.Content.ToLower().StartsWith("spirit roles"))
                {
                    //Check Guild Roles
                    string responseString = "";
                    IReadOnlyList<DiscordRole> guildRoles = e.Guild.Roles;

                    foreach (var item in guildRoles)
                    {
                        if (item.Name != "@everyone")
                        {
                            responseString += item.Name + " : " + item.Id + " | ";
                        }
                    }

                    await e.Message.RespondAsync("I found these roles..." + responseString);
                }

            };

        }
    }
}
