using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form4 : Form
    {
        // Плейсхолдери для кожного поля вводу (відповідають вимогам варіанту 16)
        private string placeholderText1 = "Вид";
        private string placeholderText2 = "Сорт";
        private string placeholderText3 = "Матеріал";
        private string placeholderText4 = "Постачальник";
        private string placeholderText5 = "Термін придатності";
        private string placeholderText6 = "Ціна";

        public Form4()
        {
            InitializeComponent();

            // Встановлюємо плейсхолдери при завантаженні форми
            SetPlaceholder(textBox1, placeholderText1);
            SetPlaceholder(textBox2, placeholderText2);
            SetPlaceholder(textBox3, placeholderText3);
            SetPlaceholder(textBox4, placeholderText4);
            SetPlaceholder(textBox5, placeholderText5);
            SetPlaceholder(textBox6, placeholderText6);
        }

        // Метод для встановлення плейсхолдера в текстове поле
        private void SetPlaceholder(TextBox tb, string placeholder)
        {
            tb.Text = placeholder;
            tb.ForeColor = Color.Gray;

            // Очищаємо плейсхолдер при вході у поле
            tb.Enter += (sender, e) =>
            {
                if (tb.Text == placeholder)
                {
                    tb.Text = "";
                    tb.ForeColor = Color.Black;
                }
            };

            // Повертаємо плейсхолдер, якщо поле порожнє при виході
            tb.Leave += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(tb.Text))
                {
                    tb.Text = placeholder;
                    tb.ForeColor = Color.Gray;
                }
            };
        }

        // Метод, що викликається при натисканні на кнопку "Додати"
        private void button1_Click(object sender, EventArgs e)
        {
            // Отримуємо дані з текстбоксів
            string type = textBox1.Text;
            string sort = textBox2.Text;
            string material = textBox3.Text;
            string supplier = textBox4.Text;
            string expiration_date = textBox5.Text;
            double price;

            // Перевіряємо, чи введена ціна — дійсне число
            if (!double.TryParse(textBox6.Text, out price))
            {
                MessageBox.Show("Ціна має бути числом");
                return;
            }

            // Створюємо підключення до бази даних
            db db = new db();
            db.openConnection();

            // SQL-запит для вставки даних у таблицю bakery
            MySqlCommand command = new MySqlCommand(
                "INSERT INTO bread (type, sort, material, supplier, expiration_date, price) " +
                "VALUES (@type, @sort, @material, @supplier, @expiration_date, @price)",
                db.getConnection()
            );

            // Передаємо параметри в запит
            command.Parameters.AddWithValue("@type", type);
            command.Parameters.AddWithValue("@sort", sort);
            command.Parameters.AddWithValue("@material", material);
            command.Parameters.AddWithValue("@supplier", supplier);
            command.Parameters.AddWithValue("@expiration_date", expiration_date);
            command.Parameters.AddWithValue("@price", price);

            try
            {
                // Виконуємо запит
                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Запис додано!");
                }
                else
                {
                    MessageBox.Show("Помилка при додаванні.");
                }
            }
            catch (Exception ex)
            {
                // Виводимо помилку SQL (наприклад, якщо таблиця не існує)
                MessageBox.Show("SQL помилка: " + ex.Message);
            }

            // Закриваємо підключення до БД
            db.closeConnection();
        }
    }
}
