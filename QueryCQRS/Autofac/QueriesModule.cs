using System.Net.NetworkInformation;
using System.Reflection;
using Autofac;
using MediatR;
using MediatR.Pipeline;
using QueryCQRS.Handlers;
using QueryCQRS.Queries;
using Services.Autofac;
using Services.Interfaces;
using Module = Autofac.Module;

namespace QueryCQRS.Autofac
{
    public class QueriesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<ServicesModule>();
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();
            var mediatrOpenTypes = new[]
            {
                typeof(IRequestHandler<,>),             // обработчик запросов
                typeof(IRequestExceptionHandler<,,>),   // обработчик исключений
                typeof(IRequestExceptionAction<,>),     // действие исключения для запроса
                typeof(INotificationHandler<>)          // обработчик для уведомления
            };

            foreach (var mediatrOpenType in mediatrOpenTypes)
            {
                builder
                    .RegisterAssemblyTypes(typeof(Ping).GetTypeInfo().Assembly)
                    .AsClosedTypesOf(mediatrOpenType)
                    // when having a single class implementing several handler types
                    // this call will cause a handler to be called twice
                    // in general you should try to avoid having a class implementing for instance `IRequestHandler<,>` and `INotificationHandler<>`
                    // the other option would be to remove this call
                    // see also https://github.com/jbogard/MediatR/issues/462
                    .AsImplementedInterfaces();
            }

            builder.Register(c => new GetInputDataHANDLER(true, c.Resolve<IInputData>()))
                .As<IRequestHandler<GetInputDataREQUEST, GetInputDataRESPONSE>>();

            builder.RegisterType<GetResultStatisticalAnalysisHANDLER>()
                .As<IRequestHandler<GetResultStatisticalAnalysisREQUEST, GetResultStatisticalAnalysisRESPONSE>>();

            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });
        }
    }
}
