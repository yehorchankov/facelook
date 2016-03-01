#region

using System;
using FaceLook.UI.Models;

#endregion

namespace FaceLook.UI.Infrastructure.Abstract
{
    public interface IAuthProvider
    {
        bool Authenticate(string nickname, string password);
        void Registrate(RegistartionViewModel registrationModel, Uri url);
        bool IsLoginUsed(string login);
        bool ConfirmAccount(string login, Guid key);
    }
}