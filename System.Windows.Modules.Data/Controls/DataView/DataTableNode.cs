﻿////////////////////////////////////////////////////////////////////////////////////////////////////
////
////  Copyright © GISExpress 2015-2022. All Rights Reserved.
////  
////  GISExpress .NET API and Component Library
////  
////  The entire contents of this file is protected by local and International Copyright Laws.
////  Unauthorized reproduction, reverse-engineering, and distribution of all or any portion of
////  the code contained in this file is strictly prohibited and may result in severe civil and 
////  criminal penalties and will be prosecuted to the maximum extent possible under the law.
////  
////  RESTRICTIONS
////  
////  THIS SOURCE CODE AND ALL RESULTING INTERMEDIATE FILES ARE CONFIDENTIAL AND PROPRIETARY TRADE SECRETS OF GISExpress
////  THE REGISTERED DEVELOPER IS LICENSED TO DISTRIBUTE THE PRODUCT AND ALL ACCOMPANYING .NET COMPONENTS AS PART OF AN EXECUTABLE PROGRAM ONLY.
////  
////  THE SOURCE CODE CONTAINED WITHIN THIS FILE AND ALL RELATED FILES OR ANY PORTION OF ITS CONTENTS SHALL AT NO TIME BE
////  COPIED, TRANSFERRED, SOLD, DISTRIBUTED, OR OTHERWISE MADE AVAILABLE TO OTHER INDIVIDUALS WITHOUT EXPRESS WRITTEN CONSENT
////  AND PERMISSION FROM GISExpress
////  
////  CONSULT THE END USER LICENSE AGREEMENT FOR INFORMATION ON ADDITIONAL RESTRICTIONS.
////  
////  Warning: This content was generated by GISExpress tools.
////  Changes to this content may cause incorrect behavior and will be lost if the content is regenerated.
////
/////////////////////////////////////////////////////////////////////////////////////////////////////

//using System.Collections.Generic;
//using System.Data;
//using System.Data.IO;
//using System.Windows.Forms;
//using System.Workspace;

//namespace System.Windows.Modules.Data.Controls
//{
//    internal class DataTableNode : DataTreeNode
//    {
//        public const string ID = "{9CBC6A20-802C-4731-9674-37ADF96D0236}";

//        public DataTableNode(DataTreeViewBuilder builder, SqlTableSource dataSource)
//            : base(builder)
//        {
//            DataSource = dataSource;
//            ImageKey = SelectedImageKey = ID;
//            Text = DataSource.GetDisplayName();
//            Nodes.Add(string.Empty);

//            ContextMenuStrip = new Forms.ContextMenuStrip();
//            ContextMenuStrip.Items.Add("add to map", null, AddTableToLayers);
//        }

//        protected readonly SqlTableSource DataSource;

//        protected void OnShowTableData(object sender, EventArgs e)
//        {
//            IDockPanel panel = View.Application.Form.DockManager[DataSource.ToString()];

//            if (panel == null)
//            {
//                panel = View.Application.Form.DockManager.AddPanel(DataSource.ToString(), DockStyle.Bottom);
                
//                panel.AllowClose = true;
//                panel.Text = DataSource.ToString(false);
//                panel.Control = new GridViewEdit
//                {
//                    Dock = DockStyle.Fill,
//                    DataSource = DataSource.Command.ExecuteReader()
//                };
//            }

//            panel.Show();
//        }

//        protected void AddTableToLayers(object sender, EventArgs e)
//        {
//            MapWorkspace ws = Builder.Application.ActiveProject.Workspace;
//            ws.Layers.Add(ws.NewLayer(DataSource));
//            TreeView.Refresh();
//        }

//        protected override IEnumerable<TreeNode> OnCreateChildNodes(NodeExpandingEventArgs e)
//        {
//            using (DataCommand command = DataSource.Command)
//            {
//                foreach (SqlClauseField field in command.Clause.Fields)
//                {
//                    yield return new DataTableFieldNode(Builder, field);

//                    if (e.Invalidate())
//                    {
//                        continue;
//                    }

//                    break;
//                }
//            }
//        }
//    }
//}
