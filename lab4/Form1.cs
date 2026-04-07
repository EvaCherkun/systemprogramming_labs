using System;
using System.Windows.Forms;

namespace lab4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var sample = new Building(
                "Residential building #5",
                9,
                1250.5,
                new[] { "basement", "1st floor — office", "residential floors 2–9" });

            PropertyReflectionHelper.FillTreeWithProperties(treeViewProperties, sample);
        }

        private void treeViewProperties_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
    }
}
