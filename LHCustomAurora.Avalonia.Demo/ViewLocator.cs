using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using LHCustomAurora.Avalonia.Demo.ViewModels;

namespace LHCustomAurora.Avalonia.Demo
{
    public class ViewLocator : IDataTemplate
    {
        public const string NULL_DATA = "Null DataContext!";
        public bool SupportsRecycling => false;

        public Control Build(object data)
        {
            var type = ((ViewModelBase)data).ViewType;
            
            if (type != null)
                return (Control)Activator.CreateInstance(type);
            
            return new TextBlock()
            {
                Text = (data != null)
                    ? $"Not Found: {data.GetType().FullName}"
                    : NULL_DATA
            };
        }

        public bool Match(object data)
        {
            return data is ViewModelBase;
        }
    }
}