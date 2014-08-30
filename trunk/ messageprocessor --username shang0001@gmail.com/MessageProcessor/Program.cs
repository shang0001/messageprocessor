using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            BirthdayMessage message1 = new BirthdayMessage
            {
                MsgId = 1,
                MsgType = MessageType.BIRTHDAY,
                Name = "Birthday message 1",
                MsgText = "Mate, Happy Birthday. To celebrate this once a year occasion we have picked the following gift: PS3. Enjoy",
                BirthDate = new DateTime(1970, 4, 29),
                gift = "new gift"
            };

            ChildBirthdayMessage message2 = new ChildBirthdayMessage
            {
                MsgId = 2,
                MsgType = MessageType.BIRTHOFCHILD,
                Name = "Birthday message 2",
                MsgText = "Mate's son, Happy Birthday.",
                BirthOfChild = new DateTime(1990, 12, 13),
                Gender = Gender.Male
            };

            Processor proc = new Processor();
            proc.ProcessMessage(message1);
            proc.ProcessMessage(message2);
        }
    }
}
