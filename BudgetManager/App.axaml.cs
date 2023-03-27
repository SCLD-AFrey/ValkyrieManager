using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using BudgetManager.Models.BackingModels;
using BudgetManager.Models.BackingModels.Login;
using BudgetManager.Services;
using BudgetManager.Services.Database;
using BudgetManager.ViewModels;
using BudgetManager.ViewModels.Login;
using BudgetManager.Views;
using BudgetManager.Views.Login;
using BudgetTracker.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace BudgetManager;

public partial class App : Application
{
    private readonly IHost m_appHost;

    public App()
    {
        m_appHost = Host.CreateDefaultBuilder()
            .ConfigureLogging(p_options =>
            {
                p_options.AddDebug();
                p_options.AddSerilog();
            })
            .ConfigureServices(ConfigureServices).Build();
    }
    private void ConfigureServices(IServiceCollection p_services)
    {
        p_services.AddSingleton<FilesService>();
        p_services.AddSingleton<EncryptionService>();
        p_services.AddSingleton<AuthenticationService>();
        p_services.AddSingleton<UsersService>();
        
        p_services.AddSingleton<DatabaseUtilities>();
        p_services.AddSingleton<DatabaseInterface>();
        p_services.AddSingleton<DatabaseInit>();
        
        p_services.AddSingleton<MainWindowModel>();
        p_services.AddSingleton<MainWindowView>();
        p_services.AddSingleton<MainWindowViewModel>();
        
        p_services.AddSingleton<UserLoginModel>();
        p_services.AddSingleton<UserLoginView>();
        p_services.AddSingleton<UserLoginViewModel>();
    }
    
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var dataService = m_appHost.Services.GetService<DatabaseInit>();
        dataService.DoFirstTimeSetup();


        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var filesService = m_appHost.Services.GetService<FilesService>();
            filesService.CreateDirectories();
            
            
            desktop.MainWindow = m_appHost.Services.GetService<UserLoginView>();
        }

        base.OnFrameworkInitializationCompleted();
    }
}