﻿namespace Cliente
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
            this.Pregunta1 = new System.Windows.Forms.Label();
            this.panelRespuesta1 = new System.Windows.Forms.Panel();
            this.panelRespuesta2 = new System.Windows.Forms.Panel();
            this.panelRespuesta3 = new System.Windows.Forms.Panel();
            this.panelRespuesta4 = new System.Windows.Forms.Panel();
            this.boton_enviar = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Chat = new System.Windows.Forms.ListBox();
            this.labelTiempoRestante = new System.Windows.Forms.Label();
            this.numForm = new System.Windows.Forms.Label();
            this.primera_preg = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Pregunta1
            // 
            this.Pregunta1.AutoSize = true;
            this.Pregunta1.Location = new System.Drawing.Point(290, 99);
            this.Pregunta1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Pregunta1.Name = "Pregunta1";
            this.Pregunta1.Size = new System.Drawing.Size(50, 13);
            this.Pregunta1.TabIndex = 0;
            this.Pregunta1.Text = "Pregunta";
           
            // 
            // panelRespuesta1
            // 
            this.panelRespuesta1.Location = new System.Drawing.Point(180, 169);
            this.panelRespuesta1.Margin = new System.Windows.Forms.Padding(2);
            this.panelRespuesta1.Name = "panelRespuesta1";
            this.panelRespuesta1.Size = new System.Drawing.Size(137, 106);
            this.panelRespuesta1.TabIndex = 9;
            this.panelRespuesta1.Paint += new System.Windows.Forms.PaintEventHandler(this.panelRespuesta1_Paint);
            // 
            // panelRespuesta2
            // 
            this.panelRespuesta2.Location = new System.Drawing.Point(321, 169);
            this.panelRespuesta2.Margin = new System.Windows.Forms.Padding(2);
            this.panelRespuesta2.Name = "panelRespuesta2";
            this.panelRespuesta2.Size = new System.Drawing.Size(130, 106);
            this.panelRespuesta2.TabIndex = 10;
            this.panelRespuesta2.Paint += new System.Windows.Forms.PaintEventHandler(this.panelRespuesta2_Paint);
            // 
            // panelRespuesta3
            // 
            this.panelRespuesta3.Location = new System.Drawing.Point(180, 279);
            this.panelRespuesta3.Margin = new System.Windows.Forms.Padding(2);
            this.panelRespuesta3.Name = "panelRespuesta3";
            this.panelRespuesta3.Size = new System.Drawing.Size(137, 102);
            this.panelRespuesta3.TabIndex = 11;
            // 
            // panelRespuesta4
            // 
            this.panelRespuesta4.Location = new System.Drawing.Point(321, 279);
            this.panelRespuesta4.Margin = new System.Windows.Forms.Padding(2);
            this.panelRespuesta4.Name = "panelRespuesta4";
            this.panelRespuesta4.Size = new System.Drawing.Size(130, 102);
            this.panelRespuesta4.TabIndex = 12;
            // 
            // boton_enviar
            // 
            this.boton_enviar.Location = new System.Drawing.Point(457, 463);
            this.boton_enviar.Name = "boton_enviar";
            this.boton_enviar.Size = new System.Drawing.Size(75, 23);
            this.boton_enviar.TabIndex = 15;
            this.boton_enviar.Text = "enviar";
            this.boton_enviar.UseVisualStyleBackColor = true;
            this.boton_enviar.Click += new System.EventHandler(this.boton_enviar_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(180, 458);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(271, 33);
            this.textBox1.TabIndex = 16;
            // 
            // Chat
            // 
            this.Chat.FormattingEnabled = true;
            this.Chat.Location = new System.Drawing.Point(519, 144);
            this.Chat.Margin = new System.Windows.Forms.Padding(2);
            this.Chat.Name = "Chat";
            this.Chat.Size = new System.Drawing.Size(155, 199);
            this.Chat.TabIndex = 17;
            // 
            // labelTiempoRestante
            // 
            this.labelTiempoRestante.AutoSize = true;
            this.labelTiempoRestante.Location = new System.Drawing.Point(177, 403);
            this.labelTiempoRestante.Name = "labelTiempoRestante";
            this.labelTiempoRestante.Size = new System.Drawing.Size(0, 13);
            this.labelTiempoRestante.TabIndex = 18;
            // 
            // numForm
            // 
            this.numForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numForm.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numForm.Location = new System.Drawing.Point(24, 221);
            this.numForm.Name = "numForm";
            this.numForm.Size = new System.Drawing.Size(98, 92);
            this.numForm.TabIndex = 19;
            // 
            // primera_preg
            // 
            this.primera_preg.Location = new System.Drawing.Point(265, 429);
            this.primera_preg.Name = "primera_preg";
            this.primera_preg.Size = new System.Drawing.Size(103, 23);
            this.primera_preg.TabIndex = 20;
            this.primera_preg.Text = "primera pregunta";
            this.primera_preg.UseVisualStyleBackColor = true;
            this.primera_preg.Click += new System.EventHandler(this.primera_preg_Click);
            // 
            // Partida
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 527);
            this.Controls.Add(this.primera_preg);
            this.Controls.Add(this.numForm);
            this.Controls.Add(this.labelTiempoRestante);
            this.Controls.Add(this.Chat);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.boton_enviar);
            this.Controls.Add(this.panelRespuesta4);
            this.Controls.Add(this.panelRespuesta3);
            this.Controls.Add(this.panelRespuesta2);
            this.Controls.Add(this.panelRespuesta1);
            this.Controls.Add(this.Pregunta1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Partida";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Partida_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Pregunta1;
        private System.Windows.Forms.Panel panelRespuesta1;
        private System.Windows.Forms.Panel panelRespuesta2;
        private System.Windows.Forms.Panel panelRespuesta3;
        private System.Windows.Forms.Panel panelRespuesta4;
        private System.Windows.Forms.Button boton_enviar;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ListBox Chat;
        private System.Windows.Forms.Label labelTiempoRestante;
        private System.Windows.Forms.Label numForm;
        private System.Windows.Forms.Button primera_preg;
    }
}