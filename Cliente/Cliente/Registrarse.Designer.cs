namespace Cliente
{
    partial class Registrarse
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
            this.label1 = new System.Windows.Forms.Label();
            this.ID = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Contraseña = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.Registrarme = new System.Windows.Forms.Button();
            this.Iniciarsesion = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(37, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "KAHOOT";
            // 
            // ID
            // 
            this.ID.AutoSize = true;
            this.ID.Location = new System.Drawing.Point(240, 119);
            this.ID.Name = "ID";
            this.ID.Size = new System.Drawing.Size(23, 16);
            this.ID.TabIndex = 1;
            this.ID.Text = "ID:";
            this.ID.Click += new System.EventHandler(this.label2_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(176, 153);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(172, 22);
            this.textBox1.TabIndex = 2;
            // 
            // Contraseña
            // 
            this.Contraseña.AutoSize = true;
            this.Contraseña.Location = new System.Drawing.Point(213, 205);
            this.Contraseña.Name = "Contraseña";
            this.Contraseña.Size = new System.Drawing.Size(79, 16);
            this.Contraseña.TabIndex = 3;
            this.Contraseña.Text = "Contraseña:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(176, 252);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(173, 22);
            this.textBox2.TabIndex = 4;
            // 
            // Registrarme
            // 
            this.Registrarme.Location = new System.Drawing.Point(50, 323);
            this.Registrarme.Name = "Registrarme";
            this.Registrarme.Size = new System.Drawing.Size(166, 47);
            this.Registrarme.TabIndex = 5;
            this.Registrarme.Text = "Registrarme";
            this.Registrarme.UseVisualStyleBackColor = true;
            // 
            // Iniciarsesion
            // 
            this.Iniciarsesion.Location = new System.Drawing.Point(270, 323);
            this.Iniciarsesion.Name = "Iniciarsesion";
            this.Iniciarsesion.Size = new System.Drawing.Size(187, 47);
            this.Iniciarsesion.TabIndex = 6;
            this.Iniciarsesion.Text = "Iniciar Sesión";
            this.Iniciarsesion.UseVisualStyleBackColor = true;
            // 
            // Registrarse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 526);
            this.Controls.Add(this.Iniciarsesion);
            this.Controls.Add(this.Registrarme);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.Contraseña);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.ID);
            this.Controls.Add(this.label1);
            this.Name = "Registrarse";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Registrarse_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label ID;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label Contraseña;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button Registrarme;
        private System.Windows.Forms.Button Iniciarsesion;
    }
}