#region

using System;

#endregion

namespace FaceLook.Security.Abstract
{
    public interface IAuthProvider
    {
        bool Authenticate(string nickname, string password);
        Guid RegistrateUser(string nickname, string password);
        void RemoveUser(string nickname, string password);
        bool IsNicknameUsed(string nickname);
        bool ConfirmAccount(string nickname, Guid key);
    }
}