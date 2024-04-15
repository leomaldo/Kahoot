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
            this.SuspendLayout();
            // 
            // Pregunta
            // 
            this.Pregunta.AutoSize = true;
            this.Pregunta.Location = new System.Drawing.Point(387, 122);
            this.Pregunta.Name = "Pregunta";
            this.Pregunta.Size = new System.Drawing.Size(61, 16);
            this.Pregunta.TabIndex = 0;
            this.Pregunta.Text = "Pregunta";
            this.Pregunta.Click += new System.EventHandler(this.label1_Click);
            // 
            // panelRespuesta1
            // 
            this.panelRespuesta1.Location = new System.Drawing.Point(124, 170);
            this.panelRespuesta1.Name = "panelRespuesta1";
            this.panelRespuesta1.Size = new System.Drawing.Size(284, 195);
            this.panelRespuesta1.TabIndex = 9;
            this.panelRespuesta1.Paint += new System.Windows.Forms.PaintEventHandler(this.panelRespuesta1_Paint);
            // 
            // panelRespuesta2
            // 
            this.panelRespuesta2.Location = new System.Drawing.Point(444, 170);
            this.panelRespuesta2.Name = "panelRespuesta2";
            this.panelRespuesta2.Size = new System.Drawing.Size(315, 195);
            this.panelRespuesta2.TabIndex = 10;
            this.panelRespuesta2.Paint += new System.Windows.Forms.PaintEventHandler(this.panelRespuesta2_Paint);
            // 
            // panelRespuesta3
            // 
            this.panelRespuesta3.Location = new System.Drawing.Point(124, 393);
            this.panelRespuesta3.Name = "panelRespuesta3";
            this.panelRespuesta3.Size = new System.Drawing.Size(284, 201);
            this.panelRespuesta3.TabIndex = 11;
            // 
            // panelRespuesta4
            // 
            this.panelRespuesta4.Location = new System.Drawing.Point(444, 393);
            this.panelRespuesta4.Name = "panelRespuesta4";
            this.panelRespuesta4.Size = new System.Drawing.Size(315, 201);
            this.panelRespuesta4.TabIndex = 12;
            // 
            // Partida
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(923, 649);
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
    }
}