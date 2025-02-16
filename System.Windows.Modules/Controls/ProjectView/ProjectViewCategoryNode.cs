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

//using System.Drawing;
//using System.Drawing.Drawing2D;
//using System.Linq;
//using System.Windows.Forms;
//using System.Workspace;

//namespace System.Windows.Modules.Controls
//{
//    [Serializable]
//    public class ProjectViewCategoryNode : ProjectViewNode
//    {
//        public ProjectViewCategoryNode(IProjectView view, MapCategory category)
//            : base(view, category)
//        {
//            Name = category.Id.ToString();

//            if (category.Parent.IsNull())
//            {
//                ImageKey = SelectedImageKey = "Layers";
//                Localization.Register(View, e => Text = e("Layers"));
//            }
//            else
//            {
//                ImageKey = "FolderClosed";
//                SelectedImageKey = "FolderOpen";
//                Text = category.Name;
//            }
//        }

//        protected Rectangle CheckEditBounds;

//        public MapCategory Category
//        {
//            get { return Tag as MapCategory; }
//        }

//        public override bool AllowDrop
//        {
//            get { return !OSEnvironment.IsMac; }
//        }

//        public override int PaddingRight
//        {
//            get { return 40; }
//        }

//        public void Add(MapLayer layer)
//        {
//            string key = layer.Id.ToString();

//            if (!Nodes.ContainsKey(key))
//            {
//                TreeNode n;

//                if (Category == null)
//                {
//                    Nodes.Add(n = new ProjectViewLayerNode(View, layer));
//                }
//                else
//                {
//                    Nodes.Insert(Category.IndexOf(layer), n = new ProjectViewLayerNode(View, layer));
//                }

//                if (TreeView.SelectedNode == null || "Dropped".Equals(layer.UserData))
//                {
//                    layer.UserData = null;
//                    n.EnsureVisible();
//                }
//            }

//            if (!IsExpanded)
//            {
//                Expand();
//            }
//        }

//        public void Remove(MapLayer layer)
//        {
//            TreeNode n = TreeView.Nodes.Find(layer.Id.ToString(), true).FirstOrDefault();

//            if (n.HasValue())
//            {
//                n.Remove();
//            }
//        }

//        protected void OnEnabledChangedClick(object sender, EventArgs e)
//        {
//            Category.Open = !Category.Open;
//            TreeView.Refresh();
//            View.Application.ActiveProject.Render();
//        }

//        protected override void OnMouseDown(object sender, MouseEventArgs e)
//        {
//            if (TreeView.HasValue())
//            {
//                if (CheckEditBounds.Contains(e.X, e.Y))
//                {
//                    OnEnabledChangedClick(sender, e);
//                }
//            }
//        }

//        protected override void OnKeyDown(object sender, KeyEventArgs e)
//        {
//            if (TreeView.HasValue() && TreeView.SelectedNode == this)
//            {
//                if (e.KeyCode == Keys.Space)
//                {
//                    OnEnabledChangedClick(sender, e);
//                }
//            }
//        }

//        protected internal override void OnDragEnter(DragEventArgs e, ProjectViewNode source)
//        {
//            if (source is ProjectViewLayerNode || source is ProjectViewCategoryNode)
//            {
//                e.Effect = DragDropEffects.Move;
//            }
//        }

//        protected internal override void OnDragOver(DragEventArgs e, ProjectViewNode source)
//        {
//            if (TreeView.HasValue())
//            {
//                var n = source as ProjectViewLayerNode;
//                var c = source as ProjectViewCategoryNode;

//                if ((c.HasValue() && Category != c.Category.Parent && !c.Category.IsChild(Category)) || (n.HasValue() && n.Layer.CanRemove && n.Layer.Category != Category))
//                {
//                    using (Graphics g = TreeView.CreateGraphics())
//                    {
//                        g.SetHighQuality();

//                        using (var pen = new Pen(SystemColors.ControlDarkDark, 3))
//                        {
//                            using (var path = new GraphicsPath())
//                            {
//                                int x = Bounds.Right + 32;
//                                int y = (Bounds.Top + Bounds.Bottom) / 2;

//                                path.Reset();
//                                path.StartFigure();
//                                path.AddLine(x, y - 3, x, y + 3);
//                                path.AddLine(x, y + 3, x - 4, y);
//                                path.AddLine(x - 4, y, x, y - 3);
//                                path.CloseFigure();

//                                g.TryDrawPath(pen, path);
//                            }
//                        }
//                    }

//                    e.Effect = DragDropEffects.Move;
//                }
//                else
//                {
//                    e.Effect = DragDropEffects.None;
//                }
//            }
//        }

//        protected internal override void OnDragDrop(DragEventArgs e, ProjectViewNode source)
//        {
//            var n = source as ProjectViewLayerNode;
//            var c = source as ProjectViewCategoryNode;

//            if (n.HasValue() && n.Layer.Category != Category)
//            {
//                Category.Layers.Add(n.Layer);
//            }
//            else if (c.HasValue())
//            {
//                if (c.Category.Remove())
//                {
//                    Category.Add(c.Category);
//                }
//            }
//        }

//        protected override void OnDrawNode(object sender, DrawTreeNodeEventArgs e)
//        {
//            if (e.Node == this)
//            {
//                CheckEditBounds.X = View.WorkingArea.Right - 24;
//                CheckEditBounds.Y = e.Bounds.Y + (e.Bounds.Height - 16) / 2;
//                CheckEditBounds.Width = 16;
//                CheckEditBounds.Height = 16;

//                if (Category == null)
//                {
//                    return;
//                }

//                e.Graphics.DrawCheckBox(CheckEditBounds, true, Category.Open);
//            }
//        }
//    }
//}
