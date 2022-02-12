using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace System.Windows.Modules.Data.Controls
{
    public partial class AddConnectionDialog : ApplicationForm
    {
        public AddConnectionDialog()
        {
            InitializeComponent();
        }

        protected DataConnectionCollection Connections;

        public string ConnectionName
        {
            get { return ConnectionText.Text; }
        }

        public DataProvider SelectedProvider
        {
            get;
            set;
        }

        public void Init(DataConnectionCollection connections)
        {
            ProviderList.SmallImageList = new ImageList();
            ProviderList.SmallImageList.ImageSize = new Size(8, 20);
            ProviderList.View = View.Details;
            ProviderList.MultiSelect = false;
            ProviderList.FullRowSelect = true;
            ProviderList.HeaderStyle = ColumnHeaderStyle.None;
            ProviderList.Columns.Add(string.Empty, 400);
            ProviderList.DoubleClick += OnProviderDoubleClick;
            ProviderList.SelectedIndexChanged += OnSelectedProviderIndexChanged;

            foreach (DataProvider provider in DataProviders.GetProviders())
            {
                if (!string.IsNullOrEmpty(provider.GetDisplayName()))
                {
                    ProviderList.Items.Add(provider.GetDisplayName()).Tag = provider;
                }
            }

            Connections = connections;
            ConnectionText.Text = Connections.GetName(Localization.Localize("DataConnection"));
            ConnectionText.TextChanged += OnConnectionTextChanged;

            ValidateButtons();
        }

        void OnConnectionTextChanged(object sender, EventArgs e)
        {
            ValidateButtons();
        }

        void OnProviderDoubleClick(object sender, EventArgs e)
        {
            Footer.Buttons["OK"].PerformClick();
        }

        void OnSelectedProviderIndexChanged(object sender, EventArgs e)
        {
            var item = ProviderList.SelectedItems.Cast<ListViewItem>().FirstOrDefault();

            if (item != null)
            {
                SelectedProvider = item.Tag as DataProvider;
                ValidateButtons();
            }
        }

        void ValidateButtons()
        {
            Footer.Buttons["OK"].Enabled = SelectedProvider != null && !string.IsNullOrEmpty(ConnectionText.Text);
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            ConnectionText.Focus();
            ConnectionText.SelectAll();
            base.OnVisibleChanged(e);
        }
    }
}
