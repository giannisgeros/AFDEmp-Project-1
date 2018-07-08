using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectV4
{
    public class Tier123UserLoggedIn
    {
        UserRepository UserRepository = UserRepository.Instance;
        MsgRepository MessageRepository = new MsgRepository();
        Menus TierMenu = new Menus();
        LogRepository LogRepository = new LogRepository();

        #region View Message Between Two Users
        public void ChooseMessageBetweenTwoUsers(string operation)
        {
            TierMenu.PickTwoUsers(operation, out int senderId, out int receiverId);
            List<Message> messages = MessageRepository.GetMessageBetweenTwoUsers(senderId, receiverId);

            if (senderId < 0 || receiverId < 0)
            {
                Console.WriteLine();
                Various.SystemMessage("View Message Between Two Users Failed");
                System.Threading.Thread.Sleep(1500);
                return;
            }

            TierMenu.ViewAMessageBetweenTwoUsers(messages);
            LogRepository.TypeInLog($"{UserRepository.currentUser.Username}", "viewed message between two users");
        }
        #endregion

        #region Delete Message Between Two Users
        public void DeleteMessageBetweenTwoUsers(string operation)
        {
            TierMenu.PickTwoUsers(operation, out int senderId, out int receiverId);
            List<Message> messages = MessageRepository.GetMessageBetweenTwoUsers(senderId, receiverId);
            
            if (senderId < 0 || receiverId < 0)
            {
                Console.WriteLine();
                Various.SystemMessage("Delete Message Between Two Users Failed");
                System.Threading.Thread.Sleep(1500);
                return;
            }

            TierMenu.DeleteAMessageBetweenTwoUsers(messages);
            LogRepository.TypeInLog($"{UserRepository.currentUser.Username}", "deleted message between two users");
        }
        #endregion

        #region Edit Message Between Two Users
        public void EditMessageBetweenTwoUsers(string operation)
        {
            TierMenu.PickTwoUsers(operation, out int senderId, out int receiverId);
            List<Message> messages = MessageRepository.GetMessageBetweenTwoUsers(senderId, receiverId);

            if (senderId < 0 || receiverId < 0)
            {
                Console.WriteLine();
                Various.SystemMessage("Edit Message Between Two Users Failed");
                System.Threading.Thread.Sleep(1500);
                return;
            }

            int choice = TierMenu.PickAMessageBetweenTwoUsers(messages);
            if (choice < 0)
            {
                Console.WriteLine();
                Various.SystemMessage("Edit Message Failed");
                System.Threading.Thread.Sleep(1500);
                return;
            }
            string edit = TierMenu.TitleOrBodyEdit(choice, out int choiceToEdit);
            if (choiceToEdit < 0)
            {
                Console.WriteLine();
                Various.SystemMessage("Edit Message Failed");
                System.Threading.Thread.Sleep(1500);
                return;
            }
            else if (choiceToEdit == 0)
            {
                MessageRepository.EditMessageTitle(choice, edit);
            }
            else if (choiceToEdit == 1)
            {
                MessageRepository.EditMessageBody(choice, edit);
            }
            LogRepository.TypeInLog($"{UserRepository.currentUser.Username}", "edited message between two users");
            Console.WriteLine();
            Various.SystemMessageSuccess("Edit Message successfull");
            System.Threading.Thread.Sleep(1500);
            return;
        }
        #endregion
    }
}
