using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

    namespace MED_4
    {
        public partial class Form2 : Form
        {
        public Form2()
        {
            InitializeComponent();
        }

        // Método para cargar medicamentos (filtrados o todos)
        private void CargarMedicamentos(string filtro)
        {
            string connectionString = "Server=localhost;Port=3306;Database=INVENTARIOFFINAL;User ID=root;Password=fhernando12;";
            string query = "SELECT m.nombre AS Medicamento, m.descripcion, m.precio, m.stock, m.fecha_vencimiento, c.nombre AS Categoria " +
                           "FROM Medicamento m " +
                           "LEFT JOIN Lote l ON m.id_Medicamento = l.id_medicamento " +
                           "LEFT JOIN Proveedor p ON l.id_proveedor = p.id_proveedor " +
                           "LEFT JOIN Categoria c ON m.id_categoria = c.id_Categoria";

            if (!string.IsNullOrEmpty(filtro))
            {
                query += " WHERE m.nombre LIKE @filtro";
            }

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(query, connection);

                    if (!string.IsNullOrEmpty(filtro))
                    {
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

        // Evento al hacer clic en el botón "Buscar"
        private void button1_Click(object sender, EventArgs e)
        {
            string textoBusqueda = textBox1.Text.Trim();

            if (textoBusqueda.ToLower() == "todos" || string.IsNullOrEmpty(textoBusqueda))
            {
                CargarMedicamentos(""); // Mostrar todos
            }
            else
            {
                CargarMedicamentos(textoBusqueda); // Buscar por nombre
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // No necesitas hacer nada aquí, a menos que quieras buscar mientras se escribe.
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear(); // Opcional, por si acaso
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Puedes dejarlo vacío si no haces nada al hacer clic en una celda.
        }
    }
}


