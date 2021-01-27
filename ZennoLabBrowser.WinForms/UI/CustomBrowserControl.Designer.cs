namespace ZennoLabBrowser.WinForms.UI
{
    partial class CustomBrowserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomBrowserControl));
            this.WebViewStatusStrip = new System.Windows.Forms.StatusStrip();
            this.CurrentToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.NavigateUrlButton = new DevExpress.XtraEditors.SimpleButton();
            this.UrlTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.RefreshButton = new DevExpress.XtraEditors.SimpleButton();
            this.RedoButton = new DevExpress.XtraEditors.SimpleButton();
            this.UndoButton = new DevExpress.XtraEditors.SimpleButton();
            this.CurrentXWebViewControl = new IRO.XWebView.CefSharp.WinForms.CefSharpXWebViewControl();
            this.WebViewStatusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UrlTextEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // WebViewStatusStrip
            // 
            this.WebViewStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CurrentToolStripStatusLabel});
            this.WebViewStatusStrip.Location = new System.Drawing.Point(0, 426);
            this.WebViewStatusStrip.Name = "WebViewStatusStrip";
            this.WebViewStatusStrip.Size = new System.Drawing.Size(832, 22);
            this.WebViewStatusStrip.TabIndex = 0;
            this.WebViewStatusStrip.Text = "webVewStatusStrip";
            // 
            // CurrentToolStripStatusLabel
            // 
            this.CurrentToolStripStatusLabel.BackColor = System.Drawing.Color.Transparent;
            this.CurrentToolStripStatusLabel.Name = "CurrentToolStripStatusLabel";
            this.CurrentToolStripStatusLabel.Size = new System.Drawing.Size(22, 17);
            this.CurrentToolStripStatusLabel.Text = "___";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.IsSplitterFixed = true;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            this.splitContainerControl1.Panel1.Controls.Add(this.NavigateUrlButton);
            this.splitContainerControl1.Panel1.Controls.Add(this.UrlTextEdit);
            this.splitContainerControl1.Panel1.Controls.Add(this.RefreshButton);
            this.splitContainerControl1.Panel1.Controls.Add(this.RedoButton);
            this.splitContainerControl1.Panel1.Controls.Add(this.UndoButton);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            this.splitContainerControl1.Panel2.Controls.Add(this.CurrentXWebViewControl);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(832, 426);
            this.splitContainerControl1.SplitterPosition = 40;
            this.splitContainerControl1.TabIndex = 6;
            // 
            // NavigateUrlButton
            // 
            this.NavigateUrlButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.NavigateUrlButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("NavigateUrlButton.ImageOptions.SvgImage")));
            this.NavigateUrlButton.ImageOptions.SvgImageSize = new System.Drawing.Size(25, 25);
            this.NavigateUrlButton.Location = new System.Drawing.Point(797, 7);
            this.NavigateUrlButton.Name = "NavigateUrlButton";
            this.NavigateUrlButton.Size = new System.Drawing.Size(30, 30);
            this.NavigateUrlButton.TabIndex = 10;
            this.NavigateUrlButton.Text = "simpleButton4";
            this.NavigateUrlButton.Click += new System.EventHandler(this.NavigateUrlButton_Click);
            // 
            // UrlTextEdit
            // 
            this.UrlTextEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UrlTextEdit.Location = new System.Drawing.Point(116, 7);
            this.UrlTextEdit.Name = "UrlTextEdit";
            this.UrlTextEdit.Properties.AutoHeight = false;
            this.UrlTextEdit.Size = new System.Drawing.Size(675, 30);
            this.UrlTextEdit.TabIndex = 9;
            this.UrlTextEdit.KeyUp += new System.Windows.Forms.KeyEventHandler(this.UrlTextEdit_KeyUp);
            // 
            // RefreshButton
            // 
            this.RefreshButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("RefreshButton.ImageOptions.SvgImage")));
            this.RefreshButton.ImageOptions.SvgImageSize = new System.Drawing.Size(25, 25);
            this.RefreshButton.Location = new System.Drawing.Point(80, 7);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(30, 30);
            this.RefreshButton.TabIndex = 8;
            this.RefreshButton.Text = "simpleButton3";
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // RedoButton
            // 
            this.RedoButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("RedoButton.ImageOptions.SvgImage")));
            this.RedoButton.ImageOptions.SvgImageSize = new System.Drawing.Size(25, 25);
            this.RedoButton.Location = new System.Drawing.Point(44, 7);
            this.RedoButton.Name = "RedoButton";
            this.RedoButton.Size = new System.Drawing.Size(30, 30);
            this.RedoButton.TabIndex = 7;
            this.RedoButton.Text = "simpleButton2";
            this.RedoButton.Click += new System.EventHandler(this.RedoButton_Click);
            // 
            // UndoButton
            // 
            this.UndoButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("UndoButton.ImageOptions.SvgImage")));
            this.UndoButton.ImageOptions.SvgImageSize = new System.Drawing.Size(25, 25);
            this.UndoButton.Location = new System.Drawing.Point(8, 7);
            this.UndoButton.Name = "UndoButton";
            this.UndoButton.Size = new System.Drawing.Size(30, 30);
            this.UndoButton.TabIndex = 6;
            this.UndoButton.Text = "simpleButton1";
            this.UndoButton.Click += new System.EventHandler(this.UndoButton_Click);
            // 
            // CurrentXWebViewControl
            // 
            this.CurrentXWebViewControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CurrentXWebViewControl.Location = new System.Drawing.Point(0, 0);
            this.CurrentXWebViewControl.Name = "CurrentXWebViewControl";
            this.CurrentXWebViewControl.Size = new System.Drawing.Size(832, 374);
            this.CurrentXWebViewControl.TabIndex = 0;
            // 
            // CustomBrowserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.WebViewStatusStrip);
            this.Name = "CustomBrowserControl";
            this.Size = new System.Drawing.Size(832, 448);
            this.WebViewStatusStrip.ResumeLayout(false);
            this.WebViewStatusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.UrlTextEdit.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.SimpleButton NavigateUrlButton;
        private DevExpress.XtraEditors.TextEdit UrlTextEdit;
        private DevExpress.XtraEditors.SimpleButton RefreshButton;
        private DevExpress.XtraEditors.SimpleButton RedoButton;
        private DevExpress.XtraEditors.SimpleButton UndoButton;
        private IRO.XWebView.CefSharp.WinForms.CefSharpXWebViewControl CurrentXWebViewControl;
        public System.Windows.Forms.ToolStripStatusLabel CurrentToolStripStatusLabel;
        private System.Windows.Forms.StatusStrip WebViewStatusStrip;
    }
}
