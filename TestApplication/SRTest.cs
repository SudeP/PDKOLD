using PDK.Tool;
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
            string pureJson = @"{""Id"":1000,""ModifiedDate"":""2020-09-04T13:01:34.94"",""IsActive"":5,""Name"":""Ahmet Can"",""Surname"":""Duyar"",""Username"":""ahmet"",""Password"":""1234""}";
            string salt = "qwerty";

            SoupRecipe sp1 = new SoupRecipe();
            SoupRecipe sp2 = new SoupRecipe();


            var impureJson1 =
            Base64.Encrypt(sp1.Stir(pureJson, salt));
            var tempPureJson1 =
            sp1.Fractionate(Base64.Decrypt(impureJson1), salt);


            var impureJson2 = sp1.Stir(pureJson, salt);
            var tempPureJson2 = sp1.Fractionate(impureJson2, salt);


            var tempPureJson12 = sp1.Fractionate(impureJson2, salt);


            var tempPureJson21 = sp1.Fractionate(impureJson1, salt);


            //SoupRecipeExtensions.SetSalt("qwerty");
        }
    }
}
