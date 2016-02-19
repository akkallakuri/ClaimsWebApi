using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;
using ClaimsWebApi.Services.Contracts;
using ClaimsWebApi.Services;
using ClaimsWebApi.Data;
using ClaimsWebApi.Data.Contracts;

namespace ClaimsWebApi
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            container.RegisterType<IClaimsRepository, ClaimsRepository>();
            container.RegisterType<IClaimsService, ClaimsService>();
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}