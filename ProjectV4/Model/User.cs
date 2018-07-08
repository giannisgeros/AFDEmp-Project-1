using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectV4
{
    public class User
    {
        #region Constructors
        public User()
        {
            MessagesSent = new List<Message>();
            MessagesReceived = new List<Message>();
        }

        public User(string username, string password)
        {
            MessagesSent = new List<Message>();
            MessagesReceived = new List<Message>();
            Username = username;
            Password = password;
            Role = Role.Tier0User;
        }

        public User(string username, string password, Role role)
        {
            MessagesSent = new List<Message>();
            MessagesReceived = new List<Message>();
            Username = username;
            Password = password;
            Role = role;
        }
        #endregion

        #region Properties
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }

        [InverseProperty("Sender")]
        public ICollection<Message> MessagesSent { get; set; }

        [InverseProperty("Receiver")]
        public ICollection<Message> MessagesReceived { get; set; }
        #endregion

    }
}
