using PDK.DB.MONGODB;
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
    public partial class MONGODB : Form
    {
        public MONGODB()
        {
            InitializeComponent();
        }

        private void MONGODB_Load(object sender, EventArgs e)
        {
            User user = new User
            {
                Name = "Afumetsu",
                Surname = "SudeP"
            };
            string json = user.ToJson();
            User jsonUser = User.FromJson(json);
            MessageBox.Show($@"{jsonUser.Name} {jsonUser.Surname}");
        }
    }
    internal class User : ObjectUniqe<User>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
