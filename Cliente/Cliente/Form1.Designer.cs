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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.oPCIONESToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rEGISTRARMEINICIARSESIÓNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jUGARPARTIDAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pETICIÓNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timerAnimacion = new System.Windows.Forms.Timer(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.contLbl = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.oPCIONESToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1048, 33);
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
            this.oPCIONESToolStripMenuItem.Size = new System.Drawing.Size(114, 29);
            this.oPCIONESToolStripMenuItem.Text = "OPCIONES";
            // 
            // rEGISTRARMEINICIARSESIÓNToolStripMenuItem
            // 
            this.rEGISTRARMEINICIARSESIÓNToolStripMenuItem.Name = "rEGISTRARMEINICIARSESIÓNToolStripMenuItem";
            this.rEGISTRARMEINICIARSESIÓNToolStripMenuItem.Size = new System.Drawing.Size(374, 34);
            this.rEGISTRARMEINICIARSESIÓNToolStripMenuItem.Text = "REGISTRARME / INICIAR SESIÓN";
            this.rEGISTRARMEINICIARSESIÓNToolStripMenuItem.Click += new System.EventHandler(this.rEGISTRARMEINICIARSESIÓNToolStripMenuItem_Click);
            // 
            // jUGARPARTIDAToolStripMenuItem
            // 
            this.jUGARPARTIDAToolStripMenuItem.Name = "jUGARPARTIDAToolStripMenuItem";
            this.jUGARPARTIDAToolStripMenuItem.Size = new System.Drawing.Size(374, 34);
            this.jUGARPARTIDAToolStripMenuItem.Text = "JUGAR PARTIDA";
            this.jUGARPARTIDAToolStripMenuItem.Click += new System.EventHandler(this.jUGARPARTIDAToolStripMenuItem_Click);
            // 
            // pETICIÓNToolStripMenuItem
            // 
            this.pETICIÓNToolStripMenuItem.Name = "pETICIÓNToolStripMenuItem";
            this.pETICIÓNToolStripMenuItem.Size = new System.Drawing.Size(374, 34);
            this.pETICIÓNToolStripMenuItem.Text = "PETICIÓN";
            this.pETICIÓNToolStripMenuItem.Click += new System.EventHandler(this.pETICIÓNToolStripMenuItem_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(276, 726);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(161, 78);
            this.button2.TabIndex = 1;
            this.button2.Text = "DESCONECTAR";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(252, 55);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(164, 70);
            this.button1.TabIndex = 0;
            this.button1.Text = "CONECTAR";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // contLbl
            // 
            this.contLbl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.contLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.contLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contLbl.Location = new System.Drawing.Point(698, 489);
            this.contLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.contLbl.Name = "contLbl";
            this.contLbl.Size = new System.Drawing.Size(221, 141);
            this.contLbl.TabIndex = 14;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1048, 909);
            this.Controls.Add(this.contLbl);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
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
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem oPCIONESToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rEGISTRARMEINICIARSESIÓNToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jUGARPARTIDAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pETICIÓNToolStripMenuItem;
        private System.Windows.Forms.Timer timerAnimación;
        private System.Windows.Forms.Timer timerAnimacion;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label contLbl;
    }
}

