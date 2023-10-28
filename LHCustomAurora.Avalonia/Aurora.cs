using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Primitives;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using System;
using System.Diagnostics;

namespace LHCustomAurora.Avalonia
{
    public partial class Aurora : TemplatedControl
    {
        /// <summary>
        /// Defines the <see cref="Hue"/> property.
        /// </summary>
        public static readonly StyledProperty<double> HueProperty =
            AvaloniaProperty.Register<Aurora, double>(nameof(Hue), 0.0);

        /// <summary>
        /// Gets or sets the hue of the aurora.
        /// </summary>
        public double Hue
        {
            get => GetValue(HueProperty);
            set => SetValue(HueProperty, value);
        }

        static Aurora()
        {
            HueProperty.Changed.AddClassHandler<Aurora>((s, e) =>
            {
                if (s != null)
                    s.OnHueChanged(e.GetNewValue<double>());
            });
        }
        
        Control _rootControl = null;
        void OnHueChanged(double newHue)
        {
            if (_rootControl != null)
                EnsureResources(newHue);
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            _rootControl = e.NameScope.Get<Control>("PART_RootControl");

            OnHueChanged(Hue);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            var availableHeight = availableSize.Height;
            var baseSize = base.MeasureOverride(availableSize);

            if (double.IsNaN(availableHeight) || double.IsInfinity(availableHeight))
                return baseSize;
            if (_rootControl == null)
                return baseSize;

            double desiredHeight = _rootControl.DesiredSize.Height;
            if (double.IsNaN(desiredHeight) || double.IsInfinity(desiredHeight))
                return baseSize;

            return baseSize.WithHeight(desiredHeight);
        }
    }
}