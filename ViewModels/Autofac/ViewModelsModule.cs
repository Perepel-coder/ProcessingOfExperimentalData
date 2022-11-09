using Autofac;
using MediatR;
using QueryCQRS.Autofac;
using ViewModels.ViewModels;

namespace ViewModels.Autofac
{
    public class ViewModelsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new QueriesModule());
            builder.Register((c, p) => new MainViewModel(c.Resolve<IMediator>(), p.Named<IContainer>("container"))).AsSelf();
        }
    }
}
