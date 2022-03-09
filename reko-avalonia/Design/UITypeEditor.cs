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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reko.UserInterfaces.Avalonia.Design
{
    public class UITypeEditor
    {
        public object EditValue(IServiceProvider provider, object value)
        {
            throw new NotImplementedException();
        }

        public virtual ValueTask<object> EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            throw new NotImplementedException();
        }

        public UITypeEditorEditStyle GetEditStyle()
        {
            throw new NotImplementedException();
        }

        public bool GetPaintValueSupported()
        {
            throw new NotImplementedException();
        }

        public virtual bool GetPaintValueSupported(ITypeDescriptorContext context)
        {
            throw new NotImplementedException();
        }


        public virtual UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            throw new NotImplementedException();
        }
        /*
        public void PaintValue(object value, Graphics canvas, Rectangle rectangle)
        {
            throw null;
        }

        public virtual void PaintValue(PaintValueEventArgs e)
        {
            throw null;
        }
*/
    }
}
