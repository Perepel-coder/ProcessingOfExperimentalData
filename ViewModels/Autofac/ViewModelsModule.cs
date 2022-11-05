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
            builder.Register(c => new MainViewModel(c.Resolve<IMediator>())).AsSelf();
            //builder.Register(c => new MainViewModel(c.Resolve<IMapper>(), c.Resolve<IMediator>())).AsSelf();
        }
    }
}
