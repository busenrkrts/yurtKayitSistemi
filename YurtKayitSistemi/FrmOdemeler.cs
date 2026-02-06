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

namespace YurtKayitSistemi
{
    public partial class FrmOdemeler : Form
    {
        public FrmOdemeler()
        {
            InitializeComponent();
        }

        SqlBaglantim bgl = new SqlBaglantim();

        private void FrmOdemeler_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'yurtKayitDataSet2.Borclar' table. You can move, or remove it, as needed.
            this.borclarTableAdapter.Fill(this.yurtKayitDataSet2.Borclar);

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            //gridView den seçilen satırın değerlerinin textBox lara aktarımı.
            int secilen;
            string id, ad, soyad, kalan;
            secilen = dataGridView1.SelectedCells[0].RowIndex;
            id = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            ad = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            soyad= dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            kalan= dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            txtAd.Text = ad;
            txtSoyad.Text= soyad;
            txtKalan.Text= kalan;
            txtOgrID.Text= id;
        }

        private void btnOdemeAl_Click(object sender, EventArgs e)
        {

            //ödenen tutarı kalandan eksiltme
            int odenen, kalan, yeniBorc;
            odenen = Convert.ToInt16(txtOdenen.Text);
            kalan=Convert.ToInt16(txtKalan.Text);
            yeniBorc = kalan - odenen;
            txtKalan.Text = yeniBorc.ToString();


            //veritabanı güncelleme
            SqlCommand komut = new SqlCommand("update Borclar set OgreKalanBorc=@p1 where OgrID=@p2", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtKalan.Text);
            komut.Parameters.AddWithValue("@p2", txtOgrID.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Borç ödendi.");

            //formda anlık datagrid güncellemesi
            this.borclarTableAdapter.Fill(this.yurtKayitDataSet2.Borclar);

            //kasa tablosuna ekleme

            SqlCommand komut2 = new SqlCommand("insert into Kasa (OdemeAy,OdemeMiktar) values (@p1,@p2)", bgl.baglanti());
            komut2.Parameters.AddWithValue("@p1", txtOdeneAy.Text);
            komut2.Parameters.AddWithValue("@p2", txtOdenen.Text);
            komut2.ExecuteNonQuery();
            bgl.baglanti().Close();

        }
    }
}
