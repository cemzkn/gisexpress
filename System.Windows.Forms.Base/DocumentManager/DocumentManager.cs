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

using System.Collections;
using System.ComponentModel;
using System.Linq;

namespace System.Windows.Forms
{
    public partial class DocumentManager : IEnumerable, IDisposable
    {
        public DocumentManager(BaseForm owner)
        {
            Owner = owner;
        }

        protected DocumentView OwnerEdit;

        public BaseForm Owner
        {
            get;
            protected set;
        }

        protected DocumentView View
        {
            get { return OwnerEdit ?? (OwnerEdit = OnCreateView()); }
        }

        public int Count
        {
            get { return View.TabPages.Count; }
        }

        public Document ActiveDocument
        {
            get { return (Document)View.SelectedTab; }
        }

        public Document AddDocument(Control value)
        {
            return AddDocument(value.Name, value);
        }

        public Document AddDocument(string name, Control value)
        {
            var document = OnCreateDocument(name, value);

            document.DocumentManager = this;

            View.TabPages.Add(document);
            View.BringToFront();

            document.Activate();

            return document;
        }

        protected virtual DocumentView OnCreateView()
        {
            var value = new DocumentView(this);
            var container = Owner.Body.Controls.OfType<SplitEdit>().FirstOrDefault();

            if (container.HasValue())
            {
                container.Panel2.Controls.Add(value);
            }
            else
            {
                Owner.Body.Controls.Add(value);
            }

            return value;
        }

        protected virtual Document OnCreateDocument(string name, Control value)
        {
            return new Document(name, value);
        }

        protected virtual void OnDocumentAdded(Document document)
        {
        }

        protected virtual void OnDocumentRemoved(Document document)
        {
        }

        protected virtual void OnDocumentActivated(Document document)
        {
        }

        protected virtual void OnDocumentDeactivated(Document document)
        {
        }

        protected virtual void OnDocumentClosing(Document document, CancelEventArgs e)
        {
            if (!e.Cancel)
            {
                OnDocumentRemoved(document);
                OnDocumentCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, document));
                OnDocumentClosed(document);
            }
        }

        protected virtual void OnDocumentClosed(Document document)
        {
        }

        protected virtual void OnDocumentCollectionChanged(CollectionChangeEventArgs e)
        {
        }

        public IEnumerator GetEnumerator()
        {
            return View.TabPages.Cast<Document>().GetEnumerator();
        }

        protected class DocumentView : TabEdit
        {
            public DocumentView(DocumentManager owner)
            {
                Owner = owner;
                ShowHeader = true;
                Dock = DockStyle.Fill;
                Padding = new Padding(1, 0, 0, 0);
            }

            protected DocumentManager Owner;

            protected override void OnSelected(TabEditEventArgs e)
            {
                base.OnSelected(e);
                Owner.OnDocumentActivated(e.TabPage as Document);
            }

            protected internal override void OnDeselected(TabEditEventArgs e)
            {
                base.OnDeselected(e);
                Owner.OnDocumentDeactivated(e.TabPage as Document);
            }

            protected override void OnSelectedIndexChanged(EventArgs e)
            {
                base.OnSelectedIndexChanged(e);

                if (SelectedIndex >= 0)
                {
                    (SelectedTab as Document).Control.Focus();
                }
            }

            protected override void OnControlAdded(ControlEventArgs e)
            {
                var document = e.Control as Document;

                if (document.HasValue())
                {
                    base.OnControlAdded(e);

                    Owner.OnDocumentAdded(document);
                    Owner.OnDocumentCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, document));
                }
            }

            protected override void Dispose(bool disposing)
            {
                base.Dispose(disposing);

                if (disposing)
                {
                    Owner = null;
                }
            }
        }

        public void Dispose()
        {
            OwnerEdit.DisposeSafely();
            OwnerEdit = null;
            GC.SuppressFinalize(this);
        }
    }
}
