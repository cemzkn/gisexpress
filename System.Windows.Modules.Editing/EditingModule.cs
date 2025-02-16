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
using System.Data;
using System.Geometries;
using System.Resources;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.Ribbon;

namespace System.Windows.Modules.Editing
{
    [Guid("EE2D572B-4D42-4F9E-B096-3C11A388C37E")]
    internal class EditingModule : ApplicationModule
    {
        public EditingModule(IApplication application)
            : base(application)
        {
        }

        const string CommandAddPoint = "Editing.AddPoint";
        const string CommandAddLine = "Editing.AddLine";
        const string CommandAddPolygon = "Editing.AddPolygon";
        const string CommandAddRectangle = "Editing.AddRectangle";
        const string CommandAddCircle = "Editing.AddCircle";
        const string CommandAddEllipse = "Editing.AddEllipse";
        const string CommandAddArc = "Editing.AddArc";
        const string CommandAddGeometry = "Editing.AddGeometry";

        protected override void OnEnabledChanged()
        {
            IApplicationCommandManager commandManager = Application.Form.CommandManager;
            RibbonPageGroup groupEditing = commandManager.Pages[1].Groups["Editing"];

            if (Enabled || groupEditing.IsNull())
            {
                commandManager.CreateCommand(CommandAddPoint, RibbonCommandVisibility.Toolbar, Keys.O, null, Images032.Point, (argument) => CreateFeature<IPoint>(), false, true);
                commandManager.CreateCommand(CommandAddLine, RibbonCommandVisibility.Toolbar, Keys.L, null, Images032.Polyline, (argument) => CreateFeature<ILineString>(), false);
                commandManager.CreateCommand(CommandAddPolygon, RibbonCommandVisibility.Toolbar, Keys.P, null, Images032.Polygon, (argument) => CreateFeature<IPolygon>(), false);

                commandManager.CreateCommand(CommandAddRectangle, RibbonCommandVisibility.Toolbar, Keys.R, null, Images032.Rectangle, (argument) => CreateFeature<IRectangle>(), false, true);
                commandManager.CreateCommand(CommandAddCircle, RibbonCommandVisibility.Toolbar, Keys.C, null, Images032.Circle, (argument) => CreateFeature<ICircle>(), false, false);
                commandManager.CreateCommand(CommandAddEllipse, RibbonCommandVisibility.Toolbar, Keys.E, null, Images032.Ellipse, (argument) => CreateFeature<IEllipse>(), false, false);
                commandManager.CreateCommand(CommandAddArc, RibbonCommandVisibility.Toolbar, Keys.A, null, Images032.Arc, (argument) => CreateFeature<IArc>(), false);

                if (DeveloperEnvironment.DebugMode)
                {
                    commandManager.CreateCommand(CommandAddGeometry, RibbonCommandVisibility.Toolbar, null, Images032.AddGeometry, (argument) => AddGeometry(), false, true);
                }

                SetCommandsEnabled(Application.ActiveProject.HasValue() && Application.ActiveProject.Designer.ActiveComponent.IsNull());
            }
            else
            {
                groupEditing.Remove();
            }

            base.OnEnabledChanged();
        }

        protected override void OnLoad(ApplicationLoadEventArgs e)
        {
            OnEnabledChanged();
        }

        protected override void OnActiveProjectChanged(ActiveProjectChangedEventArgs e)
        {
            SetCommandsEnabled(e.Project.HasValue());
            base.OnActiveProjectChanged(e);
        }

        protected override void OnActiveComponentChanged(ApplicationComponentEventArgs e)
        {
            SetCommandsEnabled(e.Component.IsNull());
            base.OnActiveComponentChanged(e);
        }

        void CreateFeature<T>() where T : IGeometry
        {
            if (ActiveLayer.HasValue())
            {
                IFeature feature = ActiveLayer.CreateFeature();

                if (feature.HasValue())
                {
                    IGeometry geometry = ActiveLayer.Workspace.Factory.Create<T>();

                    if (geometry.HasValue())
                    {
                        IApplicationComponent component = geometry.GetComponent(ActiveProject.Designer);

                        feature.BeginEdit();
                        //feature.SetGeometry(geometry);

                        component.TypeDescriptor = new FeatureItem(ActiveProject, ActiveLayer, feature);
                        component.EditCompleted += CreateFeatureCompleted;

                        SetCommandsEnabled(false);
                        ActiveProject.Designer.BeginEdit(component);
                    }
                }
            }
        }

        void CreateFeatureCompleted(ApplicationComponentEditCompletedEventArgs e)
        {
            if (e.Action == ComponentEditCompleteAction.Complete)
            {
                var item = e.Component.TypeDescriptor as FeatureItem;

                if (item.HasValue())
                {
                    item.Feature.SetGeometry((IGeometry)e.Component.Value);
                    item.Feature.EndEdit();
                    e.Component.Designer.Redraw();
                }
            }

            SetCommandsEnabled(true);
        }

        void AddGeometry()
        {
            using (var dialog = new AddGeometryDialog())
            {
                if (dialog.ShowDialog(Application.Form) == DialogResult.OK)
                {
                    //IGeometry g = ActiveProject.Workspace.GeometryFactory.FromWkt(dialog.WellKnownText);

                    //if (g.HasValue())
                    //{
                    //    IFeature feature = ActiveLayer.CreateFeature();

                    //    feature.BeginEdit();
                    //    feature.SetGeometry(g);
                    //    feature.EndEdit();

                    //    ActiveProject.ZoomTo(feature.GetGeometry().GetBounds().Zoom(120));
                    //    ActiveProject.Render();
                    //}
                }
            }
        }

        void SetCommandsEnabled(bool enabled)
        {
            Application.Form.CommandManager[CommandAddPoint].Enabled =
            Application.Form.CommandManager[CommandAddLine].Enabled =
            Application.Form.CommandManager[CommandAddPolygon].Enabled =
            Application.Form.CommandManager[CommandAddRectangle].Enabled =
            Application.Form.CommandManager[CommandAddCircle].Enabled =
            Application.Form.CommandManager[CommandAddEllipse].Enabled =
            Application.Form.CommandManager[CommandAddArc].Enabled = enabled;

            if (DeveloperEnvironment.DebugMode)
            {
                Application.Form.CommandManager[CommandAddGeometry].Enabled = enabled;
            }
        }
    }
}
