using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

    namespace MED_4
    {
        public partial class Form2 : Form
        {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // Seleccionar por defecto búsqueda por nombre
            radioButton1.Checked = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string texto = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(texto) || texto.ToLower() == "todos")
            {
                CargarMedicamentos("", "nombre");
                return;
            }

            if (radioButton1.Checked)
            {
                // Buscar por nombre
                CargarMedicamentos(texto, "nombre");
            }
            else if (radioButton2.Checked)
            {
                // Buscar por precio
                if (decimal.TryParse(texto, out _))
                {
                    CargarMedicamentos(texto, "precio");
                }
                else
                {
                    MessageBox.Show("Ingresa solo números para buscar por precio.");
                }
            }
            else if (radioButton3.Checked)
            {
                // Buscar por categoría (nombre o ID)
                CargarMedicamentos(texto, "categoria");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();
            }

            // Solo permitir números si está seleccionado "precio"
            if (radioButton2.Checked)
            {
                string texto = textBox1.Text;
                if (!Regex.IsMatch(texto, @"^\d*\.?\d*$"))
                {
                    MessageBox.Show("Solo se permiten números para el precio.");
                    textBox1.Text = "";
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // No se usa en este ejemplo.
        }

        private void CargarMedicamentos(string filtro, string tipo)
        {
            string connectionString = "Server=localhost;Port=3306;Database=INVENTARIOFFINAL;User ID=root;Password=fhernando12;";
            string query = "SELECT m.nombre AS Medicamento, m.descripcion, m.precio, m.stock, m.fecha_vencimiento, c.nombre AS Categoria " +
                "FROM Medicamento m " +
                "LEFT JOIN Lote l ON m.id_Medicamento = l.id_medicamento " +
                "LEFT JOIN Proveedor p ON l.id_proveedor = p.id_proveedor " +
                "LEFT JOIN Categoria c ON m.id_categoria = c.id_Categoria ";

            if (!string.IsNullOrEmpty(filtro))
            {
                switch (tipo)
                {
                    case "nombre":
                        query += " WHERE m.nombre LIKE @filtro";
                        break;
                    case "precio":
                        query += " WHERE m.precio = @filtro";
                        break;
                    case "categoria":
                        if (int.TryParse(filtro, out _))
                            query += " WHERE c.id_Categoria = @filtro";
                        else
                            query += " WHERE c.nombre LIKE @filtro";
                        break;
                }
            }

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(query, connection);

                    if (!string.IsNullOrEmpty(filtro))
                    {
                        if (tipo == "precio" || (tipo == "categoria" && int.TryParse(filtro, out _)))
                            command.Parameters.AddWithValue("@filtro", filtro);
                        else
                            command.Parameters.AddWithValue("@filtro", "%" + filtro + "%");
                    }

                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
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
