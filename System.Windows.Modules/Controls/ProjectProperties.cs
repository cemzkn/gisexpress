using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.Workspace;

namespace System.Windows.Modules.Controls
{
    class ProjectProperties : PropertyGridEdit, IPropertyGrid
    {
        public ProjectProperties(IProject project)
        {
            Project = project;
            BorderStyle = 0;

            View.CheckBox = true;
            View.AllowDrop = true;
            View.AllowSelection = true;
            View.AllowLabelEdit = true;
            View.CellBorderStyle = Border3DSide.Bottom;
            View.ContextMenu = new PopupMenuEdit();
            View.CategoryChanged = OnCategoryChanged;
            View.PropertyIndexChanged = OnPropertyIndexChanged;
            View.SelectedIndexChanged += OnSelectedLayerChanged;

            View.Columns[0].SizeValue = 100;
            View.Columns[1].SizeType = SizeType.Absolute;
            View.Columns[1].SizeValue = 100;

            Project.Loaded += OnProjectLoad;
            Project.Workspace.Properties.PropertyValueChanged += e => View.Refresh();
        }

        readonly IProject Project;

        public bool IsEmpty()
        {
            throw new NotImplementedException();
        }

        public PropertyDescriptor SelectedProperty
        {
            get { throw new NotImplementedException(); }
        }

        void OnProjectLoad(ProjectEventArgs e)
        {
            e.Project.View.Refresh(true);
        }

        void OnCategoryChanged(GridCategoryItem e)
        {
            var component = SelectedItem.GetValue() as MapLayer;

            if (component == null)
            {
                return;
            }

            component.Parent.Visibility = e.Checked;
            component.Workspace.Render();
        }

        void OnSelectedLayerChanged(object sender, EventArgs e)
        {
            var component = SelectedItem.GetValue() as MapLayer;

            if (component == null)
            {
                return;
            }

            var menu = View.ContextMenu as PopupMenuEdit;
            ISupportComponentActions support = SelectedItem.IsExpandable ? component.Parent : component;

            menu.MenuItems.Clear();

            if (support == null)
            {
                return;
            }

            var item = default(PopupMenuEdit.Item);

            foreach (IComponentAction action in support.GetActions())
            {
                if (action.BeginGroup)
                {
                    menu.AddSeperator();
                }

                item = menu.Add(Localization.Localize(action.Name), OnActionPerform, action.Image);
                item.Name = action.Name;
                item.Tag = action;
                item.Enabled = action.CanPerform == null || action.CanPerform(sender);
                item.Shortcut = action.Shortcut;
            }
        }

        void OnPropertyIndexChanged(GridPropertyItem item, int index)
        {
            Project.Workspace.Insert((MapLayer)item.GetValue(), index);
        }

        void OnActionPerform(object sender, EventArgs e)
        {
            var item = sender as PopupMenuEdit.Item;
            var action = item.Tag as IComponentAction;

            action.Perform(this);
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            SelectedObject = Project.Workspace.Properties;
            Refresh(true);

            base.OnVisibleChanged(e);
        }
    }
}
