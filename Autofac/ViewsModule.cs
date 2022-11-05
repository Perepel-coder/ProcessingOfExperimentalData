using Autofac;
using ViewModels.ViewModels;

namespace ProcessingOfExperimentalData.Autofac
{
    internal class ViewsModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.Register(c => new View.MainPage() { DataContext = c.Resolve<MainViewModel>() }).AsSelf();
        }
    }
}
