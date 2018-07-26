using Contacts.Management.Api.DataAccess.Interfaces;
using Contacts.Management.Api.DataAccess.Repositories;
using System.Web.Http;
using Unity;

namespace Contacts.Management.Api.Unity
{
    public class UnityConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();
            container.RegisterType<IContactsRepository, ContactsRepository>();
            config.DependencyResolver = new UnityResolver(container);
        }
    }
}