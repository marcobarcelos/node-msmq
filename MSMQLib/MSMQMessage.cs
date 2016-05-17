using System;
using System.IO;
using System.Messaging;
using System.Text;

namespace MSMQLib
{
    /// <summary>
    /// Represents a MSMQ Message.
    /// </summary>
    public class MSMQMessage
    {
        public string id { get; set; }

        public string correlationId { get; set; }

        public long lookupId { get; set; }

        public int appSpecific { get; set; }

        public string body { get; set; }

        public int size { get; set; }

        public int priority { get; set; }

        public string label { get; set; }

        public DateTime sentTime { get; set; }

        public DateTime arrivedTime { get; set; }

        /// <summary>
       /// Converts a Message to a MSMQMessage.
       /// </summary>
       /// <param name="message">The message object.</param>
        public static explicit operator MSMQMessage(Message message)
        {
            var stream = (MemoryStream)message.BodyStream;
            var bytes = stream.ToArray();
            var body = Encoding.UTF8.GetString(bytes);

            return new MSMQMessage()
            {
                id = message.Id,
                correlationId = message.CorrelationId,
                lookupId = message.LookupId,
                appSpecific = message.AppSpecific,
                body = body,
                size = body.Length,
                priority = (int)message.Priority,
                label = message.Label,
                sentTime = message.SentTime,
                arrivedTime = message.ArrivedTime
            };
        }
    }
}
