using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using System;

namespace AvaloniaAPPA
{
    public class MainWindow : Window
    {
        const string BASE_COLOR = "BaseColor";
        const string GRADIENT_COLOR = "GradientAltColor";
        const string HIGHLIGHT_COLOR_PREFIX = "Hl0x";
        const string HEX_FORMAT = "X2";
        NumericUpDown _hueSpeen;
        Slider _hueSlider;

        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            _hueSpeen = this.Find<NumericUpDown>("HueSpeen");
            _hueSlider = this.Find<Slider>("HueSlider");
            

            _hueSpeen.ValueChanged += (_s_, _e_) => WhenTheHueSpeenIsValueChanged();


            _hueSlider.AddHandler(
                Slider.PointerReleasedEvent,
                HueSlider_PointerReleased,
                RoutingStrategies.Bubble | RoutingStrategies.Direct | RoutingStrategies.Tunnel, true);
            
            WhenTheHueSpeenIsValueChanged();

            this.Resources["HueGradient"] = CreateHueGradientBrush(8, 100, 100);
        }

        void WhenTheHueSpeenIsValueChanged()
        {
            double val = _hueSpeen.Value;
            _hueSlider.Value = val;
            RefreshColoures(val);
        }

        void HueSlider_PointerReleased(object sender, PointerReleasedEventArgs e)
            => _hueSpeen.Value = _hueSlider.Value;

        GradientBrush CreateHueGradientBrush(int precision, double sat, double val)
        {
            var stops = new GradientStops();
            double stopCount = precision - 1;
            for (int stopIndex = 0; stopIndex < precision; stopIndex++)
            {
                double stopPos = stopIndex / stopCount;
                double stopHue = stopPos * 360.0;
                stops.Add(new GradientStop(HSVToRGB(stopHue, sat, val), stopPos));
            }
            
            return new LinearGradientBrush()
            {
                StartPoint = new RelativePoint(0, 0, RelativeUnit.Relative),
                EndPoint = new RelativePoint(1, 0, RelativeUnit.Relative),
                GradientStops = stops
            };
        }


        void RefreshColoures(double hue)
        {
            this.Resources[BASE_COLOR] = HSVToRGB(hue, 63, 69);
            this.Resources[GRADIENT_COLOR] = HSVToRGB(hue, 51, 80);
            
            double saturation = 40;
            double value = 89;

            for (int i = 0x00; i < 0xF1; i += 0x10)
            {
                this.Resources[$"{HIGHLIGHT_COLOR_PREFIX}{i.ToString(HEX_FORMAT)}"] = HSVToRGB(hue, saturation, value, (byte)i);
            }

            
            this.Resources[$"{HIGHLIGHT_COLOR_PREFIX}7F"] = HSVToRGB(hue, saturation, value, 0x7F);
            this.Resources[$"{HIGHLIGHT_COLOR_PREFIX}FF"] = HSVToRGB(hue, saturation, value);
        }


        // https://www.programmingalgorithms.com/algorithm/hsv-to-rgb/
        public static Color HSVToRGB(double hue, double saturation, double value, byte alpha = 0xFF)
        {
            double H = hue;
            double S = (saturation / 100);
            double V = (value / 100);


            double r = 0, g = 0, b = 0;

            if (S == 0)
            {
                r = V;
                g = V;
                b = V;
            }
            else
            {
                int i;
                double f, p, q, t;

                if (H == 360)
                    H = 0;
                else
                    H = H / 60;

                i = (int)Math.Truncate(H);
                f = H - i;

                p = V * (1.0 - S);
                q = V * (1.0 - (S * f));
                t = V * (1.0 - (S * (1.0 - f)));

                switch (i)
                {
                    case 0:
                        r = V;
                        g = t;
                        b = p;
                        break;

                    case 1:
                        r = q;
                        g = V;
                        b = p;
                        break;

                    case 2:
                        r = p;
                        g = V;
                        b = t;
                        break;

                    case 3:
                        r = p;
                        g = q;
                        b = V;
                        break;

                    case 4:
                        r = t;
                        g = p;
                        b = V;
                        break;

                    default:
                        r = V;
                        g = p;
                        b = q;
                        break;
                }

            }

            return new Color(alpha, (byte)(r * 255), (byte)(g * 255), (byte)(b * 255));
        }
    }
}