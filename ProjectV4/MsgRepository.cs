using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectV4
{
    public class MsgRepository
    {
        UserRepository userRepository = UserRepository.Instance;

        public void SendMessage(string title, string body, int receiverId)
        {
            using (var db = new myContext())
            {
                db.Messages.Add(new Message(title, body, db.Users.Find(userRepository.currentUser.Id), db.Users.Find(receiverId)));
                db.SaveChanges();
            }
        }

        public List<Message> GetMessages()
        {
            using (var db = new myContext())
            {
                var messages = db.Messages.Include("Sender").Include("Receiver").Where(x => x.Receiver.Id == userRepository.currentUser.Id).OrderByDescending(x => x.Date).ToList();
                return messages;
            }
        }

        public string ViewMessage(int messageId)
        {
            using (var db = new myContext())
            {
                var message = db.Messages.Include("Sender").Include("Receiver").Where(x => x.Id == messageId).SingleOrDefault();
                message.Read = true;
                db.SaveChanges();
                return $" {message.Date}\n From : {message.Sender.Username}  ->  To : {message.Receiver.Username}\n Title : {message.Title}\n Message : {message.Body}";
            }
        }

        public string ViewMessageWithoutChangingRead(int messageId)
        {
            using (var db = new myContext())
            {
                var message = db.Messages.Include("Sender").Include("Receiver").Where(x => x.Id == messageId).SingleOrDefault();
                return $" {message.Date}\n From : {message.Sender.Username}  ->  To : {message.Receiver.Username}\n Title : {message.Title}\n Message : {message.Body}";
            }
        }

        public int UnreadMessageCounter()
        {
            using (var db = new myContext())
            {
                var unreadMessages = db.Messages.Where(x => x.Receiver.Id == userRepository.currentUser.Id && x.Read == false).ToList();
                int counter = unreadMessages.Count;
                return counter;
            }
        }

        // Length Chekcs
        public bool TitleCorrectLength(string title)
        {
            if (title.Length > 15)
            {
                return false;
            }
            return true;
        }

        public bool BodyCorrectLength(string body)
        {
            if (body.Length > 250)
            {
                return false;
            }
            return true;
        }

        // Tier123User
        public List<Message> GetMessageBetweenTwoUsers(int senderId, int receiverId)
        {
            using (var db = new myContext())
            {
                var messages = db.Messages.Include("Sender").Include("Receiver").Where(x => (x.Sender.Id == senderId && x.Receiver.Id == receiverId) || (x.Sender.Id == receiverId && x.Receiver.Id == senderId)).ToList();
                return messages;
            }
        }

        public void DeleteMessage(int messageId)
        {
            using (var db = new myContext())
            {
                db.Messages.Remove(db.Messages.Find(messageId));
                db.SaveChanges();
            }
        }

        public void EditMessageTitle(int messageId, string title)
        {
            using (var db = new myContext())
            {
                var message = db.Messages.Find(messageId);
                message.Title = title;
                db.SaveChanges();
            }
        }

        public void EditMessageBody(int messageId, string body)
        {
            using (var db = new myContext())
            {
                var message = db.Messages.Find(messageId);
                message.Body = body;
                db.SaveChanges();
            }
        }

        // SuperAdmin
        public void ChangeIdOfUserToDelete(int userId)
        {
            using (var db = new myContext())
            {
                var deletedUserMessages = db.Messages.Include("Sender").Include("Receiver").Where(x => x.Sender.Id == userId || x.Receiver.Id == userId).ToList();
                if (deletedUserMessages.Count != 0)
                {
                    foreach (Message message in deletedUserMessages)
                    {
                        if (message.Sender.Id == userId)
                        {
                            message.Sender = db.Users.Where(x => x.Username == "DeletedUser").SingleOrDefault();
                            db.SaveChanges();
                        }
                        if (message.Receiver.Id == userId)
                        {
                            message.Receiver = db.Users.Where(x => x.Username == "DeletedUser").SingleOrDefault();
                            db.SaveChanges();
                        }
                    }
                }
            }
        }

    }
}
