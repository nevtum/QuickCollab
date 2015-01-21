using Ninject;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace QuickCollab
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel _container;

        public NinjectDependencyResolver()
        {
            _container = new StandardKernel();
            RegisterServices();
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
        }
    }
}