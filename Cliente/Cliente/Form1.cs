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

namespace Cliente
{
    public partial class Form1 : Form
    {
        Socket server;
        private Label labelKahoot;
        private Label labelPreparados;
        private System.Windows.Forms.Timer timerAnimacion2;
        private bool aumentando = true;
        private int maxSize = 75;
        private int minSize = 40;
        private int step = 1; // Reducir la velocidad de la animación

        public Form1()
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(220, 255, 220);
            InicializarComponentes();

            Registrarse registrarse = new Registrarse();
            registrarse.ShowDialog();
        }

        private void InicializarComponentes()
        {
            // Crear y configurar el label "KAHOOT"
            labelKahoot = new Label
            {
                Text = "KAHOOT",
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Verdana", minSize, FontStyle.Bold),
                ForeColor = Color.Blue
            };
            labelKahoot.Location = new Point(26, 157); // Centrar el label

            // Crear y configurar el label de la frase "¿Estáis preparados?"
            labelPreparados = new Label
            {
                Text = "¿Estáis preparados?",
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Arial", 20, FontStyle.Bold),
                ForeColor = Color.Purple
            };
            labelPreparados.Location = new Point(113,358); // Centrar el label

            // Agregar los labels al formulario
            this.Controls.Add(labelKahoot);
            this.Controls.Add(labelPreparados);

            // Crear y configurar el temporizador
            timerAnimacion = new System.Windows.Forms.Timer();
            timerAnimacion.Interval = 50; // Intervalo de tiempo más largo para una animación más lenta
            timerAnimacion.Tick += TimerAnimacion_Tick;
            timerAnimacion.Start();
        }

        private void TimerAnimacion_Tick(object sender, EventArgs e)
        {
            // Ajustar el tamaño del texto en cada tick del temporizador
            if (aumentando)
            {
                if (labelKahoot.Font.Size < maxSize)
                {
                    labelKahoot.Font = new Font(labelKahoot.Font.FontFamily, labelKahoot.Font.Size + step, FontStyle.Bold);
                }
                else
                {
                    aumentando = false;
                }
            }
            else
            {
                if (labelKahoot.Font.Size > minSize)
                {
                    labelKahoot.Font = new Font(labelKahoot.Font.FontFamily, labelKahoot.Font.Size - step, FontStyle.Bold);
                }
                else
                {
                    aumentando = true;
                }
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            IPAddress direc = IPAddress.Parse("192.168.56.102");
            IPEndPoint ipep = new IPEndPoint(direc, 9052);


            //Creamos el socket 
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);//Intentamos conectar el socket
                this.BackColor = Color.Cyan;
                MessageBox.Show("Conexión establecida correctamente");

            }
            catch (SocketException ex)
            {
                //Si hay excepcion imprimimos error y salimos del programa con return 
                MessageBox.Show("No se ha podido conectar con el servidor");
                return;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (MaxPuntuacion.Checked)
            {
                string mensaje = "1/";
                // Enviamos al servidor el codigo
                byte[] msg = Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                //Recibimos la respuesta del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                MessageBox.Show("La máxima puntuación es: " + mensaje);
            }
            else if (JugadorPuntos.Checked)
            {
                string mensaje = "2/";
                // Enviamos al servidor el codigo
                byte[] msg = Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                //Recibimos la respuesta del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                MessageBox.Show("El jugador con más puntos es:" + mensaje);

            }
            else if (Preguntas.Checked)
            {
                string mensaje = "3/";
                // Enviamos al servidor el nombre tecleado
                byte[] msg = Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                //Recibimos la respuesta del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                MessageBox.Show("La partida con menos preguntas correctas es la número:" + mensaje);
            }
            else if (listaconectados.Checked)
            {
                string mensaje = "6/";
                // Enviamos al servidor el nombre tecleado
                byte[] msg = Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                //Recibimos la respuesta del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                MessageBox.Show("La lista de conectados es:" + mensaje);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Login
            string mensaje = "4/" + ID.Text + "/" + Contraseña.Text;
            // Enviamos al servidor el nombre y contraseña tecleados
            byte[] msg = Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            //Recibimos la respuesta del servidor de si se ha entrado o no
            byte[] msg2 = new byte[80];
            server.Receive(msg2);
            mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
            MessageBox.Show(mensaje);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Registro
            string mensaje = "5/" + ID.Text + "/" + Contraseña.Text;
            // Enviamos al servidor el nombre y contraseña tecleados
            byte[] msg = Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            //Recibimos la respuesta del servidor de si se ha creado correctamente o no
            byte[] msg2 = new byte[80];
            server.Receive(msg2);
            mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
            MessageBox.Show(mensaje);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Mensaje de desconexión
            string mensaje = "0/";

            byte[] msg = Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            // Nos desconectamos
            this.BackColor = Color.Gray;
            server.Shutdown(SocketShutdown.Both);
            server.Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void oPCIONESToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void rEGISTRARMEINICIARSESIÓNToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Registrarse registrarse = new Registrarse();
            registrarse.ShowDialog(); 
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void jUGARPARTIDAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Partida partida = new Partida();
            partida.ShowDialog();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }
    }
}
