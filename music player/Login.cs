using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Web.UI.WebControls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Net;
using System.Net.Mail;
namespace music_player
{
    public partial class Login : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-32MA6TL;Initial Catalog=Informaton;Integrated Security=TrueData Source=DESKTOP-32MA6TL;Initial Catalog=Information;Integrated Security=True;Connect Timeout=30;Encrypt=False");
        public Login()
        {
            InitializeComponent();
        }

        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void loginlayout_Paint(object sender, PaintEventArgs e)
        {
            if (loginlayout.Visible == true)
            {
                this.AcceptButton = login_btn;
                //username.Focus();
            }
        }

        private void login_btn_Click(object sender, EventArgs e)
        {
            
            string un = username.Text;
            string pass = pwd.Text;
            if((un=="" || pass == "")||(un==""&&pass==""))
            {
                if (un == "" && pass == "")
                {
                    un_req.Visible = true;
                    pwd_req.Visible = true;
                    username.Focus();
                }
                else if (un == "" || pass=="")
                {
                    if (pass == "")
                    {
                        un_req.Visible = false;
                        pwd_req.Visible = true;
                        pwd.Focus();
                    }
                    else
                    {
                        un_req.Visible = true;
                        pwd_req.Visible = false;
                        username.Focus();
                    }
                }
            }
            else
            {
                un_req.Visible = false;
                pwd_req.Visible = false;
                conn.Open();
                string query = "select * from users where username =@un and password=@pass";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@un", username.Text);
                cmd.Parameters.AddWithValue("@pass", pwd.Text);
                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Login successful");
                }
                else
                {
                    MessageBox.Show("Wrong credentials");
                }

                conn.Close();
            }
            

        }

        private void signup_panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void signup_Click(object sender, EventArgs e)
        {
            loginlayout.Visible = false;
            signup_panel.Visible = true;
            su_name.Focus();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
         
        }

        

        private void username_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) 
            {
                pwd.Focus() ;
                e.Handled = true; 
            }
        }

        private void pwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) 
            {
                username.Focus(); 
                e.Handled = true; 
            }
            else if (e.KeyCode == Keys.Down)
            {
                login_btn.Focus();
                e.Handled = true;
            }
        }

        private void su_name_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                su_username.Focus();
                e.Handled = true;
            }
            

        }

        private void su_username_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                su_pwd.Focus();
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Up)
            {
                su_name.Focus();
                e.Handled = true;
            }
        }

        private void su_pwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                su_cpwd.Focus();
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Up)
            {
                su_username.Focus();
                e.Handled = true;
            }
        }

        private void su_cpwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                su_email.Focus();
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Up)
            {
                su_pwd.Focus();
                e.Handled = true;
            }
        }

        private void su_email_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                su_mobile.Focus();
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Up)
            {
                su_cpwd.Focus();
                e.Handled = true;
            }
        }

        private void su_mobile_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                signup_btn.Focus();
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Up)
            {
                su_email.Focus();
                e.Handled = true;
            }
        }

        private void signup_btn_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.Down)
            {
                log_in.Focus();
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Up)
            {
                su_mobile.Focus();
                e.Handled = true;
            }
        }

        private void signup_btn_Click_1(object sender, EventArgs e)
        {
            string n = su_name.Text;
            string usern = su_username.Text;
            string pswd = su_pwd.Text;
            string cpswd = su_cpwd.Text;
            string eml = su_email.Text;
            string mob = su_mobile.Text;
            if (n == "" || usern == "" || pswd == "" || cpswd == "" || eml == "" || mob == "")
            {
                required.Visible = true;
            }
            else
            {
                required.Visible = false;
                if(pswd.Length<8 || pswd.Length>15||pswd.Contains(" ") || !pswd.Any(char.IsDigit)||!pswd.Any(char.IsLetter))
                {
                    MessageBox.Show("Password length should be between 8 to 15 \nShould not contain whitespace\nShould contain Alphanumeric string");
                }
                else
                {
                    conn.Open();
                    string query = "select * from users where username =@usern";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@usern", usern);
                    SqlDataAdapter ad = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    ad.Fill(dt);

                    SqlCommand cmd2 = new SqlCommand("select * from users where email=@eml", conn);
                    cmd2.Parameters.AddWithValue("@eml", eml);
                    SqlDataAdapter ad2 = new SqlDataAdapter(cmd2);
                    DataTable dt2 = new DataTable();
                    ad2.Fill(dt2);

                    SqlCommand cmd3 = new SqlCommand("select * from users where mobile_no=@mob", conn);
                    cmd3.Parameters.AddWithValue("@mob", mob);
                    SqlDataAdapter ad3 = new SqlDataAdapter(cmd3);
                    DataTable dt3 = new DataTable();
                    ad3.Fill(dt3);
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show("Username not available");
                        su_username.Focus();
                    }
                    else if (pswd != cpswd)
                    {
                        MessageBox.Show("Password Not Matched!");
                        su_pwd.Focus();
                    }
                    else if (!IsValidEmail(eml))
                    {
                        MessageBox.Show("Enter a valid email address.");
                        su_email.Focus();
                        return;
                    }
                    else if (mob.Length < 10)
                    {
                        MessageBox.Show("Mobile no. should be of 10 digits");
                        su_mobile.Focus();

                    }
                    else if(dt2.Rows.Count>0)
                    {
                        MessageBox.Show("Email already registered!");
                        su_email.Focus();
                    }
                    else if (dt3.Rows.Count > 0)
                    {
                        MessageBox.Show("Mobile No. already registered!");
                        su_mobile.Focus();
                    }
                    else { 
                        SqlCommand cmd1 = new SqlCommand("insert into users values(@name,@username,@password,@email,@mobile)", conn);
                        cmd1.Parameters.AddWithValue("@name", n);
                        cmd1.Parameters.AddWithValue("@username", usern);
                        cmd1.Parameters.AddWithValue("@password", pswd);
                        cmd1.Parameters.AddWithValue("@email", eml);
                        cmd1.Parameters.AddWithValue("@mobile", mob);
                        cmd1.ExecuteNonQuery();
                        MessageBox.Show("Account Created successfully!\nLogin Now");
                        conn.Close();
                        signup_panel.Visible = false;
                        loginlayout.Visible = true;

                    }
                    conn.Close();
                }
                
            }
            
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // use Regular Expressions to validate the email address
                var regex = new System.Text.RegularExpressions.Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
                return regex.IsMatch(email.Trim());
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        private void log_in_Click_1(object sender, EventArgs e)
        {
            loginlayout.Visible = true;
            signup_panel.Visible = false;
            username.Focus();
        }

        private void signup_panel_Paint_1(object sender, PaintEventArgs e)
        {
            if (signup_panel.Visible == true)
            {
                this.AcceptButton = signup_btn;

            }
        }

        private void fpwd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
        }

        private void fpwd_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panel2.Visible = true;
            fp_email.Focus();
            loginlayout.Visible=false;
            signup_panel.Visible=false;
            send_btn.Visible = true;
            submit_btn.Visible = false;
            this.AcceptButton = send_btn;
        }

        private void back_btn_Click(object sender, EventArgs e)
        {
            if (fp_email.Visible == true)
            {
                panel2.Visible = false;
                loginlayout.Visible = true;
            }
            else
            {
                newpass_label.Visible = false;
                newpass.Visible = false;
                cnewpass_label.Visible = false;
                confirm_newpass.Visible = false;
                submit_password_btn.Visible = false;

                fp_email_label.Visible = true;
                fp_email.Visible = true;
                fp_otp.Visible = true;
                fp_otp_label.Visible = true;
                resend_btn.Visible = true;
                send_btn.Visible = true;
                submit_btn.Visible = true;
                fp_email.Text = null; 
                fp_otp.Text=null;
            }
            
        }

        

        private void send_btn_Click(object sender, EventArgs e)
        {
            
            
            if (fp_email.Text == "")
            {
                MessageBox.Show("Enter email address!");
                fp_email.Focus();
            }
            else if (!IsValidEmail(fp_email.Text))
            {
                MessageBox.Show("Enter a valid email address.");
                fp_email.Focus();
                return;
            }
            else
            {
                string confirm_email = fp_email.Text;
                string npass = newpass.Text;
                string cnpass = confirm_newpass.Text;
                conn.Open();
                SqlCommand cmd4 = new SqlCommand("Select * from users where email=@confirm_email", conn);
                cmd4.Parameters.AddWithValue("@confirm_email", confirm_email);
                SqlDataAdapter ad4 = new SqlDataAdapter(cmd4);
                DataTable dt4 = new DataTable();
                ad4.Fill(dt4);
                if (dt4.Rows.Count > 0)
                {
                    send_btn.Visible = false;
                    submit_btn.Visible = true;
                    fp_otp.Focus();
                    //SendOTP(fp_email.Text);
                    //resend_btn.Enabled = true;
                    timer1.Enabled = true;
                    this.AcceptButton = submit_btn;
                }
                else
                {
                    MessageBoxButtons btn = MessageBoxButtons.YesNo;
                    DialogResult res= MessageBox.Show("No account associated with given email\nDo you want to create account?","Alert",btn);
                    if (res == DialogResult.Yes)
                    {
                        su_email.Text = fp_email.Text;
                        fp_email.Text = null;
                        panel2.Visible = false;
                        signup_panel.Visible = true;
                        su_name.Focus();
                        
                    }
                    else
                    {
                        DialogResult ext = MessageBox.Show("Do you want to exit?", "Alert", btn);
                        if (ext == DialogResult.Yes)
                        {
                            Close();
                        }
                    }

                }
                conn.Close();
                
            }

        }

        private void resend_btn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("OTP sent!!");
            timer1.Enabled = true;
            resend_btn.Enabled = false;
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            resend_btn.Enabled= true;
            timer1.Enabled = false;
        }

        private void submit_btn_Click(object sender, EventArgs e)
        {
            if (fp_otp.Text == "1234")
            {
                fp_email_label.Visible = false;
                fp_email.Visible = false;
                fp_otp.Visible = false;
                fp_otp_label.Visible = false;
                resend_btn.Visible = false;
                send_btn.Visible = false;
                submit_btn.Visible = false;

                newpass_label.Visible = true;
                newpass.Visible = true;
                cnewpass_label.Visible = true;
                confirm_newpass.Visible = true;
                submit_password_btn.Visible = true;
                newpass.Focus();
                this.AcceptButton = submit_password_btn;
            }
            else if (fp_otp.Text == "")
            {
                MessageBox.Show("Enter OTP!");
                fp_otp.Focus();
            }
            else
            {
                MessageBox.Show("Incorrect OTP");
                fp_otp.Focus();
            }
        }

        private void submit_password_btn_Click(object sender, EventArgs e)
        {
            string confirm_email = fp_email.Text;
            string npass = newpass.Text;
            string cnpass = confirm_newpass.Text;
            if (npass != cnpass)
            {
                MessageBox.Show("Password not matched!");
            }
            else if (npass.Length < 8 || npass.Length > 15 || npass.Contains(" ") || !npass.Any(char.IsDigit) || !npass.Any(char.IsLetter))
            {
                MessageBox.Show("Password length should be between 8 to 15 \nShould not contain whitespace\nShould contain Alphanumeric string");
            }
            else
            {
                conn.Open();
                SqlCommand cmd5 = new SqlCommand("update users set password=@npass where email=@confirm_email", conn);
                cmd5.Parameters.AddWithValue("@npass", npass);
                cmd5.Parameters.AddWithValue("@confirm_email", confirm_email);
                cmd5.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Password successfully changed!");
                loginlayout.Visible = true;
                panel2.Visible = false;
                username.Text = null;
                pwd.Text = null;
                
            }
        }

        private void newpass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                confirm_newpass.Focus();
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Up)
            {
                submit_password_btn.Focus();
                e.Handled = true;
            }
        }

        private void confirm_newpass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                submit_password_btn.Focus();
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Up)
            {
                newpass.Focus();
                e.Handled = true;
            }
        }
    }
}
