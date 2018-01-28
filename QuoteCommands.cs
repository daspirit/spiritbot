using System;
using System.Collections.Generic;
using System.Text;
using DSharpPlus;
using DSharpPlus.Entities;
namespace SpiritBot
{
    class QuoteCommands
    {

        public static void AddQuotes(DiscordClient discord)
        {
            //spirit addquote
            discord.MessageCreated += async e =>
            {
                if (e.Message.Content.ToLower().StartsWith("spirit addquotes") == true)
                {
                    DiscordMember myMember = await e.Guild.GetMemberAsync(e.Message.Author.Id);
                    IEnumerable<DiscordRole> adminRoles = myMember.Roles;
                    int executePermission = 0; //if 1 execute demote
                    foreach (var item in adminRoles)
                    {
                        if (item.Name == "Manager" || item.Name == "Admin" || item.Name == "Moderator" || item.Name == "Owner")
                        {
                            executePermission = 1;
                            if (executePermission == 1)
                            {
                                break;
                            }
                        }
                    }
                    string responseString = "";

                    int numberMessagesToDelete = 0;
                    try
                    {

                    }
                    catch
                    {

                    }
                    if (executePermission == 1)
                    {
                        await e.Message.RespondAsync("spirit add quote is used to add new quotes from our quote channel! \n spirit:getquotes(gets latest quotes), showquotes [number](shows quotes to be added permanently, 5 at a time), removeallquotes(removes all quotes to be added), removequote [number](remove quote to be added), executequotes(adds the quotes to be added (FOREVER!))");

                    }
                    else
                    {
                        await e.Message.RespondAsync("I'm afraid I can't do that " + e.Author.Username);
                    }
                }
            };
        }

        public static void GetQuotes(DiscordClient discord)
        {

            //spirit addquote get
            discord.MessageCreated += async e =>
            {
                if (e.Message.Content.ToLower().StartsWith("spirit getquotes") == true)
                {
                    DiscordMember myMember = await e.Guild.GetMemberAsync(e.Message.Author.Id);
                    IEnumerable<DiscordRole> adminRoles = myMember.Roles;
                    int executePermission = 0; //if 1 execute demote
                    foreach (var item in adminRoles)
                    {
                        if (item.Name == "Manager" || item.Name == "Admin" || item.Name == "Moderator" || item.Name == "Owner")
                        {
                            executePermission = 1;
                            if (executePermission == 1)
                            {
                                break;
                            }
                        }
                    }
                    string responseString = "";

                    if (executePermission == 1)
                    {

                        List<CommandsExecuted> myCommandExecutedList = new List<CommandsExecuted>();
                        myCommandExecutedList = SQLCode.getSpiritQuotesToBeAdded();

                        if (myCommandExecutedList.Count != 0)
                        {
                            await e.Message.RespondAsync("We already have quotes to be added. removeallquotes them, remove some of them, or execute to add these before getting more");

                        }
                        else
                        {
                            try
                            {
                                SQLCode.insertQuoteToBeAdded();
                                await e.Message.RespondAsync(" New quotes are to be added! Use: spirit showquotes [1]: to start to look at these quotes and remove the spam ones for me :heart: ");
                            }
                            catch
                            {
                                await e.Message.RespondAsync("No quotes ready to be added currently");
                            }
                        }

                    }
                    else
                    {
                        await e.Message.RespondAsync("I'm afraid I can't do that " + e.Author.Username);
                    }
                }
            };
        }

        public static void RemoveAllQuotes(DiscordClient discord)
        {

            //spirit removeallquote
            discord.MessageCreated += async e =>
            {
                if (e.Message.Content.ToLower().StartsWith("spirit removeallquotes") == true)
                {
                    DiscordMember myMember = await e.Guild.GetMemberAsync(e.Message.Author.Id);
                    IEnumerable<DiscordRole> adminRoles = myMember.Roles;
                    int executePermission = 0; //if 1 execute demote
                    foreach (var item in adminRoles)
                    {
                        if (item.Name == "Manager" || item.Name == "Admin" || item.Name == "Moderator" || item.Name == "Owner")
                        {
                            executePermission = 1;
                            if (executePermission == 1)
                            {
                                break;
                            }
                        }
                    }
                    string responseString = "";

                    if (executePermission == 1)
                    {
                        List<CommandsExecuted> myCommandExecutedList = new List<CommandsExecuted>();
                        myCommandExecutedList = SQLCode.getSpiritQuotesToBeAdded();

                        if (myCommandExecutedList.Count == 0)
                        {
                            await e.Message.RespondAsync("There are no quotes to be removed from our to be added list, currently. Try to use: spirit getquotes :first!");

                        }
                        else
                        {
                            try
                            {
                                SQLCode.deleteAllQuoteToBeAdded();
                                await e.Message.RespondAsync("All quotes to be added have been removed. Use: spirit getquotes: to get some new ones :heart: ");
                            }
                            catch
                            {
                                await e.Message.RespondAsync("Whoops something may have gone wrong removing all the quotes");
                            }
                        }
                    }
                    else
                    {
                        await e.Message.RespondAsync("I'm afraid I can't do that " + e.Author.Username);
                    }
                }
            };

        }
        public static void RemoveOneQuote(DiscordClient discord)
        {
            //spirit removequote [1]
            discord.MessageCreated += async e =>
            {
                if (e.Message.Content.ToLower().StartsWith("spirit removequote") == true)
                {
                    DiscordMember myMember = await e.Guild.GetMemberAsync(e.Message.Author.Id);
                    IEnumerable<DiscordRole> adminRoles = myMember.Roles;
                    int executePermission = 0; //if 1 execute demote
                    foreach (var item in adminRoles)
                    {
                        if (item.Name == "Manager" || item.Name == "Admin" || item.Name == "Moderator" || item.Name == "Owner")
                        {
                            executePermission = 1;
                            if (executePermission == 1)
                            {
                                break;
                            }
                        }
                    }
                    string responseString = "";
                    string numberOfMessagesToDelete = e.Message.Content.Remove(0, 19);
                    int numberMessagesToDelete = 0;
                    try
                    {
                        numberMessagesToDelete = Convert.ToInt32(numberOfMessagesToDelete);
                    }
                    catch
                    {

                    }
                    if (executePermission == 1)
                    {
                        try
                        {
                            SQLCode.deleteQuoteToBeAdded(numberMessagesToDelete);
                            await e.Message.RespondAsync("I have deleted quote to be added number: " + numberMessagesToDelete);
                        }
                        catch
                        {
                            await e.Message.RespondAsync("I failed to delete that quote ! Sorry :frowning: ");
                        }

                    }
                    else
                    {
                        await e.Message.RespondAsync("I'm afraid I can't do that " + e.Author.Username);
                    }
                }
            };
        }

        public static void ShowQuotes(DiscordClient discord)
        {
            //spirit showquote
            discord.MessageCreated += async e =>
            {
                if (e.Message.Content.ToLower().StartsWith("spirit showquotes") == true)
                {
                    DiscordMember myMember = await e.Guild.GetMemberAsync(e.Message.Author.Id);
                    IEnumerable<DiscordRole> adminRoles = myMember.Roles;
                    int executePermission = 0; //if 1 execute demote
                    foreach (var item in adminRoles)
                    {
                        if (item.Name == "Manager" || item.Name == "Admin" || item.Name == "Moderator" || item.Name == "Owner")
                        {
                            executePermission = 1;
                            if (executePermission == 1)
                            {
                                break;
                            }
                        }
                    }

                    string numberOfMessagesToAdd = e.Message.Content.Remove(0, 18);
                    int quotesToShow = 0;
                    try
                    {
                        quotesToShow = Convert.ToInt32(numberOfMessagesToAdd);
                    }
                    catch
                    {
                        await e.Message.RespondAsync("You must specify a number! Then I will display 5 after that. I am sorry I can't display them all... some people have giant quotes :(");

                    }
                    if (executePermission == 1)
                    {
                        List<CommandsExecuted> myCommandExecutedList = new List<CommandsExecuted>();
                        myCommandExecutedList = SQLCode.getSpiritQuotesToBeAdded();
                        string responseString = "";
                        if (myCommandExecutedList.Count != 0)
                        {
                            for (int i = quotesToShow; i < quotesToShow + 5; i++)
                            {
                                foreach (var item in myCommandExecutedList)
                                {
                                    if (item.id == i)
                                    {
                                        responseString += item.id + ":" + item.content + ":" + item.authorParsed() + ":" + item.time + "\n";
                                    }
                                }
                            }

                        }
                        else
                        {
                            responseString = "No Quotes found! Get them first";
                        }

                        await e.Message.RespondAsync("Quotes ready to be added: ```" + responseString + "```");
                    }
                    else
                    {
                        await e.Message.RespondAsync("I'm afraid I can't do that " + e.Author.Username);
                    }
                }
            };

        }

        public static void ExecuteQuotes(DiscordClient discord)
        {
            //spirit executequote
            discord.MessageCreated += async e =>
            {
                if (e.Message.Content.ToLower().StartsWith("spirit executequote") == true)
                {
                    DiscordMember myMember = await e.Guild.GetMemberAsync(e.Message.Author.Id);
                    IEnumerable<DiscordRole> adminRoles = myMember.Roles;
                    int executePermission = 0; //if 1 execute demote
                    foreach (var item in adminRoles)
                    {
                        if (item.Name == "Manager" || item.Name == "Admin" || item.Name == "Moderator" || item.Name == "Owner")
                        {
                            executePermission = 1;
                            if (executePermission == 1)
                            {
                                break;
                            }
                        }
                    }

                    if (executePermission == 1)
                    {
                        SQLCode.insertQuoteToBeAddedExecute();
                        SQLCode.deleteAllQuoteToBeAdded();
                        await e.Message.RespondAsync("Quotes have been added! Hooray :sun_with_face: ");
                    }
                    else
                    {
                        await e.Message.RespondAsync("I'm afraid I can't do that " + e.Author.Username);
                    }
                }
            };
        }

        public static void getRandomQuote(DiscordClient discord)
        {

            //get a random quote
            discord.MessageCreated += async e =>
            {
                if (e.Message.Content.ToLower().StartsWith("spirit quote"))
                {
                    string userQuote = SQLCode.getSpiritQuote();
                    await e.Message.RespondAsync("Here is a quote I found from one of our users... ```" + userQuote + "```");
                }
            };
        }
    }
}
