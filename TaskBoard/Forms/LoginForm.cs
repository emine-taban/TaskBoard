using Microsoft.EntityFrameworkCore;
using TaskBoard.Data;

namespace TaskBoard
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ShowError("Kullanıcı adı ve şifre boş bırakılamaz.");
                return;
            }

            using var db = new AppDbContext();
            var user = db.Users.FirstOrDefault(u => u.Username == username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                ShowError("Kullanıcı adı veya şifre hatalı.");
                txtPassword.Clear();
                txtPassword.Focus();
                return;
            }

            Session.CurrentUser = user;
            this.Hide();
            var projectForm = new ProjectListForm();
            projectForm.FormClosed += (s, args) =>
            {
                if (Session.CurrentUser == null)
                {
                    // Logout yapıldı → Login ekranını yeniden göster
                    txtPassword.Clear();
                    lblError.Visible = false;
                    this.Show();
                }
                else
                {
                    // Kullanıcı pencereyi kapattı → uygulamayı kapat
                    this.Close();
                }
            };
            projectForm.Show();
        }

        private void ShowError(string msg)
        {
            lblError.Text = msg;
            lblError.Visible = true;
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnLogin_Click(sender, e);
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtPassword.Focus();
        }
    }
}
