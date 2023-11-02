namespace WinFormsApp9
{

    public partial class Form1
    {
        public class EditContactForm : Form
        {
            private Contact contactToEdit;
            private TextBox txtFirstName;
            private TextBox txtLastName;
            private TextBox txtPhoneNumber;
            private TextBox txtEmail;
            private Button btnSave;
            private Button btnCancel;

            public EditContactForm(Contact contact)
            {
                contactToEdit = contact;
                InitializeComponents();
                LoadContactData();
            }

            private void InitializeComponents()
            {
                this.Text = "Редактировать контакт";
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

                btnCancel = new Button();
                btnCancel.Text = "Отмена";
                btnCancel.Click += CancelEdit;

                FlowLayoutPanel inputPanel = new FlowLayoutPanel();
                inputPanel.Dock = DockStyle.Top;
                inputPanel.Controls.Add(txtFirstName);
                inputPanel.Controls.Add(txtLastName);
                inputPanel.Controls.Add(txtPhoneNumber);
                inputPanel.Controls.Add(txtEmail);
                inputPanel.Controls.Add(btnSave);
                inputPanel.Controls.Add(btnCancel);

                this.Controls.Add(inputPanel);
            }

            private void LoadContactData()
            {
                txtFirstName.Text = contactToEdit.FirstName;
                txtLastName.Text = contactToEdit.LastName;
                txtPhoneNumber.Text = contactToEdit.PhoneNumber;
                txtEmail.Text = contactToEdit.Email;
            }

            private void SaveContact(object sender, EventArgs e)
            {
                // Сохраните изменения в объекте contactToEdit
                contactToEdit.FirstName = txtFirstName.Text;
                contactToEdit.LastName = txtLastName.Text;
                contactToEdit.PhoneNumber = txtPhoneNumber.Text;
                contactToEdit.Email = txtEmail.Text;

                // Закройте окно редактирования
                this.DialogResult = DialogResult.OK;
                this.Close();
            }

            private void CancelEdit(object sender, EventArgs e)
            {
                // Отмените редактирование и закройте окно
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }
    }
}