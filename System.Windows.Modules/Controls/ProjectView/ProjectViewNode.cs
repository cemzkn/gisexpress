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
//using System.Drawing;
//using System.Linq;
//using System.Windows.Forms;
//using System.Workspace;

//namespace System.Windows.Modules.Controls
//{
//    public abstract partial class ProjectViewNode : TreeNode, ISupportNodeExpanding
//    {
//        protected ProjectViewNode(IProjectView view, object value)
//        {
//            View = view;
//            Tag = value;

//            GetType().AttachOverriddenEvents<IProjectView>(this, view).ToList();
//        }

//        public readonly IProjectView View;

//        protected Point DragLocation;
//        protected ProjectViewNode DragSource;
//        protected ProjectViewNode DragTarget;

//        public virtual bool AllowDrop
//        {
//            get { return false; }
//        }

//        public void Clear()
//        {
//            if (TreeView.IsDisposed)
//            {
//                return;
//            }

//            Nodes.Clear();
//        }

//        public virtual int PaddingRight
//        {
//            get { return 0; }
//        }

//        public ProjectViewCategoryNode AddCategory(MapCategory category)
//        {
//            ProjectViewCategoryNode n = FindNode(category);

//            if (n == null)
//            {
//                Nodes.Add(n = new ProjectViewCategoryNode(View, category));
//            }

//            foreach (MapLayer layer in category.Layers)
//            {
//                n.Add(layer);
//            }

//            foreach (MapCategory item in category)
//            {
//                ProjectViewCategoryNode old = FindNode(item);

//                if (old.HasValue())
//                {
//                    old.Remove();
//                }

//                n.AddCategory(item);
//            }

//            return n;
//        }

//        public ProjectViewCategoryNode FindNode(MapCategory category)
//        {
//            return TreeView.Nodes.Find(category.Id.ToString(), true).FirstOrDefault() as ProjectViewCategoryNode;
//        }

//        protected virtual void OnKeyDown(object sender, KeyEventArgs e)
//        {
//        }

//        protected virtual void OnMouseDown(object sender, MouseEventArgs e)
//        {
//        }

//        protected virtual void OnMouseMove(object sender, MouseEventArgs e)
//        {
//        }

//        protected virtual void OnDrawNode(object sender, DrawTreeNodeEventArgs e)
//        {
//        }

//        protected internal virtual void OnItemDrag(ItemDragEventArgs e)
//        {
//            TreeView.DoDragDrop(e.Item, DragDropEffects.Move);
//        }

//        protected internal virtual void OnDragEnter(DragEventArgs e, ProjectViewNode source)
//        {
//            e.Effect = DragDropEffects.None;
//        }

//        protected internal virtual void OnDragOver(DragEventArgs e, ProjectViewNode source)
//        {
//            e.Effect = DragDropEffects.None;
//        }

//        protected internal virtual void OnDragDrop(DragEventArgs e, ProjectViewNode source)
//        {
//        }

//        IEnumerable<TreeNode> ISupportNodeExpanding.CreateChildNodes(NodeExpandingEventArgs e)
//        {
//            return OnCreateChildNodes(e);
//        }

//        protected virtual IEnumerable<TreeNode> OnCreateChildNodes(NodeExpandingEventArgs e)
//        {
//            yield break;
//        }

//        public override string ToString()
//        {
//            return Text;
//        }
//    }
//}
