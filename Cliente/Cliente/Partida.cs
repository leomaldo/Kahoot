using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Globalization;

namespace Cliente
{
    public partial class Partida : Form
    {
        int identificador;

        Socket server;
        string usuario;
        List<string> mensajes = new List<string>();
        Thread atender;
        List<Partida> partidas=new List<Partida>();
        public int GetId()
        {
            return this.identificador;
        }
        public void SetNuevoMensaje (string mensaje)
        {
             
            mensajes.Add(mensaje);
        }
        public Partida(int identificador, Socket server, string usuario,ThreadStart ts, List<Partida> partidaList)
        {
            this.identificador = identificador;
            InitializeComponent();
            this.BackColor = Color.FromArgb(220, 255, 220);
            this.partidas= partidaList;
            ConfigurarOpcionesDeRespuesta();
            //this.server = server;
            this.server = server;
            this.usuario = usuario;

            ts = delegate { AtenderServidor(); };
            atender = new Thread(ts);
            atender.Start();

        }
        public void AtenderServidor()
        {

            while (true)
            {
                //try
                //{
                    //Recibimos mensaje del servidor
                    byte[] msg2 = new byte[80];
                    server.Receive(msg2);
                    string[] trozos = Encoding.ASCII.GetString(msg2).Split('/');
                    int codigo = Convert.ToInt32(trozos[0]);
                    string mensaje;


                    // MessageBox.Show("El valor de usuario es: " + usuario);

                    Peticion peticion = new Peticion(server);

                    if (codigo==10)
                    {
                        
                            mensaje = trozos[1].Split('\0')[0];
                            //string usuario = trozos[2].Split('\0')[0];
                            string  usuarioEnvia = trozos[2].Split('\0')[0];
                            mensaje = usuarioEnvia + " : " + mensaje;
                            int id = Convert.ToInt32(trozos[3].Split('\0')[0]);
                            bool encontrado = false;
                            int i = 0;
                            while (!encontrado && i < partidas.Count())
                            {
                                if (id == partidas[i].GetId())
                                {
                                    encontrado = true;
                                }
                                else
                                    i++;
                            }
                            if (encontrado == true)
                            {
                                //comprobacion
                                // estaba puesto justo debajo "partida" y he pusto "partidas" pk asi me dejaba. No entiendo mucho la diferencia entre partidas y partida pero creo qeu es con S la buena
                                partidas[i].SetNuevoMensaje(mensaje);
                                Escribirmensaje(mensaje);
                       
                            }
                            if (encontrado == false)
                            {
                                MessageBox.Show("No ha encontrado el ID de la partida ");
                                partidas[i].SetNuevoMensaje(mensaje);

                            }
                     
                            

                    }
            }





        }

        public void Escribirmensaje(string mensaje)
        {
            if (Chat.InvokeRequired)
            {
                // Si estamos en un hilo diferente al hilo de la UI, invocamos el método nuevamente en el hilo principal.
                Chat.Invoke(new Action(() => Escribirmensaje(mensaje)));
            }
            else
            {
                // Estamos en el hilo de la UI, podemos agregar el mensaje directamente.
                Chat.Items.Add(mensaje);
            }
        }
        private void AgregarMensajeConColor(string mensaje)
        {
            int indiceSeparador = mensaje.IndexOf(':');
            if (indiceSeparador != -1)
            {
                string textoAntesDelSeparador = mensaje.Substring(0, indiceSeparador);
                string textoDespuesDelSeparador = mensaje.Substring(indiceSeparador + 1);
                if (textoAntesDelSeparador==usuario)
                {
                    // Agregar el texto antes del separador con un color específico
                    Chat.Items.Add(new MensajeConColor(textoAntesDelSeparador + ":", Color.Red));

                }
                else
                {
                    // Agregar el texto antes del separador con un color específico
                    Chat.Items.Add(new MensajeConColor(textoAntesDelSeparador + ":", Color.Blue));
                }
                // Agregar el texto después del separador sin color
                Chat.Items.Add(new MensajeConColor(textoDespuesDelSeparador, Color.Black));
            }
            else
            {
                // Si no hay separador ":" en el mensaje, simplemente agregar el mensaje completo en negro
                Chat.Items.Add(new MensajeConColor(mensaje, Color.Black));
            }
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
            string mensaje = "10/" + mensaje_cliente.ToString() + "/" + usuario.ToString() + "/" + identificador.ToString();
            // Enviamos al servidor el mensaje que ha escrito un cliente
            byte[] msg = Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            string chat = usuario + " : " + mensaje_cliente;
            mensajes.Add(chat);
        }

        private void Chat_TextChanged(object sender, EventArgs e)
        {
           
        }
    }
    public class MensajeConColor
    {
        public string Texto { get; set; }
        public Color ColorTexto { get; set; }

        public MensajeConColor(string texto, Color colorTexto)
        {
            Texto = texto;
            ColorTexto = colorTexto;
        }

        public override string ToString()
        {
            return Texto;
        }
    }
}
