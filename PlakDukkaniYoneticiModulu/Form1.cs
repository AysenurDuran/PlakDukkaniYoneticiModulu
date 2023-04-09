using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PlakDukkaniYoneticiModulu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        AlbumDbContext db = new AlbumDbContext();
        private void Form1_Load(object sender, EventArgs e)
        {
            txtParola.PasswordChar = '*';
            txtParolaTekrari.PasswordChar = '*';
        }

        private void btnKullaniciOlustur_Click(object sender, EventArgs e)
        {
            string parola = txtParola.Text;

            if (!IsValidPassword(parola))
            {
                MessageBox.Show("Şifre kriterlerine uymuyor. Lütfen 8 karakterden fazla bir şifre girin ve en az 2 büyük harf, en az 3 küçük harf, (!), (:), (+), (*) karakterlerinden en az 2 tanesini içermelidir");
                return;
            }

            MessageBox.Show("Şifre kabul edildi.");


            sha256_hash(txtParola.Text);
            sha256_hash(txtParolaTekrari.Text);
            var user = db.Users.FirstOrDefault(a => a.UserName == txtKullaniciAdi.Text);
            if (user == null)
            {
                //Kayıt işlemi
                if (txtParola.Text == txtParolaTekrari.Text)
                {
                    using (var db = new AlbumDbContext())
                    {
                        db.Users.Add(new User { UserName = txtKullaniciAdi.Text, Password = sha256_hash( txtParola.Text) });
                        db.SaveChanges();
                        MessageBox.Show("Kayıt Başarılı!");
                    }
                }
                else
                {
                    MessageBox.Show("Parola Tekrarı Girmediniz veya Yanlış Girdiniz");
                }
                
            }
            else
            {
                
                MessageBox.Show("Bu kullanıcı adı daha önce alınmış!");
            }

            txtKullaniciAdi.Text = "";
            txtParola.Text = "";
            txtParolaTekrari.Text = "";
            
            
        }

        private static string sha256_hash(string sifre)
        {
            using (SHA256 hash = SHA256Managed.Create())
            {

                return string.Concat(hash.ComputeHash(Encoding.UTF8.GetBytes(sifre)).Select(c => c.ToString("X2")));

            }
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            
            sha256_hash(txtParola.Text);
            var kullanici = db.Users.FirstOrDefault(user => user.UserName == txtKullaniciAdi.Text );
            if (kullanici != null)
            {
                if (kullanici.Password == sha256_hash( txtParola.Text))
                {
                    Form2 form2 = new Form2();
                    form2.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Parolayı yanlış girdiniz !");
                }
                    
            }
            else
            {

                MessageBox.Show("Kullanıcı adını hatalı girdiniz! Lütfen tekrar deneyiniz.");
                txtKullaniciAdi.Text = txtParola.Text = "";
            }

            
        }
        bool IsValidPassword(string parola)
        {
            Regex regex = new Regex(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[!:\+\*])(?=.{8,}).*$");
            return regex.IsMatch(parola);
        }
    }
}
