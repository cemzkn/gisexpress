namespace System.Windows.Modules.Data.Controls
{
    partial class AddConnectionDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        void InitializeComponent()
        {
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ProviderList = new System.Windows.Forms.ListView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ConnectionNameLabel = new System.Windows.Forms.Label();
            this.ConnectionText = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            // 
            // 
            // 
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 1;
            this.ribbon.Name = "ribbon";
            this.ribbon.Size = new System.Drawing.Size(460, 32);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ProviderList);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 60);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(8);
            this.panel1.Size = new System.Drawing.Size(460, 224);
            this.panel1.TabIndex = 3;
            // 
            // ProviderList
            // 
            this.ProviderList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProviderList.Location = new System.Drawing.Point(8, 8);
            this.ProviderList.Name = "ProviderList";
            this.ProviderList.Size = new System.Drawing.Size(444, 208);
            this.ProviderList.TabIndex = 4;
            this.ProviderList.UseCompatibleStateImageBehavior = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ConnectionText);
            this.panel2.Controls.Add(this.ConnectionNameLabel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 284);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(460, 36);
            this.panel2.TabIndex = 6;
            // 
            // ConnectionNameLabel
            // 
            this.ConnectionNameLabel.Location = new System.Drawing.Point(12, 6);
            this.ConnectionNameLabel.Name = "ConnectionNameLabel";
            this.ConnectionNameLabel.Size = new System.Drawing.Size(66, 23);
            this.ConnectionNameLabel.TabIndex = 0;
            this.ConnectionNameLabel.Text = "Name :";
            this.ConnectionNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ConnectionName
            // 
            this.ConnectionText.Location = new System.Drawing.Point(84, 8);
            this.ConnectionText.Name = "ConnectionName";
            this.ConnectionText.Size = new System.Drawing.Size(368, 21);
            this.ConnectionText.TabIndex = 1;
            this.ConnectionText.Text = "Connection1";
            // 
            // AddConnectionDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 368);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "AddConnectionDialog";
            this.Ribbon = null;
            this.ShowFooter = true;
            this.ShowHeader = true;
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Forms.Panel panel1;
        private Forms.ListView ProviderList;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private Forms.Panel panel2;
        private Forms.TextBox ConnectionText;
        private Forms.Label ConnectionNameLabel;
    }
}