using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonShowProperties_Click(object sender, EventArgs e)
        {
            var зразок = new Споруда(
                "Адміністративна будівля",
                1250.5,
                4,
                new[] { "Хол", "Кабінет 101", "Кабінет 102", "Архів" });

            зразок.ДодатиПриміщення("Конференц-зал");

            Споруда.ВивестиВластивостіУTreeView(зразок, treeViewSporuda);
        }
    }
}
