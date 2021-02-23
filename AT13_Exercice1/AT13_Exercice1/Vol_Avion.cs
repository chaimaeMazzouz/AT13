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

namespace AT13_Exercice1
{
    public partial class Vol_Avion : Form
    {
        static string str_cn = "Server=.\\SQLEXPRESS; database=VolAvion; Integrated security=SSPI";
        SqlConnection Cn_VolAvion = new SqlConnection(str_cn);
        SqlDataReader drd_VolAvion;
        SqlCommand cmd_Passager;
        void remplirCombo()
        {
            cmd_Passager = new SqlCommand("select * from Passager ", Cn_VolAvion);
            try
            {
                Cn_VolAvion.Open();
                drd_VolAvion = cmd_Passager.ExecuteReader();
                while (drd_VolAvion.Read())
                {
                    comboBox1.Items.Add(drd_VolAvion.GetSqlInt32(0));
                }
                comboBox1.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           

        }
        public Vol_Avion()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            remplirCombo();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cn_VolAvion.Close();
            cmd_Passager = new SqlCommand("info_passager_vol", Cn_VolAvion);
            cmd_Passager.CommandType = CommandType.StoredProcedure;
            cmd_Passager.Parameters.Add("@N_pas", SqlDbType.Int);
            cmd_Passager.Parameters["@N_pas"].Value = comboBox1.Text;
          
        }

        private void btnExecuter_Click(object sender, EventArgs e)
        {
            try
            {
                Cn_VolAvion.Open();
                drd_VolAvion = cmd_Passager.ExecuteReader();
                dataGridView1.Columns.Clear();
                dataGridView1.Columns.Add("0", drd_VolAvion.GetName(0));
                dataGridView1.Columns.Add("1", drd_VolAvion.GetName(1));
                dataGridView1.Columns.Add("2", drd_VolAvion.GetName(2));
                dataGridView1.Columns.Add("3", drd_VolAvion.GetName(3));
                dataGridView1.Columns.Add("4", drd_VolAvion.GetName(4));
                while (drd_VolAvion.Read())
                {
                    dataGridView1.Rows.Add(drd_VolAvion.GetString(0), drd_VolAvion.GetString(1), drd_VolAvion.GetDateTime(2), drd_VolAvion.GetString(3), drd_VolAvion.GetString(4));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                drd_VolAvion.Close();
                Cn_VolAvion.Close();
            }
        }
    }
}
