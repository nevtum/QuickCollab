using Ninject;
using QuickCollab.Accounts;
using QuickCollab.Security;
using QuickCollab.Services;
using QuickCollab.Session;
using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;

namespace QuickCollab
{
    public class NinjectDependencyResolver : IDependencyResolver, System.Web.Mvc.IDependencyResolver
    {
        private IKernel _container;

        public NinjectDependencyResolver()
        {
            _container = new StandardKernel();
            RegisterServices();
        }

        public System.Web.Http.Dependencies.IDependencyScope BeginScope()
        {
            return this;
        }

        // Needs fixing here. Goes out of scope
        // such that there are problems in
        // next page reload
        public void Dispose()
        {
            //var disposable = _container as IDisposable;
            //if (disposable != null)
            //{
            //    disposable.Dispose();
            //}

            //_container = null;
        }

        public object GetService(Type serviceType)
        {
            return _container.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.GetAll(serviceType);
        }

        /// <summary>
        /// Use this method to do register all
        /// services to interfaces
        /// </summary>
        private void RegisterServices()
        {
            _container.Bind<IAccountsRepository>().To<AccountsRepository>();
            _container.Bind<ISessionInstanceRepository>().To<SessionInstanceRepository>();
            _container.Bind<IManageAccounts>().To<AccountsApplicationService>();
        }
    }
}