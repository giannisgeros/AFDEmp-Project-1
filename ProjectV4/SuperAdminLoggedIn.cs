using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectV4
{
    public class SuperAdminLoggedIn
    {
        Menus SuperAdminMenu = new Menus();
        UserRepository UserRepository = UserRepository.Instance;
        MsgRepository MessageRepository = new MsgRepository();
        LogRepository LogRepository = new LogRepository();


        #region Create User
        public void AdminCreateUser()
        {
            Console.Clear();
           
            Various.Header($"{UserRepository.currentUser.Username} : Create User");
            RoleOptions roleOption;
            Role role = Role.Tier0User;

            // Username
            string username = Various.UpdateUsername();
            if (username == "")
            {
                Console.WriteLine();
                Various.SystemMessage("Create User failed");
                System.Threading.Thread.Sleep(1500);
                return;
            }

            // Password
            string password = Various.UpdatePassword();
            if (password == "")
            {
                Console.WriteLine();
                Various.SystemMessage("Create User failed");
                System.Threading.Thread.Sleep(1500);
                return;
            }

            // Role Assignment
            roleOption = SuperAdminMenu.RoleMenu("Create User");
            switch (roleOption)
            {
                case RoleOptions.Tier3User:
                    role = Role.Tier3User;
                    break;
                case RoleOptions.Tier2User:
                    role = Role.Tier2User;
                    break;
                case RoleOptions.Tier1User:
                    role = Role.Tier1User;
                    break;
                case RoleOptions.Tier0User:
                    role = Role.Tier0User;
                    break;
                case RoleOptions.Exit:
                    Console.WriteLine();
                    Various.SystemMessage("Create User failed");
                    System.Threading.Thread.Sleep(1500);
                    return;
                default:
                    return;
            }
            UserRepository.CreateUserExtended(username, password, role);
            LogRepository.TypeInLog($"{UserRepository.currentUser.Username}", "created user");
            Various.SystemMessageSuccess("Create User successfull");
            System.Threading.Thread.Sleep(1500);
        }
        #endregion

        #region View Users
        public void ViewUserExtended()
        {
            SuperAdminMenu.ViewUsersExtended();
            Console.WriteLine();
            Various.SystemMessage("Press Any Key To Exit");
        }
        #endregion

        #region Delete User
        public void DeleteUser()
        {
            int userId = SuperAdminMenu.DeleteUserPick();

            if (userId == -1)
            {
                Console.WriteLine();
                Various.SystemMessage("Delete User failed");
                System.Threading.Thread.Sleep(1500);
                return;
            }

            MessageRepository.ChangeIdOfUserToDelete(userId);
            UserRepository.DeleteUser(userId);
            LogRepository.TypeInLog($"{UserRepository.currentUser.Username}", $"deleted user");
            Console.WriteLine();
            Various.SystemMessageSuccess($"User[{userId}] deleted.");
            System.Threading.Thread.Sleep(1500);
        }
        #endregion

        #region Update User
        public void UpdateUser()
        {
            int userId = SuperAdminMenu.UpdateUserPick();

            if (userId == -1)
            {
                Console.WriteLine();
                Various.SystemMessage("Update User failed");
                System.Threading.Thread.Sleep(1500);
                return;
            }

            UpdateOptions updateChoice = SuperAdminMenu.UpdateMenu();
            switch (updateChoice)
            {
                case UpdateOptions.Username:
                    string username = Various.UpdateUsername();
                    if (username == "")
                    {
                        Console.WriteLine();
                        Various.SystemMessage("Update Username failed");
                        System.Threading.Thread.Sleep(1500);
                        return;
                    }

                    UserRepository.UpdateUserUsername(userId, username);
                    LogRepository.TypeInLog($"{UserRepository.currentUser.Username}", "updated username of user");
                    Various.SystemMessageSuccess("Username updated successfully");
                    System.Threading.Thread.Sleep(1500);
                    break;
                case UpdateOptions.Password:
                    string password = Various.UpdatePassword();
                    if (password == "")
                    {
                        Console.WriteLine();
                        Various.SystemMessage("Update Password failed");
                        System.Threading.Thread.Sleep(1500);
                        return;
                    }

                    UserRepository.UpdateUserPassword(userId, password);
                    LogRepository.TypeInLog($"{UserRepository.currentUser.Username}", "updated password of user");
                    Various.SystemMessageSuccess("Password updated successfully");
                    System.Threading.Thread.Sleep(1500);
                    break;
                case UpdateOptions.Role:
                    RoleOptions updatedRoleChoice = SuperAdminMenu.RoleMenu("Update User");
                    Role role = Role.Tier0User;
                    switch (updatedRoleChoice)
                    {
                        case RoleOptions.Tier3User:
                            role = Role.Tier3User;
                            break;
                        case RoleOptions.Tier2User:
                            role = Role.Tier2User;
                            break;
                        case RoleOptions.Tier1User:
                            role = Role.Tier1User;
                            break;
                        case RoleOptions.Tier0User:
                            role = Role.Tier0User;
                            break;
                        case RoleOptions.Exit:
                            Various.SystemMessage("Update User failed");
                            System.Threading.Thread.Sleep(1500);
                            return;
                        default:
                            Various.SystemMessage("Update User failed");
                            System.Threading.Thread.Sleep(1500);
                            return;
                    }
                    UserRepository.UpdateUserRole(userId, role);
                    LogRepository.TypeInLog($"{UserRepository.currentUser.Username}", "updated role of user");
                    Various.SystemMessageSuccess("Role updated successfully");
                    System.Threading.Thread.Sleep(1500);
                    break;
                case UpdateOptions.Exit:
                    return;
                default:
                    return;
            }
        }
        #endregion

    }
}
