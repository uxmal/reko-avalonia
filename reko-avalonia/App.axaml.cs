using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Styling;
using Reko.Core;
using Reko.Core.Configuration;
using Reko.UserInterfaces.Avalonia.ViewModels;
using Reko.UserInterfaces.Avalonia.Views;
using System;
using System.ComponentModel.Design;

namespace Reko.UserInterfaces.Avalonia
{
    public class App : Application
    {       
        public static readonly Styles FluentDark = new Styles
        {
            new StyleInclude(new Uri("avares://reko-avalonia/Styles"))
            {
                Source = new Uri("avares://reko-avalonia/Themes/FluentDark.axaml")
            }
        };

        public static readonly Styles FluentLight = new Styles
        {
            new StyleInclude(new Uri("avares://reko-avalonia/Styles"))
            {
                Source = new Uri("avares://reko-avalonia/Themes/FluentLight.axaml")
            }
        };

        public static readonly Styles DefaultLight = new Styles
        {
            new StyleInclude(new Uri("avares://reko-avalonia-prototype/Styles"))
            {
                Source = new Uri("avares://reko-avalonia/Themes/DefaultLight.axaml")
            }
        };

        public static readonly Styles DefaultDark = new Styles
        {
            new StyleInclude(new Uri("avares://reko-avalonia/Styles"))
            {
                Source = new Uri("avares://reko-avalonia/Themes/DefaultDark.axaml")
            },
        };

        public override void Initialize()
        {
            Styles.Insert(0, FluentLight);

            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            // DockManager.s_enableSplitToWindow = true;

            var sc = new ServiceContainer();
            var cfgSvc = RekoConfigurationService.Load(sc, "reko/reko.config");
            sc.AddService<IConfigurationService>(cfgSvc);
            var mainWindowViewModel = new MainWindowViewModel(sc);

            switch (ApplicationLifetime)
            {
                case IClassicDesktopStyleApplicationLifetime desktopLifetime:
                {
                    var mainWindow = new MainWindow
                    {
                        DataContext = mainWindowViewModel
                    };
					//$REVIEW: is there a better way to provide the MainWindow to the
                    // view model? We want to show modal dialogs.
                    mainWindowViewModel.MainWindow = mainWindow;

                    mainWindow.Closing += (_, _) =>
                    {
                        mainWindowViewModel.CloseLayout();
                    };

                    desktopLifetime.MainWindow = mainWindow;

                    desktopLifetime.Exit += (_, _) =>
                    {
                        mainWindowViewModel.CloseLayout();
                    };
                    
                    break;
                }
                case ISingleViewApplicationLifetime singleViewLifetime:
                {
                    var mainView = new MainView()
                    {
                        DataContext = mainWindowViewModel
                    };

                    singleViewLifetime.MainView = mainView;

                    break;
                }
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
