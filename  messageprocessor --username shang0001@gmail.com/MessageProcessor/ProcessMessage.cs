using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Xml.Serialization;
using System.Web;
using System.Configuration;

namespace MessageProcessor
{
    public class Processor
    {
        private StreamWriter logger = null;

        public Processor() {
            string folderPath = ConfigurationManager.AppSettings.Get("logFolder");
            string filePath = folderPath + @"\log.txt";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            if (!File.Exists(filePath))
            {
                logger = File.CreateText(filePath);
            }
            else
            {
                logger = new StreamWriter(File.OpenWrite(filePath));
            }
        }

        public void ProcessMessage(GenericMessage message)
        {
            switch (message.MsgType)
            {
                case MessageType.BIRTHDAY:
                    ProcessBirthday((BirthdayMessage) message);
                    return;
                case MessageType.BIRTHOFCHILD:
                    ProcessBirthOfChild((ChildBirthdayMessage) message);
                    return;
                default:
                    return;
            }                   
        }

        #region Process Birthday message
        private void ProcessBirthday(BirthdayMessage msg)
        {
            // Convert 'Standard Message Text' field to all Upper case 
            msg.MsgText = msg.MsgText.ToUpper();       
            // Serialize to json
            string json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(msg);
            //write to file in directory /Birthdays
            string folderPath = ConfigurationManager.AppSettings.Get("birthdayFolder");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            string filePath = folderPath + string.Format(@"\Birthday_{0}.txt", msg.MsgId);
            StreamWriter writer = File.CreateText(filePath);
            writer.Write(json);
            writer.Close();
            LogMessages(msg, filePath);
        }
        #endregion

        #region Process Birth of Child message
        private void ProcessBirthOfChild(ChildBirthdayMessage msg)
        {
            // Base 64 encode the 'Name' field
            msg.Name = EncodeTo64(msg.Name);

            // Serialize to xml and write to file in directory /BabyBirth
            string folderPath = ConfigurationManager.AppSettings.Get("childBirthdayFolder");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            string filePath = folderPath + string.Format(@"\ChildBirthday_{0}.xml", msg.MsgId);
            XmlSerializer serializer = new XmlSerializer(typeof(ChildBirthdayMessage));
            TextWriter textWriter = new StreamWriter(filePath);
            serializer.Serialize(textWriter, msg);
            textWriter.Close();
            LogMessages(msg, filePath);
        }
        #endregion

        // Convert string to Encode64
        static public string EncodeTo64(string strField)
        {
            var strBytes = System.Text.Encoding.UTF8.GetBytes(strField);
            return System.Convert.ToBase64String(strBytes);
        }

        private void LogMessages(GenericMessage msg, string msgFilePath)
        {
            logger.WriteLine(string.Format("Processed a {0} message, whose id is {1}, locates at {2}", msg.MsgType, msg.MsgId, msgFilePath));
            logger.Flush();
        }
    }

}
