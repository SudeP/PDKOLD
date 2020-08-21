using PDK.ASPNET;
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
    public partial class ATUForm : Form
    {
        public ATUForm()
        {
            InitializeComponent();
        }

        private void ATU_Load(object sender, EventArgs e)
        {
            ATU atu = new ATU();
            string v = string.Empty;
            //v = ATU.Get(Method0);
            //v = ATU.Get(Method1);
            //v = ATU.Get(Method2);
            //v = ATU.Get(Method4);
            //v = ATU.Get(Method5);
        }
        public void Method0() => new sbyte();
        public string Method1() => "";
        public int Method2() => 1;
        public int Method3(int a, int b) => default;
        public string Method4(int a, int b) => a + b + "";
        public object Method5(dynamic a, int b, object obj) => a.ToString() + b + "" + obj.ToString();
    }
}
