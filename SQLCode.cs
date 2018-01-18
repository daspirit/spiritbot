using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace SpiritBot
{
    class SQLCode
    {
        public static string connectionstring = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SpiritDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public SQLCode()
        {
        }
        
        public static int deleteAllQuoteToBeAdded()
        {
            string sqlCommand = "dbo.DropCreateQuoteMessages";
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(sqlCommand, con))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        return command.ExecuteNonQuery();
                    }

                    catch
                    {
                        return 0;
                    }// using (SqlDataReader reader = command.ExecuteReader())

                }
            }
        }

        public static int deleteQuoteToBeAdded(int id)
        {
            string sqlCommand = "DELETE FROM dbo.QuoteMessagesToBeAdded WHERE Id = @ID";
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(sqlCommand, con))
                {
                    command.Parameters.Add("@ID", SqlDbType.Int);
                    command.Parameters["@ID"].Value = id;

                    try
                    {
                        return command.ExecuteNonQuery();
                    }

                    catch
                    {
                        return 0;
                    }// using (SqlDataReader reader = command.ExecuteReader())

                }
            }
        }

        public static List<CommandsExecuted> getSpiritQuotesToBeAdded()
        {
            List<CommandsExecuted> commandsExecuted = new List<CommandsExecuted>();
            string sqlCommand = "select * from dbo.QuoteMessagesToBeAdded";
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(sqlCommand, con))
                {
                    try
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            CommandsExecuted executedCommand = new CommandsExecuted();
                            executedCommand.id = reader.GetInt32(0);
                            executedCommand.author = reader.GetString(1);
                            executedCommand.content = reader.GetString(2);
                            executedCommand.channel = reader.GetString(4);
                            executedCommand.time = reader.GetDateTimeOffset(5);
                            commandsExecuted.Add(executedCommand);
                        }
                        return commandsExecuted;
                    }

                    catch
                    {
                        return commandsExecuted;
                    }// using (SqlDataReader reader = command.ExecuteReader())

                }
            }
        }

        public static int insertQuoteToBeAdded()
        {
            string sqlCommand = "INSERT INTO dbo.QuoteMessagesToBeAdded(Author,Content,ChannelID,Channel,CreationTimeStamp) select Author, Content, ChannelID, Channel, CreationTimeStamp from dbo.Message where ChannelID = 379487060400275457  AND CreationTimeStamp> (select max(CreationTimeStamp) as 'CreationTimeStamp' from QuoteMessages)";
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(sqlCommand, con))
                {

                    try
                    {
                        return command.ExecuteNonQuery();
                    }

                    catch
                    {
                        return 0;
                    }// using (SqlDataReader reader = command.ExecuteReader())

                }
            }
        }

        public static int insertQuoteToBeAddedExecute()
        {
            string sqlCommand = "INSERT INTO dbo.QuoteMessages(Author,Content,ChannelID,Channel,CreationTimeStamp) select Author, Content, ChannelID, Channel, CreationTimeStamp from dbo.QuoteMessagesToBeAdded";
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(sqlCommand, con))
                {

                    try
                    {
                        return command.ExecuteNonQuery();
                    }

                    catch
                    {
                        return 0;
                    }// using (SqlDataReader reader = command.ExecuteReader())

                }
            }
        }

        public static int insertComment(string Author, string Content, string ChannelID, string Channel, DateTimeOffset CreationTimeStamp)
        {
            string sqlCommand = "INSERT INTO [dbo].[Message] ([Author],[Content],[ChannelID],[Channel],[CreationTimeStamp]) VALUES(@Author,@Content,@ChannelID,@Channel,@CreationTimeStamp)";
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(sqlCommand, con))
                {
                    command.Parameters.Add("@Author", SqlDbType.NVarChar);
                    command.Parameters["@Author"].Value = Author;

                    command.Parameters.Add("@Content", SqlDbType.NVarChar);
                    command.Parameters["@Content"].Value = Content;

                    command.Parameters.Add("@ChannelID", SqlDbType.NVarChar);
                    command.Parameters["@ChannelID"].Value = ChannelID;

                    command.Parameters.Add("@Channel", SqlDbType.NVarChar);
                    command.Parameters["@Channel"].Value = Channel;

                    command.Parameters.Add("@CreationTimeStamp", SqlDbType.DateTimeOffset);
                    command.Parameters["@CreationTimeStamp"].Value = CreationTimeStamp;

                    try
                    {
                        return command.ExecuteNonQuery();
                    }

                    catch
                    {
                        return 0;
                    }// using (SqlDataReader reader = command.ExecuteReader())

                }
            }
        }

        public static int insertCommandComment(string Author, string Content, string ChannelID, string Channel, DateTimeOffset CreationTimeStamp)
        {
            string sqlCommand = "INSERT INTO [dbo].CommandExecuted ([Author],[Content],[ChannelID],[Channel],[CreationTimeStamp]) VALUES(@Author,@Content,@ChannelID,@Channel,@CreationTimeStamp)";
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(sqlCommand, con))
                {
                    command.Parameters.Add("@Author", SqlDbType.NVarChar);
                    command.Parameters["@Author"].Value = Author;

                    command.Parameters.Add("@Content", SqlDbType.NVarChar);
                    command.Parameters["@Content"].Value = Content;

                    command.Parameters.Add("@ChannelID", SqlDbType.NVarChar);
                    command.Parameters["@ChannelID"].Value = ChannelID;

                    command.Parameters.Add("@Channel", SqlDbType.NVarChar);
                    command.Parameters["@Channel"].Value = Channel;

                    command.Parameters.Add("@CreationTimeStamp", SqlDbType.DateTimeOffset);
                    command.Parameters["@CreationTimeStamp"].Value = CreationTimeStamp;

                    try
                    {
                        return command.ExecuteNonQuery();
                    }

                    catch
                    {
                        return 0;
                    }// using (SqlDataReader reader = command.ExecuteReader())

                }
            }
        }

        //get a random spiritquote
        public static string getSpiritQuote()
        {
            string myQuote = ""; //users to return

            //    select count(*) from dbo.Message where [Author] like "%son%"

            //WHERE CreatedDate >= DATEADD(day, -7, GETDATE())

            string sqlCommand = "SELECT TOP 1 Content FROM dbo.QuoteMessages ORDER BY NEWID()";
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(sqlCommand, con))
                {
                    try
                    {

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {

                            myQuote = (reader.GetString(0));

                        }

                        return myQuote;
                    }

                    catch
                    {
                        return myQuote;
                    }// using (SqlDataReader reader = command.ExecuteReader())

                }
            }
        }

        //get commands executed past 24 hr
        public static List<CommandsExecuted> getSpiritExecutedCommandsLast10Cmd()
        {
            List<CommandsExecuted> commandsExecuted = new List<CommandsExecuted>();
            string sqlCommand = "SELECT TOP(10) * FROM CommandExecuted ORDER BY [CreationTimeStamp] DESC ";
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(sqlCommand, con))
                {
                    try
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            CommandsExecuted executedCommand = new CommandsExecuted();
                            executedCommand.author = reader.GetString(1);
                            executedCommand.content = reader.GetString(2);
                            executedCommand.channel = reader.GetString(4);
                            executedCommand.time = reader.GetDateTimeOffset(5);
                            commandsExecuted.Add(executedCommand);
                        }
                        return commandsExecuted;
                    }

                    catch
                    {
                        return commandsExecuted;
                    }// using (SqlDataReader reader = command.ExecuteReader())

                }
            }
        }

        //returns list of Users Like "string"
        public static List<string> getUserList(string Author)
        {
            List<string> myUsers = new List<string>(); //users to return

            //    select count(*) from dbo.Message where [Author] like "%son%"

            //WHERE CreatedDate >= DATEADD(day, -7, GETDATE())

            string sqlCommand = "select DISTINCT[Author] from dbo.Message where [Author] like @Param";
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(sqlCommand, con))
                {
                    Author = "%" + Author + "%";
                    command.Parameters.AddWithValue("Param", Author);

                    try
                    {

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {

                            myUsers.Add(reader.GetString(0));

                        }

                        return myUsers;
                    }

                    catch
                    {
                        return myUsers;
                    }// using (SqlDataReader reader = command.ExecuteReader())

                }
            }
        }

        public static int getCommentCount1Days(string Author)
        {
            string sqlCommand = "SELECT count(*) FROM dbo.Message where AUTHOR=@Author and creationTimeStamp >= DATEADD(day, -1, GETDATE())";
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(sqlCommand, con))
                {
                    command.Parameters.Add("@Author", SqlDbType.NVarChar);
                    command.Parameters["@Author"].Value = Author;



                    try
                    {

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            int myCount = (int)reader.GetValue(0);
                            return myCount;
                        }
                    }

                    catch
                    {
                        return 0;
                    }// using (SqlDataReader reader = command.ExecuteReader())
                    return 0;
                }
            }
        }

        public static int getCommentCount7Days(string Author)
        {

            string sqlCommand = "SELECT count(*) FROM dbo.Message where AUTHOR=@Author and creationTimeStamp >= DATEADD(day, -8, GETDATE())";

            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(sqlCommand, con))
                {
                    command.Parameters.Add("@Author", SqlDbType.NVarChar);
                    command.Parameters["@Author"].Value = Author;



                    try
                    {

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            int myCount = (int)reader.GetValue(0);
                            return myCount;
                        }
                    }

                    catch
                    {
                        return 0;
                    }// using (SqlDataReader reader = command.ExecuteReader())
                    return 0;
                }
            }
        }

        public static int getCommentCount14Days(string Author)
        {

            string sqlCommand = "SELECT count(*) FROM dbo.Message where AUTHOR=@Author and creationTimeStamp >= DATEADD(day, -14, GETDATE())";

            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(sqlCommand, con))
                {
                    command.Parameters.Add("@Author", SqlDbType.NVarChar);
                    command.Parameters["@Author"].Value = Author;



                    try
                    {

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            int myCount = (int)reader.GetValue(0);
                            return myCount;
                        }
                    }

                    catch
                    {
                        return 0;
                    }// using (SqlDataReader reader = command.ExecuteReader())
                    return 0;
                }
            }
        }

        public static int getCommentCount(string Author)
        {
            string sqlCommand = "SELECT count(*) FROM dbo.Message where AUTHOR=@Author";
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(sqlCommand, con))
                {
                    command.Parameters.Add("@Author", SqlDbType.NVarChar);
                    command.Parameters["@Author"].Value = Author;
                    try
                    {

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            int myCount = (int)reader.GetValue(0);
                            return myCount;
                        }
                    }

                    catch
                    {
                        return 0;
                    }// using (SqlDataReader reader = command.ExecuteReader())
                    return 0;
                }
            }
        }

        //returns list of Users by
        public static List<string> getActiveUsers(string timeFrame)
        {
            List<string> myUsers = new List<string>(); //users to return
            string sqlCommand = "";
            switch (timeFrame)
            {
                case "lastMonth":
                    sqlCommand = "SELECT distinct(author) FROM dbo.Message where creationTimeStamp >= dateadd(day,-30,dateadd(day,-30,getdate())) AND creationTimeStamp <= dateadd(day,-30,GETDATE())";
                    break;
                case "lastWeek":
                    sqlCommand = "SELECT distinct(author) FROM dbo.Message where creationTimeStamp >= dateadd(day,-7,dateadd(day,-7,getdate())) AND creationTimeStamp <= dateadd(day,-7,GETDATE())";
                    break;
                case "thisMonth":
                    sqlCommand = "SELECT distinct(author) FROM dbo.Message where creationTimeStamp >= dateadd(day,-30,getdate())";
                    break;
                case "thisWeek":
                    sqlCommand = "SELECT distinct(author) FROM dbo.Message where creationTimeStamp >= dateadd(day,-7,getdate())";
                    break;
            }

             using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(sqlCommand, con))
                {
                    try
                    {

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {

                            myUsers.Add(reader.GetString(0));

                        }

                        return myUsers;
                    }

                    catch
                    {
                        return myUsers;
                    }// using (SqlDataReader reader = command.ExecuteReader())

                }
            }
        }

    }
}
