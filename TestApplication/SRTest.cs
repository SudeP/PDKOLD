using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestApplication
{
    public partial class SRTest : Form
    {
        public SRTest()
        {
            InitializeComponent();
        }

        private void SRTest_Load(object sender, EventArgs e)
        {
            SoupRecipeExtensions.SetSalt("qwerty");
            string pureJson = @"{""Id"":1000,""ModifiedDate"":""2020-09-04T13:01:34.94"",""IsActive"":5,""Name"":""Ahmet Can"",""Surname"":""Duyar"",""Username"":""ahmet"",""Password"":""1234""}";
            var impureJson = pureJson.Stir();
            var tempPureJson = impureJson.Fractionate();
        }
    }
}
