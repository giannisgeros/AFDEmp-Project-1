using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectV4
{
    public class Menus
    {
        UserRepository UserRepository = UserRepository.Instance;
        MsgRepository MessageRepository = new MsgRepository();
        LogRepository LogRepository = new LogRepository();

        #region MainMenu
        public MainMenuOptions MainMenu()
        {
                List<string> mainMenuItems = new List<string> { "Login", "Register", "Exit" };
                MenuDesigner MainMenu1 = new MenuDesigner(mainMenuItems, "MainMenu");
                int mainMenuChoice = MainMenu1.MenuDesign();
                switch (mainMenuChoice)
                {
                    case 0:
                        return MainMenuOptions.Login;
                    case 1:
                        return MainMenuOptions.Register;
                    case 2:
                        return MainMenuOptions.Exit;
                    default:
                        return MainMenuOptions.Exit;
                }
        }
        #endregion

        #region Username And Password
        public string UsernameMenu()
        {
            Console.Write("Username: ");
            string registerUsername = Various.TypeLetterByLetterWithoutMasking();
            return registerUsername;
        }

        public string PasswordMenu()
        {
            Console.Write("Password: ");
            string registerPassword = Various.TypeLetterByLetterWithMasking();
            return registerPassword;
        }
        #endregion

        #region SuperAdmin
        public SuperAdminOptions SuperAdminMenu()
        {
                List<string> superAdminMenuItems = new List<string> { "Create User", "Delete User", "Update User", "View Users", "Send Message", $"View Messages[{MessageRepository.UnreadMessageCounter()}]", "Exit" };
                MenuDesigner SuperAdminMenu = new MenuDesigner(superAdminMenuItems, $"{UserRepository.currentUser.Username} : MainMenu");
                int superadminMenuChoice = SuperAdminMenu.MenuDesign();
                switch(superadminMenuChoice)
                {
                    case 0:
                        return SuperAdminOptions.CreateUser;
                    case 1:
                        return SuperAdminOptions.DeleteUser;
                    case 2:
                        return SuperAdminOptions.UpdateUser;
                    case 3:
                        return SuperAdminOptions.ViewUsers;
                    case 4:
                        return SuperAdminOptions.SendMessage;
                    case 5:
                        return SuperAdminOptions.ViewMessages;
                    case 6:
                        return SuperAdminOptions.Exit;
                    default:
                        return SuperAdminOptions.Exit;
                }
        }

        public RoleOptions RoleMenu(string operation)
        {
                List<string> roleMenuItems = new List<string> { "Tier3User", "Tier2User", "Tier1User", "Tier0User", "Exit" };
                MenuDesigner RoleMenu = new MenuDesigner(roleMenuItems, $"{UserRepository.currentUser.Username} : {operation} : Choose Role");
                int roleMenuChoice = RoleMenu.MenuDesign();
                switch (roleMenuChoice)
                {
                    case 0:
                        return RoleOptions.Tier3User;
                    case 1:
                        return RoleOptions.Tier2User;
                    case 2:
                        return RoleOptions.Tier1User;
                    case 3:
                        return RoleOptions.Tier0User;
                    case 4:
                        return RoleOptions.Exit;
                    default:
                        return RoleOptions.Exit;
                }
        }

        public UpdateOptions UpdateMenu()
        {
            List<string> updateMenuItems = new List<string> { "Username", "Password", "Role", "Exit" };
            MenuDesigner UpdateMenu = new MenuDesigner(updateMenuItems, $"{UserRepository.currentUser.Username} : Update User");
            int updateMenuChoice = UpdateMenu.MenuDesign();
            switch (updateMenuChoice)
            {
                case 0:
                    return UpdateOptions.Username;
                case 1:
                    return UpdateOptions.Password;
                case 2:
                    return UpdateOptions.Role;
                case 3:
                    return UpdateOptions.Exit;
                default:
                    return UpdateOptions.Exit;
            }
        }

        public int DeleteUserPick()
        {
            Console.Clear();
            Various.Header($"{UserRepository.currentUser.Username} : Delete User");

            ViewUsersExtended();
            Console.WriteLine();

            int userId = PickUser();
            if (userId == -1)
            {
                return -1;
            }

            Console.Write("Are you sure? (Y/N): ");
            string yesOrNo = Various.TypeLetterByLetterWithoutMaskingYesOrNoSTR();
            if (yesOrNo == "Y" || yesOrNo == "y")
            {
                return userId;
            }
            else
            {
                return -1;
            }
        }

        public int UpdateUserPick()
        {
            Console.Clear();
            Various.Header($"{UserRepository.currentUser.Username} : Update User");

            ViewUsersExtended();
            Console.WriteLine();

            int userId = PickUser();
            if (userId == -1)
            {
                return -1;
            }

            return userId;
        }
        #endregion

        #region Messages
        // Choose userId to send message
        public int SelectUser()
        {
            Console.Clear();
            Various.Header($"{UserRepository.currentUser.Username} : Send Message");
            ViewUsers();
            Console.WriteLine();

            int userId = PickUser();
            if (userId == -1)
            {
                Console.WriteLine();
                Various.SystemMessage("SendMessage Failed");
                System.Threading.Thread.Sleep(1500);
            }
            return userId;
        }

        // Type the message
        public void TypeMessage()
        {
            int receiverId = SelectUser();
            if (receiverId < 0)
            {
                return;
            }
            User Receiver = new User();
            using (var db = new myContext())
            {
                Receiver = db.Users.Find(receiverId);
            }
            Console.Clear();
            Various.Header($"{ UserRepository.currentUser.Username} : Send Message to {Receiver.Username}");

            string title;
            string body;
            // Title
            Various.SystemMessageDirections("Title must be max 15 characters long");
            Console.Write("Type the title of the message: ");
            title = Various.TypeLetterByLetterWithoutMaskingSTR(15);

            // Checks if Esc is pressed
            if (title == "")
            {
                Console.WriteLine();
                Various.SystemMessage("Send Message Failed");
                System.Threading.Thread.Sleep(1500);
                return;
            }

            // Body
            Console.WriteLine();
            Console.WriteLine();
            Various.SystemMessageDirections("Body must be max 250 characters long");
            Console.Write("Type the body of the message: ");
            body = Various.TypeLetterByLetterWithoutMaskingSTR(249);

            // Checks if Esc is pressed
            if (body == "")
            {
                Console.WriteLine();
                Various.SystemMessage("Send Message Failed");
                System.Threading.Thread.Sleep(1500);
                return;
            }
            MessageRepository.SendMessage(title, body, receiverId);
            LogRepository.TypeInLog($"{UserRepository.currentUser.Username}", "sent message");
            Console.WriteLine();
            Various.SystemMessageSuccess("Message Sent Successful");
            System.Threading.Thread.Sleep(1500);
            return;
        }

        public void ViewMessages()
        {
            Console.Clear();

            var messages = MessageRepository.GetMessages();
            if (messages.Count == 0)
            {
                Various.SystemMessage("No messages");
                System.Threading.Thread.Sleep(1500);
                return;
            }

            MenuDesigner MessageMenu = new MenuDesigner(messages, $"{UserRepository.currentUser.Username} : View Messages");
            int messageViewChoice = MessageMenu.MenuDesignMessage(out int correctMessageId);
            if (correctMessageId < 0)
            {
                return;
            }
            Console.Clear();
            Various.Header($"{UserRepository.currentUser.Username} : View Messages");
            Console.WriteLine(MessageRepository.ViewMessage(correctMessageId));
            LogRepository.TypeInLog($"{UserRepository.currentUser.Username}", "viewed message");
            Console.WriteLine();
            Various.SystemMessage("Press any key to exit");
            Console.ReadKey();

        }
        #endregion

        #region Tier123User
        public Tier3UserOptions Tier3UserMenu()
        {
            List<string> Tier3UserMenuItems = new List<string> { "View Message", "Edit Message", "Delete Message", "Send Message", $"View Messages[{MessageRepository.UnreadMessageCounter()}]", "Exit" };
            MenuDesigner Tier3UserMenu = new MenuDesigner(Tier3UserMenuItems, $"{UserRepository.currentUser.Username} : MainMenu");
            int tier3UserChoice = Tier3UserMenu.MenuDesign();
            switch (tier3UserChoice)
            {
                case 0:
                    return Tier3UserOptions.ViewMessage;
                case 1:
                    return Tier3UserOptions.EditMessage;
                case 2:
                    return Tier3UserOptions.DeleteMessage;
                case 3:
                    return Tier3UserOptions.SendMessage;
                case 4:
                    return Tier3UserOptions.ViewMessages;
                case 5:
                    return Tier3UserOptions.Exit;
                default:
                    return Tier3UserOptions.Exit;
            }
        }

        public Tier2UserOptions Tier2UserMenu()
        {
            List<string> Tier2UserMenuItems = new List<string> { "View Message", "Edit Message", "Send Message", $"View Messages[{MessageRepository.UnreadMessageCounter()}]", "Exit" };
            MenuDesigner Tier2UserMenu = new MenuDesigner(Tier2UserMenuItems, $"{UserRepository.currentUser.Username} : MainMenu");
            int tier2UserChoice = Tier2UserMenu.MenuDesign();
            switch (tier2UserChoice)
            {
                case 0:
                    return Tier2UserOptions.ViewMessage;
                case 1:
                    return Tier2UserOptions.EditMessage;
                case 2:
                    return Tier2UserOptions.SendeMessage;
                case 3:
                    return Tier2UserOptions.ViewMessages;
                case 4:
                    return Tier2UserOptions.Exit;
                default:
                    return Tier2UserOptions.Exit;
            }
        }

        public Tier1UserOptions Tier1UserMenu()
        {
            List<string> Tier1UserMenuItems = new List<string> { "View Message", "Send Message", $"View Messages[{MessageRepository.UnreadMessageCounter()}]", "Exit" };
            MenuDesigner Tier1UserMenu = new MenuDesigner(Tier1UserMenuItems, $"{UserRepository.currentUser.Username} : MainMenu");
            int tier1UserChoice = Tier1UserMenu.MenuDesign();
            switch (tier1UserChoice)
            {
                case 0:
                    return Tier1UserOptions.ViewMessage;
                case 1:
                    return Tier1UserOptions.SendMessage;
                case 2:
                    return Tier1UserOptions.ViewMessages;
                case 3:
                    return Tier1UserOptions.Exit;
                default:
                    return Tier1UserOptions.Exit;
            }
        }

        public Tier0UserOptions Tier0UserMenu()
        {
            List<string> Tier1UserMenuItems = new List<string> { "Send Message", $"View Messages[{MessageRepository.UnreadMessageCounter()}]", "Exit" };
            MenuDesigner Tier1UserMenu = new MenuDesigner(Tier1UserMenuItems, $"{UserRepository.currentUser.Username} : MainMenu");
            int tier1UserChoice = Tier1UserMenu.MenuDesign();
            switch (tier1UserChoice)
            {
                case 0:
                    return Tier0UserOptions.SendMessage;
                case 1:
                    return Tier0UserOptions.ViewMessages;
                case 2:
                    return Tier0UserOptions.Exit;
                default:
                    return Tier0UserOptions.Exit;
            }
        }

        public void ViewUsers()
        {
            List<User> users = UserRepository.GetUsers();
            foreach (User user in users)
            {
                if (user.Username != "DeletedUser" && user.Username != UserRepository.currentUser.Username)
                {
                    Console.WriteLine($"User[{user.Id}] - {user.Username}");
                }
            }
        }

        public void ViewAllUsers()
        {
            List<User> users = UserRepository.GetUsers();
            foreach (User user in users)
            {
                Console.WriteLine($"User[{user.Id}] - {user.Username}");
            }
        }

        public void ViewUsersExtended()
        {
            List<User> users = UserRepository.GetUsers();
            foreach (User user in users)
            {
                if (user.Username != "DeletedUser" && user.Username != UserRepository.currentUser.Username)
                {
                    Console.WriteLine($"User[{user.Id}] - {user.Username} - {user.Role}");
                }
            }
        }

        public void PickTwoUsers(string operation, out int senderId, out int receiverId)
        {
            Console.Clear();
            Various.Header($"{UserRepository.currentUser.Username} : {operation}");
            Console.WriteLine();
            Various.SystemMessageDirections("Pick two user ids so you can see the messages between them");
            ViewAllUsers();
            Console.WriteLine();

            int userId1 = PickUserExtended();
            if (userId1 == -1)
            {
                senderId = -1;
                receiverId = -1;
                return;
            }

            int userId2 = PickUserExtended();
            if (userId2 == -1)
            {
                senderId = -1;
                receiverId = -1;
                return;
            }

            senderId = userId1;
            receiverId = userId2;
        }

        public void ViewAMessageBetweenTwoUsers(List<Message> messages)
        {
            if (messages.Count == 0)
            {
                Console.WriteLine();
                Various.SystemMessage("There are no messages");
                System.Threading.Thread.Sleep(1500);
                return;
            }
            Console.Clear();
            MenuDesigner MessageMenu = new MenuDesigner(messages, $"{UserRepository.currentUser.Username} : View Messages Between Two Users");
            int messageViewChoice = MessageMenu.MenuDesignMessage(out int correctMessageId);
            if (correctMessageId < 0)
            {
                return;
            }
            Console.Clear();
            Various.Header($"{UserRepository.currentUser.Username} : View Messages Between Two Users");
            Console.WriteLine(MessageRepository.ViewMessageWithoutChangingRead(correctMessageId));
            Console.WriteLine();
            Various.SystemMessage("Press any key to exit");
            Console.ReadKey();
        }

        public void DeleteAMessageBetweenTwoUsers(List<Message> messages)
        {
            if (messages.Count == 0)
            {
                Console.WriteLine();
                Various.SystemMessage("There are no messages");
                System.Threading.Thread.Sleep(2000);
                return;
            }
            Console.Clear();
            MenuDesigner MessageMenu = new MenuDesigner(messages, $"{UserRepository.currentUser.Username} : Delete Message");
            int messageViewChoice = MessageMenu.MenuDesignMessage(out int correctMessageId);
            if (correctMessageId < 0)
            {
                Various.SystemMessage("Delete Message Failed");
                System.Threading.Thread.Sleep(2000);
                return;
            }
            Console.Clear();
            Various.Header($"{UserRepository.currentUser.Username} : Delete Message");
            Console.WriteLine(MessageRepository.ViewMessageWithoutChangingRead(correctMessageId));
            Console.WriteLine();
            Console.Write("Are you sure? (Y / N): ");
            string yesOrno = Various.TypeLetterByLetterWithoutMaskingYesOrNoSTR();
            if (yesOrno == "y" || yesOrno == "Y")
            {
                MessageRepository.DeleteMessage(correctMessageId);
                Console.WriteLine();
                Various.SystemMessageSuccess("Message Deleted");
                System.Threading.Thread.Sleep(1500);
            }
            else
            {
                Console.WriteLine();
                Various.SystemMessage("Delete Message failed");
                System.Threading.Thread.Sleep(1500);
            }
        }

        public string TitleOrBodyEdit(int messageToBeEditedId, out int choiceToEdit)
        {
            Console.Clear();
            List<string> TitleOrBodyMenuItems = new List<string> { "Title", "Body", "Exit" };
            MenuDesigner TitleOrBodyMenu = new MenuDesigner(TitleOrBodyMenuItems, $"{UserRepository.currentUser.Username} : Edit Message");
            int choice = TitleOrBodyMenu.MenuDesign();
            Console.WriteLine();

            switch (choice)
            {
                case 0:
                    // Title
                    Console.Clear();
                    Various.Header($"{UserRepository.currentUser.Username} : Edit Message : Edit Title");
                    string title;

                    Console.WriteLine(MessageRepository.ViewMessageWithoutChangingRead(messageToBeEditedId));
                    Console.WriteLine();
                    Various.SystemMessageDirections("Title must be max 15 characters long and cannot be void");
                    Console.Write("Type the title of the message: ");
                    title = Various.TypeLetterByLetterWithoutMaskingSTR(15);

                    // Checks if Esc is pressed
                    if (title == "")
                    {
                        choiceToEdit = -1;
                        return "";
                    }
                    choiceToEdit = 0;
                    return title;
                case 1:
                    // Body
                    Console.Clear();
                    Various.Header($"{UserRepository.currentUser.Username} : Edit Message : Edit Body");

                    string body;
                    Console.WriteLine(MessageRepository.ViewMessageWithoutChangingRead(messageToBeEditedId));
                    Console.WriteLine();
                    Various.SystemMessageDirections("Body must be max 250 characters long and cannot be void");
                    Console.Write("Type the body of the message: ");
                    body = Various.TypeLetterByLetterWithoutMaskingSTR(249);

                    // Checks if Esc is pressed
                    if (body == "")
                    {
                        choiceToEdit = -1;
                        return "";
                    }
                    choiceToEdit = 1;
                    return body;
                case 2:
                    choiceToEdit = -1;
                    return "";
                default:
                    choiceToEdit = -1;
                    return "";
            }
        }

        public int PickAMessageBetweenTwoUsers(List<Message> messages)
        {
            if (messages.Count == 0)
            {
                Console.WriteLine();
                Various.SystemMessage("There are no messages");
                System.Threading.Thread.Sleep(1500);
                return -1;
            }
            Console.Clear();
            MenuDesigner MessageMenu = new MenuDesigner(messages, $"{UserRepository.currentUser.Username} : Edit Message");
            int messageViewChoice = MessageMenu.MenuDesignMessage(out int correctMessageId);
            return correctMessageId;

        }
        #endregion

        #region Pick User
        public int PickUser()
        {
            User userPicked = null;
            bool canPass = false;
            int tries = 0;
            int userId = 0;
            do
            {
                Console.Write("Select user: ");
                userId = Various.TypeLetterByLetterWithoutMaskingiNT();
                canPass = true;

                // Checks if Esc is pressed
                if (userId == -1)
                {
                    return -1;
                }

                Console.WriteLine();
                userPicked = UserRepository.GetUser(userId);

                if (userPicked == null || userPicked.Username == "DeletedUser")
                {
                    canPass = false;
                    Various.SystemMessage("This user does not exist");
                    System.Threading.Thread.Sleep(1500);
                }
                tries++;
            } while ((!canPass && tries < 3) || (!canPass && tries < 3));
            if (tries >= 3)
            {
                return -1;
            }
            return userId;
        }

        public int PickUserExtended()
        {
            User userPicked = null;
            bool canPass = false;
            int tries = 0;
            int userId = 0;
            do
            {
                Console.Write("Select user: ");
                userId = Various.TypeLetterByLetterWithoutMaskingiNT();
                canPass = true;

                // Checks if Esc is pressed
                if (userId == -1)
                {
                    return -1;
                }

                Console.WriteLine();
                userPicked = UserRepository.GetUser(userId);

                if (userPicked == null)
                {
                    canPass = false;
                    Various.SystemMessage("This user does not exist");
                    System.Threading.Thread.Sleep(1500);
                }
                tries++;
            } while ((!canPass && tries < 3) || (!canPass && tries < 3));
            if (tries >= 3)
            {
                return -1;
            }
            return userId;
        }
        #endregion
    }
}
