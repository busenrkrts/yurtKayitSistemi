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
    public partial class FrmOgrKayit : Form
    {
        public FrmOgrKayit()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        SqlBaglantim bgl = new SqlBaglantim();
        

        private void Form1_Load(object sender, EventArgs e)
        {
            //Bölüm adlarını veritabanından comboboxa çekme işlemi.
           
            SqlCommand komut = new SqlCommand("Select BolumAd From Bolumler", bgl.baglanti());
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                CmbOgrBolum.Items.Add(oku[0].ToString());

            }
            bgl.baglanti().Close();

            //Boş odaları veritabanından çekme
           
            SqlCommand komut2 = new SqlCommand("Select OdaNo From Odalar where OdaKapasite != OdaAktif", bgl.baglanti()); // Oda kapasitesi odada aktif kalan öğrenci sayısından farklı ise o oda boştur. Bu yüzden sadece öğrenci yerleştirmek için boşluğu olan odalar listelenir.                                                                                                                    // 
            SqlDataReader oku2 = komut2.ExecuteReader();
            while(oku2.Read())
            {
                CmbOdaNo.Items.Add(oku2[0].ToString());
            }
            bgl.baglanti().Close();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                //kaydet butonuna basıldığında öğrencinin bilgileri kaydedilecek. Kaydedince mesaj  kutusu geri bildirimi verecek.
               
                SqlCommand komutKaydet = new SqlCommand("insert into Ogrenci (OgreAd,OgrSoyad,OgrTC,OgrTel,OgrDogum,OgrBolum,OgrMail,OgrOdaNo,OgrVeliAdSoyad,OgrVeliTel,OgrVeliAdres) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11)", bgl.baglanti());
                komutKaydet.Parameters.AddWithValue("@p1", TxtOgrAd.Text);
                komutKaydet.Parameters.AddWithValue("@p2", TxtOgrSoyad.Text);
                komutKaydet.Parameters.AddWithValue("@p3", MskTC.Text);
                komutKaydet.Parameters.AddWithValue("@p4", MskOgrTelNo.Text);
                komutKaydet.Parameters.AddWithValue("@p5", MskDogumTarihi.Text);
                komutKaydet.Parameters.AddWithValue("@p6", CmbOgrBolum.Text);
                komutKaydet.Parameters.AddWithValue("@p7", TxtMail.Text);
                komutKaydet.Parameters.AddWithValue("@p8", CmbOdaNo.Text);
                komutKaydet.Parameters.AddWithValue("@p9", TxtVeliAdSoyad.Text);
                komutKaydet.Parameters.AddWithValue("@p10", MskVeliTel.Text);
                komutKaydet.Parameters.AddWithValue("@p11", RchAdres.Text);
                komutKaydet.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Kayıt başarılı bir şekilde eklendi");


                //öğrenci ıd yi gizli id label bölümüne çekme
                SqlCommand komut = new SqlCommand("select OgrID from Ogrenci", bgl.baglanti());
                SqlDataReader oku=komut.ExecuteReader();
                while(oku.Read())
                {
                    txtGizliID.Text = oku[0].ToString();
                }
                bgl.baglanti().Close();


                //öğrenci borç alanı oluşturma
                SqlCommand komutkaydet2 = new SqlCommand("insert into Borclar (OgrID,OgrAd,OgrSoyad) values (@b1,@b2,@b3)", bgl.baglanti());
                komutkaydet2.Parameters.AddWithValue("@b1",txtGizliID.Text);
                komutkaydet2.Parameters.AddWithValue("@b2", TxtOgrAd.Text);
                komutkaydet2.Parameters.AddWithValue("@b3", TxtOgrSoyad.Text);
                komutkaydet2.ExecuteNonQuery();
                bgl.baglanti().Close();
            }
            catch (Exception)
            {
                //Herhangi bir alan doldurmasında hata olduğunda mesaj kutusu ile geri bildirim verir ve kaydetmez.
                MessageBox.Show("HATA!!! Lütfen yeniden deneyin...");
            }

            //öğrenci oda kontenjanı arttırma
            SqlCommand komutOda = new SqlCommand("update Odalar set OdaAktif=OdaAktif+1 where OdaNo=@oda1", bgl.baglanti());
            komutOda.Parameters.AddWithValue("@oda1", CmbOdaNo.Text);
            komutOda.ExecuteNonQuery();
            bgl.baglanti().Close();

        }
    }
}
