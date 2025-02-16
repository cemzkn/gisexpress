﻿//////////////////////////////////////////////////////////////////////////////////////////////////
//
//  Copyright © GISExpress 2015-2022. All Rights Reserved.
//  
//  GISExpress .NET API and Component Library
//  
//  The entire contents of this file is protected by local and International Copyright Laws.
//  Unauthorized reproduction, reverse-engineering, and distribution of all or any portion of
//  the code contained in this file is strictly prohibited and may result in severe civil and 
//  criminal penalties and will be prosecuted to the maximum extent possible under the law.
//  
//  RESTRICTIONS
//  
//  THIS SOURCE CODE AND ALL RESULTING INTERMEDIATE FILES ARE CONFIDENTIAL AND PROPRIETARY TRADE SECRETS OF GISExpress
//  THE REGISTERED DEVELOPER IS LICENSED TO DISTRIBUTE THE PRODUCT AND ALL ACCOMPANYING .NET COMPONENTS AS PART OF AN EXECUTABLE PROGRAM ONLY.
//  
//  THE SOURCE CODE CONTAINED WITHIN THIS FILE AND ALL RELATED FILES OR ANY PORTION OF ITS CONTENTS SHALL AT NO TIME BE
//  COPIED, TRANSFERRED, SOLD, DISTRIBUTED, OR OTHERWISE MADE AVAILABLE TO OTHER INDIVIDUALS WITHOUT EXPRESS WRITTEN CONSENT
//  AND PERMISSION FROM GISExpress
//  
//  CONSULT THE END USER LICENSE AGREEMENT FOR INFORMATION ON ADDITIONAL RESTRICTIONS.
//  
//  Warning: This content was generated by GISExpress tools.
//  Changes to this content may cause incorrect behavior and will be lost if the content is regenerated.
//
///////////////////////////////////////////////////////////////////////////////////////////////////

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;

namespace System.Windows.Forms
{
    partial class ListViewEdit
    {
        public class PropertyColumn : Column, ITypeDescriptorContext
        {
            public PropertyColumn(PropertyDescriptor property)
            {
                Property = property;

                Name = Property.Name;
                Text = Property.DisplayName;
                Browsable = Property.IsBrowsable;

                TextAlign =
                ImageAlign = Property.GetTextAlignment();

                if (typeof(IBindingList).IsAssignableFrom(Property.PropertyType))
                {
                    Browsable = false;
                    IsExpandable = true;
                }
                else
                {
                    IsExpandable = Property.Converter is ExpandableObjectConverter;
                }

                Properties.HasChanges = true;
            }

            protected object Component;
            protected Control EditorObject;
            protected GridViewEdit.GridView ViewValue;

            public GridViewEdit.GridView View
            {
                get { return ViewValue ?? (ViewValue = Owner.View as GridViewEdit.GridView); }
            }

            public override bool CanFilter
            {
                get { return true; }
            }

            public override ContentAlignment TextAlign
            {
                get
                {
                    return base.TextAlign;
                }
                set
                {
                    base.TextAlign = value;
                    Editor.SetProperty("TextAlign", value);
                }
            }

            public override ContentAlignment ImageAlign
            {
                get
                {
                    return base.ImageAlign;
                }
                set
                {
                    base.ImageAlign = value;
                    Editor.SetProperty("ImageAlign", ImageAlign);
                }
            }

            public Control Editor
            {
                get
                {
                    if (EditorObject.IsNull() && Property.HasValue())
                    {
                        EditorObject = Property.GetEditor(Types.Control) as Control;

                        if (EditorObject.HasValue())
                        {
                            var edit = EditorObject as IPropertyEditor;

                            if (edit.HasValue())
                            {
                                edit.Initialize(Property);
                            }

                            EditorObject.AutoSize = false;
                            EditorObject.Font = Font;
                            EditorObject.RemoveBorders();
                            EditorObject.BackColor = Color.Transparent;
                            EditorObject.SetProperty("TextAlign", TextAlign);
                            EditorObject.SetProperty("ImageAlign", ImageAlign);
                        }
                    }

                    return EditorObject;
                }
            }

            public PropertyDescriptor Property
            {
                get;
                protected set;
            }

            protected override Size OnCalculateSize()
            {
                Size newSize = base.OnCalculateSize();

                if (Editor.HasValue())
                {
                    if (newSize.Width < Editor.MinimumSize.Width)
                    {
                        newSize.Width = Editor.MinimumSize.Width;
                    }
                }

                if (Owner.View.Items.Count > 0)
                {
                    newSize.Width = Math.Max(newSize.Width, Owner.View.Items.Take(0x100).Max(r => ControlHelper.MeasureText(DisplayText(r), Font).Width) + 6);
                }

                return newSize;
            }

            public override string OnDisplayText(Row row)
            {
                if (row.HasValue())
                {
                    return OnDisplayText(GetValue(row));
                }

                return string.Empty;
            }

            protected override string OnDisplayText(object value, IFormatProvider provider)
            {
                return Property.Converter.ConvertTo(value, Types.String) as String;
            }

            public override object GetValue(Row row)
            {
                if (row.HasValue())
                {
                    return Property.GetValue(row.Value);
                }

                return default(object);
            }

            protected override void OnPaintCell(Graphics g, Rectangle bounds, Row row)
            {
                var supportPaint = Editor as ISupportPaint;

                if (supportPaint.HasValue())
                {
                    if (!CellColor.IsEmpty)
                    {
                        Editor.ForeColor = CellColor;
                    }

                    supportPaint.PaintValue(new PaintValueEventArgs(this, DisplayText(row), g, bounds));
                }
                else
                {
                    base.OnPaintCell(g, bounds, row);
                }
            }

            protected internal override bool OnCellClick(Point point, int row)
            {
                return BeginEdit(point, row);
            }

            protected bool BeginEdit(Point point, int row)
            {
                if (View.HasValue() && Editor.HasValue())
                {
                    if (Property.IsReadOnly)
                    {
                        return false;
                    }

                    var item = View.Items[row];
                    var valueSupport = Editor as ISupportEditValue;
                    var bounds = Owner.View.GetRowRectangle(row - View.TopRowIndex);
                    var editorSize = new Size(Width, bounds.Height);

                    bounds.X = Left + View.AutoScrollPosition.X;
                    bounds.Width = Width;

                    View.EditContainer.Controls.Clear();
                    View.EditContainer.Controls.Add(Editor);
                    View.EditContainer.SetBounds(bounds.Left + 1, bounds.Top + 1, bounds.Width - 2, bounds.Height - 2, BoundsSpecified.All);

                    Component = item.Value;
                    UpdateEditorLayout(editorSize, bounds);

                    Editor.Dock = DockStyle.None;
                    Editor.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;

                    View.EditContainer.Visible = true;
                    View.EditContainer.Show();

                    if (valueSupport.HasValue())
                    {
                        var value = GetValue(item);

                        point.Offset(-bounds.Left, -bounds.Top);

                        valueSupport.EditValue = value;
                        valueSupport.BeginEdit(point, this);

                        if (valueSupport.EditValue.IsNull())
                        {
                            valueSupport.EditValue = value;
                        }
                    }
                    else
                    {
                        Editor.Text = Text;
                    }

                    UpdateEditorLayout(editorSize, bounds);

                    if (!Property.IsReadOnly)
                    {
                        Editor.KeyDown += OnEditorKeyDown;
                        Editor.Validating += OnEditorValidating;
                    }

                    Editor.UpdateFocus();

                    return true;
                }

                return false;
            }

            protected bool EndEdit(bool acceptChanges)
            {
                try
                {
                    if (View.HasValue() && Editor.HasValue())
                    {
                        if (Component.HasValue())
                        {
                            Editor.KeyDown -= OnEditorKeyDown;
                            Editor.Validating -= OnEditorValidating;

                            View.EditContainer.Visible = false;
                            View.EditContainer.Controls.Clear();

                            if (acceptChanges)
                            {
                                return SetValue(Component, Editor.Text, Editor as ISupportEditValue);
                            }
                        }
                    }
                }
                finally
                {
                    Component = null;
                    View.UpdateFocus();
                }

                return false;
            }

            protected void OnEditorKeyDown(object sender, KeyEventArgs e)
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        EndEdit(true);
                        break;
                    case Keys.Escape:
                        EndEdit(false);
                        break;
                }
            }

            protected void OnEditorValidating(object sender, CancelEventArgs e)
            {
                e.Cancel = !EndEdit(true);
            }

            protected override int OnCompareRow(Row row, Row other)
            {
                return base.OnCompareValue(Property.GetValue(row), Property.GetValue(other));
            }

            protected virtual void UpdateEditorLayout(Size size, Rectangle bounds)
            {
                Editor.MinimumSize = Size.Empty;
                Editor.MaximumSize = Size.Empty;

                Editor.SetBounds(0, -1, size.Width, size.Height, BoundsSpecified.All);

                if (Editor.Height > bounds.Height)
                {
                    Editor.Top = (bounds.Height - Editor.Height) / 2;
                }
            }

            protected bool SetValue(object component, string valueString, ISupportEditValue valueSupport)
            {
                try
                {
                    if (component.HasValue() && Property.HasValue())
                    {
                        if (valueSupport.HasValue())
                        {
                            Property.SetValue(component, valueSupport.EditValue);
                            return true;
                        }
                        else if (Property.Converter.CanConvertFrom(Types.String))
                        {
                            Property.SetValue(component, Property.Converter.ConvertFrom(valueString));
                            return true;
                        }
                    }
                }
                catch (Exception e)
                {
                    PopupMessage.Show(View.EditContainer, e.Message, MessageBoxIcon.Error);
                }

                return false;
            }

            #region ITypeDescriptorContext

            IContainer ITypeDescriptorContext.Container
            {
                get { return default(IContainer); }
            }

            object ITypeDescriptorContext.Instance
            {
                get { return this; }
            }

            void ITypeDescriptorContext.OnComponentChanged()
            {
            }

            bool ITypeDescriptorContext.OnComponentChanging()
            {
                return false;
            }

            PropertyDescriptor ITypeDescriptorContext.PropertyDescriptor
            {
                get { return Property; }
            }

            object IServiceProvider.GetService(Type serviceType)
            {
                return default(object);
            }

            #endregion

            protected override void Dispose(bool disposing)
            {
                base.Dispose(disposing);

                if (disposing)
                {
                    Property = null;
                }
            }
        }
    }
}
