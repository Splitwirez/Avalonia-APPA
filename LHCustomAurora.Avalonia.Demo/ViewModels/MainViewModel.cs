using System;

namespace LHCustomAurora.Avalonia.Demo.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        double _hue = 54;
        public double Hue
        {
            get => _hue;
            set => RASIC(ref _hue, Math.Round(value, 0));
        }
    }
}
