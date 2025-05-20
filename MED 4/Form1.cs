using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// Comentario hecho por ary el mismisimo
// test

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
            menuStrip1.BackColor = Color.Blue;
            menuStrip1.ForeColor = Color.Black;
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            // Esto lo puedes quitar si no usas un contextMenuStrip.
        }

        private void listarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            Form2 frm = new Form2();
            frm.Show();
        }

        private void listarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form3 frm = new Form3();
            frm.Show();
        }

        private void listarToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Form4 frm = new Form4();    
            frm.Show();
        }

        private void listarToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Form5 frm = new Form5();
            frm.Show();

        }

        private void iNFORMACIONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6();
            form6.Show();
        }

        private void cONTACTANOSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form7 form7 = new Form7();
            form7.Show();
        }
    }
    
}