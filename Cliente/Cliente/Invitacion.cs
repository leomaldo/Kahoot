﻿using System;
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
        int respuesta;
        string UsuEnv;
        public Invitacion(string UsuEnv)
        {
            InitializeComponent();
            this.UsuEnv = UsuEnv;
        }
      
        public int GetRespuesta()
        {
            return this.respuesta;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.respuesta = 0;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.respuesta = 1;
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Invitacion_Load_2(object sender, EventArgs e)
        {
            label1.Text = UsuEnv + " te ha invitado a una partida.\n ¿Deseas unirte?";
        }
    }
}
