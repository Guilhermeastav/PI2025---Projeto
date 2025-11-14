namespace PI2025___Projeto
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
            this.btnHistorico = new System.Windows.Forms.Button();
            this.btnAluguel = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.txbTitulo = new System.Windows.Forms.TextBox();
            this.btnDeslogar = new System.Windows.Forms.Button();
            this.lblConexaoBDA = new System.Windows.Forms.Label();
            this.pnlBackground = new System.Windows.Forms.Panel();
            this.pnlBackground.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnHistorico
            // 
            this.btnHistorico.BackColor = System.Drawing.Color.LightGray;
            this.btnHistorico.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnHistorico.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.btnHistorico.Location = new System.Drawing.Point(498, 396);
            this.btnHistorico.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnHistorico.Name = "btnHistorico";
            this.btnHistorico.Size = new System.Drawing.Size(248, 38);
            this.btnHistorico.TabIndex = 0;
            this.btnHistorico.Text = "HISTÓRICO";
            this.btnHistorico.UseVisualStyleBackColor = false;
            this.btnHistorico.Click += new System.EventHandler(this.btnHistorico_Click);
            // 
            // btnAluguel
            // 
            this.btnAluguel.BackColor = System.Drawing.Color.LightGray;
            this.btnAluguel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAluguel.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.btnAluguel.Location = new System.Drawing.Point(498, 332);
            this.btnAluguel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAluguel.Name = "btnAluguel";
            this.btnAluguel.Size = new System.Drawing.Size(248, 38);
            this.btnAluguel.TabIndex = 1;
            this.btnAluguel.Text = "ALUGUEL";
            this.btnAluguel.UseVisualStyleBackColor = false;
            this.btnAluguel.Click += new System.EventHandler(this.btnAluguel_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.LightGray;
            this.btnLogin.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.btnLogin.Location = new System.Drawing.Point(498, 261);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(248, 38);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "LOGIN";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // txbTitulo
            // 
            this.txbTitulo.BackColor = System.Drawing.Color.LightGray;
            this.txbTitulo.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbTitulo.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.txbTitulo.Location = new System.Drawing.Point(382, 35);
            this.txbTitulo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txbTitulo.Name = "txbTitulo";
            this.txbTitulo.Size = new System.Drawing.Size(488, 46);
            this.txbTitulo.TabIndex = 3;
            this.txbTitulo.Text = "PUC Motors";
            this.txbTitulo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txbTitulo.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // btnDeslogar
            // 
            this.btnDeslogar.BackColor = System.Drawing.Color.LightGray;
            this.btnDeslogar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeslogar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnDeslogar.Location = new System.Drawing.Point(1128, 532);
            this.btnDeslogar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnDeslogar.Name = "btnDeslogar";
            this.btnDeslogar.Size = new System.Drawing.Size(112, 43);
            this.btnDeslogar.TabIndex = 4;
            this.btnDeslogar.Text = "SAIR";
            this.btnDeslogar.UseVisualStyleBackColor = false;
            this.btnDeslogar.Click += new System.EventHandler(this.btnDeslogar_Click);
            // 
            // lblConexaoBDA
            // 
            this.lblConexaoBDA.AutoSize = true;
            this.lblConexaoBDA.ForeColor = System.Drawing.Color.Red;
            this.lblConexaoBDA.Location = new System.Drawing.Point(12, 555);
            this.lblConexaoBDA.Name = "lblConexaoBDA";
            this.lblConexaoBDA.Size = new System.Drawing.Size(120, 20);
            this.lblConexaoBDA.TabIndex = 5;
            this.lblConexaoBDA.Text = "Não Conectado";
            this.lblConexaoBDA.Click += new System.EventHandler(this.label1_Click);
            // 
            // pnlBackground
            // 
            this.pnlBackground.BackColor = System.Drawing.Color.Transparent;
            this.pnlBackground.Controls.Add(this.lblConexaoBDA);
            this.pnlBackground.Controls.Add(this.btnAluguel);
            this.pnlBackground.Controls.Add(this.btnDeslogar);
            this.pnlBackground.Controls.Add(this.txbTitulo);
            this.pnlBackground.Controls.Add(this.btnHistorico);
            this.pnlBackground.Controls.Add(this.btnLogin);
            this.pnlBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBackground.Location = new System.Drawing.Point(0, 0);
            this.pnlBackground.Name = "pnlBackground";
            this.pnlBackground.Size = new System.Drawing.Size(1253, 600);
            this.pnlBackground.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImage = global::PI2025___Projeto.Properties.Resources.PUCMotors;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(1253, 600);
            this.Controls.Add(this.pnlBackground);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.pnlBackground.ResumeLayout(false);
            this.pnlBackground.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnHistorico;
        private System.Windows.Forms.Button btnAluguel;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox txbTitulo;
        private System.Windows.Forms.Button btnDeslogar;
        private System.Windows.Forms.Label lblConexaoBDA;
        private System.Windows.Forms.Panel pnlBackground;
    }
}

