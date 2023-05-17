
using System.Reflection;
using Autofac;
using Nlayer.Core.NewFolder;
using Nlayer.Core.Repositories;
using Nlayer.Core.Service;
using Nlayer.Repository;
using Nlayer.Repository.Repositories;
using Nlayer.Repository.UnitOfWork;
using Nlayer.Service.Mapping;
using Nlayer.Service.Service;
using Module = Autofac.Module;
namespace Nlayer.Apı.Modules
{
    public class RepoServiceModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>)).InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitofWork>();



            var ApiAssembly = Assembly.GetExecutingAssembly();
            var respoAssembly = Assembly.GetAssembly(typeof(AppDbContext));
            var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile));


            builder.RegisterAssemblyTypes(ApiAssembly, respoAssembly, serviceAssembly).
                Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(ApiAssembly, respoAssembly, serviceAssembly).
                Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerLifetimeScope();

        }
    }
}
