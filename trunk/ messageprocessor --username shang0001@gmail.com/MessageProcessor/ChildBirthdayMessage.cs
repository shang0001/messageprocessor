using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MessageProcessor
{
    public class ChildBirthdayMessage : GenericMessage
    {
        public Gender Gender { get; set; }

        [XmlIgnore]
        public DateTime BirthOfChild { get; set; }

        [XmlElement("BirthOfChild")]
        public string BirthOfChildString
        {
            get { return this.BirthOfChild.ToString("dd MMM yyyy"); }
            set { BirthOfChild = DateTime.Parse(value); }
        }
    }

    public enum Gender
    {
        Male,
        Female
    }
}
