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

        private int intClickCount = 0;

        private void Form1_Load(object sender, EventArgs e)
        {

            cboServiceList.SelectedIndex = 0;

            WebClient Client = new WebClient();
            Client.DownloadFile("http://steeky.com/creatine/show_playlist.php?playlist=Shared List", "items.txt");

            lstItems.DataSource = File.ReadAllLines("items.txt");

            Client.DownloadFile("http://steeky.com/creatine/list_playlists.php", "playlists.txt");

            cboLists.DataSource = File.ReadAllLines("playlists.txt");


            cboLists.SelectedIndex = 0;
            cboServiceList.SelectedIndex = 0;
        }

        private void lstItems_SelectedIndexChanged(object sender, EventArgs e)
        {

       

            if ((cboServiceList.SelectedItem == "YouTube Lucky") && (intClickCount>0))
            {
                webBrowser1.Navigate("http://steeky.com/creatine/youtube_search.php?q="+lstItems.SelectedItem);
            }

            intClickCount++;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void cboLists_SelectedIndexChanged(object sender, EventArgs e)
        {

            lstItems.DataSource = null;


            WebClient Client = new WebClient();
            Client.DownloadFile("http://steeky.com/creatine/show_playlist.php?playlist="+cboLists.SelectedItem, "items.txt");

            lstItems.DataSource = File.ReadAllLines("items.txt");
        }
    }
}
