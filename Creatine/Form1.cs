using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
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
            intClickCount = 0;
        }

        private void lstItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            //https://www.yt-download.org/api/button/mp3/doeLh0Pif3s


            if ((cboServiceList.SelectedItem == "YouTube Lucky") && (intClickCount>0))
            {
              //  MessageBox.Show(intClickCount.ToString());
                webBrowser1.Navigate("http://steeky.com/creatine/youtube_search.php?q="+lstItems.SelectedItem);
            }



            if ((cboServiceList.SelectedItem == "Normal YouTube"))
            {
                //  MessageBox.Show(intClickCount.ToString());
                webBrowser1.Navigate("http://www.youtube.com/results?search_query=" + lstItems.SelectedItem);
            }


     
            intClickCount++;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void cboLists_SelectedIndexChanged(object sender, EventArgs e)
        {

            lstItems.DataSource = null;
            intClickCount = 0;

            WebClient Client = new WebClient();
            Client.DownloadFile("http://steeky.com/creatine/show_playlist.php?playlist="+cboLists.SelectedItem, "items.txt");

            lstItems.DataSource = File.ReadAllLines("items.txt");
            intClickCount = 0;
        }


        private const string YoutubeLinkRegex = "(?:.+?)?(?:\\/v\\/|watch\\/|\\?v=|\\&v=|youtu\\.be\\/|\\/v=|^youtu\\.be\\/)([a-zA-Z0-9_-]{11})+";
        private static Regex regexExtractId = new Regex(YoutubeLinkRegex, RegexOptions.Compiled);
        private static string[] validAuthorities = { "youtube.com", "www.youtube.com", "youtu.be", "www.youtu.be" };

        public string ExtractVideoIdFromUri(Uri uri)
        {
            try
            {
                string authority = new UriBuilder(uri).Uri.Authority.ToLower();

                //check if the url is a youtube url
                if (validAuthorities.Contains(authority))
                {
                    //and extract the id
                    var regRes = regexExtractId.Match(uri.ToString());
                    if (regRes.Success)
                    {
                        return regRes.Groups[1].Value;
                    }
                }
            }
            catch { }


            return null;
        }

        private void btnTmp3_Click(object sender, EventArgs e)
        {
            Uri uri = webBrowser1.Url;

            string strvideoID = ExtractVideoIdFromUri(uri);

            webBrowser1.Navigate("https://www.yt-download.org/api/button/mp3/"+ strvideoID);

        }

        private void tmrCheckURL_Tick(object sender, EventArgs e)
        {
            if (!(ExtractVideoIdFromUri(webBrowser1.Url) == null))
            {
                btnTmp3.BackColor = Color.Green;
            }
            else
            {
                btnTmp3.BackColor = Color.Red;
            }

        }

        private void cboServiceList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
