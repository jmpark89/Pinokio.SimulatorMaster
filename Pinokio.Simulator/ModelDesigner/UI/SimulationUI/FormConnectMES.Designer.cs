
namespace Pinokio.Designer
{
    partial class FormConnectMES
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
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConnectMES));
            this.wizardControl1 = new DevExpress.XtraWizard.WizardControl();
            this.welcomeWizardPage1 = new DevExpress.XtraWizard.WelcomeWizardPage();
            this.tePort = new DevExpress.XtraEditors.TextEdit();
            this.tePassword = new DevExpress.XtraEditors.TextEdit();
            this.teDBName = new DevExpress.XtraEditors.TextEdit();
            this.teUserName = new DevExpress.XtraEditors.TextEdit();
            this.teIPAddress = new DevExpress.XtraEditors.TextEdit();
            this.peError = new DevExpress.XtraEditors.PictureEdit();
            this.peCheck = new DevExpress.XtraEditors.PictureEdit();
            this.btnTestConnection = new DevExpress.XtraEditors.SimpleButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.completionWizardPage1 = new DevExpress.XtraWizard.CompletionWizardPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.teTableName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).BeginInit();
            this.wizardControl1.SuspendLayout();
            this.welcomeWizardPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tePort.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tePassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teDBName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teUserName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teIPAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.peError.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.peCheck.Properties)).BeginInit();
            this.completionWizardPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.teTableName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // wizardControl1
            // 
            this.wizardControl1.Controls.Add(this.welcomeWizardPage1);
            this.wizardControl1.Controls.Add(this.completionWizardPage1);
            this.wizardControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardControl1.ImageWidth = 216;
            this.wizardControl1.MinimumSize = new System.Drawing.Size(117, 108);
            this.wizardControl1.Name = "wizardControl1";
            this.wizardControl1.Pages.AddRange(new DevExpress.XtraWizard.BaseWizardPage[] {
            this.welcomeWizardPage1,
            this.completionWizardPage1});
            this.wizardControl1.Size = new System.Drawing.Size(790, 465);
            this.wizardControl1.Text = "MES Connection";
            this.wizardControl1.WizardStyle = DevExpress.XtraWizard.WizardStyle.WizardAero;
            // 
            // welcomeWizardPage1
            // 
            this.welcomeWizardPage1.AllowNext = false;
            this.welcomeWizardPage1.Controls.Add(this.teTableName);
            this.welcomeWizardPage1.Controls.Add(this.labelControl6);
            this.welcomeWizardPage1.Controls.Add(this.tePort);
            this.welcomeWizardPage1.Controls.Add(this.tePassword);
            this.welcomeWizardPage1.Controls.Add(this.teDBName);
            this.welcomeWizardPage1.Controls.Add(this.teUserName);
            this.welcomeWizardPage1.Controls.Add(this.teIPAddress);
            this.welcomeWizardPage1.Controls.Add(this.peError);
            this.welcomeWizardPage1.Controls.Add(this.peCheck);
            this.welcomeWizardPage1.Controls.Add(this.btnTestConnection);
            this.welcomeWizardPage1.Controls.Add(this.panel1);
            this.welcomeWizardPage1.Controls.Add(this.labelControl5);
            this.welcomeWizardPage1.Controls.Add(this.labelControl4);
            this.welcomeWizardPage1.Controls.Add(this.labelControl3);
            this.welcomeWizardPage1.Controls.Add(this.labelControl2);
            this.welcomeWizardPage1.Controls.Add(this.labelControl1);
            this.welcomeWizardPage1.IntroductionText = "Build a context of MES Database(MySQL, Oracle, Sql, etc) configuration settings";
            this.welcomeWizardPage1.Name = "welcomeWizardPage1";
            this.welcomeWizardPage1.Size = new System.Drawing.Size(730, 298);
            this.welcomeWizardPage1.Text = "Build a context of MES Database(MySQL, Oracle, Sql, etc) configuration settings";
            // 
            // tePort
            // 
            this.tePort.EditValue = "3306";
            this.tePort.Location = new System.Drawing.Point(391, 56);
            this.tePort.Name = "tePort";
            this.tePort.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.tePort.Properties.Appearance.Options.UseFont = true;
            this.tePort.Properties.Mask.EditMask = "0000";
            this.tePort.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
            this.tePort.Size = new System.Drawing.Size(92, 24);
            this.tePort.TabIndex = 2;
            // 
            // tePassword
            // 
            this.tePassword.EditValue = "0125";
            this.tePassword.Location = new System.Drawing.Point(391, 211);
            this.tePassword.Name = "tePassword";
            this.tePassword.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.tePassword.Properties.Appearance.Options.UseFont = true;
            this.tePassword.Properties.PasswordChar = '*';
            this.tePassword.Size = new System.Drawing.Size(175, 24);
            this.tePassword.TabIndex = 6;
            // 
            // teDBName
            // 
            this.teDBName.EditValue = "digitaltwin";
            this.teDBName.Location = new System.Drawing.Point(391, 95);
            this.teDBName.Name = "teDBName";
            this.teDBName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.teDBName.Properties.Appearance.Options.UseFont = true;
            this.teDBName.Size = new System.Drawing.Size(175, 24);
            this.teDBName.TabIndex = 3;
            // 
            // teUserName
            // 
            this.teUserName.EditValue = "root";
            this.teUserName.Location = new System.Drawing.Point(391, 168);
            this.teUserName.Name = "teUserName";
            this.teUserName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.teUserName.Properties.Appearance.Options.UseFont = true;
            this.teUserName.Size = new System.Drawing.Size(175, 24);
            this.teUserName.TabIndex = 5;
            // 
            // teIPAddress
            // 
            this.teIPAddress.EditValue = "127.0.0.1";
            this.teIPAddress.Location = new System.Drawing.Point(391, 17);
            this.teIPAddress.Name = "teIPAddress";
            this.teIPAddress.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.teIPAddress.Properties.Appearance.Options.UseFont = true;
            this.teIPAddress.Properties.Mask.EditMask = "([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\\.([0-9]|[1-9][0-9]|1[0-9][0-9" +
    "]|2[0-4][0-9]|25[0-5])){3}";
            this.teIPAddress.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.teIPAddress.Size = new System.Drawing.Size(175, 24);
            this.teIPAddress.TabIndex = 1;
            // 
            // peError
            // 
            this.peError.EditValue = ((object)(resources.GetObject("peError.EditValue")));
            this.peError.Location = new System.Drawing.Point(572, 262);
            this.peError.Name = "peError";
            this.peError.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.peError.Size = new System.Drawing.Size(25, 23);
            this.peError.TabIndex = 7;
            this.peError.Visible = false;
            // 
            // peCheck
            // 
            this.peCheck.EditValue = ((object)(resources.GetObject("peCheck.EditValue")));
            this.peCheck.Location = new System.Drawing.Point(572, 262);
            this.peCheck.Name = "peCheck";
            this.peCheck.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.peCheck.Size = new System.Drawing.Size(25, 23);
            this.peCheck.TabIndex = 6;
            this.peCheck.Visible = false;
            // 
            // btnTestConnection
            // 
            this.btnTestConnection.Location = new System.Drawing.Point(391, 262);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(175, 23);
            this.btnTestConnection.TabIndex = 7;
            this.btnTestConnection.Text = "Test Connection";
            this.btnTestConnection.Click += new System.EventHandler(this.btnTestConnection_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SteelBlue;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(257, 298);
            this.panel1.TabIndex = 4;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(301, 97);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(80, 19);
            this.labelControl5.TabIndex = 2;
            this.labelControl5.Text = "Database : ";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(298, 213);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(83, 19);
            this.labelControl4.TabIndex = 3;
            this.labelControl4.Text = "Password : ";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(289, 170);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(92, 19);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "User name : ";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(336, 58);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(45, 19);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Port : ";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(289, 19);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(92, 19);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Host name : ";
            // 
            // completionWizardPage1
            // 
            this.completionWizardPage1.Controls.Add(this.panel2);
            this.completionWizardPage1.FinishText = " ";
            this.completionWizardPage1.Name = "completionWizardPage1";
            this.completionWizardPage1.Size = new System.Drawing.Size(730, 298);
            this.completionWizardPage1.Text = "Completing the MES connection";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.SteelBlue;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(257, 298);
            this.panel2.TabIndex = 5;
            // 
            // teTableName
            // 
            this.teTableName.EditValue = "sensor_data,stocker_data";
            this.teTableName.Location = new System.Drawing.Point(391, 129);
            this.teTableName.Name = "teTableName";
            this.teTableName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.teTableName.Properties.Appearance.Options.UseFont = true;
            this.teTableName.Size = new System.Drawing.Size(175, 24);
            this.teTableName.TabIndex = 4;
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Location = new System.Drawing.Point(326, 131);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(55, 19);
            this.labelControl6.TabIndex = 8;
            this.labelControl6.Text = "Table : ";
            // 
            // FormConnectMES
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 465);
            this.Controls.Add(this.wizardControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormConnectMES";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MES 연동 설정";
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).EndInit();
            this.wizardControl1.ResumeLayout(false);
            this.welcomeWizardPage1.ResumeLayout(false);
            this.welcomeWizardPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tePort.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tePassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teDBName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teUserName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teIPAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.peError.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.peCheck.Properties)).EndInit();
            this.completionWizardPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.teTableName.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraWizard.WizardControl wizardControl1;
        private DevExpress.XtraWizard.WelcomeWizardPage welcomeWizardPage1;
        private DevExpress.XtraWizard.CompletionWizardPage completionWizardPage1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton btnTestConnection;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraEditors.PictureEdit peError;
        private DevExpress.XtraEditors.PictureEdit peCheck;
        private DevExpress.XtraEditors.TextEdit tePort;
        private DevExpress.XtraEditors.TextEdit tePassword;
        private DevExpress.XtraEditors.TextEdit teUserName;
        private DevExpress.XtraEditors.TextEdit teIPAddress;
        private DevExpress.XtraEditors.TextEdit teDBName;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit teTableName;
        private DevExpress.XtraEditors.LabelControl labelControl6;
    }
}