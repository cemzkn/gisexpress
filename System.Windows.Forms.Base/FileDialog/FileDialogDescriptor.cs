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
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;

namespace System.Windows.Forms
{
    public class FileDialogDescriptor : ICustomTypeDescriptor
    {
        static FileDialogDescriptor()
        {
            Desktop = new FileDialogItem("Desktop", "Favorites", Environment.GetFolderPath(Environment.SpecialFolder.Desktop), Images032.Desktop);
            UserFolder = new FileDialogItem(Environment.UserName, "Favorites", Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), Images032.UserFolder);
        }

        public static readonly PropertyDescriptor Desktop;
        public static readonly PropertyDescriptor UserFolder;

        public string GetClassName()
        {
            return string.Empty;
        }

        public string GetComponentName()
        {
            throw new NotImplementedException();
        }

        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }

        public TypeConverter GetConverter()
        {
            return default(TypeConverter);
        }

        public AttributeCollection GetAttributes()
        {
            return default(AttributeCollection);
        }

        public EventDescriptor GetDefaultEvent()
        {
            return default(EventDescriptor);
        }

        public PropertyDescriptor GetDefaultProperty()
        {
            return default(PropertyDescriptor);
        }

        public object GetEditor(Type editorBaseType)
        {
            return default(object);
        }

        public EventDescriptorCollection GetEvents()
        {
            return GetEvents(default(Attribute[]));
        }

        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return default(EventDescriptorCollection);
        }

        public PropertyDescriptorCollection GetProperties()
        {
            return GetProperties(default(Attribute[]));
        }

        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            return new PropertyDescriptorCollection(EnumerateProperties().ToArray());
        }

        protected virtual IEnumerable<PropertyDescriptor> EnumerateProperties()
        {
            yield return Desktop;
            yield return UserFolder;

            foreach (FileDialogItem item in GetDrives(DriveType.Fixed, "Computer", Images032.DriveFixed))
            {
                yield return item;
            }

            foreach (FileDialogItem item in GetDrives(DriveType.Removable, "RemovableStorageDevice", Images032.DriveRemovable))
            {
                yield return item;
            }

            foreach (FileDialogItem item in GetDrives(DriveType.CDRom, "OpticalDiscDevice", Images032.DriveCDRom))
            {
                yield return item;
            }
        }

        protected IEnumerable<FileDialogItem> GetDrives(DriveType driveType, string category, Image img)
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (DeveloperEnvironment.ReleaseMode && !drive.IsReady)
                {
                    continue;
                }

                if (drive.DriveType == driveType)
                {
                    string name = drive.Name.Split(Path.DirectorySeparatorChar).RemoveNullOrEmptyElements().LastOrDefault();

                    if (string.IsNullOrEmpty(name))
                    {
                        name = Enums.GetDisplayName(driveType);
                    }
                    else
                    {
                        name = name.TrimEnd(Path.DirectorySeparatorChar).TrimEnd(Path.VolumeSeparatorChar);
                    }

                    if (drive.IsReady)
                    {
                        string label = drive.VolumeLabel.Split(Path.DirectorySeparatorChar).RemoveNullOrEmptyElements().LastOrDefault();

                        if (!string.IsNullOrEmpty(label) && !label.EqualsIgnoreCase(name))
                        {
                            name = string.Concat(label, " ( ", name, " )");
                        }
                    }

                    yield return new FileDialogItem(name, category, drive.Name, img);
                }
            }
        }
    }
}