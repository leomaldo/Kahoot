namespace Cliente
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ID = new System.Windows.Forms.TextBox();
            this.Contraseña = new System.Windows.Forms.TextBox();
            this.MaxPuntuacion = new System.Windows.Forms.RadioButton();
            this.JugadorPuntos = new System.Windows.Forms.RadioButton();
            this.Preguntas = new System.Windows.Forms.RadioButton();
            this.button5 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(64, 56);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 49);
            this.button1.TabIndex = 0;
            this.button1.Text = "CONECTAR";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(64, 304);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(147, 49);
            this.button2.TabIndex = 1;
            this.button2.Text = "DESCONECTAR";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(235, 166);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(120, 49);
            this.button3.TabIndex = 2;
            this.button3.Text = "Login";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(436, 166);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(120, 49);
            this.button4.TabIndex = 3;
            this.button4.Text = "Registrarme";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(380, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "ID:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(349, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Contraseña:";
            // 
            // ID
            // 
            this.ID.Location = new System.Drawing.Point(313, 54);
            this.ID.Name = "ID";
            this.ID.Size = new System.Drawing.Size(176, 26);
            this.ID.TabIndex = 5;
            // 
            // Contraseña
            // 
            this.Contraseña.Location = new System.Drawing.Point(313, 117);
            this.Contraseña.Name = "Contraseña";
            this.Contraseña.Size = new System.Drawing.Size(176, 26);
            this.Contraseña.TabIndex = 7;
            // 
            // MaxPuntuacion
            // 
            this.MaxPuntuacion.AutoSize = true;
            this.MaxPuntuacion.Location = new System.Drawing.Point(359, 250);
            this.MaxPuntuacion.Name = "MaxPuntuacion";
            this.MaxPuntuacion.Size = new System.Drawing.Size(312, 24);
            this.MaxPuntuacion.TabIndex = 8;
            this.MaxPuntuacion.TabStop = true;
            this.MaxPuntuacion.Text = "Dime lo máximos puntos en una partida";
            this.MaxPuntuacion.UseVisualStyleBackColor = true;
            // 
            // JugadorPuntos
            // 
            this.JugadorPuntos.AutoSize = true;
            this.JugadorPuntos.Location = new System.Drawing.Point(363, 299);
            this.JugadorPuntos.Name = "JugadorPuntos";
            this.JugadorPuntos.Size = new System.Drawing.Size(261, 24);
            this.JugadorPuntos.TabIndex = 9;
            this.JugadorPuntos.TabStop = true;
            this.JugadorPuntos.Text = "Dime el jugador con más puntos";
            this.JugadorPuntos.UseVisualStyleBackColor = true;
            // 
            // Preguntas
            // 
            this.Preguntas.AutoSize = true;
            this.Preguntas.Location = new System.Drawing.Point(364, 348);
            this.Preguntas.Name = "Preguntas";
            this.Preguntas.Size = new System.Drawing.Size(368, 24);
            this.Preguntas.TabIndex = 10;
            this.Preguntas.TabStop = true;
            this.Preguntas.Text = "Dime la partida con menos preguntas correctas";
            this.Preguntas.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(334, 420);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(163, 37);
            this.button5.TabIndex = 11;
            this.button5.Text = "ENVIAR";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(779, 532);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.Preguntas);
            this.Controls.Add(this.JugadorPuntos);
            this.Controls.Add(this.MaxPuntuacion);
            this.Controls.Add(this.Contraseña);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ID;
        private System.Windows.Forms.TextBox Contraseña;
        private System.Windows.Forms.RadioButton MaxPuntuacion;
        private System.Windows.Forms.RadioButton JugadorPuntos;
        private System.Windows.Forms.RadioButton Preguntas;
        private System.Windows.Forms.Button button5;
    }
}

