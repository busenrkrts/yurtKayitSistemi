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
    public partial class FrmAdminGiris : Form
    {
        public FrmAdminGiris()
        {
            InitializeComponent();
        }


        SqlBaglantim bgl=new SqlBaglantim();
        private void btnGirisYap_Click(object sender, EventArgs e)
        {
            SqlCommand giris=new SqlCommand("select * from Admin where YoneticiAd=@p1 and YoneticiSifre=@p2",bgl.baglanti());
            giris.Parameters.AddWithValue("@p1", txtKullaniciAd.Text);
            giris.Parameters.AddWithValue("@p2", txtSifre.Text);
            SqlDataReader oku=giris.ExecuteReader();
            if (oku.Read())
            {
                FrmAnaForm git=new FrmAnaForm();
                git.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı Kuıllanıcı Adı veya Şifre");
                txtKullaniciAd.Clear();
                txtSifre.Clear();
                txtKullaniciAd.Focus();
            }
            bgl.baglanti().Close();

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void FrmAdminGiris_Load(object sender, EventArgs e)
        {
            
        }
    }
}
