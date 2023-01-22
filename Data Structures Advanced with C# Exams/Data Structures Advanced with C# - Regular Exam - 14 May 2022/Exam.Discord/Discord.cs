using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam.Discord
{
    public class Discord : IDiscord
    {
        private Dictionary<string, List<Message>> messagesByChannel;
        private Dictionary<string, Message> messagesById;

        public Discord()
        {
            messagesByChannel = new Dictionary<string, List<Message>>();
            messagesById = new Dictionary<string, Message>();
        }

        public int Count => messagesByChannel.Count;

        public bool Contains(Message message)
            => messagesById.ContainsKey(message.Id);
        

        public void DeleteMessage(string messageId)
        {
            var message = GetMessage(messageId);

            messagesByChannel[message.Channel].Remove(message);
            messagesById.Remove(messageId);
        }

        public IEnumerable<Message> GetAllMessagesOrderedByCountOfReactionsThenByTimestampThenByLengthOfContent()
            => messagesById.Values.OrderByDescending(x => x.Reactions.Count).ThenBy(x => x.Timestamp).ThenBy(x => x.Content.Length);

        public IEnumerable<Message> GetChannelMessages(string channel)
        {
            if (!messagesByChannel.ContainsKey(channel))
            {
                throw new ArgumentException();
            }

            var messages = messagesByChannel[channel];

            if(messages.Count == 0)
            {
                throw new ArgumentException();
            }

            return messages;
        }

        public Message GetMessage(string messageId)
        {
            var message = messagesById.Values.FirstOrDefault(x => x.Id == messageId);
            
            if(message == null)
            {
                throw new ArgumentException();
            }

            return message;
        }

        public IEnumerable<Message> GetMessageInTimeRange(int lowerBound, int upperBound)
            => messagesById.Values.Where(x => x.Timestamp >= lowerBound && x.Timestamp <= upperBound)
                                  .OrderByDescending(x => messagesByChannel[x.Channel].Count);

        public IEnumerable<Message> GetMessagesByReactions(List<string> reactions)
        {
            var messages = messagesById.Values
                .Where(x => reactions.All(y => x.Reactions.Contains(y)))
                .OrderByDescending(x => x.Reactions.Count)
                .ThenBy(x => x.Timestamp);

            return messages;
        }

        public IEnumerable<Message> GetTop3MostReactedMessages()
            => messagesById.Values.OrderByDescending(x => x.Reactions.Count).Take(3);


        public void ReactToMessage(string messageId, string reaction)
        {
            var message = GetMessage(messageId);

            message.Reactions.Add(reaction);
        }

        public void SendMessage(Message message)
        {
            if (!messagesByChannel.ContainsKey(message.Channel))
            {
                messagesByChannel.Add(message.Channel, new List<Message>());
            }

            messagesByChannel[message.Channel].Add(message);
            messagesById.Add(message.Id, message);
        }
    }
}
