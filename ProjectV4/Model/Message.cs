using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectV4
{
    public class Message
    {
        #region Constructors
        public Message()
        {

        }

        public Message(string title, string body, User sender, User receiver)
        {
            Title = title;
            Body = body;
            Date = DateTime.Now;
            Read = false;
            Receiver = receiver;
            Sender = sender;
        }
        #endregion

        #region Properties   
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime Date { get; set; }
        public bool Read { get; set; }

        public User Sender { get; set; }
        public User Receiver { get; set; }
        #endregion

    }
}
