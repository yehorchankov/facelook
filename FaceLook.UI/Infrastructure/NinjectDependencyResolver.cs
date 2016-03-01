#region

using System;
using System.Collections.Generic;
using System.Web.Mvc;
using FaceLook.BL.Abstract;
using FaceLook.BL.Concrete;
using FaceLook.DAL.Abstract;
using FaceLook.DAL.Concrete;
using FaceLook.Security.Abstract;
using FaceLook.Security.Concrete;
using FaceLook.UI.Infrastructure.Concrete;
using Ninject;

#endregion

namespace FaceLook.UI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            _kernel.Bind<IUserRepository>().To<UserRepository>();
            _kernel.Bind<IConversationManager>().To<ConversationManager>();
            _kernel.Bind<IMailSender>().To<EmailSender>();
            _kernel.Bind<IAuthProvider>().To<FormAuthProvider>();
            _kernel.Bind<Abstract.IAuthProvider>().To<AuthenticationProvider>();
            _kernel.Bind<IPostManager>().To<PostManager>();
        }
    }
}