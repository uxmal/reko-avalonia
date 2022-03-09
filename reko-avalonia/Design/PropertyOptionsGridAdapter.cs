#region License
/* 
 * Copyright (C) 1999-2022 John Källén.
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2, or (at your option)
 * any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; see the file COPYING.  If not, write to
 * the Free Software Foundation, 675 Mass Ave, Cambridge, MA 02139, USA.
 */
#endregion

using Avalonia.Controls;
using Reko.Core;
using Reko.Core.Configuration;
using Reko.Core.Services;
using Reko.Gui.Design;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Threading.Tasks;

namespace Reko.UserInterfaces.Avalonia.Design
{
    public class PropertyOptionsGridAdapter : AbstractPropertyOptionsGridAdapter
    {
        public PropertyOptionsGridAdapter(IDictionary values, List<PropertyOption> options)
            : base(values, options)
        {
        }

        protected override AbstractDictionaryPropertyDescriptor CreatePropertyDescriptor(PropertyOption option)
        {
            return new DictionaryPropertyDescriptor(values, option);
        }

        public class DictionaryPropertyDescriptor : AbstractDictionaryPropertyDescriptor
        {
            public DictionaryPropertyDescriptor(IDictionary values, PropertyOption option)
                : base(values, option)
            {
            }

            protected override EditorAttribute MakeEditorAttribute()
            {
                return new EditorAttribute(typeof(PropertyOptionEditor), typeof(UITypeEditor));
            }
        }

        /// <summary>
        /// Allows a custom editor to pop up a dialog box to collect a value from the user.
        /// </summary>
        /// <remarks>
        /// The custom editor must be a class derived from System.Windows.Forms and have 
        /// a public read/write property called "Value".
        /// </remarks>
        class PropertyOptionEditor : UITypeEditor
        {
            public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
            {
                return UITypeEditorEditStyle.Modal;
            }

            public override async ValueTask<object> EditValue(ITypeDescriptorContext context, System.IServiceProvider provider, object value)
            {
                var svc = provider.RequireService<IWindowsFormsEditorService>();
                var pd = DictionaryPropertyDescriptor.GetFromContext(context);
                var pluginSvc = provider.RequireService<IPluginLoaderService>();

                if (pd.Option.TypeName is null)
                    return value;
                var dlgType = pluginSvc.GetType(pd.Option.TypeName);
                if (dlgType == null)
                    return value;
                if (!(Activator.CreateInstance(dlgType) is Window form))
                    return value;
                var valueProperty = dlgType.GetProperty("Value");
                if (valueProperty == null)
                    return value;

                valueProperty.SetValue(form, value);
                if (await svc.ShowDialog(form))
                {
                    value = valueProperty.GetValue(form)!;
                }
                form.Close();
                return value;
            }
        }
    }
}
