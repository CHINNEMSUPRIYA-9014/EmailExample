using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailSMTP
{
    public static class DIContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<EmailService>().As<IEmailService>().SingleInstance();
            return builder.Build();
        }
    }
}
