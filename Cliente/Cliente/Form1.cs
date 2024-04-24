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
        Thread atender;

        private Label labelKahoot;
        private Label labelPreparados;
        private System.Windows.Forms.Timer timerAnimacion2;
        private bool aumentando = true;
        private int maxSize = 75;
        private int minSize = 40;
        private int step = 1; // Reducir la velocidad de la animación
      
        //private Thread atender;

        List<Peticion> formularios = new List<Peticion>();
       
        public Form1()
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(220, 255, 220);
            InicializarComponentes();

           
            //Registrarse registrarse = new Registrarse(server);
            //registrarse.ShowDialog
        }

        private void AtenderServidor()
        {
            while (true)
            {
                //Recibimos mensaje del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                string[] trozos = Encoding.ASCII.GetString(msg2).Split('/');
                int codigo = Convert.ToInt32(trozos[0]);
                string mensaje;

                int nform;

                    // Mostrar el mensaje en función de su identificador
                    switch (codigo)
                    {
                        case 0:
                        // Actualizar el Label de la lista de conectados
                        mensaje = trozos[1].Split('\0')[0];

                        //Haz tu lo que no me dejas hacer a mi
                        contLbl.Invoke(new Action(() =>
                        {
                            contLbl.Text = mensaje;
                        }));
                        break;
                        case 1:
                        // Mostrar la máxima puntuación
                        nform = Convert.ToInt32(trozos[1]);
                        mensaje = trozos[2].Split('\0')[0];
                        formularios[nform].TomaRespuesta1(mensaje);

                        break;
                        case 2:
                        // Mostrar el jugador con más puntos
                        nform = Convert.ToInt32(trozos[1]);
                        mensaje = trozos[2].Split('\0')[0];
                        formularios[nform].TomaRespuesta2(mensaje);
                            break;
                        case 3:
                        // Mostrar la partida con menos preguntas correctas
                        nform = Convert.ToInt32(trozos[1]);
                        mensaje = trozos[2].Split('\0')[0];
                        formularios[nform].TomaRespuesta3(mensaje);

                        break;
                        case 4:
                        // Mostrar el resultado del inicio de sesión
                        nform = Convert.ToInt32(trozos[1]);
                        mensaje = trozos[2].Split('\0')[0];
                        MessageBox.Show(mensaje);
                       
                            break;
                        case 5:
                        // Mostrar el resultado del registro
                        nform = Convert.ToInt32(trozos[1]);
                        mensaje = trozos[2].Split('\0')[0];
                        MessageBox.Show(mensaje);

                        break;
                      
                        default:
                        // Mostrar un mensaje de error para identificadores desconocidos
                        MessageBox.Show("Mensaje no reconocido del servidor: ");
                        break;

                    }
            }
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
            int puerto = 50020;
            IPAddress direc = IPAddress.Parse("10.4.119.5");
            IPEndPoint ipep = new IPEndPoint(direc, puerto);


            //Creamos el socket 
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);//Intentamos conectar el socket
                this.BackColor = Color.Cyan;
                MessageBox.Show("Conexión establecida correctamente");
                ThreadStart ts = delegate { AtenderServidor(); };
                atender = new Thread(ts);
                atender.Start();
            }
            catch (SocketException ex)
            {
                //Si hay excepcion imprimimos error y salimos del programa con return 
                MessageBox.Show("No se ha podido conectar con el servidor");
                return;
            }
        }

       
        private void button2_Click(object sender, EventArgs e)
        {
            //Mensaje de desconexión
            string mensaje = "0/";

            byte[] msg = Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            // Nos desconectamos
            atender.Abort();
            this.BackColor = Color.Gray;
            server.Shutdown(SocketShutdown.Both);
            server.Close();
        }

        private void rEGISTRARMEINICIARSESIÓNToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Registrarse registrarse = new Registrarse(server);
            registrarse.ShowDialog(); 
        }

      
        private void jUGARPARTIDAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Partida partida = new Partida();
            partida.ShowDialog();
        }

      
        private void pETICIÓNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThreadStart ts = delegate { PonerEnMarchaFormulario(); };
            Thread T = new Thread(ts);
            T.Start();

        }
        private void PonerEnMarchaFormulario()
        {
            int cont = formularios.Count;
            Peticion f = new Peticion(cont, server);
            formularios.Add(f);
            f.ShowDialog();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Mensaje de desconexión
            string mensaje = "0/";

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            // Nos desconectamos
            atender.Abort();
            this.BackColor = Color.Gray;
            server.Shutdown(SocketShutdown.Both);
            server.Close();

        }

       
    }
}
