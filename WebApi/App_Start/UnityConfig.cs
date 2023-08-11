using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using Employee_Services.EmployeeManipulate;
using Unity.Lifetime;

namespace WebApi
{
    public static class UnityConfig
    {
        public static IUnityContainer RegisterComponents()
        {
			var container = new UnityContainer();
            container.RegisterType<IEmployeeService, EmployeeService>(new HierarchicalLifetimeManager());

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            System.Web.Mvc.DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            return container;
        }
    }
}