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

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string type = textBox1.Text;

            db db = new db();
            db.openConnection();

            MySqlCommand command = new MySqlCommand("SELECT * FROM bread WHERE type LIKE @type", db.getConnection());
            command.Parameters.AddWithValue("@type", "%" + type + "%");
            
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            dataGridView1.DataSource = table;

            db.closeConnection();
        }
    }
}
