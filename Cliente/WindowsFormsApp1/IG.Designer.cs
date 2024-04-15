namespace WindowsFormsApp1
{
    partial class IG
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.oPCIONESToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rEGISTRARMEINICIARSESIÓNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jUGARPARTIDAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pETICIÓNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Location = new System.Drawing.Point(0, 28);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(994, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuStrip2
            // 
            this.menuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.oPCIONESToolStripMenuItem});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(994, 28);
            this.menuStrip2.TabIndex = 1;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // oPCIONESToolStripMenuItem
            // 
            this.oPCIONESToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rEGISTRARMEINICIARSESIÓNToolStripMenuItem,
            this.jUGARPARTIDAToolStripMenuItem,
            this.pETICIÓNToolStripMenuItem});
            this.oPCIONESToolStripMenuItem.Name = "oPCIONESToolStripMenuItem";
            this.oPCIONESToolStripMenuItem.Size = new System.Drawing.Size(93, 24);
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
            // 
            // pETICIÓNToolStripMenuItem
            // 
            this.pETICIÓNToolStripMenuItem.Name = "pETICIÓNToolStripMenuItem";
            this.pETICIÓNToolStripMenuItem.Size = new System.Drawing.Size(307, 26);
            this.pETICIÓNToolStripMenuItem.Text = "PETICIÓN";
            // 
            // IG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(994, 632);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.menuStrip2);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "IG";
            this.Text = "Form1";
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem oPCIONESToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rEGISTRARMEINICIARSESIÓNToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jUGARPARTIDAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pETICIÓNToolStripMenuItem;
    }
}

