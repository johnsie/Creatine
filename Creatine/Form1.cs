using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Creatine
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            WebClient Client = new WebClient();
            Client.DownloadFile("http://steeky.com/creatine/show_playlist.php?playlist=Shared List", "items.txt");

            lstItems.DataSource = File.ReadAllLines("items.txt");
        }

        private void lstItems_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cboServiceList.SelectedItem == "Youtube Lucky")
            {
                webBrowser1.Navigate("http://steeky.com/creatine/youtube_search.php?q="+lstItems.SelectedItem);
            }

        }
    }
}
