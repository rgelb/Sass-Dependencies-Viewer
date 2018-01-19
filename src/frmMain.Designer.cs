namespace SASSFileViewer
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.appContainer = new System.Windows.Forms.ToolStripContainer();
            this.panelText = new System.Windows.Forms.Panel();
            this.treeSass = new System.Windows.Forms.TreeView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolbtnOpen = new System.Windows.Forms.ToolStripButton();
            this.imgListApp = new System.Windows.Forms.ImageList(this.components);
            this.appContainer.ContentPanel.SuspendLayout();
            this.appContainer.TopToolStripPanel.SuspendLayout();
            this.appContainer.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // appContainer
            // 
            this.appContainer.BottomToolStripPanelVisible = false;
            // 
            // appContainer.ContentPanel
            // 
            this.appContainer.ContentPanel.Controls.Add(this.panelText);
            this.appContainer.ContentPanel.Controls.Add(this.treeSass);
            this.appContainer.ContentPanel.Size = new System.Drawing.Size(935, 695);
            this.appContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.appContainer.LeftToolStripPanelVisible = false;
            this.appContainer.Location = new System.Drawing.Point(0, 0);
            this.appContainer.Name = "appContainer";
            this.appContainer.RightToolStripPanelVisible = false;
            this.appContainer.Size = new System.Drawing.Size(935, 734);
            this.appContainer.TabIndex = 0;
            // 
            // appContainer.TopToolStripPanel
            // 
            this.appContainer.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // panelText
            // 
            this.panelText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelText.Location = new System.Drawing.Point(242, 0);
            this.panelText.Name = "panelText";
            this.panelText.Size = new System.Drawing.Size(693, 695);
            this.panelText.TabIndex = 1;
            // 
            // treeSass
            // 
            this.treeSass.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeSass.Location = new System.Drawing.Point(0, 0);
            this.treeSass.Name = "treeSass";
            this.treeSass.Size = new System.Drawing.Size(242, 695);
            this.treeSass.TabIndex = 0;
            this.treeSass.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeSass_AfterSelect);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolbtnOpen});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(84, 39);
            this.toolStrip1.TabIndex = 0;
            // 
            // toolbtnOpen
            // 
            this.toolbtnOpen.Image = global::SASSFileViewer.Properties.Resources.icon_file_open;
            this.toolbtnOpen.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolbtnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbtnOpen.Name = "toolbtnOpen";
            this.toolbtnOpen.Size = new System.Drawing.Size(72, 36);
            this.toolbtnOpen.Text = "Open";
            this.toolbtnOpen.Click += new System.EventHandler(this.toolbtnOpen_Click);
            // 
            // imgListApp
            // 
            this.imgListApp.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imgListApp.ImageSize = new System.Drawing.Size(16, 16);
            this.imgListApp.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(935, 734);
            this.Controls.Add(this.appContainer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.Text = "SASS File Viewer";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.appContainer.ContentPanel.ResumeLayout(false);
            this.appContainer.TopToolStripPanel.ResumeLayout(false);
            this.appContainer.TopToolStripPanel.PerformLayout();
            this.appContainer.ResumeLayout(false);
            this.appContainer.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer appContainer;
        private System.Windows.Forms.TreeView treeSass;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolbtnOpen;
        private System.Windows.Forms.Panel panelText;
        private System.Windows.Forms.ImageList imgListApp;
    }
}

