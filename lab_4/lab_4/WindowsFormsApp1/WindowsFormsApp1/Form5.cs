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
    public partial class Form5 : Form
    {
        private string placeholderText1 = "ID";
        private string placeholderText2 = "Кількість";

        public Form5()
        {
            InitializeComponent();

            SetPlaceholder(textBox1, placeholderText1);
            SetPlaceholder(textBox2, placeholderText2);
        }

        private void SetPlaceholder(TextBox tb, string placeholder)
        {
            tb.Text = placeholder;
            tb.ForeColor = Color.Gray;

            tb.Enter += (sender, e) =>
            {
                if (tb.Text == placeholder)
                {
                    tb.Text = "";
                    tb.ForeColor = Color.Black;
                }
            };

            tb.Leave += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(tb.Text))
                {
                    tb.Text = placeholder;
                    tb.ForeColor = Color.Gray;
                }
            };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int id = int.Parse(textBox1.Text);
            int produceAmount = int.Parse(textBox2.Text);

            db db = new db();
            db.openConnection();

            MySqlCommand command = new MySqlCommand("SELECT amount FROM bread WHERE id = @id", db.getConnection());
            command.Parameters.AddWithValue("@id", id);

            object result = command.ExecuteScalar();

            if (result == null)
            {
                MessageBox.Show($"Can`t find ID: {id}");
            }

            int amount = Convert.ToInt32(result);

            MySqlCommand updateCmd = new MySqlCommand("UPDATE bread SET amount = amount + @produceAmount WHERE id = @id", db.getConnection());
            updateCmd.Parameters.AddWithValue("@produceAmount", produceAmount);
            updateCmd.Parameters.AddWithValue("@id", id);
            updateCmd.ExecuteNonQuery();

            MessageBox.Show("Васько");
            db.closeConnection();
        }
    }
}
