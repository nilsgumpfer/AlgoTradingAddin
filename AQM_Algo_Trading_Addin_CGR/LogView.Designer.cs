namespace AQM_Algo_Trading_Addin_CGR
{
    partial class LogView
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.onklickReload = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(2, 2);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(715, 504);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // onklickReload
            // 
            this.onklickReload.Location = new System.Drawing.Point(312, 512);
            this.onklickReload.Name = "onklickReload";
            this.onklickReload.Size = new System.Drawing.Size(93, 23);
            this.onklickReload.TabIndex = 1;
            this.onklickReload.Text = "Aktualisieren";
            this.onklickReload.UseVisualStyleBackColor = true;
            this.onklickReload.Click += new System.EventHandler(this.onklickReload_Click);
            // 
            // LogView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 543);
            this.Controls.Add(this.onklickReload);
            this.Controls.Add(this.richTextBox1);
            this.Name = "LogView";
            this.Text = "LogView";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button onklickReload;
    }
}