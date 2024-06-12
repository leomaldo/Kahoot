using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Cliente
{
    public partial class Baja : Form
    {
        Socket server;
        public Baja(Socket server)
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(180, 255, 220);
            this.server = server;
        }

        private void Baja_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string mensaje = "14/" + textBox1.Text + "/" + textBox2.Text;
            // Enviamos al servidor el nombre y contraseña tecleados
            byte[] msg = Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }
    }
}
