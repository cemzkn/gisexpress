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

namespace System.Windows.Forms
{
    public interface IDockPanel : IDisposable
    {
        event CancelEventHandler Closing;
        event EventHandler Closed;
        event EventHandler Disposed;

        DockManager Owner
        {
            get;
        }

        bool IsActive
        {
            get;
        }

        void Activate();

        string Name
        {
            get;
            set;
        }

        string Text
        {
            get;
            set;
        }

        Image Image
        {
            get;
            set;
        }

        bool AllowClose
        {
            get;
            set;
        }

        void Show();
        void Hide();
        void Close();

        int Index
        {
            get;
            set;
        }

        Padding Padding
        {
            get;
            set;
        }

        Size Size
        {
            get;
            set;
        }

        Size MinimumSize
        {
            get;
            set;
        }

        Size MaximumSize
        {
            get;
            set;
        }

        Border3DSide SizableSides
        {
            get;
        }

        Control Control
        {
            get;
            set;
        }
    }
}
