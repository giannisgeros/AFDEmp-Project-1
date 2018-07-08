using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectV4
{
    public enum Role
    {
        SuperAdmin,
        Tier3User,
        Tier2User,
        Tier1User,
        Tier0User
    }

    public enum MainMenuOptions
    {
        Login,
        Register,
        Exit
    }

    public enum SuperAdminOptions
    {
        CreateUser,
        DeleteUser,
        UpdateUser,
        ViewUsers,
        SendMessage,
        ViewMessages,
        Exit
    }

    public enum RoleOptions
    {
        Tier3User,
        Tier2User,
        Tier1User,
        Tier0User,
        Exit
    }

    public enum UpdateOptions
    {
        Username,
        Password,
        Role,
        Exit
    }

    public enum Tier3UserOptions
    {
        EditMessage,
        DeleteMessage,
        ViewMessage,
        SendMessage,
        ViewMessages,
        Exit
    }

    public enum Tier2UserOptions
    {
        EditMessage,
        ViewMessage,
        SendeMessage,
        ViewMessages,
        Exit
    }

    public enum Tier1UserOptions
    {
        ViewMessage,
        SendMessage,
        ViewMessages,
        Exit
    }

    public enum Tier0UserOptions
    {
        SendMessage,
        ViewMessages,
        Exit
    }
}
