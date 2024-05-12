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
    public partial class Jugadores : Form
    {
        string jugadores;
        List<string> listaJugadores;
        Timer timer = new Timer();
        int tiempoRestante = 10;

        public Jugadores(string jugadores)
        {
            InitializeComponent();
            label2.Font = new Font("Arial", 18); // Cambiar el tamaño de la fuente a 14 puntos
            label2.AutoSize = true;
            this.jugadores=jugadores;
            listaJugadores = new List<string>();

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
            string[] nombres = jugadores.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
            listaJugadores.AddRange(nombres);

            // Llenar el DataGridView con los nombres de los jugadores
            foreach (string nombre in listaJugadores)
            {
                dataGridView1.Rows.Add(nombre, ""); // Agregar un string vacío para los puntos
            }
            panel1.Size = dataGridView1.Size;
        }
    }
}
