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
using System.Runtime.InteropServices;

namespace YurtKayitSistemi
{
    public partial class FrmBolumler : Form
    {
        public FrmBolumler()
        {
            InitializeComponent();
        }

        SqlBaglantim bgl=new SqlBaglantim();


        private void FrmBolumler_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'yurtKayitDataSet.Bolumler' table. You can move, or remove it, as needed.
            this.bolumlerTableAdapter.Fill(this.yurtKayitDataSet.Bolumler);

        }

        private void pcbBolumEkle_Click(object sender, EventArgs e)
        {
            try
            {
                //Bölüm ekleme
               
                SqlCommand komut1 = new SqlCommand("insert into Bolumler(BolumAd) values (@p1)", bgl.baglanti());
                komut1.Parameters.AddWithValue("@p1", txtBolumAd.Text);
                komut1.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Bölüm eklendi");
                this.bolumlerTableAdapter.Fill(this.yurtKayitDataSet.Bolumler); //Bölüm eklendikten sonra tekrar formu refresh ederek göstermeye yarar.

            }
            catch
            {
                //herhangi bir hata oluştuğunda hata mesajı gösterir.
                MessageBox.Show("Hata oluştu yeniden deneyin");
            }
           

        }

        private void pcbBolumSil_Click(object sender, EventArgs e)
        {
            try
            {
               
                SqlCommand komut2 = new SqlCommand("delete from Bolumler where BolumID=@p1", bgl.baglanti());
                komut2.Parameters.AddWithValue("@p1", txtBolumID.Text);
                komut2.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Silme işlemi gerçekleşti");
                this.bolumlerTableAdapter.Fill(this.yurtKayitDataSet.Bolumler); //Bölüm silindikten sonra tekrar formu refresh ederek göstermeye yarar.

            }

            catch (Exception)
            {
                MessageBox.Show("Hata oluştu tekrar deneyiniz!");
            }
           
        }
        int secilen;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string id, bolumad;
            //datagridde seçilen satırın indexini secilen adı altında hafızaya attık.
            secilen = dataGridView1.SelectedCells[0].RowIndex;
            //secilen indexteki 0. ve 1. sütundaki bilgileri yani o satırın id ve ad bilgilerini id ve bolumad isimli geçici değişkenlerde tuttuk.
            id = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            bolumad = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            //geçici değişkenlerdeki değerleri uygun olan text araçlarımıza atayıp gözükmesini sağladık.
            txtBolumID.Text = id;
            txtBolumAd.Text = bolumad;

        }

        private void pcbBolumDuzenle_Click(object sender, EventArgs e)
        {
            try
            {
               
                // güncelleme işlemi
                SqlCommand komut3 = new SqlCommand("update Bolumler set BolumAd=@p1 where BolumID=@p2", bgl.baglanti());
                komut3.Parameters.AddWithValue("@p1", txtBolumAd.Text);
                komut3.Parameters.AddWithValue("@p2", txtBolumID.Text);
                komut3.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Güncelleme tamamlandı.");
                this.bolumlerTableAdapter.Fill(this.yurtKayitDataSet.Bolumler);
            }
            catch
            {
                MessageBox.Show("Hata");

            }
        }
    }
}
