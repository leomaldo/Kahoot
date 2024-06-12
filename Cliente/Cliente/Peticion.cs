using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace Cliente
{
    public partial class Peticion : Form
    {
        int nForm;
        Socket server;
        string usuario;
        string fecha1;
        string fecha2;
        
        
        public Peticion(int nForm,Socket server, string usuario)
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(180, 255, 220);
            this.nForm = nForm;
            this.server = server;
            this.usuario = usuario;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show( usuario);
            if (MaxPuntuacion.Checked)
            {

                string mensaje = "1/"+ nForm.ToString();
                // Enviamos al servidor el codigo
                byte[] msg = Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                // Mostrar la máxima puntuación
              

            }
            else if (JugadorPuntos.Checked)
            {
                string mensaje = "2/" + nForm.ToString();
                // Enviamos al servidor el codigo
                byte[] msg = Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
               
            }
            else if (Preguntas.Checked)
            {
                string mensaje = "3/" + nForm.ToString();
                // Enviamos al servidor el nombre tecleado
                byte[] msg = Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
               
            }
            else if(preg_4.Checked)
            {
                string mensaje = "15/" + usuario.ToString() + "/" + nForm.ToString();
                // Enviamos al servidor el nombre tecleado
                byte[] msg = Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            else if (preg_5.Checked)
            {
                string mensaje = "16/" + usuario.ToString() + "/" +  textBox1.Text + "/" + nForm.ToString();
                // Enviamos al servidor el nombre tecleado
                byte[] msg = Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            else if (preg_6.Checked)
            {
                string mensaje = "17/" + usuario.ToString() + "/" + fecha1 + "/" + fecha2 + "/" + nForm.ToString();
                // Enviamos al servidor el nombre tecleado
                byte[] msg = Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }


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

        private void Peticion_Load(object sender, EventArgs e)
        {
            numForm.Text = nForm.ToString();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime fecha_1 = dateTimePicker1.Value;

            // MessageBox.Show("fecha: " + fecha.ToString("yyyy-mm-dd"));
            //version  12/6--que me ha pasado el moha
            //LOGUARDO

            fecha1 = fecha_1.ToString("yyyy-MM-dd");
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            DateTime fecha_2 = dateTimePicker2.Value;

            // MessageBox.Show("fecha: " + fecha.ToString("yyyy-mm-dd"));

            fecha2 = fecha_2.ToString("yyyy-MM-dd");
        }
    }
}
