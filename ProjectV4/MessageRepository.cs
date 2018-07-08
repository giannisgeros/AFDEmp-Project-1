using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectV4
{
    public class MessageRepository
    {
        UserRepository userRepository = UserRepository.Instance;
        //public Message currentMessage;

        //// Singleton Pattern
        //private static MessageRepository instance = null;
        //public static MessageRepository Instance
        //{
        //    get
        //    {
        //        if (instance == null)
        //        {
        //            instance = new MessageRepository();
        //        }
        //        return instance;
        //    }
        //}

        //private MessageRepository()
        //{

        //}

        public bool SendMessageToUser(string title, string body, int receiverId)
        {
            using (var db = new myContext())
            {
                db.Messages.Add(new Message(title, body, userRepository.currentUser.Id, receiverId));
            }
        }

        public void ViewMessageFromUser(int senderId)
        { }
    }
}
