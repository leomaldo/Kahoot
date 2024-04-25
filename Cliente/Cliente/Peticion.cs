using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace Cliente
{
    public partial class Peticion : Form
    {
        int nForm;
        Socket server;
      
        public Peticion()
        {



        }
        
        
        public Peticion(/*int nForm,*/ Socket server)
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(180, 255, 220);
            //this.nForm = nForm;
            this.server = server;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (MaxPuntuacion.Checked)
            {
                string mensaje = "1/"+ nForm;
                // Enviamos al servidor el codigo
                byte[] msg = Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                // Mostrar la máxima puntuación
              

            }
            else if (JugadorPuntos.Checked)
            {
                string mensaje = "2/" + nForm;
                // Enviamos al servidor el codigo
                byte[] msg = Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
               
            }
            else if (Preguntas.Checked)
            {
                string mensaje = "3/" + nForm;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
               
            }
           
        }

        private void Peticion_Load(object sender, EventArgs e) 
        {
            numForm.Text = nForm.ToString();

        }
       

        
    }
}
