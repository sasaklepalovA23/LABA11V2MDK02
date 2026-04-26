using System;
using System.Collections.Generic;
using System.IO; 
using System.Windows.Forms;


namespace LABA11V2MDK02
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateListBox();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                    string.IsNullOrWhiteSpace(txtLastName.Text) ||
                    string.IsNullOrWhiteSpace(txtCreditCard.Text))
                {
                    MessageBox.Show("Пожалуйста, заполните Фамилию, Имя и Номер кредитной карты.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var customer = new Customer
                {
                    LastName = txtLastName.Text,
                    FirstName = txtFirstName.Text,
                    Patronymic = txtPatronymic.Text,
                    Address = txtAddress.Text,
                    CreditCardNumber = txtCreditCard.Text,
                    BankAccountNumber = txtBankAccount.Text
                };

                CustomerManager.AddCustomer(customer);
                UpdateListBox(); 

                MessageBox.Show("Клиент успешно добавлен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearInputFields();
            }
            catch (FormatException)
            {
                MessageBox.Show("Номер кредитной карты и номер счета должны содержать только цифры.", "Ошибка формата", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnSort_Click(object sender, EventArgs e)
        {
            CustomerManager.Customers.Sort();
            UpdateListBox();
            MessageBox.Show("Список отсортирован по именам.", "Сортировка", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnFindByName_Click(object sender, EventArgs e)
        {
            string name = txtSearchName.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Введите имя для поиска.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Customer found = CustomerManager.GetByName(name);

            lstCustomers.Items.Clear(); 

            if (found != null)
            {
                lstCustomers.Items.Add(found.ToString());
            }
            else
            {
                lstCustomers.Items.Add($"Клиент с именем '{name}' не найден.");
            }
        }

        private void btnFindByCard_Click(object sender, EventArgs e)
        {
            string cardNumberText = txtCardMin.Text.Trim();

            if (string.IsNullOrWhiteSpace(cardNumberText))
            {
                MessageBox.Show("Введите номер кредитной карты.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Customer found = CustomerManager.GetByCardNumber(cardNumberText);

            lstCustomers.Items.Clear();

            if (found != null)
            {
                lstCustomers.Items.Add(found.ToString());
            }
            else
            {
                lstCustomers.Items.Add($"Клиент с картой {cardNumberText} не найден.");
            }
        }

        private void btnFindByRange_Click(object sender, EventArgs e)
        {
            try
            {
                string minText = txtCardMin.Text.Trim();
                string maxText = txtCardMax.Text.Trim();

                if (string.IsNullOrWhiteSpace(minText) || string.IsNullOrWhiteSpace(maxText))
                {
                    MessageBox.Show("Введите оба значения диапазона.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                List<Customer> foundList = CustomerManager.GetByCreditCardRange(minText, maxText);

                lstCustomers.Items.Clear();

                if (foundList.Count > 0)
                {
                    foreach (var c in foundList)
                    {
                        lstCustomers.Items.Add(c.ToString());
                    }
                }
                else
                {
                    lstCustomers.Items.Add($"Клиенты с картами в диапазоне {minText} - {maxText} не найдены.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string filename = "customers.dat";

                CustomerManager.SaveToFile(filename);

                MessageBox.Show($"Данные успешно сохранены в файл '{filename}'.", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                string filename = "customers.dat";

                if (!File.Exists(filename))
                {
                    MessageBox.Show($"Файл '{filename}' не найден.", "Ошибка файла", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                CustomerManager.LoadFromFile(filename);

                UpdateListBox(); 

                MessageBox.Show($"Данные успешно загружены из файла '{filename}'.", "Загрузка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void UpdateListBox()
        {
            lstCustomers.Items.Clear(); 

            if (CustomerManager.Customers.Count == 0)
            {
                lstCustomers.Items.Add("Список клиентов пуст.");
                return;
            }

            foreach (var c in CustomerManager.Customers)
            {
                lstCustomers.Items.Add(c.ToString());
            }
        }

        private void ClearInputFields()
        {
            txtLastName.Clear();
            txtFirstName.Clear();
            txtPatronymic.Clear();
            txtAddress.Clear();
            txtCreditCard.Clear();
            txtBankAccount.Clear();
            txtSearchName.Clear();
            txtCardMin.Clear();
            txtCardMax.Clear();
        }
    }
}