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
using System.Drawing;
using System.Geometries;
using System.IO;
using System.Windows.Forms;
using System.Windows.Modules.Controls;
using System.Workspace;

namespace System.Windows.Modules
{
    public interface IProject : ISupportInitialize, IKeyedObject, ISupportTransactionLog, IDisposable
    {
        event Action Cancel;
        event ProjectEventHandler Saved;
        event ProjectEventHandler Loaded;
        event ProjectEventHandler Changed;
        event ProjectEventHandler Disposed;
        event KeyEventHandler KeyDown;
        event KeyEventHandler KeyUp;
        event EventHandler CursorEnter;
        event MapEventHandler CursorButtonUp;
        event MapEventHandler CursorButtonDown;
        event MapEventHandler CursorLocationChanged;
        event MapEventHandler ScaleChanged;
        event FeatureEventHandler FeatureSelect;
        event ApplicationComponentEventHandler ActiveComponentChanged;

        IProjectFileInfo FileInfo
        {
            get;
        }

        IPropertyGrid View
        {
            get;
        }

        IApplication Application
        {
            get;
        }

        MapWorkspace Workspace
        {
            get;
        }

        ISnapObjectCollection SnapObjects
        {
            get;
        }

        IPropertyCollection Properties
        {
            get;
        }

        IUnitConverter UnitConverter
        {
            get;
        }

        IApplicationComponentDesigner Designer
        {
            get;
        }

        IApplicationFile File
        {
            get;
            set;
        }

        IGeometryFactory Factory
        {
            get;
        }

        bool IsActive { get; }

        bool IsModified { get; }

        void Activate();

        //bool Normalize();

        bool ZoomToExtent();

        bool Zoom(int percent);

        bool ZoomTo(IGeometry g);

        bool ZoomTo(IEnvelope bounds);

        bool ZoomTo(RectangleF rect);

        void Render();

        void Load(string fileName);

        bool Save();

        void Save(string fileName);

        void Save(Stream output);

        bool SaveAs();

        void SetModified();

        bool Close();
    }
}
