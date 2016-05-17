using System;
using System.Collections.Generic;
using System.Text;
using System.Messaging;
using System.Threading.Tasks;
using System.IO;

namespace MSMQLib
{
    /// <summary>
    /// Represents an interface to MSMQ.
    /// </summary>
    public class MSMQInterface
    {
        /// <summary>
        /// Creates a MSMQ queue.
        /// </summary>
        /// <param name="path">The queue's path.</param>
        /// <returns>True if queue was created or false if path already exists.</returns>
        public async Task<object> CreateQueue(string path)
        {
            if (MessageQueue.Exists(path))
                return false;

            MessageQueue.Create(path);
            return true;
        }

        /// <summary>
        /// Checks whether a queue exists or not.
        /// </summary>
        /// <param name="path">The queue's path.</param>
        /// <returns>A boolean indicating if queue exists or not.</returns>
        public async Task<object> ExistsQueue(string path)
        {
            return MessageQueue.Exists(path);
        }

        /// <summary>
        /// Removes all queue's messages.
        /// </summary>
        /// <param name="path">The queue's path.</param>
        public async Task<object> PurgeQueue(string path)
        {
            MessageQueue queue = new MessageQueue(path);
            queue.Purge();
            return true;
        }

        /// <summary>
        /// Sends a message to a queue.
        /// </summary>
        /// <param name="input">The queue's path and message.</param>
        public async Task<object> SendMessage(dynamic input)
        {
            string path = (string)input.path;
            string message = (string)input.message;

            byte[] bytes = Encoding.UTF8.GetBytes(message);
            MessageQueue queue = new MessageQueue(path);
            Message msg = new Message();
            msg.BodyStream = new MemoryStream(bytes);

            await Task.Run(() => queue.Send(msg));
            return true;
        }

        /// <summary>
        /// Receives messages from a queue.
        /// </summary>
        /// <param name="input">The queue's path and receive callback.</param>
        public async Task<object> ReceiveMessages(dynamic input)
        {
            var path = (string)input.path;
            var receive = (Func<object, Task<object>>)input.receive;

            MessageQueue queue = new MessageQueue(path);
            queue.Formatter = new BinaryMessageFormatter();
            queue.MessageReadPropertyFilter.SetAll();

            while (true)
            {
                var msg = await Task.Factory.FromAsync<Message>(
                           queue.BeginReceive(),
                           queue.EndReceive);

                var message = (MSMQMessage)msg;
                receive(message);
            }
        }

        /// <summary>
        /// Gets all messages from a queue without removing them.
        /// </summary>
        /// <param name="path">The queue's path.</param>
        /// <returns>A list of messages.</returns>
        public async Task<object> GetAllMessages(string path)
        {
            List<MSMQMessage> messages = new List<MSMQMessage>();
            MessageQueue queue = new MessageQueue(path);
            queue.MessageReadPropertyFilter.SetAll();

            foreach (var msg in queue.GetAllMessages())
            {
                var message = (MSMQMessage)msg;
                messages.Add(message);
            }

            return messages;
        }
    }
}
