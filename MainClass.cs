using System.Threading.Tasks;
using DSharpPlus;
using System;
using DSharpPlus.Entities;
using System.Collections.Generic;

namespace SpiritBot
{
    class MainClass
    {
        static void Main(string[] args)
        {
            MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        static DiscordClient discord;

        static async Task MainAsync(string[] args)
        {

            discord = new DiscordClient(new DiscordConfiguration
            {
                Token = "PutTokenHere",
                TokenType = TokenType.Bot
            });
            
            //New users get a hello message and assigned to guest role
            PromoteDemoteMembers.NewMemberPromote(discord);
            //admin command to demote lurkers
            PromoteDemoteMembers.DemoteLurkers(discord);

            //tells users what commands there are
            HelpCommands.SpiritHelpCommands(discord);
            HelpCommands.SpiritAdminCommands(discord);
            HelpCommands.SpiritMaintenanceCommands(discord);

            //spirit clear
            discord.MessageCreated += async e =>
            {        
                if (e.Message.Content.ToLower().StartsWith("spirit clear") == true)
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
                    string numberOfMessagesToDelete = e.Message.Content.Remove(0, 13);


                    int numberMessagesToDelete = 0;
                    try
                    {
                        numberMessagesToDelete = Convert.ToInt32(numberOfMessagesToDelete);
                    }

                    catch
                    {

                    }
                    if (numberMessagesToDelete < 15)
                    {
                        if (executePermission == 1)
                        {
                            await e.Channel.DeleteMessagesAsync(await e.Message.Channel.GetMessagesAsync(numberMessagesToDelete, e.Message.Id));
                        }
                        else
                        {
                            await e.Message.RespondAsync("I'm afraid I can't do that " + e.Author.Username);
                        }
                    }
                }

            };

            QuoteCommands.AddQuotes(discord);
            QuoteCommands.ExecuteQuotes(discord);
            QuoteCommands.GetQuotes(discord);
            QuoteCommands.getRandomQuote(discord);
            QuoteCommands.RemoveAllQuotes(discord);
            QuoteCommands.RemoveOneQuote(discord);
            QuoteCommands.ShowQuotes(discord);

            //spirit whatdo
            discord.MessageCreated += async e =>
            {
                if (e.Message.Content.ToLower().StartsWith("spirit whatdo"))
                {
                    List<CommandsExecuted> myCommandExecutedList = new List<CommandsExecuted>();
                    myCommandExecutedList = SQLCode.getSpiritExecutedCommandsLast10Cmd();
                    string responseString = "";
                    if (myCommandExecutedList.Count != 0)
                    {
                        foreach (var item in myCommandExecutedList)
                        {
                            responseString += item.authorParsed() + " :" + item.content + " : " + item.channel + " : " + item.time + "\n";
                        }
                    }
                    else
                    {
                        responseString = "No commands found";
                    }

                    await e.Message.RespondAsync("Last 10 commands found: ```" + responseString + "```");
                }
            };



            //whats your purpose (first command ever!)
            discord.MessageCreated += async e => //message created
            {
                if (e.Message.Content.ToLower().StartsWith("bot whats your purpose"))
                {
                    await e.Message.RespondAsync("zee tells me I make moderation easier, :heart:, and spam /r/meditation... o my god.");
                }
            };

            //whats your purpose (first command ever!)
            discord.MessageCreated += async e =>
            {
                if (e.Message.Content.ToLower().StartsWith("bot what's your purpose"))
                {
                    await e.Message.RespondAsync("zee tells me I make moderation easier, :heart:, and spam /r/meditation... o my god.");
                }
            };

            //Print out discord channels or roles in numeric format for updating code for server
            //Member = 346470543446507522
            //Guest = 354466276338434070
            //Lurker = 357638353534976020
            //OG = 379502868409090058
            //Social channel is 375849395406503937
            //readme is 374064827984773120
            //general is 346358184069300227
            BotUtility.CurrentDiscordChannels(discord);
            BotUtility.CurrentDiscordRoles(discord);


            //activeUsers get a list of active users and counts
            discord.MessageCreated += async e =>
            {

                if (e.Message.Content.ToLower().StartsWith("spirit activeusers") == true || e.Message.Content.ToLower().StartsWith("spirit active users") == true)
                {
                    //get member permission
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
                        try { 

                                //lastMonth, lastWeek, thisWeek, thisMonth
                                List<string> userListLastMonth = SQLCode.getActiveUsers("lastMonth");
                            List<string> userListLastWeek = SQLCode.getActiveUsers("lastWeek");
                            List<string> userListThisWeek = SQLCode.getActiveUsers("thisWeek");
                            List<string> userListThisMonth = SQLCode.getActiveUsers("thisMonth");
                             string responseString = "```Active Users Last Month:" + userListLastMonth.Count + " | Last Week:" + userListLastWeek.Count + " | This Month: " + userListThisMonth.Count + " | This Week: " + userListThisWeek.Count + "```";
                                    //if (item.IndexOf)userList.Count
                              


                                if (responseString != "")
                                {
                                    await e.Message.RespondAsync("I found these users..." + responseString);
                                }
                                else
                                {
                                    await e.Message.RespondAsync("I found no users! I am so sorry :frowning:...");
                                }
                            }
                        catch
                        {
                            await e.Message.RespondAsync("I found no users! I am so sorry :frowning:...");
                        }
                    }

                    else //if execute permission is not 1
                    {
                        await e.Message.RespondAsync("No permission to execute");

                    }

                }
        };


            //usersLike get a list of users like xyz
            discord.MessageCreated += async e =>
            {
                string authorStringToCheck;
                string responseString = "";
                try
                {
                    if (e.Message.Content.ToLower().StartsWith("spirit users"))
                    {
                        authorStringToCheck = e.Message.Content.Remove(0, 13);

                        List<string> userList = SQLCode.getUserList(authorStringToCheck);
                        foreach (var item in userList)
                        {
                            int commentCount1Days = SQLCode.getCommentCount1Days(item);
                            int commentCount7Days = SQLCode.getCommentCount7Days(item);
                            int commentCountTotal = SQLCode.getCommentCount(item);
                            string authorParsed = item;
                            authorParsed = authorParsed.Substring(authorParsed.IndexOf(';') + 1);
                            responseString += "```" + authorParsed + " : Comments today:" + commentCount1Days + " | 7 Days:" + commentCount7Days + " | Total: " + commentCountTotal + "```";
                            //if (item.IndexOf)userList.Count
                        }


                        if (responseString != "")
                        {
                            await e.Message.RespondAsync("I found these users..." + responseString);
                        }
                        else
                        {
                            await e.Message.RespondAsync("I found no users! I am so sorry :frowning:...");
                        }
                    }
                }
                   catch
                        {
                    await e.Message.RespondAsync("I found no users! I am so sorry :frowning:...");
                }

                //SQLCode.getUserList
            };

            //Insert Comment into Database, promote comments
            discord.MessageCreated += async e =>
            {
                string author = e.Message.Author.ToString();
                string content = e.Message.Content;
                string channelID = e.Message.ChannelId.ToString();
                string channel = e.Message.Channel.ToString();
                DateTimeOffset creationTimeStamp = e.Message.CreationTimestamp; 
                SQLCode.insertComment(author, content, channelID, channel, creationTimeStamp);


                ulong authorID = e.Message.Author.Id;
                DiscordRole guestRole = e.Guild.GetRole(354466276338434070);
                DiscordRole memberRole = e.Guild.GetRole(346470543446507522);
                DiscordRole inactiveRole = e.Guild.GetRole(389201764466819073);
                DiscordMember myMember;
                DiscordRole lurkerRole = e.Guild.GetRole(357638353534976020);

                int commentCount = SQLCode.getCommentCount(author);

                if (commentCount > 10)
                {
                    //Probably make this more async, probably change table so that we ignore checking members or above
                    myMember = await e.Guild.GetMemberAsync(authorID);
                    IEnumerable<DiscordRole> memberRoles = myMember.Roles;
                    foreach (var item in memberRoles)
                    {
                        if (commentCount > 100)
                        {
                            if (item.Name == "Guest")
                            {
                                string authorParsed = author;

                                authorParsed = authorParsed.Substring(authorParsed.IndexOf(';') + 1);
                                await e.Guild.RevokeRoleAsync(myMember, guestRole, "100 comments!");
                                await e.Guild.GrantRoleAsync(myMember, memberRole, "100 comments!");
                                await e.Message.RespondAsync("Congrats " + authorParsed + " you are now a member :heart: ");
                            }

                            if (item.Name == "Inactive")
                            {
                                string authorParsed = author;

                                authorParsed = authorParsed.Substring(authorParsed.IndexOf(';') + 1);
                                await e.Guild.RevokeRoleAsync(myMember, inactiveRole, "100 comments!");
                                await e.Guild.GrantRoleAsync(myMember, memberRole, "100 comments!");
                                await e.Message.RespondAsync("Welcome back " + authorParsed + " you are now a member again :heart: ");
                            }
                        }

                        //;urker more than 10 comments
                        if (item.Name == "Lurker")
                        {
                            string authorParsed = author;

                            authorParsed = authorParsed.Substring(authorParsed.IndexOf(';') + 1);
                            await e.Guild.RevokeRoleAsync(myMember, lurkerRole, "100 comments!");
                            await e.Guild.GrantRoleAsync(myMember, guestRole, "100 comments!");
                            await e.Message.RespondAsync("Congrats " + authorParsed + " you are no longer a lurker :heart: ");
                        }
                    }
                }
            };
            await discord.ConnectAsync();
            await Task.Delay(-1);
        }

    }
}
