using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using LHCustomAurora.Avalonia.Demo.ViewModels;
using LHCustomAurora.Avalonia.Demo.Views;
using LHCustomAurora.Avalonia.Util;

namespace LHCustomAurora.Avalonia.Demo
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            Resources["HueGradient"] = CreateHueGradientBrush(8, 100, 100);

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainViewModel(),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }

        static GradientBrush CreateHueGradientBrush(int precision, double sat, double val)
        {
            var stops = new GradientStops();
            double stopCount = precision - 1;
            for (int stopIndex = 0; stopIndex < precision; stopIndex++)
            {
                double stopPos = stopIndex / stopCount;
                double stopHue = stopPos * 360.0;
                stops.Add(new GradientStop(ColorHelper.HSVToRGB(stopHue, sat, val), stopPos));
            }
            
            return new LinearGradientBrush()
            {
                StartPoint = new RelativePoint(0, 0, RelativeUnit.Relative),
                EndPoint = new RelativePoint(1, 0, RelativeUnit.Relative),
                GradientStops = stops
            };
        }
    }
}