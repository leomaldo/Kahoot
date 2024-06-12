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
using System.Reflection.Emit;

namespace Cliente
{
    public partial class Partida : Form
    {
        int identificador;
        int nForm;
        Socket server;
        string usuario;
        List<string> mensajes = new List<string>();
        Thread atender;
        List<Partida> partidas=new List<Partida>();
        string jugadores;
        string puntosForms=null;
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        int tiempoRestante = 30;
        int puntos = 0;
        // declaramos las preguntas y respuestas como variables globales.
        string pregunta_db;
        string respuesta_correcta ;
        string resp_inc_1;
        string resp_inc_2;
        string resp_inc_3;
        bool podercontestar = true;
        bool host = false;
        bool seguirpartida = true;
        // Crear una instancia de la clase Random
        Random random = new Random();


        //int variable global para asignar el valor de random para ver si se selecciona la respuesta correcta
        int rectificador;




        System.Windows.Forms.Label labelRespuesta1 = new System.Windows.Forms.Label { Text = "", AutoSize = false, TextAlign = ContentAlignment.MiddleCenter };
        System.Windows.Forms.Label labelRespuesta2 = new System.Windows.Forms.Label { Text = "", AutoSize = false, TextAlign = ContentAlignment.MiddleCenter };
        System.Windows.Forms.Label labelRespuesta3 = new System.Windows.Forms.Label { Text = "", AutoSize = false, TextAlign = ContentAlignment.MiddleCenter };
        System.Windows.Forms.Label labelRespuesta4 = new System.Windows.Forms.Label { Text = "", AutoSize = false, TextAlign = ContentAlignment.MiddleCenter };
        System.Windows.Forms.Label labelPregunta = new System.Windows.Forms.Label { Text = "", AutoSize = false, TextAlign = ContentAlignment.MiddleCenter };



        public int GetId()
        {
            return this.identificador;
        }
        public void SetNuevoMensaje (string mensaje)
        {
             
            mensajes.Add(mensaje);
        }
        public Partida (int nForm, Socket server)
        {
            this.nForm = nForm;
            this.server = server;
        }
        public Partida(int identificador, string usuario, string jugadores, Socket server, string Host)
        {
            this.jugadores= jugadores;
            InitializeComponent();
            this.identificador = identificador;
            Jugadores FormJugadores=new Jugadores(this.identificador,jugadores,this.puntosForms,seguirpartida);
            FormJugadores.ShowDialog();
            this.BackColor = Color.FromArgb(220, 255, 220);
            if (Host==usuario)
            {
                this.host=true;
            }
           
          
            timer.Interval = 1000; // 1000 ms = 1 segundo
            timer.Tick += Timer_Tick;
            timer.Start(); // Iniciar el temporizador
            ConfigurarOpcionesDeRespuesta();
            //this.server = server;
          
            this.usuario = usuario;
            this.server= server;
            Chat.Height = 200;
          

            labelTiempoRestante.ForeColor = Color.Purple;



            //aqui queremos preguntar por una pregunta cualquiera a la base de datos
            //string mensaje_cliente = textBox1.Text;
            // Generar un número aleatorio entre 0 y 10
            //int numero_random_primera_preg = random.Next(1, 4);
            //string mensaje = "11/" + numero_random_primera_preg + identificador.ToString(); 
            //// Enviamos al servidor el mensaje que ha escrito un cliente
            //byte[] msg = Encoding.ASCII.GetBytes(mensaje);
            //server.Send(msg);
            

        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            tiempoRestante--; // Decrementar el tiempo restante
            if (tiempoRestante <= 0)
            {
                // Detener el temporizador
                timer.Stop();
                string mensaje13 = "13/"+ identificador.ToString(); ;
                // Enviamos al servidor el mensaje que ha escrito un cliente
                byte[] msg13 = Encoding.ASCII.GetBytes(mensaje13);
                // Abrir el formulario de jugadores nuevamente
                Jugadores formJugadores = new Jugadores(this.identificador,this.jugadores, this.puntosForms,seguirpartida);
                formJugadores.Show();
                    formJugadores.FormClosed += (s, args) =>
                    {
                        if (seguirpartida==true)
                        {
                            podercontestar=true;
                            //labelPregunta=null;
                            //labelRespuesta1 = null;
                            //labelRespuesta2 = null;
                            //labelRespuesta3 = null;
                            //labelRespuesta4 = null;
                            //labelTiempoRestante=null;
                            if (host==true)
                            {
                                string mensaje11 = "11/" + identificador.ToString();
                                // Enviamos al servidor el mensaje que ha escrito un cliente
                                byte[] msg11 = Encoding.ASCII.GetBytes(mensaje11);
                                server.Send(msg11);
                            }
                            // Volver a iniciar el temporizador cuando se cierre el formulario de jugadores
                            tiempoRestante = 30;
                            timer.Start();
                        }
                        else
                            this.Close();
                    };
            }
            else
            {
                // Actualizar la etiqueta de la cuenta regresiva
                labelTiempoRestante.Text = $"Tiempo restante: {tiempoRestante} segundos";

            }
        }
        
        public void recibirmensaje(int codigo,string[] trozos)
        {

                if (codigo == 10)
                {
                    string mensaje = mensaje = trozos[1].Split('\0')[0];
                    string usuarioEnvia = trozos[2].Split('\0')[0];
                    mensaje = usuarioEnvia + " : " + mensaje;
                    int id = Convert.ToInt32(trozos[3].Split('\0')[0]);
                    bool encontrado = false;
                    int i = 0;
                   
                    Escribirmensaje(mensaje);
                   
                }
                if (codigo == 11) 
                {
                    //pregunta_db = trozos[1].Split('\0')[0];
                  
                    pregunta_db= trozos[2].Split('\0')[0];
                    respuesta_correcta = trozos[3].Split('\0')[0];
                    resp_inc_1 = trozos[4].Split('\0')[0];
                    resp_inc_2 = trozos[5].Split('\0')[0];
                    resp_inc_3 = trozos[6].Split('\0')[0];

                    ConfigurarOpcionesDeRespuesta();
                  
                }
                if(codigo==12)
                {
                    string puntosRecibidos = trozos[2].Split('\0')[0];
                    this.puntosForms=this.puntosForms+"&"+puntosRecibidos;
                }
                if(codigo==13)
                {
                    tiempoRestante=0;
                }
                if(codigo==19)
                {
                if (host==false)
                {
                    MessageBox.Show("El Host ha terminado la partida");
                    string mensaje = "20/" + usuario + "*" + puntos.ToString() + "/" + identificador.ToString();
                    byte[] msg = Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);
                }
                }
                if(codigo==20)
                {
                  seguirpartida=false;
                  tiempoRestante=0;
                }
                if (codigo == 0)
                {

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
        private void ConfigurarOpcionesDeRespuesta()
        {
            // Asigna un color diferente a cada panel
            panelRespuesta1.BackColor = Color.Blue;
            panelRespuesta2.BackColor = Color.Green;
            panelRespuesta3.BackColor = Color.Red;
            panelRespuesta4.BackColor = Color.Purple;


            // Asigna la pregunta
            if (labelPregunta.InvokeRequired)
            {
                labelPregunta.Invoke(new Action(() => ConfigurarLabelPregunta()));
            }
            else
            {
                ConfigurarLabelPregunta();
            }
            // Configura y agrega los labels a los panels
            labelRespuesta1.AutoSize = false;
            labelRespuesta1.TextAlign = ContentAlignment.MiddleCenter;
            labelRespuesta1.Font = new Font(Font.FontFamily, 10, FontStyle.Bold);
            labelRespuesta1.Size = panelRespuesta1.Size; // Ajustar según el panel correspondiente
            labelRespuesta1.Dock = DockStyle.Fill;
            labelRespuesta1.Click += new EventHandler(panelRespuesta1_Click);
            ConfigurarLabelRespuesta(labelRespuesta1, respuesta_correcta, panelRespuesta1);

            labelRespuesta2.AutoSize = false;
            labelRespuesta2.TextAlign = ContentAlignment.MiddleCenter;
            labelRespuesta2.Font = new Font(Font.FontFamily, 10, FontStyle.Bold);
            labelRespuesta2.Size = panelRespuesta2.Size; // Ajustar según el panel correspondiente
            labelRespuesta2.Dock = DockStyle.Fill;
            labelRespuesta2.Click += new EventHandler(panelRespuesta2_Click);
            ConfigurarLabelRespuesta(labelRespuesta2, resp_inc_1, panelRespuesta2);

            labelRespuesta3.AutoSize = false;
            labelRespuesta3.TextAlign = ContentAlignment.MiddleCenter;
            labelRespuesta3.Font = new Font(Font.FontFamily, 10, FontStyle.Bold);
            labelRespuesta3.Size = panelRespuesta3.Size; // Ajustar según el panel correspondiente
            labelRespuesta3.Dock = DockStyle.Fill;
            labelRespuesta3.Click += new EventHandler(panelRespuesta3_Click);
            ConfigurarLabelRespuesta(labelRespuesta3, resp_inc_2, panelRespuesta3);

            labelRespuesta4.AutoSize = false;
            labelRespuesta4.TextAlign = ContentAlignment.MiddleCenter;
            labelRespuesta4.Font = new Font(Font.FontFamily, 10, FontStyle.Bold);
            labelRespuesta4.Size = panelRespuesta4.Size; // Ajustar según el panel correspondiente
            labelRespuesta4.Dock = DockStyle.Fill;
            labelRespuesta4.Click += new EventHandler(panelRespuesta4_Click);
            ConfigurarLabelRespuesta(labelRespuesta4, resp_inc_3, panelRespuesta4);

            // Asigna texto a cada panel para representar la respuesta
             int randomNumber = random.Next(1, 5);
            rectificador = randomNumber;
            
             

            if (randomNumber == 1)
            {
                // Respuesta correcta en primer panel
                ConfigurarLabelRespuesta(labelRespuesta1, respuesta_correcta, panelRespuesta1);
                ConfigurarLabelRespuesta(labelRespuesta2, resp_inc_1, panelRespuesta2);
                ConfigurarLabelRespuesta(labelRespuesta3, resp_inc_2, panelRespuesta3);
                ConfigurarLabelRespuesta(labelRespuesta4, resp_inc_3, panelRespuesta4);
            }
            else if (randomNumber == 2)
            {
                // Respuesta correcta en segundo panel
                ConfigurarLabelRespuesta(labelRespuesta1, resp_inc_1, panelRespuesta1);
                ConfigurarLabelRespuesta(labelRespuesta2, respuesta_correcta, panelRespuesta2);
                ConfigurarLabelRespuesta(labelRespuesta3, resp_inc_2, panelRespuesta3);
                ConfigurarLabelRespuesta(labelRespuesta4, resp_inc_3, panelRespuesta4);
            }
            else if (randomNumber == 3)
            {
                // Respuesta correcta en tercer panel
                ConfigurarLabelRespuesta(labelRespuesta1, resp_inc_1, panelRespuesta1);
                ConfigurarLabelRespuesta(labelRespuesta2, resp_inc_2, panelRespuesta2);
                ConfigurarLabelRespuesta(labelRespuesta3, respuesta_correcta, panelRespuesta3);
                ConfigurarLabelRespuesta(labelRespuesta4, resp_inc_3, panelRespuesta4);
            }
            else if (randomNumber == 4)
            {
                // Respuesta correcta en cuarto panel
                ConfigurarLabelRespuesta(labelRespuesta1, resp_inc_1, panelRespuesta1);
                ConfigurarLabelRespuesta(labelRespuesta2, resp_inc_2, panelRespuesta2);
                ConfigurarLabelRespuesta(labelRespuesta3, resp_inc_3, panelRespuesta3);
                ConfigurarLabelRespuesta(labelRespuesta4, respuesta_correcta, panelRespuesta4);
            }

        }
        private void ConfigurarLabelPregunta()
        {
            labelPregunta.AutoSize = false;
            labelPregunta.TextAlign = ContentAlignment.MiddleCenter;
            labelPregunta.Font = new Font(Font.FontFamily, 10, FontStyle.Bold);
            labelPregunta.Size = new Size(panelPregunta.Width, panelPregunta.Height);
            labelRespuesta1.Dock = DockStyle.Fill;
            labelPregunta.Text = pregunta_db;
            panelPregunta.Controls.Add(labelPregunta);
        }
        private void ConfigurarLabelRespuesta(System.Windows.Forms.Label label, string respuesta, Panel panel)
        {
            if (label.InvokeRequired)
            {
                label.Invoke(new Action(() =>
                {
                    label.Text = respuesta;
                    panel.Controls.Add(label);
                }));
            }
            else
            {
                label.Text = respuesta;
                panel.Controls.Add(label);
            }
        }




        private void Partida_Load(object sender, EventArgs e)
        {
            numForm.Text = identificador.ToString();
            if (host==true)
            {
                string mensaje = "11/" + identificador.ToString();
                // Enviamos al servidor el mensaje que ha escrito un cliente
                byte[] msg = Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            //button1.SendToBack();


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

        private void panelRespuesta1_Click(object sender, EventArgs e)
        {
           
            if (podercontestar==true)
            {
                panelRespuesta1.BackColor = Color.Blue;
                panelRespuesta2.BackColor = Color.Gray;
                panelRespuesta3.BackColor = Color.Gray;
                panelRespuesta4.BackColor = Color.Gray;              
                if (rectificador == 1)
                {
                    puntos =puntos +100 * tiempoRestante;
                }

                //enviamos al servidor el identificador de la partida y los puntos
                string mensaje = "12/" + usuario + "*" + puntos.ToString() + "/" + identificador.ToString();              
                byte[] msg = Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                podercontestar = false;

            }
        }

        private void panelRespuesta2_Click(object sender, EventArgs e)
        {
            if (podercontestar==true)
            {
                panelRespuesta1.BackColor = Color.Gray;
                panelRespuesta2.BackColor = Color.Green;
                panelRespuesta3.BackColor = Color.Gray;
                panelRespuesta4.BackColor = Color.Gray;
                if (rectificador == 2)
                {
                    puntos = puntos+100 * tiempoRestante;
                }
                //enviamos al servidor el identificador de la partida y los puntos
                string mensaje = "12/" + usuario + "*" + puntos.ToString() + "/" + identificador.ToString();
                byte[] msg = Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                podercontestar = false;

                

            }
        }

        private void panelRespuesta3_Click(object sender, EventArgs e)
        {
            if (podercontestar==true)
            {
                panelRespuesta1.BackColor = Color.Gray;
                panelRespuesta2.BackColor = Color.Gray;
                panelRespuesta3.BackColor = Color.Red;
                panelRespuesta4.BackColor = Color.Gray;
                if (rectificador == 3)
                {
                    puntos = puntos+ 100 * tiempoRestante;
                }
                //enviamos al servidor el identificador de la partida y los puntos
                string mensaje = "12/" + usuario + "*" + puntos.ToString() + "/" + identificador.ToString();
                byte[] msg = Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                podercontestar = false;

               

            }
        }

        private void panelRespuesta4_Click(object sender, EventArgs e)
        {
            if (podercontestar==true)
            {
                panelRespuesta1.BackColor = Color.Gray;
                panelRespuesta2.BackColor = Color.Gray;
                panelRespuesta3.BackColor = Color.Gray;
                panelRespuesta4.BackColor = Color.Purple;
                if (rectificador == 4)
                {
                    puntos = puntos +100 * tiempoRestante;
                }
                //enviamos al servidor el identificador de la partida y los puntos
                string mensaje = "12/" + usuario + "*" + puntos.ToString() + "/" + identificador.ToString();
                byte[] msg = Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                podercontestar = false;

                

            }
        }

        private void panelRespuesta1_DoubleClick(object sender, EventArgs e)
        {
            if (podercontestar == true)
            {
                panelRespuesta1.BackColor = Color.Blue;
                panelRespuesta2.BackColor = Color.Gray;
                panelRespuesta3.BackColor = Color.Gray;
                panelRespuesta4.BackColor = Color.Gray;
                if (rectificador == 1)
                {
                    puntos = 100 * tiempoRestante;
                }
                podercontestar = false;

               

            }
        }

        private void panelRespuesta2_DoubleClick(object sender, EventArgs e)
        {
            if (podercontestar == true)
            {
                panelRespuesta1.BackColor = Color.Gray;
                panelRespuesta2.BackColor = Color.Green;
                panelRespuesta3.BackColor = Color.Gray;
                panelRespuesta4.BackColor = Color.Gray;
                if (rectificador == 1)
                {
                    puntos = 100 * tiempoRestante;
                }
                podercontestar = false;


            }
        }

        private void panelRespuesta3_DoubleClick(object sender, EventArgs e)
        {
            if (podercontestar == true)
            {
                panelRespuesta1.BackColor = Color.Gray;
                panelRespuesta2.BackColor = Color.Gray;
                panelRespuesta3.BackColor = Color.Red;
                panelRespuesta4.BackColor = Color.Gray;
                if (rectificador == 1)
                {
                    puntos = 100 * tiempoRestante;
                }
                podercontestar = false;


            }
        }

        private void panelRespuesta4_DoubleClick(object sender, EventArgs e)
        {
            if (podercontestar == true)
            {
                panelRespuesta1.BackColor = Color.Gray;
                panelRespuesta2.BackColor = Color.Gray;
                panelRespuesta3.BackColor = Color.Gray;
                panelRespuesta4.BackColor = Color.Purple;
                if (rectificador == 1)
                {
                    puntos = 100 * tiempoRestante;
                }
                podercontestar = false;


            }
        }

        private void panelRespuesta1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void numForm_Click(object sender, EventArgs e)
        {

        }

        private void terminar_Click(object sender, EventArgs e)
        {
            if (host==true)
            {
                DialogResult resultado = MessageBox.Show("Quieres terminar la partida?", "Terminar", MessageBoxButtons.YesNo);
                if (resultado == DialogResult.Yes)
                {
                    string mensaje = "19/"+ this.identificador.ToString();
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);
                    string mensaje2 = "20/" + usuario + "*" + puntos.ToString() + "/" + identificador.ToString();
                    byte[] msg2 = Encoding.ASCII.GetBytes(mensaje2);
                    server.Send(msg2);
                }

            }
            else
            {
                MessageBox.Show("No eres el Host de la partida, la partida terminará solo para ti");
                string mensaje = "20/" + usuario + "*" + puntos.ToString() + "/" + identificador.ToString();
                byte[] msg = Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
                this.Close();
            }
        }
    }

}
