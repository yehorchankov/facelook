#region

using System;
using System.Text;
using System.Web.Security;
using FaceLook.Security.Abstract;
using FaceLook.UI.Models;
using IAuthProvider = FaceLook.UI.Infrastructure.Abstract.IAuthProvider;

#endregion

namespace FaceLook.UI.Infrastructure.Concrete
{
    public class AuthenticationProvider : IAuthProvider
    {
        private readonly Security.Abstract.IAuthProvider _authProvider;
        private readonly IMailSender _mailSender;

        public AuthenticationProvider(IMailSender mailSender, Security.Abstract.IAuthProvider authProvider)
        {
            _mailSender = mailSender;
            _authProvider = authProvider;
        }

        /// <summary>
        ///     Provides users' authentication mechanism
        /// </summary>
        /// <param name="userNickname">User's login</param>
        /// <param name="password">User's password</param>
        /// <returns>The result of authentication</returns>
        public bool Authenticate(string userNickname, string password)
        {
            bool result = _authProvider.Authenticate(userNickname, password);
            if (result)
            {
                FormsAuthentication.SetAuthCookie(userNickname, false);
            }

            return result;
        }

        /// <summary>
        ///     Provides logic to registrate new user, sends confirmation mail if everything's done ok
        /// </summary>
        /// <param name="registrationModel"></param>
        /// <param name="uri"></param>
        public void Registrate(RegistartionViewModel registrationModel, Uri uri)
        {
            Guid key = _authProvider.RegistrateUser(registrationModel.User.NickName,
                registrationModel.Password);

            StringBuilder mailBody = new StringBuilder();
            mailBody.Append("<html><head></head><body><div>Click link to confirm your account <a href=\"http://");
            mailBody.Append(uri.Authority);
            mailBody.Append("/Confirm/");
            mailBody.Append(registrationModel.User.NickName);
            mailBody.Append("/");
            mailBody.Append(key);
            mailBody.Append("\">here</a></body></html>");

            _mailSender.SendMail("Your new account", mailBody.ToString(), registrationModel.User.Email);
        }

        /// <summary>
        ///     Checks wether the login i salready in use
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public bool IsLoginUsed(string login)
        {
            return _authProvider.IsNicknameUsed(login);
        }

        /// <summary>
        ///     Checks wether account was successfully confirmed
        /// </summary>
        /// <param name="login"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ConfirmAccount(string login, Guid key)
        {
            return _authProvider.ConfirmAccount(login, key);
        }
    }
}