using MySql.Data.MySqlClient;
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
    public partial class Form5 : Form
    {
        string cadenaConexion = "server=localhost;database=inventarioffinal;uid=root;pwd=fhernando12;";

        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string consulta = "";
            string texto = textBox1.Text.Trim();

            if (radioButton1.Checked) // Buscar por nombre
            {
                if (string.IsNullOrWhiteSpace(texto))
                {
                    dataGridView1.DataSource = null;
                    dataGridView1.Rows.Clear();
                    return;
                }

                if (texto.ToLower() == "todos")
                {
                    consulta = "SELECT * FROM empleado";
                }
                else
                {
                    consulta = "SELECT * FROM empleado WHERE nombre LIKE @valor";
                }
            }
            else if (radioButton2.Checked) // Buscar por ID
            {
                consulta = "SELECT * FROM empleado WHERE id_Empleado = @valor";
            }
            else if (radioButton3.Checked) // Buscar por cargo
            {
                if (string.IsNullOrWhiteSpace(texto))
                {
                    dataGridView1.DataSource = null;
                    dataGridView1.Rows.Clear();
                    return;
                }

                consulta = "SELECT * FROM empleado WHERE cargo LIKE @valor";
            }
            else
            {
                MessageBox.Show("Selecciona un criterio de búsqueda.");
                return;
            }

            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                try
                {
                    conexion.Open();
                    MySqlCommand cmd = new MySqlCommand(consulta, conexion);

                    if (consulta.Contains("@valor"))
                    {
                        if (radioButton2.Checked)
                            cmd.Parameters.AddWithValue("@valor", texto); // ID exacto
                        else
                            cmd.Parameters.AddWithValue("@valor", "%" + texto + "%"); // LIKE para nombre y cargo
                    }

                    MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adaptador.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al buscar: " + ex.Message);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                if (radioButton1.Checked || radioButton3.Checked)
                {
                    dataGridView1.DataSource = null;
                    dataGridView1.Rows.Clear();
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}