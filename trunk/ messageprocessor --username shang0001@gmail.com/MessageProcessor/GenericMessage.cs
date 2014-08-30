using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageProcessor
{
    public class GenericMessage
    {
        public int MsgId { get; set; }
        public MessageType MsgType { get; set; }

        public string Name { get; set; }
        public string MsgText { get; set; }
    }

    public enum MessageType
    {
        BIRTHDAY,
        BIRTHOFCHILD
    }
}
