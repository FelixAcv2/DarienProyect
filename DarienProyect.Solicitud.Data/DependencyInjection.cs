

using Acv2.SharedKernel.Crosscutting.Adapter;
using Acv2.SharedKernel.Crosscutting.NetFramerwork.Adapter;
using Acv2.SharedKernel.Crosscutting.NetFramerwork.Validator;
using Acv2.SharedKernel.Crosscutting.ServicesApi;
using Acv2.SharedKernel.Crosscutting.Validator;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Unity;
using Unity.Lifetime;

namespace DarienProyect.Solicitud.Data
{
   public static class DependencyInjection
    {

        public static IUnityContainer AddContainerDataSolicitud(this IUnityContainer container)
        {


          
            container.RegisterType(typeof(DbContext), typeof(SolicitudDbContext), new TransientLifetimeManager());


            container.RegisterType(typeof(ITypeAdapterFactory));

            container.RegisterType<IServicebApi, ServicebApi>();

            ////-> Adapters
            container.RegisterType<ITypeAdapterFactory, AutomapperTypeAdapterFactory>(new ContainerControlledLifetimeManager());

            //LoggerFactory.SetCurrent(new TraceSourceLogFactory());
            EntityValidatorFactory.SetCurrent(new DataAnnotationsEntityValidatorFactory());
            ////  AutomapperTypeAdapter

            ////container.RegisterType(typeof(ITypeAdapterFactory), new PerResolveLifetimeManager());
            var typeAdapterFactory = container.Resolve<ITypeAdapterFactory>();
                TypeAdapterFactory.SetCurrent(typeAdapterFactory);


            return container;

        }

    }
}
