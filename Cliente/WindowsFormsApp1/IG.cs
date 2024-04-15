using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class IG : Form
    {
        public IG()
        {
            InitializeComponent();
        }

        private void rEGISTRARMEINICIARSESIÓNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new Form();
            form.ShowDialog();
        }
    }
}
