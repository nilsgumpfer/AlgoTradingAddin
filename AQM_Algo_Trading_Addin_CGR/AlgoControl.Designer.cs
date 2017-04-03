namespace AQM_Algo_Trading_Addin_CGR
{
    partial class AlgoControl
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.label_Status = new System.Windows.Forms.Label();
            this.label_Kontostand = new System.Windows.Forms.Label();
            this.lblAlgoStatus = new System.Windows.Forms.Label();
            this.lblKS_Saldo = new System.Windows.Forms.Label();
            this.lblGewinn = new System.Windows.Forms.Label();
            this.label_Gewinn = new System.Windows.Forms.Label();
            this.label_Einstiegspreis = new System.Windows.Forms.Label();
            this.lbl_EinstPreis = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label_Status
            // 
            this.label_Status.AutoSize = true;
            this.label_Status.Location = new System.Drawing.Point(3, 15);
            this.label_Status.Name = "label_Status";
            this.label_Status.Size = new System.Drawing.Size(40, 13);
            this.label_Status.TabIndex = 0;
            this.label_Status.Text = "Status:";
            // 
            // label_Kontostand
            // 
            this.label_Kontostand.AutoSize = true;
            this.label_Kontostand.Location = new System.Drawing.Point(3, 37);
            this.label_Kontostand.Name = "label_Kontostand";
            this.label_Kontostand.Size = new System.Drawing.Size(61, 13);
            this.label_Kontostand.TabIndex = 1;
            this.label_Kontostand.Text = "Kontostand";
            // 
            // lblAlgoStatus
            // 
            this.lblAlgoStatus.AutoSize = true;
            this.lblAlgoStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlgoStatus.Location = new System.Drawing.Point(104, 15);
            this.lblAlgoStatus.Name = "lblAlgoStatus";
            this.lblAlgoStatus.Size = new System.Drawing.Size(11, 13);
            this.lblAlgoStatus.TabIndex = 2;
            this.lblAlgoStatus.Text = "-";
            // 
            // lblKS_Saldo
            // 
            this.lblKS_Saldo.AutoSize = true;
            this.lblKS_Saldo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKS_Saldo.Location = new System.Drawing.Point(104, 37);
            this.lblKS_Saldo.Name = "lblKS_Saldo";
            this.lblKS_Saldo.Size = new System.Drawing.Size(11, 13);
            this.lblKS_Saldo.TabIndex = 3;
            this.lblKS_Saldo.Text = "-";
            // 
            // lblGewinn
            // 
            this.lblGewinn.AutoSize = true;
            this.lblGewinn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGewinn.Location = new System.Drawing.Point(104, 81);
            this.lblGewinn.Name = "lblGewinn";
            this.lblGewinn.Size = new System.Drawing.Size(11, 13);
            this.lblGewinn.TabIndex = 4;
            this.lblGewinn.Text = "-";
            // 
            // label_Gewinn
            // 
            this.label_Gewinn.AutoSize = true;
            this.label_Gewinn.Location = new System.Drawing.Point(3, 81);
            this.label_Gewinn.Name = "label_Gewinn";
            this.label_Gewinn.Size = new System.Drawing.Size(46, 13);
            this.label_Gewinn.TabIndex = 5;
            this.label_Gewinn.Text = "Gewinn:";
            // 
            // label_Einstiegspreis
            // 
            this.label_Einstiegspreis.AutoSize = true;
            this.label_Einstiegspreis.Location = new System.Drawing.Point(3, 59);
            this.label_Einstiegspreis.Name = "label_Einstiegspreis";
            this.label_Einstiegspreis.Size = new System.Drawing.Size(74, 13);
            this.label_Einstiegspreis.TabIndex = 6;
            this.label_Einstiegspreis.Text = "Einstiegspreis:";
            // 
            // lbl_EinstPreis
            // 
            this.lbl_EinstPreis.AutoSize = true;
            this.lbl_EinstPreis.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_EinstPreis.Location = new System.Drawing.Point(104, 59);
            this.lbl_EinstPreis.Name = "lbl_EinstPreis";
            this.lbl_EinstPreis.Size = new System.Drawing.Size(11, 13);
            this.lbl_EinstPreis.TabIndex = 7;
            this.lbl_EinstPreis.Text = "-";
            // 
            // AlgoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbl_EinstPreis);
            this.Controls.Add(this.label_Einstiegspreis);
            this.Controls.Add(this.label_Gewinn);
            this.Controls.Add(this.lblGewinn);
            this.Controls.Add(this.lblKS_Saldo);
            this.Controls.Add(this.lblAlgoStatus);
            this.Controls.Add(this.label_Kontostand);
            this.Controls.Add(this.label_Status);
            this.Name = "AlgoControl";
            this.Load += new System.EventHandler(this.AlgoControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_Status;
        private System.Windows.Forms.Label label_Kontostand;
        public System.Windows.Forms.Label lblAlgoStatus;
        public System.Windows.Forms.Label lblKS_Saldo;
        public System.Windows.Forms.Label lblGewinn;
        private System.Windows.Forms.Label label_Gewinn;
        private System.Windows.Forms.Label label_Einstiegspreis;
        public System.Windows.Forms.Label lbl_EinstPreis;
    }
}
