using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace Reko.UserInterfaces.Avalonia.Controls
{
    internal class PropertyGridViewModel : ReactiveObject
    {
        private readonly Dictionary<string, PropertyDescriptor> propsByName;

        public PropertyGridViewModel(object? o)
        {
            this.Object = o;
            if (o is not null)
            {
                var properties = TypeDescriptor.GetProperties(o);
                this.PropertyItems = new ObservableCollection<PropertyItem>(
                    properties
                        .Cast<PropertyDescriptor>()
                        .Select(pd => CreatePropertyItem(pd)));
                this.propsByName = properties.Cast<PropertyDescriptor>()
                    .ToDictionary(p => p.Name);
            }
            else
            {
                PropertyItems = new ObservableCollection<PropertyItem>();
                propsByName = new();
            }
        }

        private string? selectedPropertyName;
        public string? SelectedPropertyName
        {
            get => selectedPropertyName;
            set => this.RaiseAndSetIfChanged(ref selectedPropertyName, value, nameof(SelectedPropertyName));
        }

        private string? selectedPropertyDescription;
        public string? SelectedPropertyDescription
        {
            get => selectedPropertyDescription;
            set => this.RaiseAndSetIfChanged(ref selectedPropertyDescription, value, nameof(SelectedPropertyDescription));
        }

        public object? Object { get; set; }

        public ObservableCollection<PropertyItem> PropertyItems { get; }

        private PropertyItem? selectedPropertyItem;
        public PropertyItem? SelectedPropertyItem { 
            get => selectedPropertyItem;
            set => OnSelectedPropertyItemChanged(value);
        }

        private void CallIfChanged<T>(ref T backingField, T newValue, Action fn)
        {
            if (object.Equals(backingField, newValue))
                return;
            backingField = newValue;
            fn();
        }

        private PropertyItem CreatePropertyItem(PropertyDescriptor pd)
        {
            var value = pd.GetValue(this.Object);
            return new PropertyItem(pd.Name, value);
        }

        private void OnSelectedPropertyItemChanged(PropertyItem? newValue)
        {
            if (object.Equals(selectedPropertyItem, newValue))
                return;
            SelectedPropertyName = newValue?.Name ?? "";
            if (newValue is not null)
            {
                var pd = propsByName[newValue.Name];
                var desc = pd.Attributes.OfType<DescriptionAttribute>().FirstOrDefault();
                if (desc is not null)
                {
                    SelectedPropertyDescription = desc.Description;
                }
            }
        }
    }

    public class PropertyItem : ReactiveObject
    {
        public PropertyItem(string name, object? value)
        {
            this.Name = name;
            this.Value = value;
        }

        public string Name { get; set; }
        public object? Value { get; set; }

        
        public bool Selected {
            get => selected;
            set => this.RaiseAndSetIfChanged(ref selected, value, nameof(Selected));
        }
        private bool selected;
    }
}
