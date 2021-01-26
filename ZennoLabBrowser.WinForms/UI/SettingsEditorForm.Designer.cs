namespace ZennoLabBrowser.WinForms.UI
{
    partial class SettingsEditorForm
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
            this.SaveButton = new DevExpress.XtraEditors.SimpleButton();
            this.CancelButton = new DevExpress.XtraEditors.SimpleButton();
            this.CurrentPropertyGridControl = new DevExpress.XtraVerticalGrid.PropertyGridControl();
            this.RestoreDefaultButton = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.CurrentPropertyGridControl)).BeginInit();
            this.SuspendLayout();
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(12, 555);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(118, 34);
            this.SaveButton.TabIndex = 0;
            this.SaveButton.Text = "Save";
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(294, 555);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(148, 34);
            this.CancelButton.TabIndex = 1;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // CurrentPropertyGridControl
            // 
            this.CurrentPropertyGridControl.Cursor = System.Windows.Forms.Cursors.Default;
            this.CurrentPropertyGridControl.Location = new System.Drawing.Point(-1, 0);
            this.CurrentPropertyGridControl.Name = "CurrentPropertyGridControl";
            this.CurrentPropertyGridControl.Size = new System.Drawing.Size(456, 504);
            this.CurrentPropertyGridControl.TabIndex = 2;
            // 
            // RestoreDefaultButton
            // 
            this.RestoreDefaultButton.Location = new System.Drawing.Point(138, 555);
            this.RestoreDefaultButton.Name = "RestoreDefaultButton";
            this.RestoreDefaultButton.Size = new System.Drawing.Size(150, 34);
            this.RestoreDefaultButton.TabIndex = 3;
            this.RestoreDefaultButton.Text = "Restore default";
            this.RestoreDefaultButton.Click += new System.EventHandler(this.RestoreDefaultButton_Click);
            // 
            // SettingsEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 601);
            this.Controls.Add(this.RestoreDefaultButton);
            this.Controls.Add(this.CurrentPropertyGridControl);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.SaveButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "SettingsEditorForm";
            this.Text = "SettingsEditorForm";
            ((System.ComponentModel.ISupportInitialize)(this.CurrentPropertyGridControl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton SaveButton;
        private DevExpress.XtraEditors.SimpleButton CancelButton;
        private DevExpress.XtraVerticalGrid.PropertyGridControl CurrentPropertyGridControl;
        private DevExpress.XtraEditors.SimpleButton RestoreDefaultButton;
    }
}