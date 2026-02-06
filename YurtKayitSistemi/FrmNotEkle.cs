using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace YurtKayitSistemi
{
    public partial class FrmNotEkle : Form
    {
        public FrmNotEkle()
        {
            InitializeComponent();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Title = "Kayıt Yeri Seçin"; //a.ılan pencerenin başlığı
            saveFileDialog1.Filter = "Metin Dosyası | *.txt"; //sadece txt uzantılı dosya olarak kaydedilebilsin
            saveFileDialog1.InitialDirectory = "C:\\Proje Notlar"; // c sürücüsündeki proje notlar kısmı açık olarak gelsin.

            saveFileDialog1.ShowDialog();
            StreamWriter kaydet= new StreamWriter(saveFileDialog1.FileName); // yazılan dosya adını kaydet
            kaydet.WriteLine(richTextBox1.Text);
            kaydet.Close();
            MessageBox.Show("Not Kaydedildi.");
        }
    }
}
