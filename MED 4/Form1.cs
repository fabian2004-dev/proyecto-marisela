using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MED_4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Puedes dejarlo vacío o cargar datos si necesitas.
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            // Esto lo puedes quitar si no usas un contextMenuStrip.
        }

        private void listarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Abre el Form2 al hacer clic en la opción del menú
            Form2 frm = new Form2();
            frm.Show();
        }

        private void listarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form3 frm = new Form3();
            frm.Show();
        }
    }
}