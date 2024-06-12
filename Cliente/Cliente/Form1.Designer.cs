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
            this.pETICIÓNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timerAnimacion = new System.Windows.Forms.Timer(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.USUARIOS = new System.Windows.Forms.DataGridView();
            this.botonInvitar = new System.Windows.Forms.Button();
            this.EmpezarPart = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.USUARIOS)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.oPCIONESToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(6, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(1048, 36);
            this.menuStrip1.TabIndex = 13;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // oPCIONESToolStripMenuItem
            // 
            this.oPCIONESToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pETICIÓNToolStripMenuItem});
            this.oPCIONESToolStripMenuItem.Name = "oPCIONESToolStripMenuItem";
            this.oPCIONESToolStripMenuItem.Size = new System.Drawing.Size(114, 30);
            this.oPCIONESToolStripMenuItem.Text = "OPCIONES";
            // 
            // pETICIÓNToolStripMenuItem
            // 
            this.pETICIÓNToolStripMenuItem.Name = "pETICIÓNToolStripMenuItem";
            this.pETICIÓNToolStripMenuItem.Size = new System.Drawing.Size(190, 34);
            this.pETICIÓNToolStripMenuItem.Text = "PETICIÓN";
            this.pETICIÓNToolStripMenuItem.Click += new System.EventHandler(this.pETICIÓNToolStripMenuItem_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(276, 726);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(160, 77);
            this.button2.TabIndex = 1;
            this.button2.Text = "DESCONECTAR";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(252, 55);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(165, 71);
            this.button1.TabIndex = 0;
            this.button1.Text = "CONECTAR";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // USUARIOS
            // 
            this.USUARIOS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.USUARIOS.Location = new System.Drawing.Point(723, 437);
            this.USUARIOS.Name = "USUARIOS";
            this.USUARIOS.RowHeadersWidth = 51;
            this.USUARIOS.RowTemplate.Height = 24;
            this.USUARIOS.Size = new System.Drawing.Size(246, 294);
            this.USUARIOS.TabIndex = 15;
            this.USUARIOS.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.USUARIOS_CellClick_1);
            this.USUARIOS.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.USUARIOS_CellContentClick_1);
            // 
            // botonInvitar
            // 
            this.botonInvitar.Location = new System.Drawing.Point(786, 751);
            this.botonInvitar.Name = "botonInvitar";
            this.botonInvitar.Size = new System.Drawing.Size(102, 52);
            this.botonInvitar.TabIndex = 16;
            this.botonInvitar.Text = "Invitar";
            this.botonInvitar.UseVisualStyleBackColor = true;
            this.botonInvitar.Click += new System.EventHandler(this.botonInvitar_Click_1);
            // 
            // EmpezarPart
            // 
            this.EmpezarPart.Location = new System.Drawing.Point(741, 825);
            this.EmpezarPart.Name = "EmpezarPart";
            this.EmpezarPart.Size = new System.Drawing.Size(186, 48);
            this.EmpezarPart.TabIndex = 17;
            this.EmpezarPart.Text = "Empezar Partida";
            this.EmpezarPart.UseVisualStyleBackColor = true;
            this.EmpezarPart.Click += new System.EventHandler(this.EmpezarPart_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(733, 133);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(219, 66);
            this.button3.TabIndex = 18;
            this.button3.Text = "DARME DE BAJA";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1048, 909);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.EmpezarPart);
            this.Controls.Add(this.botonInvitar);
            this.Controls.Add(this.USUARIOS);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.USUARIOS)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem oPCIONESToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pETICIÓNToolStripMenuItem;
        private System.Windows.Forms.Timer timerAnimación;
        private System.Windows.Forms.Timer timerAnimacion;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView USUARIOS;
        private System.Windows.Forms.Button botonInvitar;
        private System.Windows.Forms.Button EmpezarPart;
        private System.Windows.Forms.Button button3;
    }
}

