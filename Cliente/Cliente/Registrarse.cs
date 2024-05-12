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

namespace Cliente
{
    public partial class Registrarse : Form
    {
        Socket server;
        public Registrarse(Socket server)
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(180, 255, 220);
            this.server= server;
        }  
      
        private void Registrarme_Click(object sender, EventArgs e)
        {
            //Registro
            string mensaje = "5/" + textBox1.Text + "/" + textBox2.Text;
            // Enviamos al servidor el nombre y contraseña tecleados
            byte[] msg = Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }

        private void Iniciarsesion_Click(object sender, EventArgs e)
        {
            //Login
            string mensaje = "4/" + textBox1.Text + "/" + textBox2.Text;
            // Enviamos al servidor el nombre y contraseña tecleados
            byte[] msg = Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }

        private void Registrarse_Load(object sender, EventArgs e)
        {

        }
    }
}
