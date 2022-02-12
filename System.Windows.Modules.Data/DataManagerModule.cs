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
using System.ComponentModel.Design;
using System.Resources;
using System.Runtime.InteropServices;
using System.Windows.Modules.Controls;
using System.Windows.Modules.Data.Controls;
using System.Workspace;

namespace System.Windows.Modules.Data
{
    [Guid("88568E3A-99B8-4E90-90EE-A7498B2E33A2")]
    internal partial class DataManagerModule : ApplicationModule
    {
        public DataManagerModule(IApplication application)
            : base(application)
        {
            if (LicenseManager.IsLicensed(GetType()))
            {
                ViewBuilder = new DataTreeViewBuilder(application);
            }
        }

        protected DataTreeViewBuilder ViewBuilder;

        protected override void OnLayerCollectionChanged(CollectionChangeAction action, MapLayer layer)
        {
            if (LicenseManager.IsLicensed(GetType()))
            {
                if (action == CollectionChangeAction.Add && layer.FeatureSupport)
                {
                    //layer.Actions.Add("MapDataLayer.ShowData", Images016.Grid, PerformShowData, CanPerformShowData).BeginGroup = true;
                    //layer.Actions.Add("MapDataLayer.EditAttributes", Images016.EditProperty, PerformEditAttributes, CanPerformEditAttributes);
                }
            }
        }

        protected override void OnActiveComponentChanged(ApplicationComponentEventArgs e)
        {
            IPropertiesDockPanel properties = Application.Form.DockManager.PropertiesPanel;

            if (e.Component.HasValue() && properties.HasValue())
            {
                FeatureItem item;
                IPropertyGrid propertyGrid = properties.Control;

                if (propertyGrid.HasValue() && (item = e.Component.TypeDescriptor as FeatureItem).HasValue())
                {
                    e.Handled = true;
                    propertyGrid.SelectedObject = item;

                    if (propertyGrid.IsEmpty() == false)
                    {
                        properties.Activate();
                    }
                }
            }
        }

        protected void CanPerformEditAttributes(object sender)
        {
        }

        protected void PerformEditAttributes(object sender)
        {
            //using (var dialog = new DataSourceEditDialog())
            {
                //if (e.Bounds.IsEmpty)
                //{
                //    dialog.StartPosition = FormStartPosition.CenterParent;
                //}
                //else
                //{
                //    dialog.StartPosition = FormStartPosition.Manual;
                //    dialog.Location = new Point(e.Bounds.Left, e.Bounds.Top + e.Bounds.Height);
                //}

                //dialog.TableSource = (e.Component as MapLayer).Command.Clause.TableSources.First;
                //dialog.ShowDialog(Application.Form);
            }
        }

        protected bool CanPerformShowData(object sender)
        {
            //var item = sender as MapLayer;

            //if (item.HasValue())
            //{
            //    e.Cancel = item.Properties.IsNull() || item.Properties.Count(c => c.IsBrowsable) < 1;
            //}

            return false;
        }

        protected void PerformShowData(object sender)
        {
            //var item = e.Component as MapLayer;

            //if (item.HasValue())
            //{
            //    IDockPanel panel = Application.Form.DockManager[item.Id.ToString()];

            //    if (panel.IsNull())
            //    {
            //        item.Disposed += (s, e2) =>
            //        {
            //            panel.Close();
            //            panel.Dispose();
            //        };

            //        panel = Application.Form.DockManager.AddPanel(item.Id.ToString(), DockStyle.Bottom);

            //        panel.AllowClose = true;
            //        panel.Text = item.Name;
            //        panel.Padding = new Padding(1);

            //        panel.Control = new GridViewEdit
            //        {
            //            AutoFit = true,
            //            Dock = DockStyle.Fill,
            //            BorderStyle = default(Border3DSide),
            //            DataSource = item.ExecuteReader()
            //        };
            //    }

            //    panel.Activate();
            //}
        }

        protected override void OnDispose()
        {
            ViewBuilder.DisposeSafely();
            ViewBuilder = null;
        }
    }
}
