using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliente
{
    public partial class Invitacion : Form
    {
        string respuesta;
        string UsuEnv;
        public Invitacion()
        {
            InitializeComponent();
        }
        public void SetUsuEnv(string U)
        {
            this.UsuEnv = U;
        }
        public string GetRespuesta()
        {
            return this.respuesta;
        }
      
        private void Invitacion_Load(object sender, EventArgs e)
        {
            label1.Text = UsuEnv + " te ha invitado a una partida.\n ¿Deseas unirte?";
        }
        private void Invitacion_Load_1(object sender, EventArgs e)
        {
            label1.Text = UsuEnv + " te ha invitado a una partida.\n ¿Deseas unirte?";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.respuesta = "NO";
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.respuesta = "SI";
            this.Close();
        }
    }
}
