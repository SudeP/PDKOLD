using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PDK.Tool;
namespace TestApplication
{
    public partial class Json : Form
    {
        public Json()
        {
            InitializeComponent();
        }

        private void Json_Load(object sender, EventArgs e)
        {
            Test test = Test.FromJson("");
            test.ToJson();
        }
    }
    public class Test : JsonObject<Test>
    {

    }
}
