namespace PI2025___Projeto
{
    partial class FormAlugueis
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
            this.dgvAlugueis = new System.Windows.Forms.DataGridView();
            this.btnSair = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlugueis)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvAlugueis
            // 
            this.dgvAlugueis.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvAlugueis.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvAlugueis.ColumnHeadersHeight = 34;
            this.dgvAlugueis.GridColor = System.Drawing.Color.Silver;
            this.dgvAlugueis.Location = new System.Drawing.Point(12, 16);
            this.dgvAlugueis.Name = "dgvAlugueis";
            this.dgvAlugueis.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvAlugueis.RowHeadersWidth = 62;
            this.dgvAlugueis.RowTemplate.Height = 28;
            this.dgvAlugueis.Size = new System.Drawing.Size(1297, 598);
            this.dgvAlugueis.TabIndex = 0;
            this.dgvAlugueis.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAlugueis_CellContentClick);
            // 
            // btnSair
            // 
            this.btnSair.BackColor = System.Drawing.Color.Lavender;
            this.btnSair.ForeColor = System.Drawing.Color.Navy;
            this.btnSair.Location = new System.Drawing.Point(12, 622);
            this.btnSair.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(152, 35);
            this.btnSair.TabIndex = 1;
            this.btnSair.Text = "VOLTAR";
            this.btnSair.UseVisualStyleBackColor = false;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // FormAlugueis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(1321, 688);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.dgvAlugueis);
            this.Name = "FormAlugueis";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlugueis)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvAlugueis;
        private System.Windows.Forms.Button btnSair;
    }
}