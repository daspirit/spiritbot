using System;
using System.Collections.Generic;
using System.Text;
using DSharpPlus;
using DSharpPlus.Entities;

namespace SpiritBot
{
    class HelpCommands
    {
        public static void SpiritHelpCommands(DiscordClient discord)
        {

            //spirit commands, help
            discord.MessageCreated += async e =>
            {
                if (e.Message.Content.ToLower().StartsWith("spirit commands") == true || e.Message.Content.ToLower().StartsWith("spirit help") == true)
                {
                    string commandList = "quote, users, whatdo, admin, maintenance"; //also update the spirit help function below
                    await e.Message.RespondAsync("My current commands, spirit: " + commandList);
                }
            };

        }
        public static void SpiritMaintenanceCommands(DiscordClient discord)
        {
            //spirit maintenance commands
            discord.MessageCreated += async e =>
            {
                if (e.Message.Content.ToLower().StartsWith("spirit maintenance") == true)
                {
                    string commandList = "roles, channels"; //also update the spirit help function below
                    await e.Message.RespondAsync("My current commands, spirit: " + commandList);
                }
            };
        }
            public static void SpiritAdminCommands(DiscordClient discord)
            {
                //spirit admin commands
                discord.MessageCreated += async e =>
            {
                if (e.Message.Content.ToLower().StartsWith("spirit admin") == true)
                {
                    string commandList = "demote, clear [1-14], addquotes"; //also update the spirit help function below
                    await e.Message.RespondAsync("My current commands, spirit: " + commandList);
                }
            };
            }
        
    }
}
