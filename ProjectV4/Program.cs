using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectV4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Menus Menu = new Menus();
            UserRepository UserRepository = UserRepository.Instance;
            LogRepository LogRepository = new LogRepository();

            LogRepository.TypeInLog("Application", "started");
            do
            {
                try
                {
                    do
                    {
                        MainMenuOptions mainMenuChoice = Menu.MainMenu();
                        switch (mainMenuChoice)
                        {
                            case MainMenuOptions.Login:
                                #region Login
                                Console.Clear();
                                Various.Header("Login");

                                bool usernameExists;
                                bool correctPasswordLength;
                                bool usernameMatchesPassword;
                                int tries = 0;
                                string loginUsername;
                                string loginPassword;

                                // Username Check
                                do
                                {
                                    loginUsername = Menu.UsernameMenu();
                                    usernameExists = UserRepository.UsernameExists(loginUsername);
                                    tries++;

                                    if (usernameExists == false && tries < 3)
                                    {
                                        Console.WriteLine();
                                        Various.SystemMessage("Username does not exist");
                                    }
                                } while (usernameExists == false && tries < 3);
                                if (tries >= 3 && usernameExists == false)
                                {
                                    Console.WriteLine();
                                    Various.SystemMessage("Login failed");
                                    System.Threading.Thread.Sleep(1500);
                                    break;
                                }
                                else
                                {
                                    Console.Write("   ");
                                    Various.SystemMessageSuccess("OK");
                                }

                                // Password Check
                                tries = 0;
                                do
                                {
                                    loginPassword = Menu.PasswordMenu();
                                    usernameMatchesPassword = UserRepository.UsernameAndPasswordMatch(loginUsername, loginPassword);
                                    correctPasswordLength = UserRepository.CorrectPasswordLength(loginPassword);
                                    tries++;

                                    if (correctPasswordLength == false && tries < 3)
                                    {
                                        Console.WriteLine();
                                        Various.SystemMessage("Password must be between 5 and 12 characters");
                                    }

                                    if (usernameMatchesPassword == false && correctPasswordLength == true && tries < 3)
                                    {
                                        Console.WriteLine();
                                        Various.SystemMessage("Wrong password");

                                    }
                                } while (correctPasswordLength == false && usernameMatchesPassword == false && tries < 3 || correctPasswordLength == true && usernameMatchesPassword == false && tries < 3);

                                if (tries >= 3 && correctPasswordLength == false || tries >= 3 && usernameMatchesPassword == false)
                                {
                                    Console.WriteLine();
                                    Various.SystemMessage("Login failed");
                                    System.Threading.Thread.Sleep(1500);
                                    break;
                                }
                                else
                                {
                                    Console.Write("   ");
                                    Various.SystemMessageSuccess("OK");
                                    UserRepository.AssignCurrentUser(loginUsername, loginPassword);
                                    LogRepository.TypeInLog(UserRepository.currentUser.Username, "logged in");
                                    Various.SystemMessageSuccess("Logged in successfully");
                                    System.Threading.Thread.Sleep(1500);

                                }
                                // Role Choose
                                switch (UserRepository.currentUser.Role)
                                {
                                    case Role.SuperAdmin:
                                        #region SuperAdmin
                                        SuperAdminLoggedIn sa = new SuperAdminLoggedIn();
                                        bool superAdminExit = false;
                                        do
                                        {
                                            SuperAdminOptions superAdminOption = Menu.SuperAdminMenu();
                                            switch (superAdminOption)
                                            {
                                                case SuperAdminOptions.CreateUser:
                                                    sa.AdminCreateUser();
                                                    break;
                                                case SuperAdminOptions.DeleteUser:
                                                    sa.DeleteUser();
                                                    break;
                                                case SuperAdminOptions.UpdateUser:
                                                    sa.UpdateUser();
                                                    break;
                                                case SuperAdminOptions.ViewUsers:
                                                    Console.Clear();
                                                    Various.Header($"{ UserRepository.currentUser.Username} : View Users");
                                                    sa.ViewUserExtended();
                                                    Console.ReadKey();
                                                    break;
                                                case SuperAdminOptions.SendMessage:
                                                    Menu.TypeMessage();
                                                    break;
                                                case SuperAdminOptions.ViewMessages:
                                                    Menu.ViewMessages();
                                                    break;
                                                case SuperAdminOptions.Exit:
                                                    superAdminExit = true;
                                                    break;
                                                default:
                                                    break;
                                            }

                                        } while (!superAdminExit);

                                        #endregion
                                        break;
                                    case Role.Tier3User:
                                        #region Tier3User
                                        Tier123UserLoggedIn Tier3User = new Tier123UserLoggedIn();
                                        bool tier3UserExit = false;
                                        do
                                        {
                                            Tier3UserOptions Tier3option = Menu.Tier3UserMenu();
                                            switch (Tier3option)
                                            {
                                                case Tier3UserOptions.EditMessage:
                                                    Tier3User.EditMessageBetweenTwoUsers("Edit Message Between Two Users");
                                                    break;
                                                case Tier3UserOptions.DeleteMessage:
                                                    Tier3User.DeleteMessageBetweenTwoUsers("Delete Message Between Two Users");
                                                    break;
                                                case Tier3UserOptions.ViewMessage:
                                                    Tier3User.ChooseMessageBetweenTwoUsers("View Message Between Two Users");
                                                    break;
                                                case Tier3UserOptions.SendMessage:
                                                    Menu.TypeMessage();
                                                    break;
                                                case Tier3UserOptions.ViewMessages:
                                                    Menu.ViewMessages();
                                                    break;
                                                case Tier3UserOptions.Exit:
                                                    tier3UserExit = true;
                                                    break;
                                                default:
                                                    break;
                                            }
                                        } while (!tier3UserExit);
                                        #endregion
                                        break;
                                    case Role.Tier2User:
                                        #region Tier2User
                                        Tier123UserLoggedIn Tier2User = new Tier123UserLoggedIn();
                                        bool Tier2UserExit = false;
                                        do
                                        {
                                            Tier2UserOptions Tier2Option = Menu.Tier2UserMenu();
                                            switch (Tier2Option)
                                            {
                                                case Tier2UserOptions.EditMessage:
                                                    Tier2User.EditMessageBetweenTwoUsers("Edit Message Between Two Users");
                                                    break;
                                                case Tier2UserOptions.ViewMessage:
                                                    Tier2User.ChooseMessageBetweenTwoUsers("View Message Between Two Users");
                                                    break;
                                                case Tier2UserOptions.SendeMessage:
                                                    Menu.TypeMessage();
                                                    break;
                                                case Tier2UserOptions.ViewMessages:
                                                    Menu.ViewMessages();
                                                    break;
                                                case Tier2UserOptions.Exit:
                                                    Tier2UserExit = true;
                                                    break;
                                                default:
                                                    break;
                                            }
                                        } while (!Tier2UserExit);
                                        #endregion
                                        break;
                                    case Role.Tier1User:
                                        #region Tier1User
                                        Tier123UserLoggedIn Tier1User = new Tier123UserLoggedIn();
                                        bool Tier1UserExit = false;
                                        do
                                        {
                                            Tier1UserOptions Tier1Option = Menu.Tier1UserMenu();
                                            switch (Tier1Option)
                                            {
                                                case Tier1UserOptions.ViewMessage:
                                                    Tier1User.ChooseMessageBetweenTwoUsers("View Message Between Two Users");
                                                    break;
                                                case Tier1UserOptions.SendMessage:
                                                    Menu.TypeMessage();
                                                    break;
                                                case Tier1UserOptions.ViewMessages:
                                                    Menu.ViewMessages();
                                                    break;
                                                case Tier1UserOptions.Exit:
                                                    Tier1UserExit = true;
                                                    break;
                                                default:
                                                    break;
                                            }
                                        } while (!Tier1UserExit);
                                        #endregion
                                        break;
                                    case Role.Tier0User:
                                        #region Tier0User
                                        Tier123UserLoggedIn Tier0User = new Tier123UserLoggedIn();
                                        bool Tier0UserExit = false;
                                        do
                                        {
                                            Tier0UserOptions Tier0Option = Menu.Tier0UserMenu();
                                            switch (Tier0Option)
                                            {
                                                case Tier0UserOptions.SendMessage:
                                                    Menu.TypeMessage();
                                                    break;
                                                case Tier0UserOptions.ViewMessages:
                                                    Menu.ViewMessages();
                                                    break;
                                                case Tier0UserOptions.Exit:
                                                    Tier0UserExit = true;
                                                    break;
                                                default:
                                                    break;
                                            }
                                        } while (!Tier0UserExit);
                                        #endregion
                                        break;
                                    default:
                                        break;
                                }
                                LogRepository.TypeInLog($"{UserRepository.currentUser.Username}", "logged out");
                                UserRepository.NullCurrentUser();
                                #endregion
                                break;
                            case MainMenuOptions.Register:
                                #region Register
                                Console.Clear();
                                Various.Header("Register");

                                //bool usernameExists;
                                tries = 0;
                                string registerUsername;
                                string registerPassword;


                                // Username Check
                                do
                                {
                                    registerUsername = Menu.UsernameMenu();
                                    usernameExists = UserRepository.UsernameExists(registerUsername);
                                    tries++;

                                    if (usernameExists == true && tries < 3)
                                    {
                                        Console.WriteLine();
                                        Various.SystemMessage("Username already exists");
                                    }
                                } while (usernameExists == true && tries < 3);
                                if (tries >= 3 && usernameExists == true)
                                {
                                    Console.WriteLine();
                                    Various.SystemMessage("Registration failed");
                                    System.Threading.Thread.Sleep(2000);
                                    break;
                                }
                                else
                                {
                                    Console.Write("   ");
                                    Various.SystemMessageSuccess("OK");
                                }

                                // Password Check
                                tries = 0;
                                do
                                {
                                    registerPassword = Menu.PasswordMenu();
                                    correctPasswordLength = UserRepository.CorrectPasswordLength(registerPassword);
                                    tries++;

                                    if (correctPasswordLength == false && tries < 3)
                                    {
                                        Console.WriteLine();
                                        Various.SystemMessage("Password must be between 5 and 12 characters");
                                    }
                                } while (correctPasswordLength == false && tries < 3);
                                if (tries >= 3 && correctPasswordLength == false)
                                {
                                    Console.WriteLine();
                                    Various.SystemMessage("Registration failed");
                                    System.Threading.Thread.Sleep(2000);
                                    break;
                                }
                                else
                                {
                                    Console.Write("   ");
                                    Various.SystemMessageSuccess("OK");
                                    UserRepository.CreateUserSimple(registerUsername, registerPassword);
                                    LogRepository.TypeInLog($"{registerUsername}", "registered");
                                    Various.SystemMessageSuccess("Registration successfull");
                                    System.Threading.Thread.Sleep(2000);
                                }
                                UserRepository.NullCurrentUser();
                                #endregion
                                break;
                            case MainMenuOptions.Exit:
                                #region Exit
                                Console.Clear();
                                Various.SystemMessage("GoodBye");
                                System.Threading.Thread.Sleep(1500);
                                LogRepository.TypeInLog("Application", "exited");
                                Environment.Exit(0);
                                #endregion
                                break;

                            default:
                                break;
                        }
                    } while (true);
                }
                catch (Exception e)
                {
                    UserRepository.NullCurrentUser();
                    LogRepository.TypeInLog($"Error: {e.Message}", "occured");
                    Various.SystemMessageError("An error has occured. We are sorry");
                }
            } while (true);
        }
    }
}
