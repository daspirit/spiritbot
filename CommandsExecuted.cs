using System;
using System.Collections.Generic;
using System.Text;

namespace SpiritBot
{
    class CommandsExecuted
    {
        public CommandsExecuted()
            {
            }
        public string author;

       public string authorParsed()
        {
            return author.Substring(author.IndexOf(';') + 1);
        }
       
                          
        public string content;
        public string channel;
        public DateTimeOffset time;
        public int id;
       
    }
}
