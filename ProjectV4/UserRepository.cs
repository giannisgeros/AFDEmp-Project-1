using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectV4
{
    public class UserRepository
    {
        public User currentUser;

        // Singleton Pattern
        private static UserRepository instance = null;
        public static UserRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UserRepository();
                }
                return instance;
            }
        }

        private UserRepository()
        {

        }

        public void AssignCurrentUser(string username, string password)
        {
            using (var db = new myContext())
            {
                currentUser = db.Users.Where(x => x.Username == username && x.Password == password).SingleOrDefault();
            }
        }

        public bool UsernameExists(string username)
        {
            using (var db = new myContext())
            {
                var user = db.Users.Where(x => x.Username == username).SingleOrDefault();
                return UserExists(user);
            }

        }

        public bool UserExists(User user)
        {
            if (user == null)
            {
                return false;
            }
            return true;
        }

        public bool UserIndexExists(int index)
        {
            using (var db = new myContext())
            {
                var user = db.Users.Find(index);
                return UserExists(user);
            }  
        }

        public bool CorrectPasswordLength(string password)
        {
            if (password.Length < 5 || password.Length > 12)
            {
                return false;
            }
            return true;
        }

        public void CreateUserSimple(string registerUsername, string registerPassword)
        {
            using (var db = new myContext())
            {
                db.Users.Add(new User(registerUsername, registerPassword));
                db.SaveChanges();
            }
        }

        public bool UsernameAndPasswordMatch(string loginUsername, string loginPassword)
        {
            using (var db = new myContext())
            {
                var user = db.Users.Where(x => x.Username == loginUsername && x.Password == loginPassword).SingleOrDefault();
                return UserExists(user);
            }
            
        }

        public List<User> GetUsers()
        {
            using (var db = new myContext())
            {
                var users = db.Users.ToList();
                return users;
            }
        }

        public void NullCurrentUser()
        {
            currentUser = null;
        }


        // Superadmin

        public void CreateUserExtended(string username, string password, Role role)
        {
            using (var db = new myContext())
            {
                db.Users.Add(new User(username, password, role));
                db.SaveChanges();
            }
        }

        public void DeleteUser(int userId)
        {
            using (var db = new myContext())
            {
                db.Users.Remove(db.Users.Find(userId));
                db.SaveChanges();
            }
        }

        public User GetUser(int userId)
        {
            using (var db = new myContext())
            {
                return db.Users.Find(userId);
            } 
        }

        public void UpdateUserUsername(int userId, string username)
        {
            using (var db = new myContext())
            {
                var user = db.Users.Find(userId);
                user.Username = username;
                db.SaveChanges();
            }
        }

        public void UpdateUserPassword(int userId, string password)
        {
            using (var db = new myContext())
            {
                var user = db.Users.Find(userId);
                user.Password = password;
                db.SaveChanges();
            }
        }

        public void UpdateUserRole(int userId, Role role)
        {
            using (var db = new myContext())
            {
                var user = db.Users.Find(userId);
                user.Role = role;
                db.SaveChanges();
            }
        }
    }
}
