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

using System.Data;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using System.Workspace;

namespace System.Windows.Modules.Data
{
    internal class MapDataLayerControl : MapLayerControl
    {
        private ButtonEdit AddNewConnection;
        private PanelEdit Panel;
        private ListViewEdit ListTables;

        public MapDataLayerControl()
        {
            InitializeComponent();

            ListTables.View.ColumnLayout += OnTableViewColumnLayout;

            AddNewConnection.Image = Images016.NewDatabase;
            AddNewConnection.ImageAlign = ContentAlignment.MiddleLeft;
            AddNewConnection.Text = Localization.Localize("AddDataConnection");
        }

        public override MapWorkspace Workspace
        {
            get { return base.Workspace; }
            set
            {
                base.Workspace = value;
                RefreshTables();
            }
        }

        protected void RefreshTables()
        {
            ListTables.CheckBox = true;
            ListTables.DataSource = new DataTableItemCollection(Workspace);
        }

        protected void OnTableViewColumnLayout()
        {
            if (ListTables.Columns.Contains("Name"))
            {
                ListTables.Columns["Name"].MinWidth = 120;
                ListTables.Columns["Owner"].MinWidth = 90;
                ListTables.Columns["Catalog"].MinWidth = 90;
            }
        }

        protected void OnAddNewConnectionClick(object sender, EventArgs e)
        {
            //using (var dialog = new DataConnectionDialog())
            //{
            //    if (dialog.ShowDialog(this) == DialogResult.OK)
            //    {
            //        Workspace.Connections.Add(dialog.CreateConnection());
            //        RefreshTables();
            //    }
            //}
        }

        protected override void OnCreateLayers(MapCategory parent)
        {
            foreach (ListViewEdit.Row r in ListTables.CheckedRows)
            {
                var item = r.Value as DataTableItem;

                if (item.HasValue())
                {
                    MapLayer layer = MapLayerAttribute.Create(item.Table.Command.Connection.Factory.LayerClsId);

                    layer.Name = item.Name;
                    layer.Command = item.Table.Command;

                    parent.Layers.Add(layer);
                }
            }
        }

        void InitializeComponent()
        {
            this.AddNewConnection = new System.Windows.Forms.ButtonEdit();
            this.ListTables = new System.Windows.Forms.ListViewEdit();
            this.Panel = new System.Windows.Forms.PanelEdit();
            this.Panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // AddNewConnection
            // 
            this.AddNewConnection.Location = new System.Drawing.Point(0, 4);
            this.AddNewConnection.Name = "AddNewConnection";
            this.AddNewConnection.Size = new System.Drawing.Size(165, 26);
            this.AddNewConnection.TabIndex = 0;
            this.AddNewConnection.Text = "Add New Connection";
            this.AddNewConnection.Click += new System.EventHandler(this.OnAddNewConnectionClick);
            // 
            // ListTables
            // 
            this.ListTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListTables.Location = new System.Drawing.Point(3, 3);
            this.ListTables.Name = "ListTables";
            this.ListTables.Padding = new System.Windows.Forms.Padding(1);
            this.ListTables.Size = new System.Drawing.Size(317, 197);
            this.ListTables.TabIndex = 1;
            this.ListTables.TabStop = false;
            // 
            // panelEdit1
            // 
            this.Panel.Controls.Add(this.AddNewConnection);
            this.Panel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Panel.Location = new System.Drawing.Point(3, 200);
            this.Panel.Name = "panelEdit1";
            this.Panel.Size = new System.Drawing.Size(317, 30);
            this.Panel.TabIndex = 2;
            // 
            // MapDataLayerControl
            // 
            this.Controls.Add(this.ListTables);
            this.Controls.Add(this.Panel);
            this.Name = "MapDataLayerControl";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Size = new System.Drawing.Size(323, 233);
            this.Panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
