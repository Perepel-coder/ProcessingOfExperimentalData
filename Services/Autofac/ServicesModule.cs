using Autofac;
using Services.Interfaces;
using System.Collections.Generic;

namespace Services.Autofac
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<InputData>().AsSelf().As<IInputData>(); ;
            builder.Register((c, p) => new StatisticalAnalysis(
                p.Named<IEnumerable<double>>("arr"), 
                p.Named<double>("step"), 
                p.Named<int>("num"))).AsSelf().As<IStatisticalAnalysis>();
        }
    }
}
