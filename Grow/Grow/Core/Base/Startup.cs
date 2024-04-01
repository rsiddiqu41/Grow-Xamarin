using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Grow.Core.Authentication;
using Grow.Models;
using Grow.ViewModels.LoginVM;
using Grow.ViewModels;
using Grow.ViewModels.RegistrationVM;

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Grow.Core.Database;
using Grow.ElementViewModels;
using Grow.Core.Mediator;
using Grow.Core.Plaid;
using Grow.Core.Finance;
using Grow.Core.Adapter;

namespace Grow.Core.Base
{
    public static class Startup
    {
        public static IServiceProvider ServiceProvider { get; set; }

        public static void Init(Action<HostBuilderContext, IServiceCollection> nativeConfigureServices)
        {
            var root = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "root");
            Directory.CreateDirectory(root);

            var host = new HostBuilder().ConfigureHostConfiguration(c =>
            {
                c.AddInMemoryCollection(new Dictionary<string, string>
                {
                    { HostDefaults.ContentRootKey, root }
                });
            })
            //ConfigureHostConfiguration
            .ConfigureServices((c, x) =>
            {
                //Add this line to call back into your native code
                nativeConfigureServices(c, x);
                InitializeDependencyInjectionContainer(c, x);
            })
            // Add Logging to Hostbuilder here
            .Build();
            ServiceProvider = host.Services;
        }

        private static void InitializeDependencyInjectionContainer(HostBuilderContext c, IServiceCollection InServices)
        {
            ConfigureServices(c, InServices);
            ConfigureViewModels(c, InServices);
        }

        static void ConfigureServices(HostBuilderContext ctx, IServiceCollection services)
        {
            services.AddSingleton<ISystemLayoutModel, SystemLayoutModel>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IMediator, UnityMessenger>();
            services.AddSingleton<IPlaidService, PlaidService>();
            services.AddSingleton<IPlaidAdapter, PlaidAdapter>();
            services.AddSingleton<IFinancialSummaryService, FinancialSummaryService>();
            //services.AddSingleton<>();
            //services.AddHttpClient<IPlaidAPI>();
        }

        static void ConfigureViewModels(HostBuilderContext c, IServiceCollection services)
        {
            // add the ViewModel, but as a Transient, which means it will create a new one each time.
            services.AddTransient<StartViewModel>();
            services.AddTransient<RegistrationViewModel>();
            services.AddTransient<RegistrationEmailViewModel>();
            services.AddTransient<RegistrationPhoneViewModel>();
            services.AddTransient<RegistrationPhoneOTPViewModel>();
            services.AddTransient<RegistrationPhoneExistsViewModel>();
            services.AddTransient<RegistrationGoogleViewModel>();
            services.AddTransient<EmailVerificationSentViewModel>();
            services.AddTransient<LoginViewModel>();
            services.AddTransient<LoginEmailViewModel>();
            services.AddTransient<LoginPhoneViewModel>();
            services.AddTransient<LoginPhoneOTPViewModel>();
            services.AddTransient<LoginGoogleViewModel>();
            services.AddTransient<FinanceOverviewViewModel>();
            services.AddTransient<RegisterationPageAfterLoginViewModel>();
            services.AddTransient<PlaidUIViewModel>();
        }
    }





    /// <summary>
    /// Implementation using Microsoft.Extensions.DependencyInjection library only (No hostbuilder)
    /// </summary>

    /*public static class Startup
    {
        public static IServiceProvider _ServiceProvider { get; set; }

        public static IServiceProvider Init()
        {
            // Initialize all viewmodels and services with DI container
            var serviceProvider = new ServiceCollection().ConfigureServices().ConfigureViewModels().BuildServiceProvider();

            _ServiceProvider = serviceProvider;

            return serviceProvider;
        }
    }*/
}
