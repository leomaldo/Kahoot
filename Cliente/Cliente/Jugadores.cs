using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliente
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    public partial class Jugadores : Form
    {
        string jugadores;
        List<string> listaJugadores;
        Dictionary<string, int> puntosJugadores;
        Timer timer = new Timer();
        int tiempoRestante = 10;
        int identificador;
        bool seguirpartida;
        public Jugadores(int identificador,string jugadores, string puntos, bool seguirpartida)
        {
            InitializeComponent();
            label2.Font = new Font("Arial", 18); // Cambiar el tamaño de la fuente a 18 puntos
            label2.AutoSize = true;
            this.jugadores = jugadores;
            listaJugadores = new List<string>();
            puntosJugadores = new Dictionary<string, int>();
            this.identificador = identificador;
            this.seguirpartida = seguirpartida;
            // Configurar el temporizador
            timer.Interval = 1000; // 1000 ms = 1 segundo
            timer.Tick += Timer_Tick;
            timer.Start(); // Iniciar el temporizador

            // Configuración del DataGridView
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.AllowUserToAddRows = false;

            // Ajustar el tamaño de las celdas
            DataGridViewCellStyle style = dataGridView1.DefaultCellStyle;
            style.Font = new Font("Arial", 12F, FontStyle.Regular); // Tamaño de fuente
            style.Padding = new Padding(5);

            panel1.AutoScroll = true;

            this.BackColor = Color.LightBlue;

            // Agregamos las columnas al DataGridView
            dataGridView1.Columns.Add("Jugadores", "Jugadores");
            dataGridView1.Columns.Add("Puntos", "Puntos");

            // Procesar los puntos si la cadena no es nula
            if (!string.IsNullOrEmpty(puntos))
            {
                string[] pares = puntos.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string par in pares)
                {
                    string[] datos = par.Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries);
                    if (datos.Length == 2 && int.TryParse(datos[1], out int puntosJugador))
                    {
                        puntosJugadores[datos[0]] = puntosJugador;
                    }
                }
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tiempoRestante--; // Decrementar el tiempo restante
            if (tiempoRestante <= 0)
            {
                // Detener el temporizador y cerrar el formulario
                timer.Stop();
                this.Close();
            }
            else
            {
                // Actualizar la etiqueta de la cuenta regresiva
                label1.Font = new Font("Arial", 14); // Cambiar el tamaño de la fuente a 14 puntos
                label1.AutoSize = true;
                label1.Text = Convert.ToString(tiempoRestante);
            }
        }

        private void Jugadores_Load(object sender, EventArgs e)
        {
            iden.Text = identificador.ToString();
            string[] nombres = jugadores.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
            listaJugadores.AddRange(nombres);

            // Llenar el DataGridView con los nombres de los jugadores y sus puntos correspondientes
            foreach (string nombre in listaJugadores)
            {
                int puntos = puntosJugadores.ContainsKey(nombre) ? puntosJugadores[nombre] : 0;
                dataGridView1.Rows.Add(nombre, puntos);
            }
            panel1.Size = dataGridView1.Size;

            if(seguirpartida==false)
            {
                label2.Text="Partida finalizada, hasta pronto";
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Aquí puedes manejar eventos cuando se hace clic en una celda
        }
    }


    //public partial class Jugadores : Form
    //{
    //    string jugadores;
    //    List<string> listaJugadores;
    //    Timer timer = new Timer();
    //    int tiempoRestante = 10;

    //    public Jugadores(string jugadores, string puntos)
    //    {
    //        InitializeComponent();
    //        label2.Font = new Font("Arial", 18); // Cambiar el tamaño de la fuente a 14 puntos
    //        label2.AutoSize = true;
    //        this.jugadores=jugadores;
    //        listaJugadores = new List<string>();

    //        // Configurar el temporizador
    //        timer.Interval = 1000; // 1000 ms = 1 segundo
    //        timer.Tick += Timer_Tick;
    //        timer.Start(); // Iniciar el temporizador

    //        // Configuración del DataGridView
    //        dataGridView1.Dock = DockStyle.Fill;
    //        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
    //        dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
    //        dataGridView1.AllowUserToAddRows = false;

    //        // Ajustar el tamaño de las celdas
    //        DataGridViewCellStyle style = dataGridView1.DefaultCellStyle;
    //        style.Font = new Font("Arial", 12F, FontStyle.Regular); // Tamaño de fuente
    //        style.Padding = new Padding(5);

    //        panel1.AutoScroll = true;

    //        this.BackColor = Color.LightBlue;

    //        // Agregamos las columnas al DataGridView
    //        dataGridView1.Columns.Add("Jugadores", "Jugadores");
    //        dataGridView1.Columns.Add("Puntos", "Puntos");
    //    }
    //    private void Timer_Tick(object sender, EventArgs e)
    //    {
    //        tiempoRestante--; // Decrementar el tiempo restante
    //        if (tiempoRestante <= 0)
    //        {
    //            // Detener el temporizador y cerrar el formulario
    //            timer.Stop();
    //            this.Close();
    //        }
    //        else
    //        {
    //            // Actualizar la etiqueta de la cuenta regresiva
    //            label1.Font = new Font("Arial", 14); // Cambiar el tamaño de la fuente a 14 puntos
    //            label1.AutoSize = true;
    //            label1.Text = Convert.ToString(tiempoRestante);
    //        }
    //    }

    //    private void Jugadores_Load(object sender, EventArgs e)
    //    {
    //        string[] nombres = jugadores.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
    //        listaJugadores.AddRange(nombres);

    //        // Llenar el DataGridView con los nombres de los jugadores
    //        foreach (string nombre in listaJugadores)
    //        {
    //            dataGridView1.Rows.Add(nombre, ""); // Agregar un string vacío para los puntos
    //        }
    //        panel1.Size = dataGridView1.Size;
    //    }

    //    private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
    //    {

    //    }
    //}
}
