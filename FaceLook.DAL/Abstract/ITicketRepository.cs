#region

using System;

#endregion

namespace FaceLook.DAL.Abstract
{
    public interface ITicketRepository
    {
        bool Authenticate(string nickname, string hash);
        Guid CreateTicket(string nickname, string hash);
        void RemoveTicket(string nickname, string hash);
        bool ConfirmTicket(string nickname, Guid key);
        bool IsNicknameUsed(string nickname);
    }
}