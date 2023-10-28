using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using System;

namespace LHCustomAurora.Avalonia.Util
{
    internal static class ColorHelper
    {
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