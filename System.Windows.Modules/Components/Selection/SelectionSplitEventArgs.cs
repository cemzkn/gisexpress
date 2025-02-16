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

using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Geometries;
using System.Workspace;

namespace System.Windows.Modules
{
    partial class FeatureSelectionComponent
    {
        protected class SelectionSplitEventArgs : SelectionDoWorkEventArgs
        {
            public SelectionSplitEventArgs(FeatureSelectionComponent component)
                : base(component, ComponentEditCompleteAction.Cancel)
            {
            }

            public override bool AlwaysTransactional
            {
                get { return true; }
            }

            protected override void OnInitialize()
            {
            }

            protected override bool OnDoWork(MapFeature item)
            {
                IEnumerable<ILineSegment> segments = item.Geometry.GetSegments();

                if (segments.HasValue())
                {
                    if (!Copy)
                    {
                        item.Feature.BeginEdit();
                        item.Feature.Delete();
                        item.Feature.EndEdit();
                    }

                    foreach (ILineSegment segment in segments)
                    {
                        item.Feature.SetFeatureId(default);
                        item.Feature.SetGeometry(item.Geometry.Factory.Create<ILineString>(segment.P0, segment.P1));
                        item.Feature.SetAdded();
                        item.Feature.EndEdit();
                    }
                }

                return true;
            }
        }
    }
}