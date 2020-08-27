using System;
using System.Windows.Forms;
using PDK.SQL;

namespace TestApplication
{
    public partial class MSSQL : Form
    {
        public MSSQL()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //MSSQLSupporter mssql = new MSSQLSupporter("");
            //DataSet dataSet = mssql.ToDataSet("Select * From Tbl_Admin Select * From Tbl_Statu_Havuz");//Okki
            //SqlDataReader sqlDataReader = mssql.ToReader("Select * From Tbl_Admin Select * From Tbl_Statu_Havuz");//Okki
            //object obj = mssql.ToScalar("Select Top 1 Ad_Soyad From Tbl_Admin");//Okki
            //bool isSuccess = mssql.ToQuery("PRINT 'test'; Select * From Tbl_Admin");//Okki
        }
    }
}
