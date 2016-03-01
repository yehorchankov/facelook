#region

using System;
using FaceLook.DAL.Abstract;
using FaceLook.DAL.Concrete;
using FaceLook.Security.Abstract;

#endregion

namespace FaceLook.Security.Concrete
{
    public class FormAuthProvider : IAuthProvider
    {
        private readonly Md5CryptoProvider _cryptoProvider = new Md5CryptoProvider();
        private readonly ITicketRepository _repository = new TicketRepository();

        public bool Authenticate(string nickname, string password)
        {
            string hash = _cryptoProvider.GetMd5Hash(password);
            return _repository.Authenticate(nickname, hash);
        }

        public Guid RegistrateUser(string nickname, string password)
        {
            string hash = _cryptoProvider.GetMd5Hash(password);
            return _repository.CreateTicket(nickname, hash);
        }

        public void RemoveUser(string nickname, string password)
        {
            string hash = _cryptoProvider.GetMd5Hash(password);
            _repository.RemoveTicket(nickname, hash);
        }

        public bool ConfirmAccount(string nickname, Guid key)
        {
            return _repository.ConfirmTicket(nickname, key);
        }

        public bool IsNicknameUsed(string nickname)
        {
            return _repository.IsNicknameUsed(nickname);
        }
    }
}