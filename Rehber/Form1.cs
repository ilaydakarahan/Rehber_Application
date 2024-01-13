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

namespace Rehber
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-BFPJH2M;
        Initial Catalog=Rehber;Integrated Security=True");
        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from KISILER", baglanti);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        void temizle()
        {
            txtad.Text = "";
            txtid.Text = "";
            txtSoyad.Text = "";
            txtMail.Text = "";
            mskTel.Text = "";
            pictureBox1.Refresh();
            txtad.Focus();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into KISILER (AD,SOYAD,TELEFON,MAIL,FOTOGRAF) " +
                "values (@P1,@P2,@P3,@P4,@P5)", baglanti);
            komut.Parameters.AddWithValue("@P1", txtad.Text);
            komut.Parameters.AddWithValue("@P2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@P3", mskTel.Text);
            komut.Parameters.AddWithValue("@P4", txtMail.Text);
            komut.Parameters.AddWithValue("@P5",txtFoto.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kişi sisteme kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            txtid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtad.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            mskTel.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            txtMail.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            txtFoto.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();

            pictureBox1.ImageLocation= dataGridView1.Rows[secilen].Cells[5].Value.ToString();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            //baglanti.Open();
            //SqlCommand komut = new SqlCommand("Delete From KISILER where ID=" + txtid.Text, baglanti);
            //komut.ExecuteNonQuery();
            //baglanti.Close();
            //MessageBox.Show("Kişi rehberden silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //listele();
            //temizle();

            baglanti.Open();

            DialogResult soru = new DialogResult();
            soru = MessageBox.Show("Seçilen kaydı silmek istediğinize emin misiniz?", "UYARI", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (soru == DialogResult.Yes)
            {

                SqlCommand cmd = new SqlCommand("DELETE FROM KISILER WHERE ID=@P1", baglanti);
                cmd.Parameters.AddWithValue("@P1", txtid.Text);
                cmd.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Seçilen kişi silindi.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                temizle();
            }
            else
            {
                baglanti.Close();
                MessageBox.Show("Kişi rehberden silinemedi.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                temizle();
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update KISILER set " +
                "AD=@P1,SOYAD=@P2,TELEFON=@P3,MAIL=@P4,FOTOGRAF=@P5 where ID=@P6", baglanti);
            komut.Parameters.AddWithValue("@P1",txtad.Text);
            komut.Parameters.AddWithValue("@P2",txtSoyad.Text);
            komut.Parameters.AddWithValue("@P3",mskTel.Text);
            komut.Parameters.AddWithValue("@P4",txtMail.Text);
            komut.Parameters.AddWithValue("@P5",txtFoto.Text);
            komut.Parameters.AddWithValue("@P6",txtid.Text);

            komut.ExecuteNonQuery();
            baglanti.Close() ;
            MessageBox.Show("Kişi bilgisi güncellendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
            temizle();        
        }

        private void btnFoto_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            pictureBox1.ImageLocation = openFileDialog1.FileName;
            txtFoto.Text = openFileDialog1.FileName.ToString();
        }
    }
}
