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
    public partial class Peticion : Form
    {
        int nForm;
        Socket server;
        public Peticion(int nForm, Socket server)
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(180, 255, 220);
            this.nForm = nForm;
            this.server = server;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (MaxPuntuacion.Checked)
            {
                string mensaje = "1/";
                // Enviamos al servidor el codigo
                byte[] msg = Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

            }
            else if (JugadorPuntos.Checked)
            {
                string mensaje = "2/";
                // Enviamos al servidor el codigo
                byte[] msg = Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

            }
            else if (Preguntas.Checked)
            {
                string mensaje = "3/";
                // Enviamos al servidor el nombre tecleado
                byte[] msg = Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            else if (listaconectados.Checked)
            {
                string mensaje = "6/";
                // Enviamos al servidor el nombre tecleado
                byte[] msg = Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

            }
        }

        private void Peticion_Load(object sender, EventArgs e) 
        {
            numForm.Text = nForm.ToString();

        }
        public void TomaRespuesta1(string mensaje)
        {
            MessageBox.Show("La máxima puntuación es: " + mensaje);
        }
        public void TomaRespuesta2(string mensaje)
        {
            MessageBox.Show("El jugador con más puntos es: " + mensaje);
        }
        public void TomaRespuesta3(string mensaje)
        {
            MessageBox.Show("La partida con menos preguntas correctas es la número: " + mensaje);

        }
        public void TomaRespuesta4(string mensaje)
        {
            MessageBox.Show(mensaje);
        }
    }
}
