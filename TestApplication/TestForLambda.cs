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
    public partial class TestForLambda : Form
    {
        public TestForLambda()
        {
            InitializeComponent();
        }

        private void TestForLambda_Load(object sender, EventArgs e)
        {
            Lambda
                .Create()
                .Try(() => MessageBox.Show("1"))
                .Try(() => MessageBox.Show("2"))
                .Try(() => Convert.ToInt32("test"))
                .Catch((ex) => MessageBox.Show("3"), 2)
                .Finally(() => MessageBox.Show("9"), 1)
                .Finally(() => MessageBox.Show("91"), 1)
                .Finally(() => MessageBox.Show("10"), 1)
                .Run();
        }
    }
}
