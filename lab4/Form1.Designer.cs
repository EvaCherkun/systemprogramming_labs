namespace lab4
{
    partial class Form1
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
            this.treeViewProperties = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // treeViewProperties
            // 
            this.treeViewProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewProperties.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.treeViewProperties.Location = new System.Drawing.Point(14, 13);
            this.treeViewProperties.Name = "treeViewProperties";
            this.treeViewProperties.Size = new System.Drawing.Size(886, 454);
            this.treeViewProperties.TabIndex = 0;
            this.treeViewProperties.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewProperties_AfterSelect);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 480);
            this.Controls.Add(this.treeViewProperties);
            this.MinimumSize = new System.Drawing.Size(455, 317);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lab 5 — Building (property reflection)";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewProperties;
    }
}
