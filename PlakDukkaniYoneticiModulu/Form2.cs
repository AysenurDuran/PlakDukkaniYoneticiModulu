using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PlakDukkaniYoneticiModulu
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        AlbumDbContext db = new AlbumDbContext();
        private void btnEkle_Click(object sender, EventArgs e)
        {
            Album veriEkleme = new Album()
            {
                AlbumAdi=txtAlbumAdi.Text,
                AlbumSanatcisi=txtAlbumSanatcisi.Text,
                AlbumCikisTarihi=dateTimePicker1.Value,
                AlbumFiyati=Convert.ToDecimal(txtFiyati.Text),
                İndirimOrani=Convert.ToDouble(txtIndirim.Text),
                SatisDevami=txtDurumu.Text,

            };
            db.Albums.Add(veriEkleme);
            db.SaveChanges();
            dataGridView1.DataSource = db.Albums.ToList();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Albums.ToList();
            var albumler1 = (from a in db.Albums
                            where
                            a.SatisDevami == "satışı yok"
                            select new { a.AlbumAdi, a.AlbumSanatcisi }).ToList();
            dataGridView2.DataSource = albumler1;

            var albumler2 = (from a in db.Albums
                             where
                             a.SatisDevami == "satışı devam ediyor"
                             select new { a.AlbumAdi, a.AlbumSanatcisi }).ToList();
            dataGridView3.DataSource = albumler2;
            var albumler3 = (from a in db.Albums
                             orderby a.AlbumId descending
                             select new { a.AlbumAdi, a.AlbumSanatcisi }).Take(10).ToList();
            dataGridView4.DataSource = albumler3;
            var albumler4 = (from a in db.Albums
                             where a.İndirimOrani>0
                             orderby a.İndirimOrani descending
                             select new { a.AlbumAdi, a.AlbumSanatcisi }).ToList();
            dataGridView5.DataSource=albumler4; 
           
            

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            var guncelle = db.Albums.Where(x => x.AlbumId.ToString()== txtId.Text).FirstOrDefault();
            if (txtId.Text!= null && txtAlbumAdi.Text != null && txtAlbumSanatcisi.Text != null && txtDurumu.Text != null && txtFiyati.Text != null && txtIndirim.Text != null)
            {

                guncelle.AlbumAdi = txtAlbumAdi.Text;
                guncelle.AlbumSanatcisi = txtAlbumSanatcisi.Text;
                guncelle.AlbumCikisTarihi = dateTimePicker1.Value;
                guncelle.AlbumFiyati = Convert.ToDecimal(txtFiyati.Text);
                guncelle.İndirimOrani = Convert.ToDouble(txtIndirim.Text);
                guncelle.SatisDevami = txtDurumu.Text;
                db.SaveChanges();
                dataGridView1.DataSource = db.Albums.ToList();
            }
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 1)
            {
                txtId.Text = dataGridView1.SelectedCells[0].Value.ToString();
                txtAlbumAdi.Text = dataGridView1.SelectedCells[1].Value.ToString();
                txtAlbumSanatcisi.Text = dataGridView1.SelectedCells[2].Value.ToString();
                dateTimePicker1.Text = dataGridView1.SelectedCells[3].Value.ToString();
                txtFiyati.Text = dataGridView1.SelectedCells[4].Value.ToString();
                txtIndirim.Text = dataGridView1.SelectedCells[5].Value.ToString();
                txtDurumu.Text = dataGridView1.SelectedCells[6].Value.ToString();
            }
        }
        private void btnSil_Click(object sender, EventArgs e)
        {
            var sil = db.Albums.Where(x => x.AlbumId.ToString() == txtId.Text).FirstOrDefault();
            db.Albums.Remove(sil);
            db.SaveChanges();
            dataGridView1.DataSource = db.Albums.ToList();
        }

        
    }
}
