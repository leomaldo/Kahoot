namespace Cliente
{
    partial class Peticion
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
            this.button5 = new System.Windows.Forms.Button();
            this.Preguntas = new System.Windows.Forms.RadioButton();
            this.JugadorPuntos = new System.Windows.Forms.RadioButton();
            this.MaxPuntuacion = new System.Windows.Forms.RadioButton();
            this.numForm = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(270, 395);
            this.button5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(163, 38);
            this.button5.TabIndex = 16;
            this.button5.Text = "ENVIAR";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // Preguntas
            // 
            this.Preguntas.AutoSize = true;
            this.Preguntas.Location = new System.Drawing.Point(270, 239);
            this.Preguntas.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Preguntas.Name = "Preguntas";
            this.Preguntas.Size = new System.Drawing.Size(368, 24);
            this.Preguntas.TabIndex = 15;
            this.Preguntas.TabStop = true;
            this.Preguntas.Text = "Dime la partida con menos preguntas correctas";
            this.Preguntas.UseVisualStyleBackColor = true;
            // 
            // JugadorPuntos
            // 
            this.JugadorPuntos.AutoSize = true;
            this.JugadorPuntos.Location = new System.Drawing.Point(270, 166);
            this.JugadorPuntos.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.JugadorPuntos.Name = "JugadorPuntos";
            this.JugadorPuntos.Size = new System.Drawing.Size(261, 24);
            this.JugadorPuntos.TabIndex = 14;
            this.JugadorPuntos.TabStop = true;
            this.JugadorPuntos.Text = "Dime el jugador con más puntos";
            this.JugadorPuntos.UseVisualStyleBackColor = true;
            // 
            // MaxPuntuacion
            // 
            this.MaxPuntuacion.AutoSize = true;
            this.MaxPuntuacion.Location = new System.Drawing.Point(270, 104);
            this.MaxPuntuacion.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaxPuntuacion.Name = "MaxPuntuacion";
            this.MaxPuntuacion.Size = new System.Drawing.Size(312, 24);
            this.MaxPuntuacion.TabIndex = 13;
            this.MaxPuntuacion.TabStop = true;
            this.MaxPuntuacion.Text = "Dime lo máximos puntos en una partida";
            this.MaxPuntuacion.UseVisualStyleBackColor = true;
            // 
            // numForm
            // 
            this.numForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numForm.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numForm.Location = new System.Drawing.Point(39, 227);
            this.numForm.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.numForm.Name = "numForm";
            this.numForm.Size = new System.Drawing.Size(146, 140);
            this.numForm.TabIndex = 18;
            // 
            // Peticion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 562);
            this.Controls.Add(this.numForm);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.Preguntas);
            this.Controls.Add(this.JugadorPuntos);
            this.Controls.Add(this.MaxPuntuacion);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Peticion";
            this.Text = "Peticion";
            this.Load += new System.EventHandler(this.Peticion_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.RadioButton Preguntas;
        private System.Windows.Forms.RadioButton JugadorPuntos;
        private System.Windows.Forms.RadioButton MaxPuntuacion;
        private System.Windows.Forms.Label numForm;
    }
}