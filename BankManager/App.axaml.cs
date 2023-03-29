using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using BankManager.Models;
using BankManager.Services;
using BankManager.Services.Database;
using BankManager.ViewModels;
using BankManager.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace BankManager;

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
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }
    private void ConfigureServices(IServiceCollection p_services)
    {
        p_services.AddSingleton<FileService>();
        p_services.AddSingleton<SettingsService>();
        p_services.AddSingleton<EncryptionService>();
        p_services.AddSingleton<UserService>();
        
        p_services.AddSingleton<DatabaseInterface>();
        p_services.AddSingleton<DatabaseUtilities>();
        p_services.AddSingleton<DatabaseInit>();
        
        p_services.AddSingleton<MainWindowView>();
        p_services.AddSingleton<MainWindowViewModel>();
        
        p_services.AddSingleton<LoginWindowModel>();
        p_services.AddSingleton<LoginWindowView>();
        p_services.AddSingleton<LoginWindowViewModel>();
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var fileService = m_appHost.Services.GetRequiredService<FileService>();
        var settingsService = m_appHost.Services.GetRequiredService<SettingsService>();
        var userService = m_appHost.Services.GetRequiredService<UserService>();
        fileService.CreateDirectories();
        fileService.InitSettingsFiles();
        userService.Init();
        settingsService.LoadSettings();
        

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = (userService.CurrentUser != null)
                ? m_appHost.Services.GetRequiredService<MainWindowView>()
                : m_appHost.Services.GetRequiredService<LoginWindowView>();
        }

        base.OnFrameworkInitializationCompleted();
    }
}