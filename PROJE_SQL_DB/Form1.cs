using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROJE_SQL_DB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection bgl = new SqlConnection(@"Data Source=DESKTOP-QUL77PV\SQLEXPRESS;Initial Catalog=SatisVT;Integrated Security=True");
        private void BtnKategori_Click(object sender, EventArgs e)
        {
            FrmKategoriler fr = new FrmKategoriler();
            fr.Show();
        }

        private void BtnMusteriler_Click(object sender, EventArgs e)
        {
            FrmMusteriler fr = new FrmMusteriler();
            fr.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //********Ürünlerin Durum Seviyesi*************
            //Ürünlerin Durum Seviyesi Procedure
            SqlCommand komut = new SqlCommand("Execute Durum", bgl);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            //********Grafiğe veri Çekme***********
            bgl.Open();
            SqlCommand grafik = new SqlCommand("select KATEGORIAD,Count(*) as 'ADET' from TBLKATEGORI inner join TBLURUNLER\r\non TBLKATEGORI.KATEGORIID=TBLURUNLER.KATEGORI group by KATEGORIAD", bgl);
            SqlDataReader dr = grafik.ExecuteReader();
            while (dr.Read())
            {
                chart1.Series["Kategoriler"].Points.AddXY(dr[0], dr[1]);
            }
            bgl.Close();
            //*************2.Grafik Şehirler*****************
            bgl.Open();
            SqlCommand grafik2 = new SqlCommand("select MUSTERISEHIR,COUNT(*) as 'Toplam Şehir' from TBLMUSTERI group by MUSTERISEHIR", bgl);
            SqlDataReader dr2 = grafik2.ExecuteReader();
            while (dr2.Read())
            {
                chart2.Series["Şehirler"].Points.AddXY(dr2[0], dr2[1]);
            }
            bgl.Close();
        }
    }
}
