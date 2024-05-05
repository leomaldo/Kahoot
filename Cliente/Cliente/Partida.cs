using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace Cliente
{
    public partial class Partida : Form
    {
        int identificador;

        Socket server;
        string usuario;
        //public Partida(int identificador, Socket server, string usuario)
        public Partida(int identificador, Socket server, string usuario)
        {
            this.identificador = identificador;
            InitializeComponent();
            this.BackColor = Color.FromArgb(220, 255, 220);

            ConfigurarOpcionesDeRespuesta();
            this.server = server;
            this.server = server;
            this.usuario = usuario;

        }
        private void ConfigurarOpcionesDeRespuesta()
        {
            Chat.Height = 200;
            // Configura cada panel como una opción de respuesta
            panelRespuesta1.Click += OpcionDeRespuesta_Click;
            panelRespuesta2.Click += OpcionDeRespuesta_Click;
            panelRespuesta3.Click += OpcionDeRespuesta_Click;
            panelRespuesta4.Click += OpcionDeRespuesta_Click;

            // Asigna un color diferente a cada panel
            // Asigna un color diferente a cada panel
            panelRespuesta1.BackColor = Color.Blue;
            panelRespuesta2.BackColor = Color.Green;
            panelRespuesta3.BackColor = Color.Red;
            panelRespuesta4.BackColor = Color.Purple;

            // Asigna texto a cada panel para representar la respuesta
            Label labelRespuesta1 = new Label { Text = "Respuesta 1", AutoSize = false, TextAlign = ContentAlignment.MiddleCenter, Font = new Font(Font.FontFamily, 10, FontStyle.Bold) };
            labelRespuesta1.Size = panelRespuesta1.Size;
            labelRespuesta1.Dock = DockStyle.Fill;
            panelRespuesta1.Controls.Add(labelRespuesta1);

            Label labelRespuesta2 = new Label { Text = "Respuesta 2", AutoSize = false, TextAlign = ContentAlignment.MiddleCenter, Font = new Font(Font.FontFamily, 10, FontStyle.Bold) };
            labelRespuesta2.Size = panelRespuesta2.Size;
            labelRespuesta2.Dock = DockStyle.Fill;
            panelRespuesta2.Controls.Add(labelRespuesta2);

            Label labelRespuesta3 = new Label { Text = "Respuesta 3", AutoSize = false, TextAlign = ContentAlignment.MiddleCenter, Font = new Font(Font.FontFamily, 10, FontStyle.Bold) };
            labelRespuesta3.Size = panelRespuesta3.Size;
            labelRespuesta3.Dock = DockStyle.Fill;
            panelRespuesta3.Controls.Add(labelRespuesta3);

            Label labelRespuesta4 = new Label { Text = "Respuesta 4", AutoSize = false, TextAlign = ContentAlignment.MiddleCenter, Font = new Font(Font.FontFamily, 10, FontStyle.Bold) };
            labelRespuesta4.Size = panelRespuesta4.Size;
            labelRespuesta4.Dock = DockStyle.Fill;
            panelRespuesta4.Controls.Add(labelRespuesta4);


        }

        private void OpcionDeRespuesta_Click(object sender, EventArgs e)
        {
            // Cuando se hace clic en un panel, determina cuál fue la respuesta seleccionada
            Panel panelClicado = sender as Panel;
            string respuestaSeleccionada = panelClicado?.Controls[0]?.Text;

            // Aquí puedes hacer lo que necesites con la respuesta seleccionada
            MessageBox.Show($"Seleccionaste: {respuestaSeleccionada}");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Partida_Load(object sender, EventArgs e)
        {

        }

        private void panelRespuesta2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelRespuesta1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void boton_enviar_Click(object sender, EventArgs e)
        {
            string mensaje_cliente = textBox1.Text;
            string mensaje = "10/" + mensaje_cliente.ToString() + "/" + usuario.ToString();
            // Enviamos al servidor el mensaje que ha escrito un cliente
            byte[] msg = Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }

        private void Chat_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
