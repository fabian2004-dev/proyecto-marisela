using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MED_4
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            radioButton3.Checked = true; // Por defecto buscar por ID
            dataGridView1.DataSource = null; // Vaciar al iniciar para que no muestre datos
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string texto = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(texto) || texto.ToLower() == "todos")
            {
                CargarProveedores("", "nombre");
                return;
            }

            if (radioButton3.Checked) // ID
            {
                CargarProveedores(texto, "id_proveedor");
            }
            else if (radioButton1.Checked) // Nombre
            {
                CargarProveedores(texto, "nombre");
            }
            else if (radioButton2.Checked) // Contacto
            {
                CargarProveedores(texto, "contacto");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();
            }

            if (radioButton3.Checked) // Si busca por ID
            {
                string texto = textBox1.Text;
                if (!Regex.IsMatch(texto, @"^\d*$"))
                {
                    MessageBox.Show("Solo se permiten números para buscar por ID.");
                    textBox1.Text = "";
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e) // Nombre
        {
            textBox1.Text = "";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e) // Contacto
        {
            textBox1.Text = "";
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e) // ID
        {
            textBox1.Text = "";
        }

        private void CargarProveedores(string filtro, string tipo)
        {
            string connectionString = "Server=localhost;Port=3306;Database=INVENTARIOFFINAL;User ID=root;Password=fhernando12;";
            string query = "SELECT id_proveedor AS ID, nombre AS Proveedor, contacto AS Encargado, direccion AS Direccion, telefono AS Telefono FROM Proveedor";

            if (!string.IsNullOrEmpty(filtro))
            {
                switch (tipo)
                {
                    case "id_proveedor":
                        query += " WHERE id_proveedor = @filtro";
                        break;
                    case "nombre":
                        query += " WHERE nombre LIKE @filtro";
                        break;
                    case "contacto":
                        query += " WHERE contacto LIKE @filtro";
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
                        if (tipo == "id_proveedor")
                            command.Parameters.AddWithValue("@filtro", filtro);
                        else
                            command.Parameters.AddWithValue("@filtro", "%" + filtro + "%");
                    }

                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView1.DataSource = dt;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar proveedores: " + ex.Message);
                }
            }
        }
    }
}

