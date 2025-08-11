using Autofac;
using EmailSMTP;
using Microsoft.Extensions.Configuration;
using System;

var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
    .Build();

var container = DIContainerConfig.Configure();

using var scope = container.BeginLifetimeScope();

// Resolve only the dependency
var greetingService = scope.Resolve<IEmailService>();

// Manually construct App with resolved dependency
App app = new App(greetingService);

app.Run();
