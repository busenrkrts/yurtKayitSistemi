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
    public partial class FrmGelirİstatistik : Form
    {
        public FrmGelirİstatistik()
        {
            InitializeComponent();
        }


        SqlBaglantim bgl = new SqlBaglantim();

        private void FrmGelirİstatistik_Load(object sender, EventArgs e)
        {
            //odemeleri toplayıp gelirlerin toplamını bulma
            SqlCommand komut = new SqlCommand("select Sum(OdemeMiktar) from Kasa", bgl.baglanti());
            SqlDataReader oku=komut.ExecuteReader();
            while (oku.Read())
            {
                lblToplam.Text = oku[0].ToString() + " TL";
            }
            bgl.baglanti().Close();


            // grafiklere veritabanından veri çekme
            SqlCommand komutVeriCek = new SqlCommand("select OdemeAy,sum(OdemeMiktar) from kasa group by OdemeAy", bgl.baglanti());
            SqlDataReader veriOku=komutVeriCek.ExecuteReader();
            while(veriOku.Read())
            {
                this.chart1.Series["Aylık"].Points.AddXY(veriOku[0], veriOku[1]);

            }
            bgl.baglanti().Close();

            //combobox a ayları getirme
            SqlCommand kmt = new SqlCommand("select distinct(OdemeAy) from Kasa", bgl.baglanti());
            SqlDataReader oku2=kmt.ExecuteReader();
            while (oku2.Read())
            {
                cmbAy.Items.Add(oku2[0].ToString());
            }
            bgl.baglanti().Close();
        }

        private void cmbAy_SelectedIndexChanged(object sender, EventArgs e)
        {

            //comboboxta seçilen ayın gelirinin toplamını bulma
            SqlCommand komut = new SqlCommand("select sum(OdemeMiktar) from Kasa Where OdemeAy=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", cmbAy.Text);
            SqlDataReader oku= komut.ExecuteReader();
            while (oku.Read()) 
            {
                lblAyKazanc.Text = oku[0].ToString();

            }
            bgl.baglanti().Close();
        }
    }
}
