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
            discord.GuildMemberAdded += async e =>
            {

                DiscordChannel socialChannel;

                DiscordRole guestRole = e.Guild.GetRole(354466276338434070);
                DiscordMember memberRole = e.Member;

                await e.Member.GrantRoleAsync(guestRole);
                socialChannel = e.Guild.GetChannel(375849395406503937);
                //channel to send message to

                string author = "SpiritBot"; //person executing command
                string content = e.Member.DisplayName + " welcomed to channel"; //command executed (comment)
                string channelID = "375849395406503937"; //social# - channel id
                string channel = socialChannel.Name; //social -channel name
                DateTimeOffset creationTimeStamp = e.Member.CreationTimestamp;

                //inserts into command executed the command executed with timestamp
                SQLCode.insertCommandComment(author, content, channelID, channel, creationTimeStamp);

                //respond with greeting
                await discord.SendMessageAsync(socialChannel, "Greetings, " + e.Member.DisplayName + ", and welcome in Spirituality ! Be sure to check out the other rooms within the server, and the rules.There's space for all tastes ! If you have any issues, questions or concerns, you may want to contact a Manager, an Administrator or a Moderator. Enjoy your stay and remember to Have Fun " + e.Member.Mention + "! :smile: :sun_with_face:");
            };

            //command list - spirit commands, help
            //ctrl + F the below commands
            //spirit quote
            //spirit users
            //spirit whatdo - spirit 10 commands executed 
            //spirit roles
            //bot whats your purpose
            //bot what's your purpose
            //spirit leaderboard, addquote, clear -- on the todo list
       
            //spirit commands, help
            discord.MessageCreated += async e =>
            {
                if (e.Message.Content.ToLower().StartsWith("spirit commands") == true || e.Message.Content.ToLower().StartsWith("spirit help") == true)
                {
                    string commandList = "quote, users, whatdo, admin, maintenance"; //also update the spirit help function below
                    await e.Message.RespondAsync("My current commands, spirit: " + commandList);
                }
            };


            //spirit maintenance commands
            discord.MessageCreated += async e =>
            {
                if (e.Message.Content.ToLower().StartsWith("spirit maintenance") == true)
                {
                    string commandList = "roles, channels"; //also update the spirit help function below
                    await e.Message.RespondAsync("My current commands, spirit: " + commandList);
                }
            };

            //spirit admin commands
            discord.MessageCreated += async e =>
            {
                if (e.Message.Content.ToLower().StartsWith("spirit admin") == true)
                {
                    string commandList = "demote, clear [1-14], addquotes"; //also update the spirit help function below
                    await e.Message.RespondAsync("My current commands, spirit: " + commandList);
                }
            };

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
                            await e.Message.RespondAsync("I have deleted quote to be added number: "+numberMessagesToDelete);
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
            /*
            //spirit removequotes [1]
            discord.MessageCreated += async e =>
            {
                if (e.Message.Content.ToLower().StartsWith("spirit removequotes") == true)
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
                    string numberOfMessagesToDelete = e.Message.Content.Remove(0, 20);
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
            */

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
                            for (int i = quotesToShow; i < quotesToShow+5; i++)
                            {
                                foreach (var item in myCommandExecutedList)
                                {
                                    if (item.id == i)
                                    {
                                        responseString += item.id + ":" + item.content + ":" + item.authorParsed() +":"+ item.time + "\n";
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

            //get a random quote
            discord.MessageCreated += async e =>
            {
                if (e.Message.Content.ToLower().StartsWith("spirit quote"))
                {
                    string userQuote = SQLCode.getSpiritQuote();
                    await e.Message.RespondAsync("Here is a quote I found from one of our users... ```" + userQuote + "```");
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

            //demote lurkers
            discord.MessageCreated += async e =>
            {
                if (e.Message.Content.ToLower().StartsWith("spirit demote"))
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
                        DiscordRole guestRole = e.Guild.GetRole(354466276338434070);
                        DiscordRole memberRole = e.Guild.GetRole(346470543446507522);
                        DiscordRole lurkerRole = e.Guild.GetRole(357638353534976020);
                        DiscordRole inactiveRole = e.Guild.GetRole(389201764466819073);

                        IReadOnlyList<DiscordMember> discordMembers = e.Guild.Members; //All Members in "Guild"
                        List<string> eligibleToDemoteMembers = new List<string>(); //members to demote
                        List<string> eligibleToDemoteGuests = new List<string>(); //guests to demote
                                                                                  //Loop through all members in guild
                        try
                        {
                            foreach (var item in discordMembers)
                            {
                                IEnumerable<DiscordRole> memberRoles = item.Roles;


                                string authorToCheck = item.ToString();
                                string authorParsed = item.Username;
                                //Check the member role
                                foreach (var item2 in memberRoles)
                                {
                                    if (item2.Name == "OG")
                                    {
                                        continue; //ignore OG
                                      //  continue;
                                    }
                                    if (item2.Name == "Member")
                                    {
                                        int commentCount14Days = SQLCode.getCommentCount14Days(authorToCheck);
                                        if (commentCount14Days == 0)
                                        {
                                            if (eligibleToDemoteMembers.Count < 100)
                                            {
                                                eligibleToDemoteMembers.Add(authorParsed);
                                                await e.Guild.GrantRoleAsync(item, inactiveRole, "INACTIVE: No comments in 14 days");
                                                await e.Guild.RevokeRoleAsync(item, memberRole, "No comments in 14 days");
                                            }
                                        }
                                        continue;
                                    }

                                    if (item2.Name == "Guest")
                                    {
                                        int commentCount7Days = SQLCode.getCommentCount7Days(authorToCheck);

                                        if (commentCount7Days == 0)
                                        {
                                            if (eligibleToDemoteGuests.Count < 100)
                                            {
                                                eligibleToDemoteGuests.Add(authorParsed);
                                                await e.Guild.GrantRoleAsync(item, lurkerRole, "LURKER: No comments in 7 days");
                                                await e.Guild.RevokeRoleAsync(item, guestRole, "No comments in 7 days");
                                            }
                                        }
                                    }


                                }
                            }

                        }

                        catch (Exception)
                        {

                            throw;
                        }

                        string responseStringGuests = "";
                            string responseStringMembers = "";
                            //respond with list of users demoted
                            foreach (var item in eligibleToDemoteGuests)
                            {
                                responseStringGuests += item + " ";

                            }

                            foreach (var item in eligibleToDemoteMembers)
                            {
                                responseStringMembers += item + " ";
                            }

                            int executeCommentLog = SQLCode.insertCommandComment(e.Message.Author.ToString(), "lurkers demoted:" + responseStringGuests + " Members inactive: " + responseStringMembers, e.Channel.Id.ToString(), e.Channel.ToString(), e.Message.CreationTimestamp);

                            if (responseStringGuests != "")
                            {

                                await e.Message.RespondAsync("I demoted these Guests..." + responseStringGuests);

                            }
                            else
                            {
                                await e.Message.RespondAsync("I found no Guests to demote! I am so happy :grin:...");
                            }

                            if (responseStringMembers != "")
                            {
                                await e.Message.RespondAsync("These Members are inactive..." + responseStringMembers);
                            }
                            else
                            {
                                await e.Message.RespondAsync("I found no Members to inactive! I am so happy :grin:...");
                            }
        
                    }
                            else //if execute permission is not 1
                    {
                        await e.Message.RespondAsync("No permission to execute");

                    }
                }
        };
            




            //Member = 346470543446507522
            //Guest = 354466276338434070
            //Lurker = 357638353534976020
            //OG = 379502868409090058
            //Social channel is 375849395406503937
            //readme is 374064827984773120
            //general is 346358184069300227


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
