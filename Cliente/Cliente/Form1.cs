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
    
    public partial class Form1 : Form
    {
        string usuario;
        int nForm_p;
        Socket server;
        Thread atender;
        ThreadStart ts;
        int numPartidas = 0;
        private Label labelKahoot;
        private Label labelPreparados;
        private System.Windows.Forms.Timer timerAnimacion2;
        private bool aumentando = true;
        private int maxSize = 75;
        private int minSize = 40;
        private int step = 1; // Reducir la velocidad de la animación
        private bool atendercliente = true;
        //private Thread atender;
        
        //string usuario;
        //Peticion peticion = new Peticion();
        delegate void DelegadoParaEscribir(string[] conectados);
        List<string> invitados = new List<string>();
        List<Partida> partidas = new List<Partida>();
        List<Peticion> formularios = new List<Peticion>();
        List<Partida> formularios2 = new List<Partida>();

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
            while (atendercliente)
            {
               
                    //Recibimos mensaje del servidor
                    byte[] msg2 = new byte[80];
                    server.Receive(msg2);
                    string[] trozos = Encoding.ASCII.GetString(msg2).Split('/');
                    int codigo = Convert.ToInt32(trozos[0]);
                    string mensaje = mensaje = trozos[1].Split('\0')[0];
                    int nform;

                // Peticion peticion = new Peticion(nform,server);

                switch (codigo)
                {
                    case -1:
                        mensaje = trozos[1].Split('\0')[0];
                        mensaje = mensaje.Substring(0, mensaje.Length - 1);
                        string[] conectados = mensaje.Split('&');
                        DelegadoParaEscribir delegado = new DelegadoParaEscribir(ListaConectados);
                        USUARIOS.Invoke(delegado, new object[] { conectados });
                        break;
                    case 0:
                        // Actualizar el Label de la lista de conectados


                        MessageBox.Show("Te has desconectado");

                        break;
                    case 1:
                        // Mostrar la máxima puntuación

                        nform = Convert.ToInt32(trozos[1]);
                        mensaje = trozos[2].Split('\0')[0];
                        formularios[nform].TomaRespuesta1(mensaje);

                        // MessageBox.Show("La máxima puntuación es: " + mensaje);
                        break;
                    case 2:
                        // Mostrar el jugador con más puntos

                        nform = Convert.ToInt32(trozos[1]);
                        mensaje = trozos[2].Split('\0')[0];
                        formularios[nform].TomaRespuesta2(mensaje);

                        //MessageBox.Show("El jugador con más puntos es: " + mensaje);
                        break;
                    case 3:
                        // Mostrar la partida con menos preguntas correctas

                        nform = Convert.ToInt32(trozos[1]);
                        mensaje = trozos[2].Split('\0')[0];
                        formularios[nform].TomaRespuesta3(mensaje);

                        //MessageBox.Show("La partida con menos preguntas correctas es la número: " + mensaje);
                        break;
                    case 4:
                        // Mostrar el resultado del inicio de sesión


                        mensaje = trozos[1].Split('\0')[0];

                        int num99 = 99;

                        if (int.TryParse(mensaje, out int numero_mensaje))
                        {
                            if (numero_mensaje == num99)
                            {
                                MessageBox.Show("Credenciales incorrectas");
                            }
                            else
                            {
                                MessageBox.Show("Inicio de sesión exitoso para:" + mensaje);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Inicio de sesión exitoso para:" + mensaje);
                        }
                        //AgregarValorUsuarios(usuario);
                        usuario = mensaje;
                        break;
                    case 5:
                        // Mostrar el resultado del registro
                        mensaje = trozos[1].Split('\0')[0];

                        MessageBox.Show("Registro exitoso para usuario: " + mensaje);
                        usuario = mensaje;
                        //AgregarValorUsuarios(mensaje);

                        break;
                    case 6: //Respuesta a la peticion de invitacion

                        mensaje = trozos[1].Split('\0')[0];

                        if (Convert.ToInt32(mensaje) == 0)
                        {
                            MessageBox.Show("Invitación hecha correctamente");
                        }
                        else
                            MessageBox.Show("Petición erronea, el usuario se ha desconectado");

                        break;
                    case 7://Notificación de invitacion a una partida

                        Invitacion invitacion = new Invitacion(mensaje);
                        invitacion.ShowDialog();
                        string respuesta = "7/" + invitacion.GetRespuesta() + "/" + mensaje + "\0";
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(respuesta);
                        server.Send(msg);
                        break;
                    case 8:

                        mensaje = trozos[1].Split('\0')[0];
                        string inv = trozos[2].Split('\0')[0];

                        if (Convert.ToInt32(mensaje) == 1)
                        {
                            MessageBox.Show("Invitación aceptada");
                            invitados.Add(inv);
                        }
                        else
                            MessageBox.Show("Invitación rechazada");
                        break;
                    case 9:

                        nform = Convert.ToInt32(trozos[1]);
                        mensaje = trozos[2].Split('\0')[0];
                        string jugadores = trozos[3].Split('\0')[0];
                        Partida f2 = new Partida(Convert.ToInt32(mensaje), server, usuario, ts, jugadores, nform);
                        formularios2.Add(f2);
                        f2.ShowDialog();
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
            labelPreparados.Location = new Point(113, 358); // Centrar el label

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
            if (button1.Text=="CONECTAR")
            {
                int puerto = 50023;
                IPAddress direc = IPAddress.Parse("192.168.56.102");
                IPEndPoint ipep = new IPEndPoint(direc, puerto);

                //Creamos el socket 
                server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    server.Connect(ipep);//Intentamos conectar el socket
                    MessageBox.Show("Conexión establecida correctamente");
                    this.BackColor = Color.FromArgb(220, 255, 220);
                    ts = delegate { AtenderServidor(); };
                    atender = new Thread(ts);
                    atender.Start();
                }
                catch (SocketException ex)
                {
                    //Si hay excepcion imprimimos error y salimos del programa con return 
                    MessageBox.Show("No se ha podido conectar con el servidor");
                    return;
                }
                Registrarse registrarse = new Registrarse(server);
                registrarse.ShowDialog();
                button1.Text="Cambiar de cuenta";
            }
            else
                MessageBox.Show("Debes desconectarte para poder cambiar de cuenta");
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
            button1.Text="CONECTAR";
        }

        private void rEGISTRARMEINICIARSESIÓNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Registrarse registrarse = new Registrarse(server);
            registrarse.ShowDialog();
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

        public void ListaConectados(string[] conectados)
        {
            // Mostrar el DataGridView
            USUARIOS.Visible = true;

            // Establecer la cantidad de columnas y filas
            USUARIOS.ColumnCount = 1;
            USUARIOS.RowCount = conectados.Length;

            // Ocultar los encabezados de las filas y las columnas
            USUARIOS.RowHeadersVisible = false;
            USUARIOS.ColumnHeadersVisible = true;

            // Establecer el encabezado de la columna
            USUARIOS.Columns[0].HeaderText = "CONECTADOS:";

            // Ajustar el tamaño de las columnas y filas para que muestren todo el contenido
            USUARIOS.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            USUARIOS.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            // Agregar los nombres de usuarios a las celdas
            for (int i = 0; i < conectados.Length; i++)
            {
                USUARIOS.Rows[i].Cells[0].Value = conectados[i];
            }
            USUARIOS.Show();
        }

        private void botonInvitar_Click_1(object sender, EventArgs e)
        {
            if (botonInvitar.Text == "Invitar")
            {
                //Iniciamos la recopilacion de invitados
                MessageBox.Show("Haz click sobre los jugadores que quieras invitar");
                botonInvitar.Text="Invitando";
                invitados = new List<string>();
            }
            else
            {
                botonInvitar.Text = "Invitar";

                //si no se clica a nadie no hace nada y vuelve al estado inicial
                if (invitado != null)
                {
                    //Construimos el mensaje
                    string mensaje = "6/";

                    mensaje=mensaje+invitado+"/"+usuario;
                }
            }
        }

        private void USUARIOS_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            //Solo funciona cuando se habilita la funcion de invitar con el boton invitarButton
            if ((botonInvitar.Text == "Invitando"))
            {
                 invitado = USUARIOS.CurrentCell.Value.ToString();
               
                //Comprovamos que no somos nosotros mismos
                if (invitado == usuario)
                     MessageBox.Show("No te puedes autoinvitar1");
                else
                {
                    MessageBox.Show("Has enviado invitacion a " + invitado);  
                }
            }
            USUARIOS.SelectAll();
        }
        string invitado;
        private void USUARIOS_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            //Solo funciona cuando se habilita la funcion de invitar con el boton invitarButton
            if ((botonInvitar.Text == "Invitando") && (invitados.Count <= 3))
            {
                invitado = USUARIOS.CurrentCell.Value.ToString();
                //Comprovamos que no somos nosotros mismos
                if (invitado == usuario)
                {
                    MessageBox.Show("No te puedes autoinvitar2");
                }
                else
                {
                    try
                    { 
                        string mensaje = "6/" + invitado.ToString() + "/" + usuario.ToString();
                        byte[] msg = Encoding.ASCII.GetBytes(mensaje);
                        server.Send(msg); 
                    }
                    catch
                    {
                        MessageBox.Show("mal");
                    }
                }
            }
            USUARIOS.SelectAll();
        }
        
        private void EmpezarPart_Click(object sender, EventArgs e)
        {
            int cont = formularios2.Count();
            invitados.Add(usuario);
           
            if (invitados == null)
            {
                MessageBox.Show("Invita a tus amigos para comenzar a jugar");
            }
            else
            {
                DialogResult resultado = MessageBox.Show("Quieres invitar a alguien más?", "Empezar", MessageBoxButtons.YesNo);
                if (resultado == DialogResult.No)
                {
                    string mensaje = "8/"+ cont.ToString()+ "/";
                    int i = 0;
                    while (i<invitados.Count())
                    {
                        mensaje=mensaje+"&"+invitados[i].ToString();
                        i++;
                    }
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);
                }
            }
        }
    }
}
