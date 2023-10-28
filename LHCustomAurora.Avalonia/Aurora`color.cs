using LHCustomAurora.Avalonia.Util;

namespace LHCustomAurora.Avalonia
{
    public partial class Aurora
    {
        const string _BASE_COLOR = "BaseColor";
        const string _GRADIENT_COLOR = "GradientAltColor";
        const string _HIGHLIGHT_COLOR_PREFIX = "Hl0x";
        const string _HEX_FORMAT = "X2";

        void EnsureResources(double hue)
        {
            _rootControl.Resources[_BASE_COLOR] = ColorHelper.HSVToRGB(hue, 63, 69);
            _rootControl.Resources[_GRADIENT_COLOR] = ColorHelper.HSVToRGB(hue, 51, 80);
            
            double saturation = 40;
            double value = 89;

            for (int i = 0x00; i < 0xF1; i += 0x10)
            {
                _rootControl.Resources[$"{_HIGHLIGHT_COLOR_PREFIX}{i.ToString(_HEX_FORMAT)}"] = ColorHelper.HSVToRGB(hue, saturation, value, (byte)i);
            }

            
            _rootControl.Resources[$"{_HIGHLIGHT_COLOR_PREFIX}7F"] = ColorHelper.HSVToRGB(hue, saturation, value, 0x7F);
            _rootControl.Resources[$"{_HIGHLIGHT_COLOR_PREFIX}FF"] = ColorHelper.HSVToRGB(hue, saturation, value);
        }
    }
}