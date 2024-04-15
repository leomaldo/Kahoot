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
            this.components = new System.ComponentModel.Container();
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
            this.listaconectados = new System.Windows.Forms.RadioButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.oPCIONESToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rEGISTRARMEINICIARSESIÓNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jUGARPARTIDAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pETICIÓNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timerAnimacion = new System.Windows.Forms.Timer(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(784, 534);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(134, 49);
            this.button3.TabIndex = 2;
            this.button3.Text = "Login";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(997, 534);
            this.button4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(134, 49);
            this.button4.TabIndex = 3;
            this.button4.Text = "Registrarme";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(853, 363);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "ID:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(853, 439);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "Contraseña:";
            // 
            // ID
            // 
            this.ID.Location = new System.Drawing.Point(856, 393);
            this.ID.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ID.Name = "ID";
            this.ID.Size = new System.Drawing.Size(196, 22);
            this.ID.TabIndex = 5;
            // 
            // Contraseña
            // 
            this.Contraseña.Location = new System.Drawing.Point(856, 473);
            this.Contraseña.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Contraseña.Name = "Contraseña";
            this.Contraseña.Size = new System.Drawing.Size(196, 22);
            this.Contraseña.TabIndex = 7;
            // 
            // MaxPuntuacion
            // 
            this.MaxPuntuacion.AutoSize = true;
            this.MaxPuntuacion.Location = new System.Drawing.Point(856, 128);
            this.MaxPuntuacion.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaxPuntuacion.Name = "MaxPuntuacion";
            this.MaxPuntuacion.Size = new System.Drawing.Size(262, 20);
            this.MaxPuntuacion.TabIndex = 8;
            this.MaxPuntuacion.TabStop = true;
            this.MaxPuntuacion.Text = "Dime lo máximos puntos en una partida";
            this.MaxPuntuacion.UseVisualStyleBackColor = true;
            // 
            // JugadorPuntos
            // 
            this.JugadorPuntos.AutoSize = true;
            this.JugadorPuntos.Location = new System.Drawing.Point(856, 168);
            this.JugadorPuntos.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.JugadorPuntos.Name = "JugadorPuntos";
            this.JugadorPuntos.Size = new System.Drawing.Size(220, 20);
            this.JugadorPuntos.TabIndex = 9;
            this.JugadorPuntos.TabStop = true;
            this.JugadorPuntos.Text = "Dime el jugador con más puntos";
            this.JugadorPuntos.UseVisualStyleBackColor = true;
            // 
            // Preguntas
            // 
            this.Preguntas.AutoSize = true;
            this.Preguntas.Location = new System.Drawing.Point(856, 214);
            this.Preguntas.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Preguntas.Name = "Preguntas";
            this.Preguntas.Size = new System.Drawing.Size(310, 20);
            this.Preguntas.TabIndex = 10;
            this.Preguntas.TabStop = true;
            this.Preguntas.Text = "Dime la partida con menos preguntas correctas";
            this.Preguntas.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(856, 314);
            this.button5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(145, 30);
            this.button5.TabIndex = 11;
            this.button5.Text = "ENVIAR";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // listaconectados
            // 
            this.listaconectados.AutoSize = true;
            this.listaconectados.Location = new System.Drawing.Point(856, 263);
            this.listaconectados.Margin = new System.Windows.Forms.Padding(4);
            this.listaconectados.Name = "listaconectados";
            this.listaconectados.Size = new System.Drawing.Size(151, 20);
            this.listaconectados.TabIndex = 12;
            this.listaconectados.TabStop = true;
            this.listaconectados.Text = "Lista de Conectados";
            this.listaconectados.UseVisualStyleBackColor = true;
            this.listaconectados.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.oPCIONESToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1310, 38);
            this.menuStrip1.TabIndex = 13;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // oPCIONESToolStripMenuItem
            // 
            this.oPCIONESToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rEGISTRARMEINICIARSESIÓNToolStripMenuItem,
            this.jUGARPARTIDAToolStripMenuItem,
            this.pETICIÓNToolStripMenuItem});
            this.oPCIONESToolStripMenuItem.Name = "oPCIONESToolStripMenuItem";
            this.oPCIONESToolStripMenuItem.Size = new System.Drawing.Size(93, 34);
            this.oPCIONESToolStripMenuItem.Text = "OPCIONES";
            // 
            // rEGISTRARMEINICIARSESIÓNToolStripMenuItem
            // 
            this.rEGISTRARMEINICIARSESIÓNToolStripMenuItem.Name = "rEGISTRARMEINICIARSESIÓNToolStripMenuItem";
            this.rEGISTRARMEINICIARSESIÓNToolStripMenuItem.Size = new System.Drawing.Size(307, 26);
            this.rEGISTRARMEINICIARSESIÓNToolStripMenuItem.Text = "REGISTRARME / INICIAR SESIÓN";
            this.rEGISTRARMEINICIARSESIÓNToolStripMenuItem.Click += new System.EventHandler(this.rEGISTRARMEINICIARSESIÓNToolStripMenuItem_Click);
            // 
            // jUGARPARTIDAToolStripMenuItem
            // 
            this.jUGARPARTIDAToolStripMenuItem.Name = "jUGARPARTIDAToolStripMenuItem";
            this.jUGARPARTIDAToolStripMenuItem.Size = new System.Drawing.Size(307, 26);
            this.jUGARPARTIDAToolStripMenuItem.Text = "JUGAR PARTIDA";
            this.jUGARPARTIDAToolStripMenuItem.Click += new System.EventHandler(this.jUGARPARTIDAToolStripMenuItem_Click);
            // 
            // pETICIÓNToolStripMenuItem
            // 
            this.pETICIÓNToolStripMenuItem.Name = "pETICIÓNToolStripMenuItem";
            this.pETICIÓNToolStripMenuItem.Size = new System.Drawing.Size(307, 26);
            this.pETICIÓNToolStripMenuItem.Text = "PETICIÓN";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(245, 581);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(143, 62);
            this.button2.TabIndex = 1;
            this.button2.Text = "DESCONECTAR";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(242, 29);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(146, 56);
            this.button1.TabIndex = 0;
            this.button1.Text = "CONECTAR";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1048, 727);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listaconectados);
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
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
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
        private System.Windows.Forms.RadioButton listaconectados;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem oPCIONESToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rEGISTRARMEINICIARSESIÓNToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jUGARPARTIDAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pETICIÓNToolStripMenuItem;
        private System.Windows.Forms.Timer timerAnimación;
        private System.Windows.Forms.Timer timerAnimacion;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
    }
}

