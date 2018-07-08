using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectV4
{
    public static class Various
    {

        #region Type Letter By Letter
        // Type Letter By Letter With Masking
        public static string TypeLetterByLetterWithMasking()
        {
            string input = "";
            ConsoleKeyInfo pressedKey;
            do
            {
                pressedKey = Console.ReadKey(true);
                if (pressedKey.Key != ConsoleKey.Backspace && pressedKey.Key != ConsoleKey.Enter && pressedKey.Key != ConsoleKey.Spacebar && pressedKey.Key != ConsoleKey.Escape)
                {
                    input += pressedKey.KeyChar;
                    Console.Write("*");
                }
                else if (pressedKey.Key == ConsoleKey.Spacebar || pressedKey.Key == ConsoleKey.Escape)
                {
                    Console.Beep(200, 300);
                }
                else
                {
                    if (pressedKey.Key == ConsoleKey.Backspace && input.Length > 0)
                    {
                        input = input.Substring(0, (input.Length - 1));
                        Console.Write("\b \b");
                    }
                    else if (pressedKey.Key == ConsoleKey.Backspace && input.Length == 0)
                    {
                        Console.Beep(200, 300);
                    }
                }
            } while (pressedKey.Key != ConsoleKey.Enter);

            return input;
        }

        // Type Letter By Letter Without Masking
        public static string TypeLetterByLetterWithoutMasking()
        {
            string input = "";
            ConsoleKeyInfo pressedKey;

            do
            {
                pressedKey = Console.ReadKey(true);
                if (pressedKey.Key != ConsoleKey.Backspace && pressedKey.Key != ConsoleKey.Enter && pressedKey.Key != ConsoleKey.Spacebar && pressedKey.Key != ConsoleKey.Escape)
                {
                    input += pressedKey.KeyChar;
                    Console.Write(pressedKey.KeyChar.ToString());
                }
                else if (pressedKey.Key == ConsoleKey.Escape)
                {
                    Console.Beep(200, 300);
                }
                else
                {
                    if (pressedKey.Key == ConsoleKey.Backspace && input.Length > 0)
                    {
                        input = input.Substring(0, (input.Length - 1));
                        Console.Write("\b \b");
                    }
                    else if (pressedKey.Key == ConsoleKey.Backspace && input.Length == 0)
                    {
                        Console.Beep(200, 300);
                    }
                }
            } while (pressedKey.Key != ConsoleKey.Enter);

            return input;
        }

        public static int TypeLetterByLetterWithoutMaskingiNT()
        {
            string input = "";
            ConsoleKeyInfo pressedKey;
            char[] availableCharacters = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            do
            {
                pressedKey = Console.ReadKey(true);
                if (availableCharacters.Contains(pressedKey.KeyChar))
                {
                    input += pressedKey.KeyChar;
                    Console.Write(pressedKey.KeyChar.ToString());
                }
                else if (pressedKey.Key == ConsoleKey.Escape)
                {
                    return -1;
                }
                else
                {
                    if (pressedKey.Key == ConsoleKey.Backspace && input.Length > 0)
                    {
                        input = input.Substring(0, (input.Length - 1));
                        Console.Write("\b \b");
                    }
                    else if (pressedKey.Key == ConsoleKey.Backspace && input.Length == 0)
                    {
                        Console.Beep(200, 300);
                    }
                }
            } while (pressedKey.Key != ConsoleKey.Enter);

            if (input == "")
            {
                return -2;
            }
            return int.Parse(input);
        }

        public static string TypeLetterByLetterWithoutMaskingSTR(int maxLength)
        {
            string input = "";
            ConsoleKeyInfo pressedKey;
            do
            {
                pressedKey = Console.ReadKey(true);
                if (pressedKey.Key != ConsoleKey.Backspace && pressedKey.Key != ConsoleKey.Enter && pressedKey.Key != ConsoleKey.Escape && input.Length <= maxLength)
                {
                    input += pressedKey.KeyChar;
                    Console.Write(pressedKey.KeyChar.ToString());
                }
                else if (pressedKey.Key == ConsoleKey.Escape)
                {
                    return "";
                }
                else
                {
                    if (pressedKey.Key == ConsoleKey.Backspace && input.Length > 0)
                    {
                        input = input.Substring(0, (input.Length - 1));
                        Console.Write("\b \b");
                    }
                    else if ((pressedKey.Key == ConsoleKey.Backspace && input.Length == 0) || input.Length > maxLength)
                    {
                        Console.Beep(200, 300);
                    }
                }
            } while (pressedKey.Key != ConsoleKey.Enter);
            return input;
        }

        public static string TypeLetterByLetterWithoutMaskingYesOrNoSTR()
        {
            string input = "";
            char[] availableCharacters = { 'Y', 'y', 'N', 'n' };
            ConsoleKeyInfo pressedKey;
            do
            {
                pressedKey = Console.ReadKey(true);
                if (availableCharacters.Contains(pressedKey.KeyChar) && input.Length < 1)
                {
                    input += pressedKey.KeyChar;
                    Console.Write(pressedKey.KeyChar.ToString());
                }
                else if (pressedKey.Key == ConsoleKey.Escape)
                {
                    return "";
                }
                else
                {
                    if (pressedKey.Key == ConsoleKey.Backspace && input.Length > 0)
                    {
                        input = input.Substring(0, (input.Length - 1));
                        Console.Write("\b \b");
                    }
                    else if ((pressedKey.Key == ConsoleKey.Backspace && input.Length == 0) || input.Length > 1)
                    {
                        Console.Beep(200, 300);
                    }
                }
            } while (pressedKey.Key != ConsoleKey.Enter);
            return input;
        }
        #endregion

        #region Colors
        // System Message
        public static void SystemMessage(string systemMessage)
        {
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(systemMessage);
            Console.ResetColor();
        }

        public static void SystemMessageError(string systemMessage)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(systemMessage);
            Console.ResetColor();
        }

        // System Message Success
        public static void SystemMessageSuccess(string successMessage)
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(successMessage);
            Console.ResetColor();
            Console.WriteLine();
        }

        // System Message Directions
        public static void SystemMessageDirections(string directionsMessage)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(directionsMessage);
            Console.ResetColor();
            Console.WriteLine();
        }
        // Header
        public static void Header(string header)
        {
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"-------------- {header} --------------");
            Console.ResetColor();
            Console.WriteLine();

        }
        #endregion

        #region Username and Password
        public static string UpdateUsername()
        {
            Menus Menu = new Menus();
            UserRepository UserRepository = UserRepository.Instance;
            bool usernameExists;
            int tries = 0;
            string username;

            do
            {
                username = Menu.UsernameMenu();
                usernameExists = UserRepository.UsernameExists(username);
                tries++;

                if (usernameExists == true && tries < 3)
                {
                    Console.WriteLine();
                    SystemMessage("Username already exists");
                }
            } while (usernameExists == true && tries < 3);
            if (tries >= 3 && usernameExists == true)
            {
                return "";
            }
            else
            {
                Console.Write("   ");
                SystemMessageSuccess("OK");
                return username;
            }
        }

        public static string UpdatePassword()
        {
            Menus Menu = new Menus();
            UserRepository UserRepository = UserRepository.Instance;

            bool correctPasswordLength;
            int tries = 0;
            string password;
            do
            {
                password = Menu.PasswordMenu();
                correctPasswordLength = UserRepository.CorrectPasswordLength(password);
                tries++;

                if (correctPasswordLength == false && tries < 3)
                {
                    Console.WriteLine();
                    SystemMessage("Password must be between 5 and 12 characters");
                }
            } while (correctPasswordLength == false && tries < 3);
            if (tries >= 3 && correctPasswordLength == false)
            {
                return "";
            }
            else
            {
                Console.Write("   ");
                SystemMessageSuccess("OK");
                return password;
            }
        }
        #endregion

    }
}
