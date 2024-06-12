namespace Cliente
{
    partial class Partida
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
            this.panelRespuesta1 = new System.Windows.Forms.Panel();
            this.panelRespuesta2 = new System.Windows.Forms.Panel();
            this.panelRespuesta3 = new System.Windows.Forms.Panel();
            this.panelRespuesta4 = new System.Windows.Forms.Panel();
            this.boton_enviar = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Chat = new System.Windows.Forms.ListBox();
            this.labelTiempoRestante = new System.Windows.Forms.Label();
            this.numForm = new System.Windows.Forms.Label();
            this.panelPregunta = new System.Windows.Forms.Panel();
            this.terminar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panelRespuesta1
            // 
            this.panelRespuesta1.Location = new System.Drawing.Point(240, 208);
            this.panelRespuesta1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelRespuesta1.Name = "panelRespuesta1";
            this.panelRespuesta1.Size = new System.Drawing.Size(183, 130);
            this.panelRespuesta1.TabIndex = 9;
            this.panelRespuesta1.Click += new System.EventHandler(this.panelRespuesta1_Click);
            this.panelRespuesta1.Paint += new System.Windows.Forms.PaintEventHandler(this.panelRespuesta1_Paint);
            // 
            // panelRespuesta2
            // 
            this.panelRespuesta2.Location = new System.Drawing.Point(428, 208);
            this.panelRespuesta2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelRespuesta2.Name = "panelRespuesta2";
            this.panelRespuesta2.Size = new System.Drawing.Size(173, 130);
            this.panelRespuesta2.TabIndex = 10;
            this.panelRespuesta2.Click += new System.EventHandler(this.panelRespuesta2_Click);
            // 
            // panelRespuesta3
            // 
            this.panelRespuesta3.Location = new System.Drawing.Point(240, 343);
            this.panelRespuesta3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelRespuesta3.Name = "panelRespuesta3";
            this.panelRespuesta3.Size = new System.Drawing.Size(183, 126);
            this.panelRespuesta3.TabIndex = 11;
            this.panelRespuesta3.Click += new System.EventHandler(this.panelRespuesta3_Click);
            // 
            // panelRespuesta4
            // 
            this.panelRespuesta4.Location = new System.Drawing.Point(428, 343);
            this.panelRespuesta4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelRespuesta4.Name = "panelRespuesta4";
            this.panelRespuesta4.Size = new System.Drawing.Size(173, 126);
            this.panelRespuesta4.TabIndex = 12;
            this.panelRespuesta4.Click += new System.EventHandler(this.panelRespuesta4_Click);
            // 
            // boton_enviar
            // 
            this.boton_enviar.Location = new System.Drawing.Point(609, 570);
            this.boton_enviar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.boton_enviar.Name = "boton_enviar";
            this.boton_enviar.Size = new System.Drawing.Size(100, 28);
            this.boton_enviar.TabIndex = 15;
            this.boton_enviar.Text = "enviar";
            this.boton_enviar.UseVisualStyleBackColor = true;
            this.boton_enviar.Click += new System.EventHandler(this.boton_enviar_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(240, 564);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(360, 40);
            this.textBox1.TabIndex = 16;
            // 
            // Chat
            // 
            this.Chat.FormattingEnabled = true;
            this.Chat.ItemHeight = 16;
            this.Chat.Location = new System.Drawing.Point(692, 177);
            this.Chat.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Chat.Name = "Chat";
            this.Chat.Size = new System.Drawing.Size(205, 244);
            this.Chat.TabIndex = 17;
            // 
            // labelTiempoRestante
            // 
            this.labelTiempoRestante.AutoSize = true;
            this.labelTiempoRestante.Location = new System.Drawing.Point(236, 496);
            this.labelTiempoRestante.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTiempoRestante.Name = "labelTiempoRestante";
            this.labelTiempoRestante.Size = new System.Drawing.Size(0, 16);
            this.labelTiempoRestante.TabIndex = 18;
            // 
            // numForm
            // 
            this.numForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numForm.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numForm.Location = new System.Drawing.Point(32, 272);
            this.numForm.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.numForm.Name = "numForm";
            this.numForm.Size = new System.Drawing.Size(130, 113);
            this.numForm.TabIndex = 19;
            this.numForm.Click += new System.EventHandler(this.numForm_Click);
            // 
            // panelPregunta
            // 
            this.panelPregunta.Location = new System.Drawing.Point(147, 92);
            this.panelPregunta.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelPregunta.Name = "panelPregunta";
            this.panelPregunta.Size = new System.Drawing.Size(539, 94);
            this.panelPregunta.TabIndex = 20;
            // 
            // terminar
            // 
            this.terminar.Location = new System.Drawing.Point(742, 455);
            this.terminar.Name = "terminar";
            this.terminar.Size = new System.Drawing.Size(105, 73);
            this.terminar.TabIndex = 21;
            this.terminar.Text = "Terminar Partida";
            this.terminar.UseVisualStyleBackColor = true;
            this.terminar.Click += new System.EventHandler(this.terminar_Click);
            // 
            // Partida
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(923, 649);
            this.Controls.Add(this.terminar);
            this.Controls.Add(this.panelPregunta);
            this.Controls.Add(this.numForm);
            this.Controls.Add(this.labelTiempoRestante);
            this.Controls.Add(this.Chat);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.boton_enviar);
            this.Controls.Add(this.panelRespuesta4);
            this.Controls.Add(this.panelRespuesta3);
            this.Controls.Add(this.panelRespuesta2);
            this.Controls.Add(this.panelRespuesta1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Partida";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Partida_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panelRespuesta1;
        private System.Windows.Forms.Panel panelRespuesta2;
        private System.Windows.Forms.Panel panelRespuesta3;
        private System.Windows.Forms.Panel panelRespuesta4;
        private System.Windows.Forms.Button boton_enviar;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ListBox Chat;
        private System.Windows.Forms.Label labelTiempoRestante;
        private System.Windows.Forms.Label numForm;
        private System.Windows.Forms.Panel panelPregunta;
        private System.Windows.Forms.Button terminar;
    }
}