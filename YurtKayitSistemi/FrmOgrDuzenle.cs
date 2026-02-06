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
    public partial class FrmOgrDuzenle : Form
    {
        public FrmOgrDuzenle()
        {
            InitializeComponent();
        }

        SqlBaglantim bgl=new SqlBaglantim();

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public string id, ad, soyad, TC, telefon, dogum, bolum;

        private void button1_Click(object sender, EventArgs e)
        {
            //öğrenciyi silme
            SqlCommand komutSil=new SqlCommand("delete from Ogrenci where OgrID=@k1",bgl.baglanti());
            komutSil.Parameters.AddWithValue("@k1", txtİD.Text);
            komutSil.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kayıt silindi");

            //oda kontenjanı arttırma yani aktif kalan öğrenci sayısını azaltma
            SqlCommand komutOda = new SqlCommand("update Odalar set OdaAktif=OdaAktif-1 where OdaNo=@o1", bgl.baglanti());
            komutOda.Parameters.AddWithValue("@o1", CmbOdaNo.Text);
            komutOda.ExecuteNonQuery();
            bgl.baglanti().Close();
            
        }

        public string mail, odaNo, veliAdSoyad, veliTelefon, adres;

        private void FrmOgrDuzenle_Load(object sender, EventArgs e)
        {
            txtİD.Text = id;
            TxtOgrAd.Text = ad;
            TxtOgrSoyad.Text = soyad;
            MskTC.Text = TC;
            MskOgrTelNo.Text = telefon;
            MskDogumTarihi.Text = dogum;
            CmbOgrBolum.Text = bolum;
            TxtMail.Text = mail;
            CmbOdaNo.Text = odaNo;
            TxtVeliAdSoyad.Text= veliAdSoyad;
            MskVeliTel.Text= veliTelefon;
            RchAdres.Text = adres;
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update Ogrenci set OgreAd=@p2,OgrSoyad=@p3,OgrTC=@p4,OgrTel=@p5,OgrDogum=@p6,OgrBolum=@p7,OgrMail=@p8,OgrOdaNo=@p9,OgrVeliAdSoyad=@p10,OgrVeliTel=@p11,OgrVeliAdres=@p12 where OgrID=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtİD.Text);
            komut.Parameters.AddWithValue("@p2", TxtOgrAd.Text);
            komut.Parameters.AddWithValue("@p3", TxtOgrSoyad.Text);
            komut.Parameters.AddWithValue("@p4", MskTC.Text);
            komut.Parameters.AddWithValue("@p5", MskOgrTelNo.Text);
            komut.Parameters.AddWithValue("@p6", MskDogumTarihi.Text);
            komut.Parameters.AddWithValue("@p7", CmbOgrBolum.Text);
            komut.Parameters.AddWithValue("@p8", TxtMail.Text);
            komut.Parameters.AddWithValue("@p9", CmbOdaNo.Text);
            komut.Parameters.AddWithValue("@p10", TxtVeliAdSoyad.Text);
            komut.Parameters.AddWithValue("@p11", MskVeliTel.Text);
            komut.Parameters.AddWithValue("@p12", RchAdres.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();

        }   
    }
}
