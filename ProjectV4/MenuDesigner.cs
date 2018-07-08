using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectV4
{
    public class MenuDesigner
    {
        #region Constructors
        public MenuDesigner(List<string> menuItems, string currentLogo)
        {
            CurrentItem = 0;
            this.MenuItems = menuItems;
            this.CurrentLogo = currentLogo;
        }

        // Constructor for Message Viewing
        public MenuDesigner(List<Message> messages, string currentLogo)
        {
            CurrentItem = 0;
            this.Messages = messages;
            this.CurrentLogo = currentLogo;
        }
    
        #endregion

        #region Properties   
        public int CurrentItem { get; set; }
        public List<string> MenuItems { get; set; }
        public List<Message> Messages { get; set; }
        public string CurrentLogo { get; set; }
        private int i = 0;
        private ConsoleKeyInfo pressedKey;
        #endregion

        #region Methods
        // Menu Design
        public int MenuDesign()
        {
            do
            {
                Console.Clear();
                Various.Header(CurrentLogo);

                for (i = 0; i < MenuItems.Count; i++)
                {
                    if (CurrentItem == i)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine(MenuItems[i]);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine(MenuItems[i]);
                    }
                }

                pressedKey = Console.ReadKey(true);

                if (pressedKey.Key.ToString() == "DownArrow")
                {
                    CurrentItem++;
                    if (CurrentItem > MenuItems.Count - 1)
                    {
                        CurrentItem = 0;
                    }
                }
                else if (pressedKey.Key.ToString() == "UpArrow")
                {
                    CurrentItem--;
                    if (CurrentItem < 0)
                    {
                        CurrentItem = MenuItems.Count - 1;
                    }
                }
                else if (pressedKey.Key == ConsoleKey.Escape)
                {
                    return -1;
                }
                
            } while (pressedKey.Key != ConsoleKey.Enter);

            return CurrentItem;
        }

        // Menu Design For Messages
        public int MenuDesignMessage(out int correctMessageId)
        {
            do
            {
                Console.Clear();
                Console.CursorVisible = false;
                Various.Header(CurrentLogo);
                Console.WriteLine();
                Various.SystemMessageDirections("Press [1] to sort by Date descending, [2] to sort by Sender name, [3] to sort by Title or [4] to sort by Unread/Read");
                Console.WriteLine();

                for (i = 0; i < Messages.Count; i++)
                {
                    // Length Fix
                    int length = 6;
                    if (Messages[i].Body.Length < 6)
                    {
                        length = Messages[i].Body.Length;
                    }

                    if (CurrentItem == i)
                    {
                        if (Messages[i].Read == false)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkMagenta;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine($"{Messages[i].Date} - From: {Messages[i].Sender.Username} To: {Messages[i].Receiver.Username} - Title: {Messages[i].Title} - Message: {Messages[i].Body.Substring(0,length) + "..."}");
                            Console.ResetColor();
                        }
                        else if (Messages[i].Read == true)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkMagenta;
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.WriteLine($"{Messages[i].Date} - From: {Messages[i].Sender.Username} To: {Messages[i].Receiver.Username} - Title: {Messages[i].Title} - Message: {Messages[i].Body.Substring(0,length) + "..."}");
                            Console.ResetColor();
                        }
                        
                    }
                    else
                    {
                        if (Messages[i].Read == false)
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine($"{Messages[i].Date} - From: {Messages[i].Sender.Username} To: {Messages[i].Receiver.Username} - Title: {Messages[i].Title} - Message: {Messages[i].Body.Substring(0,length) + "..."}");
                            Console.ResetColor();
                        }
                        else if (Messages[i].Read == true)
                        {
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.WriteLine($"{Messages[i].Date} - From: {Messages[i].Sender.Username} To: {Messages[i].Receiver.Username} - Title: {Messages[i].Title} - Message: {Messages[i].Body.Substring(0,length) + "..."}");
                            Console.ResetColor();
                        }
                    }
                }

                pressedKey = Console.ReadKey(true);

                if (pressedKey.Key.ToString() == "DownArrow")
                {
                    CurrentItem++;
                    if (CurrentItem > Messages.Count - 1)
                    {
                        CurrentItem = 0;
                    }
                }
                else if (pressedKey.Key.ToString() == "UpArrow")
                {
                    CurrentItem--;
                    if (CurrentItem < 0)
                    {
                        CurrentItem = Messages.Count - 1;
                    }
                }
                else if (pressedKey.Key == ConsoleKey.Escape)
                {
                    correctMessageId = -1;
                    return -1;
                }
                else if (pressedKey.KeyChar.ToString() == "1")
                {
                    Messages = Messages.OrderByDescending(x => x.Date).ToList();
                }
                else if (pressedKey.KeyChar.ToString() == "2")
                {
                    Messages = Messages.OrderBy(x => x.Sender.Username).ToList();
                }
                else if (pressedKey.KeyChar.ToString() == "3")
                {
                    Messages = Messages.OrderBy(x => x.Title).ToList();
                }
                else if (pressedKey.KeyChar.ToString() == "4")
                {
                    Messages = Messages.OrderBy(x => x.Read).ToList();
                }
            } while (pressedKey.Key != ConsoleKey.Enter);

            correctMessageId = Messages[CurrentItem].Id;
            return CurrentItem;
        }
        #endregion
    }
}
