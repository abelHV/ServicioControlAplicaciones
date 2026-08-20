namespace appServeis
{
    partial class FrmMain
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.lstLog = new System.Windows.Forms.ListBox();
            this.label = new System.Windows.Forms.Label();
            this.btnInterruptor = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstLog
            // 
            this.lstLog.FormattingEnabled = true;
            this.lstLog.ItemHeight = 16;
            this.lstLog.Location = new System.Drawing.Point(12, 85);
            this.lstLog.Name = "lstLog";
            this.lstLog.Size = new System.Drawing.Size(304, 340);
            this.lstLog.TabIndex = 0;
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Font = new System.Drawing.Font("Arial Narrow", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label.Location = new System.Drawing.Point(130, 9);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(57, 27);
            this.label.TabIndex = 1;
            this.label.Text = "Estat";
            this.label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnInterruptor
            // 
            this.btnInterruptor.BackColor = System.Drawing.Color.LightGreen;
            this.btnInterruptor.Location = new System.Drawing.Point(122, 43);
            this.btnInterruptor.Name = "btnInterruptor";
            this.btnInterruptor.Size = new System.Drawing.Size(75, 30);
            this.btnInterruptor.TabIndex = 2;
            this.btnInterruptor.Text = "Activar";
            this.btnInterruptor.UseVisualStyleBackColor = false;
            this.btnInterruptor.Click += new System.EventHandler(this.btnInterruptor_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 450);
            this.Controls.Add(this.btnInterruptor);
            this.Controls.Add(this.label);
            this.Controls.Add(this.lstLog);
            this.Name = "FrmMain";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstLog;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Button btnInterruptor;
    }
}

