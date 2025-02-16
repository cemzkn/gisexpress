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
using System.Linq;

namespace System.Windows.Forms
{
    [ToolboxItem(false)]
    public class ColorPickEdit : BaseEdit
    {
        public ColorPickEdit()
        {
            Padding = new Padding(3);
            Panel = new TablePanelEdit { Dock = DockStyle.Fill };

            ColorEditor = new ColorEdit { Dock = DockStyle.Fill, Visible = false };
            ColorPaletteEditor = new ColorPalette { Dock = DockStyle.Fill, Visible = false };
            ColorPaletteEditor.SelectedColorChanged += OnSelectedColorChanged;
            PanelGroup = new PanelEdit { BorderStyle = Border3DSide.All, Dock = DockStyle.Fill, Padding = new Padding(8) };

            Buttons = new ButtonEditCollection { Dock = DockStyle.Right, ButtonSize = new Size(80, 22) };
            Buttons.SetButtons(OnButtonClick, "OK", "Cancel");

            PanelLeft = new TableLayoutPanel { Dock = DockStyle.Left, Width = 100 };
            PanelLeft.RowCount = ColorPalette.Themes.Count + 1;

            foreach (string name in ColorPalette.Themes.Keys)
            {
                PanelLeft.Controls.Add(CreateCheckEdit(name), 0, PanelLeft.Controls.Count);
            }

            PanelLeft.Controls.Add(CreateCheckEdit("MoreColors"), 0, PanelLeft.Controls.Count);

            PanelGroup.Controls.Add(ColorEditor);
            PanelGroup.Controls.Add(ColorPaletteEditor);
            PanelGroup.Controls.Add(PanelLeft);
            Controls.Add(Panel);

            ShowButtons = false;
            OnCheckedChanged(this, EventArgs.Empty);
        }

        public event Action Cancel;
        public event Action<Color> SelectedColorChanged;

        protected TableLayoutPanel Panel;
        protected PanelEdit PanelGroup;
        protected TableLayoutPanel PanelLeft;
        protected ColorEdit ColorEditor;
        protected ColorPalette ColorPaletteEditor;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color SelectedColor
        {
            get { return ColorEditor.SelectedColor; }
            set { ColorEditor.SelectedColor = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool ShowButtons
        {
            get { return Panel.RowCount > 1; }
            set
            {
                const int height = 240;

                if (value)
                {
                    Panel.RowCount = 2;
                    Panel.Controls.Clear();
                    Panel.RowStyles.Clear();
                    Panel.RowStyles.Add(new RowStyle(SizeType.Absolute, height));
                    Panel.RowStyles.Add(new RowStyle(SizeType.Absolute, Buttons.ButtonSize.Height + Padding.Vertical));
                    Panel.Controls.Add(PanelGroup, 0, 0);
                    Panel.Controls.Add(Buttons, 0, 1);

                    Size = MinimumSize = new Size(430, height + Buttons.ButtonSize.Height + Padding.Vertical * 4);
                }
                else
                {
                    Panel.RowCount = 1;
                    Panel.Controls.Clear();
                    Panel.RowStyles.Clear();
                    Panel.RowStyles.Add(new RowStyle(SizeType.Absolute, height));
                    Panel.Controls.Add(PanelGroup, 0, 0);

                    Size = MinimumSize = new Size(430, height + Padding.Vertical);
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ButtonEditCollection Buttons
        {
            get;
            protected set;
        }

        protected void OnButtonClick(object sender, EventArgs e)
        {
            if (Buttons["OK"].Equals(sender))
            {
                OnSelectedColorChanged(SelectedColor);
            }
            else
            {
                ColorEditor.Cancel();
                Cancel.InvokeSafely();
            }
        }

        protected void OnSelectedColorChanged(Color color)
        {
            SelectedColorChanged.InvokeSafely(SelectedColor = color);
        }

        protected Control CreateCheckEdit(string name)
        {
            var edit = new CheckEdit { Name = name, Dock = DockStyle.Fill };

            edit.IsRadio = true;
            edit.RadioGroupIndex = 0;
            edit.CheckedChanged += OnCheckedChanged;
            Localization.Register(edit, e => edit.Text = e(name));

            return edit;
        }

        protected void OnCheckedChanged(object sender, EventArgs e)
        {
            var edit = sender as CheckEdit;

            if (edit.IsNull())
            {
                edit = PanelLeft.Controls.Cast<CheckEdit>().First();
                edit.Checked = true;
            }

            if (edit.Checked)
            {
                switch (edit.Name)
                {
                    case "MoreColors":
                        ShowMoreColors();
                        break;
                    default:
                        ShowThemeColors(edit.Name);
                        break;
                }
            }
        }

        protected void ShowThemeColors(string themeName)
        {
            ColorEditor.HideColorBox();
            ColorPaletteEditor.ThemeName = themeName;
            ColorPaletteEditor.Show();
            ColorPaletteEditor.BringToFront();
        }

        protected void ShowMoreColors()
        {
            ColorPaletteEditor.Hide();
            ColorEditor.ShowColorBox();
        }
    }
}
