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
    public partial class Form4 : Form
    {
        // Ajusta tu cadena de conexión aquí si es necesario
        string cadenaConexion = "Server=localhost;Database=inventarioffinal;Uid=root;Pwd=fhernando12;";

        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear(); // Opcional, para asegurarse
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            // Buscar por Nombre
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            // Buscar por ID
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            // No hace nada pero se deja para evitar errores
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string consulta = "";
            string texto = textBox1.Text.Trim();

            // Si está seleccionado buscar por Nombre
            if (radioButton1.Checked)
            {
                if (string.IsNullOrWhiteSpace(texto))
                {
                    dataGridView1.DataSource = null;
                    dataGridView1.Rows.Clear();
                    return;
                }

                if (texto.ToLower() == "todos")
                {
                    consulta = "SELECT * FROM fabricante";
                }
                else
                {
                    consulta = "SELECT * FROM fabricante WHERE Nombre LIKE @valor";
                }
            }
            else if (radioButton2.Checked) // Buscar por ID
            {
                consulta = "SELECT * FROM fabricante WHERE id_Fabricante = @valor";
            }
            else
            {
                MessageBox.Show("Selecciona un criterio de búsqueda (por nombre o por ID).");
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
                        if (radioButton1.Checked)
                            cmd.Parameters.AddWithValue("@valor", "%" + texto + "%");
                        else
                            cmd.Parameters.AddWithValue("@valor", texto);
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
    }
}
