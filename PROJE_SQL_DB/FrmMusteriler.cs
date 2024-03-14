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
    public partial class FrmMusteriler : Form
    {
        public FrmMusteriler()
        {
            InitializeComponent();
        }

        SqlConnection bgl = new SqlConnection(@"Data Source=DESKTOP-QUL77PV\SQLEXPRESS;Initial Catalog=SatisVT;Integrated Security=True");

        void list()
        {
            SqlCommand komut = new SqlCommand("Select * from TBLMUSTERI", bgl);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }
        void clear()
        {
            Txtid.Text = "";
            Txtad.Text = "";
            TxtSoyad.Text = "";
            CmbSehir.Text = "";
            TxtBakiye.Text = "";

        }
        private void FrmMusteri_Load(object sender, EventArgs e)
        {
            list();

            bgl.Open();
            SqlCommand komut = new SqlCommand("Select * from iller", bgl);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                CmbSehir.Items.Add(dr["sehir"]);
            }
            bgl.Close();
        }

        private void BtnListele_Click(object sender, EventArgs e)
        {
            list();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Txtid.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            Txtad.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            CmbSehir.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            TxtBakiye.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();

        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            bgl.Open();
            SqlCommand komut = new SqlCommand("insert into TBLMUSTERI(MUSTERIAD,MUSTERISOYAD,MUSTERISEHIR,MUSTERIBAKIYE) values (@P1,@P2,UPPER(@P3),@P4)", bgl);
            komut.Parameters.AddWithValue("@P1", Txtad.Text);
            komut.Parameters.AddWithValue("@P2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@P3", CmbSehir.Text);
            komut.Parameters.AddWithValue("@P4", decimal.Parse(TxtBakiye.Text));
            komut.ExecuteNonQuery();
            bgl.Close();
            MessageBox.Show("Müşteri Sisteme Kaydedilmiştir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            list();

        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            bgl.Open();
            SqlCommand komut = new SqlCommand("Delete from TBLMUSTERI where MUSTERIID=@p1", bgl);
            komut.Parameters.AddWithValue("@p1", Txtid.Text);
            komut.ExecuteNonQuery();
            bgl.Close();
            MessageBox.Show("Müşteri Sistemden Silinmiştir", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            clear();
            list();

        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            bgl.Open();
            SqlCommand komut = new SqlCommand("Update TBLMUSTERI set MUSTERIAD=@p1,MUSTERISOYAD=@p2,MUSTERISEHIR=@p3,MUSTERIBAKIYE=@p4 where MUSTERIID=@p5", bgl);
            komut.Parameters.AddWithValue("@p1", Txtad.Text);
            komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", CmbSehir.Text);
            komut.Parameters.AddWithValue("@p4", decimal.Parse(TxtBakiye.Text));
            komut.Parameters.AddWithValue("@p5", Txtid.Text);
            komut.ExecuteNonQuery();
            bgl.Close();
            MessageBox.Show("Müşteri Sistemde Güncellenmiştir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            clear();
            list();
        }

        private void BtnAra_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Select * From TBLMUSTERI where MUSTERIAD like '%'+@p1+'%'", bgl);
            komut.Parameters.AddWithValue("@p1", Txtad.Text);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
