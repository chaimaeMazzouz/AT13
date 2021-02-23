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

namespace AT13_Exercice2
{
    public partial class CompagnieVoyage : Form
    {
        static string str_cn = "Server=.\\SQLEXPRESS; database=CompagnieVoyage; Integrated security=SSPI";
        SqlConnection Cn_voyage = new SqlConnection(str_cn);
        SqlDataReader drd_voyage;
        SqlCommand cmd_voyage;
        void remplirCombo()
        {
            cmd_voyage = new SqlCommand("select * from voyage", Cn_voyage);
            try
            {
                Cn_voyage.Open();
                drd_voyage = cmd_voyage.ExecuteReader();
                while (drd_voyage.Read())
                {
                    comboBox1.Items.Add(drd_voyage.GetSqlString(2));
                    comboBox2.Items.Add(drd_voyage.GetSqlString(3));
                }
                comboBox1.SelectedIndex = 0;
                comboBox2.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cn_voyage.Close();
            }


        }
        public CompagnieVoyage()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Cn_voyage.Close();
            //cmd_voyage = new SqlCommand("ps_vol_ville", Cn_voyage);
            //cmd_voyage.CommandType = CommandType.StoredProcedure;
            //cmd_voyage.Parameters.Add("@Ville_Dep", SqlDbType.VarChar);
            //cmd_voyage.Parameters["@Ville_Dep"].Value =comboBox1.Text;
        }

        private void CompagnieVoyage_Load(object sender, EventArgs e)
        {
            remplirCombo();
        }

        private void btnExecuter_Click(object sender, EventArgs e)
        {
            try
            {
                Cn_voyage.Open();
                cmd_voyage = new SqlCommand("ps_vol_ville", Cn_voyage);
                cmd_voyage.CommandType = CommandType.StoredProcedure;
                cmd_voyage.Parameters.Add("@Ville_Dep", SqlDbType.VarChar);
                cmd_voyage.Parameters["@Ville_Dep"].Value = comboBox1.Text;
                cmd_voyage.Parameters.Add("@Ville_Arr", SqlDbType.VarChar);
                cmd_voyage.Parameters["@Ville_Arr"].Value = comboBox2.Text;
                drd_voyage = cmd_voyage.ExecuteReader();
                dataGridView1.Columns.Clear();
                dataGridView1.Columns.Add("0", drd_voyage.GetName(0));
                dataGridView1.Columns.Add("1", drd_voyage.GetName(1));
                dataGridView1.Columns.Add("2", drd_voyage.GetName(2));
                dataGridView1.Columns.Add("3", drd_voyage.GetName(3));
                while (drd_voyage.Read())
                {
                    dataGridView1.Rows.Add(drd_voyage.GetInt32(0), drd_voyage.GetDateTime(1), drd_voyage.GetInt32(2), drd_voyage.GetDecimal(3));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                drd_voyage.Close();
                Cn_voyage.Close();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Cn_voyage.Close();
            //cmd_voyage = new SqlCommand("ps_vol_ville", Cn_voyage);
            //cmd_voyage.CommandType = CommandType.StoredProcedure;
            //cmd_voyage.Parameters.Add("@Ville_Arr", SqlDbType.VarChar);
            //cmd_voyage.Parameters["@Ville_Arr"].Value = comboBox2.Text;
        }
    }
}
