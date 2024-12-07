using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Collections;
using System.Diagnostics;

namespace KR_BD
{
    public partial class FormLogin : Form
    {
        

        // Словарь для хранения хешированных логинов и паролей
        static Dictionary<string, string> userDatabase = new Dictionary<string, string>();
        static string ByteArrayToString(byte[] arrInput)
        {
            int i;
            StringBuilder sOutput = new StringBuilder(arrInput.Length);
            for (i = 0; i < arrInput.Length; i++)
            {
                sOutput.Append(arrInput[i].ToString("X2"));
            }
            return sOutput.ToString();
        }
        // Метод для хеширования строки
        public static string ComputeHash(string inputStr)
        {
            byte[] tmpSource;
            byte[] tmpHash;
            tmpSource = ASCIIEncoding.ASCII.GetBytes(inputStr);

            tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
            return ByteArrayToString(tmpHash);
        }

        public FormLogin()
        {
            InitializeComponent();

            string hashedLogin1 = ComputeHash("admin");
            string hashedPassword1 = ComputeHash("admin");
            string hashedLogin2 = ComputeHash("user");
            string hashedPassword2 = ComputeHash("user");
            userDatabase.Add(hashedLogin1, hashedPassword1);
            userDatabase.Add(hashedLogin2, hashedPassword2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = ComputeHash(textBox1.Text);
            string password = ComputeHash(textBox2.Text);
            if (userDatabase.ContainsKey(login) && userDatabase[login] == password)
            {
                var firstElement = userDatabase.First(); 
                if (login == firstElement.Key)
                {
                    this.Hide();
                    Form2 fm2 = new Form2();
                    fm2.ShowDialog();
                }
                else
                {
                    this.Hide();
                    Form fm1 = new Form1();
                    fm1.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, проверьте логин и пароль");
            }
         

        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Закрыть программу?", "Выход", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
                Environment.Exit(1);
                //Process.GetCurrentProcess().Kill();
            }
            else
            {
                Close();
            }
        }

        private void FormLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show(
            "Вы действительно хотите закрыть программу?",
            "Подтверждение закрытия",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                e.Cancel = true; 
            }
        }

    }
}
