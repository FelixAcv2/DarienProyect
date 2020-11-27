﻿using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Unity;

namespace DarienProyect.Solicitud.Aplication
{
   public static class DependencyInjection
    {
        public static IUnityContainer AddContainerAplicationSolicitud(this IUnityContainer container)
        {


            return container;

        }

        public static IServiceCollection AddAplicationSolicitud(this IServiceCollection services)
        {

            services.AddMediatR(Assembly.GetExecutingAssembly());


            return services;
        }

    }
}
