using Autofac;
using Services.Interfaces;
using System.Collections.Generic;

namespace Services.Autofac
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<InputData>().AsSelf().As<IInputData>();
            builder.RegisterType<StatisticalAnalysis>().As<IStatisticalAnalysis>();
        }
    }
}
