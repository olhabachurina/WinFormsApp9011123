using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace WinFormsApp9
{
    public partial class Form1 : Form
    {
        private List<Contact> contacts = new List<Contact>();
        private TableLayoutPanel tableLayoutPanelContactList;
        private TextBox txtFirstName;
        private TextBox txtLastName;
        private TextBox txtPhoneNumber;
        private TextBox txtEmail;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;

        public class Contact
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string PhoneNumber { get; set; }
            public string Email { get; set; }

            public override string ToString()
            {
                return $"{FirstName} {LastName}";
            }
        }

        public Form1()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "Список контактов";
            this.Size = new System.Drawing.Size(600, 400);

            tableLayoutPanelContactList = new TableLayoutPanel();
            tableLayoutPanelContactList.Dock = DockStyle.Left;
            tableLayoutPanelContactList.Width = 200;
            this.Controls.Add(tableLayoutPanelContactList);

            txtFirstName = new TextBox();
            txtFirstName.PlaceholderText = "Имя";
            txtLastName = new TextBox();
            txtLastName.PlaceholderText = "Фамилия";
            txtPhoneNumber = new TextBox();
            txtPhoneNumber.PlaceholderText = "Номер телефона";
            txtEmail = new TextBox();
            txtEmail.PlaceholderText = "Email";

            btnAdd = new Button();
            btnAdd.Text = "Добавить";
            btnAdd.Size = new System.Drawing.Size(100, 40);
            btnAdd.BackColor = Color.Green;
            btnAdd.ForeColor = Color.White;
            btnAdd.Click += AddContact;

            btnEdit = new Button();
            btnEdit.Text = "Редактировать";
            btnEdit.Size = new System.Drawing.Size(100, 40);
            btnEdit.Click += EditContact;

            btnDelete = new Button();
            btnDelete.Text = "Удалить";
            btnDelete.Size = new System.Drawing.Size(100, 40);
            btnDelete.BackColor = Color.Red;
            btnDelete.ForeColor = Color.White;
            btnDelete.Click += DeleteContact;

            FlowLayoutPanel inputPanel = new FlowLayoutPanel();
            inputPanel.Dock = DockStyle.Top;
            inputPanel.Controls.Add(txtFirstName);
            inputPanel.Controls.Add(txtLastName);
            inputPanel.Controls.Add(txtPhoneNumber);
            inputPanel.Controls.Add(txtEmail);
            inputPanel.Controls.Add(btnAdd);
            inputPanel.Controls.Add(btnEdit);
            inputPanel.Controls.Add(btnDelete);

            this.Controls.Add(inputPanel);
        }

        private void AddContact(object sender, EventArgs e)
        {
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            string phoneNumber = txtPhoneNumber.Text;
            string email = txtEmail.Text;

            Contact contact = new Contact
            {
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phoneNumber,
                Email = email
            };

            contacts.Add(contact);
            DisplayContacts();
        }

        private void EditContact(object sender, EventArgs e)
        {
            TableLayoutPanel selectedPanel = sender as TableLayoutPanel;
            if (tableLayoutPanelContactList.Controls.Count == 0)
            {
                MessageBox.Show("Список контактов пуст.", "Предупреждение");
                return;
            }

            
            if (tableLayoutPanelContactList.GetControlFromPosition(0, tableLayoutPanelContactList.GetRow(tableLayoutPanelContactList)) is TableLayoutPanel)
            {
                Label nameLabel = selectedPanel.Controls[0] as Label;
                Label phoneLabel = selectedPanel.Controls[1] as Label;

                if (nameLabel != null && phoneLabel != null)
                {
                    string[] nameParts = nameLabel.Text.Split(' ');
                    string firstName = nameParts[0];
                    string lastName = nameParts.Length > 1 ? nameParts[1] : string.Empty;

                    
                    Contact contactToEdit = contacts.Find(c => c.FirstName == firstName && c.LastName == lastName && c.PhoneNumber == phoneLabel.Text);

                    if (contactToEdit == null)
                    {
                        MessageBox.Show("Не удалось найти выбранный контакт.", "Ошибка");
                        return;
                    }
                    EditContactForm editForm = new EditContactForm(contactToEdit);
                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        
                        DisplayContacts();
                    }
                }
            }
        }

        private void DeleteContact(object sender, EventArgs e)
        {
            tableLayoutPanelContactList.Controls.Clear();

            
            foreach (Contact contact in contacts)
            {
                TableLayoutPanel contactPanel = new TableLayoutPanel();
                contactPanel.ColumnCount = 1;
                contactPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
                contactPanel.BackColor = Color.LightGray; 
                
                Label nameLabel = new Label();
                nameLabel.Text = contact.FirstName + " " + contact.LastName;
                nameLabel.Dock = DockStyle.Fill;

                Label phoneLabel = new Label();
                phoneLabel.Text = contact.PhoneNumber;
                phoneLabel.Dock = DockStyle.Fill;

                
                contactPanel.Controls.Add(nameLabel);
                contactPanel.Controls.Add(phoneLabel);

                
                contactPanel.Click += (sender, e) => EditContact(sender, e);

               
                tableLayoutPanelContactList.Controls.Add(contactPanel);
            }
        }

        private void DisplayContacts()
        {
           
            tableLayoutPanelContactList.Controls.Clear();

            
            foreach (Contact contact in contacts)
            {
                TableLayoutPanel contactPanel = new TableLayoutPanel();
                contactPanel.ColumnCount = 1;
                contactPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
                contactPanel.BackColor = Color.LightGray; 

                
                Label nameLabel = new Label();
                nameLabel.Text = contact.FirstName + " " + contact.LastName;
                nameLabel.Dock = DockStyle.Fill;

                Label phoneLabel = new Label();
                phoneLabel.Text = contact.PhoneNumber;
                phoneLabel.Dock = DockStyle.Fill;

               
                contactPanel.Controls.Add(nameLabel);
                contactPanel.Controls.Add(phoneLabel);

                
                contactPanel.Click += (sender, e) => EditContact(sender, e);

                
                tableLayoutPanelContactList.Controls.Add(contactPanel);
            }
        }
    }
}
        /*
        // Класс для редактирования контакта
        public class EditContactForm : Form
        {
            private Contact contact;
            private TextBox txtFirstName;
            private TextBox txtLastName;
            private TextBox txtPhoneNumber;
            private TextBox txtEmail;
            private Button btnSave;

            public EditContactForm(Contact contact)
            {
                this.contact = contact;
                InitializeComponents();
                LoadContactData();
            }

            private void InitializeComponents()
            {
                this.Text = "Редактирование контакта";
                this.Size = new System.Drawing.Size(300, 200);

                txtFirstName = new TextBox();
                txtFirstName.PlaceholderText = "Имя";

                txtLastName = new TextBox();
                txtLastName.PlaceholderText = "Фамилия";

                txtPhoneNumber = new TextBox();
                txtPhoneNumber.PlaceholderText = "Номер телефона";

                txtEmail = new TextBox();
                txtEmail.PlaceholderText = "Email";

                btnSave = new Button();
                btnSave.Text = "Сохранить";
                btnSave.Click += SaveContact;

                FlowLayoutPanel inputPanel = new FlowLayoutPanel();
                inputPanel.Dock = DockStyle.Top;
                inputPanel.Controls.Add(txtFirstName);
                inputPanel.Controls.Add(txtLastName);
                inputPanel.Controls.Add(txtPhoneNumber);
                inputPanel.Controls.Add(txtEmail);
                inputPanel.Controls.Add(btnSave);

                this.Controls.Add(inputPanel);
            }

            private void LoadContactData()
            {
                txtFirstName.Text = contact.FirstName;
                txtLastName.Text = contact.LastName;
                txtPhoneNumber.Text = contact.PhoneNumber;
                txtEmail.Text = contact.Email;
            }

            private void SaveContact(object sender, EventArgs e)
            {
                contact.FirstName = txtFirstName.Text;
                contact.LastName = txtLastName.Text;
                contact.PhoneNumber = txtPhoneNumber.Text;
                contact.Email = txtEmail.Text;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}*/