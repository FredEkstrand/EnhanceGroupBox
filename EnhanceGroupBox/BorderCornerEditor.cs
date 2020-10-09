using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing.Design;
using System.Globalization;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using Ekstrand.Drawing;

namespace Ekstrand.Windows.Forms
{
    internal class BorderCornerEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            if (context != null && context.Instance != null)
            {
                return UITypeEditorEditStyle.DropDown;
            }
            return base.GetEditStyle(context);
        }

        public override object EditValue(ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {
            if (context != null && context.Instance != null && provider != null)
            {
                IWindowsFormsEditorService editorService = provider.GetService(
                            typeof(IWindowsFormsEditorService))
                            as IWindowsFormsEditorService;
                if (editorService != null)                
				{                    
                    CornerSelector cs = new CornerSelector(value);
                    editorService.DropDownControl(cs);
                    value = cs.BorderCorners;
                }
                
            }
            return value;
        }
    }
}
