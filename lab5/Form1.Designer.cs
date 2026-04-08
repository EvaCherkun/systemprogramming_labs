namespace lab5
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
            this.components = new System.ComponentModel.Container();
            this.treeViewSporuda = new System.Windows.Forms.TreeView();
            this.buttonShowProperties = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // treeViewSporuda
            // 
            this.treeViewSporuda.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewSporuda.Location = new System.Drawing.Point(12, 48);
            this.treeViewSporuda.Name = "treeViewSporuda";
            this.treeViewSporuda.Size = new System.Drawing.Size(776, 390);
            this.treeViewSporuda.TabIndex = 0;
            // 
            // buttonShowProperties
            // 
            this.buttonShowProperties.Location = new System.Drawing.Point(12, 12);
            this.buttonShowProperties.Name = "buttonShowProperties";
            this.buttonShowProperties.Size = new System.Drawing.Size(220, 28);
            this.buttonShowProperties.TabIndex = 1;
            this.buttonShowProperties.Text = "Показати властивості (Споруда)";
            this.buttonShowProperties.UseVisualStyleBackColor = true;
            this.buttonShowProperties.Click += new System.EventHandler(this.buttonShowProperties_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonShowProperties);
            this.Controls.Add(this.treeViewSporuda);
            this.MinimumSize = new System.Drawing.Size(400, 300);
            this.Text = "Тема 5 — Споруда та TreeView";
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.TreeView treeViewSporuda;
        private System.Windows.Forms.Button buttonShowProperties;

        #endregion
    }
}

