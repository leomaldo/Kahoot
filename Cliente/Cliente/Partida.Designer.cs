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
            this.Pregunta = new System.Windows.Forms.Label();
            this.panelRespuesta1 = new System.Windows.Forms.Panel();
            this.panelRespuesta2 = new System.Windows.Forms.Panel();
            this.panelRespuesta3 = new System.Windows.Forms.Panel();
            this.panelRespuesta4 = new System.Windows.Forms.Panel();
            this.Chat = new System.Windows.Forms.TextBox();
            this.boton_enviar = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Pregunta
            // 
            this.Pregunta.AutoSize = true;
            this.Pregunta.Location = new System.Drawing.Point(435, 152);
            this.Pregunta.Name = "Pregunta";
            this.Pregunta.Size = new System.Drawing.Size(74, 20);
            this.Pregunta.TabIndex = 0;
            this.Pregunta.Text = "Pregunta";
            this.Pregunta.Click += new System.EventHandler(this.label1_Click);
            // 
            // panelRespuesta1
            // 
            this.panelRespuesta1.Location = new System.Drawing.Point(270, 260);
            this.panelRespuesta1.Name = "panelRespuesta1";
            this.panelRespuesta1.Size = new System.Drawing.Size(206, 163);
            this.panelRespuesta1.TabIndex = 9;
            this.panelRespuesta1.Paint += new System.Windows.Forms.PaintEventHandler(this.panelRespuesta1_Paint);
            // 
            // panelRespuesta2
            // 
            this.panelRespuesta2.Location = new System.Drawing.Point(482, 260);
            this.panelRespuesta2.Name = "panelRespuesta2";
            this.panelRespuesta2.Size = new System.Drawing.Size(195, 163);
            this.panelRespuesta2.TabIndex = 10;
            this.panelRespuesta2.Paint += new System.Windows.Forms.PaintEventHandler(this.panelRespuesta2_Paint);
            // 
            // panelRespuesta3
            // 
            this.panelRespuesta3.Location = new System.Drawing.Point(270, 429);
            this.panelRespuesta3.Name = "panelRespuesta3";
            this.panelRespuesta3.Size = new System.Drawing.Size(206, 157);
            this.panelRespuesta3.TabIndex = 11;
            // 
            // panelRespuesta4
            // 
            this.panelRespuesta4.Location = new System.Drawing.Point(482, 429);
            this.panelRespuesta4.Name = "panelRespuesta4";
            this.panelRespuesta4.Size = new System.Drawing.Size(195, 157);
            this.panelRespuesta4.TabIndex = 12;
            // 
            // Chat
            // 
            this.Chat.Location = new System.Drawing.Point(270, 591);
            this.Chat.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Chat.Multiline = true;
            this.Chat.Name = "Chat";
            this.Chat.Size = new System.Drawing.Size(404, 92);
            this.Chat.TabIndex = 14;
            this.Chat.TextChanged += new System.EventHandler(this.Chat_TextChanged);
            // 
            // boton_enviar
            // 
            this.boton_enviar.Location = new System.Drawing.Point(686, 712);
            this.boton_enviar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.boton_enviar.Name = "boton_enviar";
            this.boton_enviar.Size = new System.Drawing.Size(112, 35);
            this.boton_enviar.TabIndex = 15;
            this.boton_enviar.Text = "enviar";
            this.boton_enviar.UseVisualStyleBackColor = true;
            this.boton_enviar.Click += new System.EventHandler(this.boton_enviar_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(270, 705);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(404, 48);
            this.textBox1.TabIndex = 16;
            // 
            // Partida
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1038, 811);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.boton_enviar);
            this.Controls.Add(this.Chat);
            this.Controls.Add(this.panelRespuesta4);
            this.Controls.Add(this.panelRespuesta3);
            this.Controls.Add(this.panelRespuesta2);
            this.Controls.Add(this.panelRespuesta1);
            this.Controls.Add(this.Pregunta);
            this.Name = "Partida";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Partida_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Pregunta;
        private System.Windows.Forms.Panel panelRespuesta1;
        private System.Windows.Forms.Panel panelRespuesta2;
        private System.Windows.Forms.Panel panelRespuesta3;
        private System.Windows.Forms.Panel panelRespuesta4;
        private System.Windows.Forms.TextBox Chat;
        private System.Windows.Forms.Button boton_enviar;
        private System.Windows.Forms.TextBox textBox1;
    }
}