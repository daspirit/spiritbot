using System;
using System.Collections.Generic;
using System.Text;
using DSharpPlus;
using DSharpPlus.Entities;
namespace SpiritBot
{
    class PromoteDemoteMembers
    {


    public static void NewMemberPromote(DiscordClient discord)
    {
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

    }


        public static void DemoteLurkers(DiscordClient discord)
        {
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
                                            if (eligibleToDemoteMembers.Count< 100)
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
                                            if (eligibleToDemoteGuests.Count< 100)
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
            }

    }
}
