using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
namespace DREMOV
{
    public partial class Form1 : Form
    {
        WebClient a = null;
        List<string> list = new List<string>();
        private const string APIKey = "Input your API Key Here";
        public Form1()
        {
            InitializeComponent();
                a = new WebClient();
                MaximizeBox = false;
                FormBorderStyle = FormBorderStyle.None;
                try
                {
                var top = a.DownloadString("https://api.themoviedb.org/3/discover/movie?api_key="+APIKey);
                    JObject pop = JObject.Parse(top);
                    tbResult.ReadOnly = true;
                    tbOther.ReadOnly = true;
                    labTitle.Show();
                    labId.Hide();
                    tbResult.Hide();
                    tbOther.Hide();
                    pbRelated.Hide();
                    pbPoster.Hide();
                    btnPict1.Hide();
                    btnPict2.Hide();
                    btnPict3.Hide();
                    labTitle.Show();
                    label3.Hide();
                    label1.Show();
                    tbSearch.Show();
                    tbId.Hide();
                    btnSearchid.Hide();
                    pbPop1.ImageLocation = "http://image.tmdb.org/t/p/w154///" + pop["results"][0]["poster_path"];
                    pbPop2.ImageLocation = "http://image.tmdb.org/t/p/w154///" + pop["results"][1]["poster_path"];
                    pbPop3.ImageLocation = "http://image.tmdb.org/t/p/w154///" + pop["results"][2]["poster_path"];
                    pbPop4.ImageLocation = "http://image.tmdb.org/t/p/w154///" + pop["results"][3]["poster_path"];
                    pbPop5.ImageLocation = "http://image.tmdb.org/t/p/w154///" + pop["results"][4]["poster_path"];
                    pbPop6.ImageLocation = "http://image.tmdb.org/t/p/w154///" + pop["results"][5]["poster_path"];
                }
                catch (WebException)
                {
                    MessageBox.Show("Connection Error");
                }
            
        }
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;
        [DllImport("User32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        public void btnSearch_Click(object sender, EventArgs e)
        {
            pnRight.Location = new Point(pnRight.Location.X - 162, pnRight.Location.Y);
            for (int i = -(pnRight.Width); i > 0; i++)
            {
                pnRight.Location = new Point(i, pnRight.Location.Y);
            }
            if (NetworkInterface.GetIsNetworkAvailable() == true)
            {
                try
                {
                    tbResult.Show();
                    tbOther.Show();
                    pbRelated.Show();
                    pbPoster.Show();
                    btnPict1.Show();
                    btnPict2.Show();
                    btnPict3.Show();
                    label3.Show();
                    label1.Hide();
                    pbPop1.Hide();
                    pbPop2.Hide();
                    pbPop3.Hide();
                    pbPop4.Hide();
                    pbPop5.Hide();
                    pbPop6.Hide();
                    String search;
                    search = tbSearch.Text;
                    list.Insert(0, search);
                    tbResult.Clear();
                    tbOther.Clear();
                    pbPoster.Image = null;
                    pbRelated.Image = null;
                    var data = a.DownloadString("https://api.themoviedb.org/3/search/movie?api_key=" + APIKey + "&query=" + list[0]);
                    JObject obj = JObject.Parse(data);                   
                    String result = "";
                    String other = "";
                    result += "Title: " + obj["results"][0]["title"];
                    result += "\nRelease Date: " + obj["results"][0]["release_date"];
                    result += "\nRating: " + obj["results"][0]["vote_average"];
                    result += "\nAdult Category: " + obj["results"][0]["adult"];
                    result += "\nSynopsis: " + obj["results"][0]["overview"];
                    result += "\nLanguage: " + obj["results"][0]["original_language"];
                    other += "1\nTitle: " + obj["results"][1]["title"];
                    other += "\nRelease Date: " + obj["results"][1]["release_date"];
                    other += "\nRating: " + obj["results"][1]["vote_average"];
                    other += "\nAdult Category: " + obj["results"][1]["adult"];
                    other += "\nSynopsis: " + obj["results"][1]["overview"];
                    other += "\nLanguage: " + obj["results"][1]["original_language"];
                    tbResult.Text = result;
                    tbOther.Text = other;
                    pbPoster.ImageLocation = "http://image.tmdb.org/t/p/w154///" + obj["results"][0]["poster_path"];
                    pbRelated.ImageLocation = "http://image.tmdb.org/t/p/w92///" + obj["results"][1]["poster_path"];
                    
                }
                catch (Exception)
                {
                    if (tbSearch.Text == "")
                    {
                        tbResult.Hide();
                        tbOther.Hide();
                        pbRelated.Hide();
                        pbPoster.Hide();
                        btnPict1.Hide();
                        btnPict2.Hide();
                        btnPict3.Hide();
                        label3.Hide();
                        MessageBox.Show("Title Can Not Be Empty");
                    }
                    if (tbResult.Text == "")
                    {
                        tbResult.Hide();
                        pbPoster.Hide();
                        MessageBox.Show("No Data Found");
                    }
                    if (tbOther.Text == "")
                    {
                        btnPict1.Hide();
                        btnPict2.Hide();
                        btnPict3.Hide();
                        label3.Hide();
                        MessageBox.Show("No Related Searches");
                    }
                    else
                    {
                        MessageBox.Show("Something Wrong");
                    }
                }
            }
            else
            {
                MessageBox.Show("Connection Error");
            }            
        }
        private void btnPict1_Click(object sender, EventArgs e)
        {
            try {
                var data =
                a.DownloadString($"https://api.themoviedb.org/3/search/movie?api_key=" + APIKey + "&query=" + list[0]);
                JObject obj = JObject.Parse(data);
                String other = "";
                other += "1\nTitle: " + obj["results"][1]["title"];
                other += "\nRelease Date: " + obj["results"][1]["release_date"];
                other += "\nRating: " + obj["results"][1]["vote_average"];
                other += "\nAdult Category: " + obj["results"][1]["adult"];
                other += "\nSynopsis: " + obj["results"][1]["overview"];
                other += "\nLanguage: " + obj["results"][1]["original_language"];
                tbOther.Text = other;
                pbRelated.ImageLocation = "http://image.tmdb.org/t/p/w92///" + obj["results"][1]["poster_path"];
            }
            catch (Exception)
            {
                MessageBox.Show("Connection Error");
            }
        }
        private void btnPict2_Click(object sender, EventArgs e)
        {
            try {
                var data =
                a.DownloadString("https://api.themoviedb.org/3/search/movie?api_key="+APIKey+"&query=" + list[0]);

                JObject obj = JObject.Parse(data);
                String other = "";
                other += "2\nTitle: " + obj["results"][2]["title"];
                other += "\nRelease Date: " + obj["results"][2]["release_date"];
                other += "\nRating: " + obj["results"][2]["vote_average"];
                other += "\nAdult Category: " + obj["results"][2]["adult"];
                other += "\nSynopsis: " + obj["results"][2]["overview"];
                other += "\nLanguage: " + obj["results"][2]["original_language"];
                tbOther.Text = other;
                pbRelated.ImageLocation = "http://image.tmdb.org/t/p/w92///" + obj["results"][2]["poster_path"];
            }
            catch (Exception)
            {
                MessageBox.Show("Connection Error");
            }
        }
        private void btnPict3_Click(object sender, EventArgs e)
        {
            try {
                var data =
                a.DownloadString("https://api.themoviedb.org/3/search/movie?api_key="+APIKey+"&query=" + list[0]);

                JObject obj = JObject.Parse(data);
                String other = "";
                other += "3\nTitle: " + obj["results"][3]["title"];
                other += "\nRelease Date: " + obj["results"][3]["release_date"];
                other += "\nRating: " + obj["results"][3]["vote_average"];
                other += "\nAdult Category: " + obj["results"][3]["adult"];
                other += "\nSynopsis: " + obj["results"][3]["overview"];
                other += "\nLanguage: " + obj["results"][3]["original_language"];
                tbOther.Text = other;
                pbRelated.ImageLocation = "http://image.tmdb.org/t/p/w92///" + obj["results"][3]["poster_path"];
            }
            catch (Exception)
            {
                MessageBox.Show("Connection Error");
            }
        }
        private void tbSearch_Enter(object sender, EventArgs e)
        {
            this.AcceptButton = btnSearch;

        }
        private void tbSearch_Leave(object sender, EventArgs e)
        {

        }
        private void btnTriple_Click(object sender, EventArgs e)
        {
            pnRight.Location = new Point(pnRight.Location.X - 162, pnRight.Location.Y);
            for (int i = -(pnRight.Width); i < 0; i++)
            {
                pnRight.Location = new Point(i, pnRight.Location.Y);
            }
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            pnRight.Location = new Point(pnRight.Location.X - 162, pnRight.Location.Y);
            for (int i = -(pnRight.Width); i > 0; i++)
            {
                pnRight.Location = new Point(i, pnRight.Location.Y);
            }
        }
        private void btnAbout_Click(object sender, EventArgs e)
        {
            Form3 frm3 = new Form3();
            frm3.Show();
        }
        private void btnHome_Click(object sender, EventArgs e)
        {
            pnRight.Location = new Point(pnRight.Location.X - 162, pnRight.Location.Y);
            for (int i = -(pnRight.Width); i > 0; i++)
            {
                pnRight.Location = new Point(i, pnRight.Location.Y);
            }
            try
            {
                var top = a.DownloadString("https://api.themoviedb.org/3/discover/movie?api_key=" + APIKey);
                JObject pop = JObject.Parse(top);
                labTitle.Show();
                labId.Hide();
                btnSearch.Show();
                btnSearchid.Hide();
                tbResult.Hide();
                tbOther.Hide();
                pbRelated.Hide();
                tbSearch.Show();
                tbId.Hide();
                pbPoster.Hide();
                btnPict1.Hide();
                btnPict2.Hide();
                btnPict3.Hide();
                label3.Hide();
                label1.Show();
                pbPop1.Show();
                pbPop2.Show();
                pbPop3.Show();
                pbPop4.Show();
                pbPop5.Show();
                pbPop6.Show();
            }
            catch (WebException)
            {
                MessageBox.Show("Connection Error");
            }
        }
        private void btnID_Click(object sender, EventArgs e)
        {
            pnRight.Location = new Point(pnRight.Location.X - 162, pnRight.Location.Y);
            for (int i = -(pnRight.Width); i > 0; i++)
            {
                pnRight.Location = new Point(i, pnRight.Location.Y);
            }
            tbSearch.Hide();
            tbId.Show();
            btnSearch.Hide();
            btnSearchid.Show();
            labTitle.Hide();
            labId.Show();
        }
        private void tbId_Enter(object sender, EventArgs e)
        {
            this.AcceptButton = btnSearchid;
        }
        private void tbId_Leave(object sender, EventArgs e)
        {

        }
        private void btnSearchid_Click(object sender, EventArgs e)
        {
            pnRight.Location = new Point(pnRight.Location.X - 162, pnRight.Location.Y);
            for (int i = -(pnRight.Width); i > 0; i++)
            {
                pnRight.Location = new Point(i, pnRight.Location.Y);
            }
            if (NetworkInterface.GetIsNetworkAvailable() == true)
            {
                try
                {
                    tbResult.Show();
                    tbOther.Hide();
                    pbRelated.Hide();
                    pbPoster.Show();
                    btnPict1.Hide();
                    btnPict2.Hide();
                    btnPict3.Hide();
                    label3.Hide();
                    label1.Hide();
                    pbPop1.Hide();
                    pbPop2.Hide();
                    pbPop3.Hide();
                    pbPop4.Hide();
                    pbPop5.Hide();
                    pbPop6.Hide();
                    String idt;
                    idt = tbId.Text;
                    tbSearch.Clear();
                    tbResult.Clear();
                    tbOther.Clear();
                    pbPoster.Image = null;
                    pbRelated.Image = null;
                    var idm = a.DownloadString("https://api.themoviedb.org/3/find/" + idt + "?api_key=" + APIKey + "&external_source=imdb_id");
                    JObject idg = JObject.Parse(idm);
                    String result = "";
                    result += "Title: " + idg["movie_results"][0]["title"];
                    result += "\nRelease Date: " + idg["movie_results"][0]["release_date"];
                    result += "\nRating: " + idg["movie_results"][0]["vote_average"];
                    result += "\nAdult Category: " + idg["movie_results"][0]["adult"];
                    result += "\nSynopsis: " + idg["movie_results"][0]["overview"];
                    result += "\nLanguage: " + idg["movie_results"][0]["original_language"];
                    tbResult.Text = result;
                    pbPoster.ImageLocation = "http://image.tmdb.org/t/p/w154///" + idg["movie_results"][0]["poster_path"];

                }
                catch (Exception)
                {
                    if (tbId.Text == "")
                    {
                        tbResult.Hide();
                        tbOther.Hide();
                        pbRelated.Hide();
                        pbPoster.Hide();
                        btnPict1.Hide();
                        btnPict2.Hide();
                        btnPict3.Hide();
                        label3.Hide();
                        MessageBox.Show("ID Can Not Be Empty");
                    }
                    if (tbResult.Text == "")
                    {
                        MessageBox.Show("No Data Found");
                    }
                    else
                    {
                        MessageBox.Show("Something Wrong");
                    }
                }
            }
            else
            {
                MessageBox.Show("Connection Error");
            }
        }
        private void pbPop1_Click(object sender, EventArgs e)
        {
            pnRight.Location = new Point(pnRight.Location.X - 162, pnRight.Location.Y);
            for (int i = -(pnRight.Width); i > 0; i++)
            {
                pnRight.Location = new Point(i, pnRight.Location.Y);
            }
            try {
                var top = a.DownloadString("https://api.themoviedb.org/3/discover/movie?api_key=" + APIKey);
                JObject pop = JObject.Parse(top);
                tbResult.Show();
                pbPoster.Show();
                pbPop1.Hide();
                pbPop2.Hide();
                pbPop3.Hide();
                pbPop4.Hide();
                pbPop5.Hide();
                pbPop6.Hide();
                label1.Hide();
                String result = "";
                result += "Title: " + pop["results"][0]["title"];
                result += "\nRelease Date: " + pop["results"][0]["release_date"];
                result += "\nRating: " + pop["results"][0]["vote_average"];
                result += "\nAdult Category: " + pop["results"][0]["adult"];
                result += "\nSynopsis: " + pop["results"][0]["overview"];
                result += "\nLanguage: " + pop["results"][0]["original_language"];
                pbPoster.ImageLocation = "http://image.tmdb.org/t/p/w154///" + pop["results"][0]["poster_path"];
                tbResult.Text = result;
            }
            catch (WebException)
            {
                MessageBox.Show("Connection Error");
            }
        }
        private void pbPop2_Click(object sender, EventArgs e)
        {
            pnRight.Location = new Point(pnRight.Location.X - 162, pnRight.Location.Y);
            for (int i = -(pnRight.Width); i > 0; i++)
            {
                pnRight.Location = new Point(i, pnRight.Location.Y);
            }
            try {
                var top = a.DownloadString("https://api.themoviedb.org/3/discover/movie?api_key=" + APIKey);
                JObject pop = JObject.Parse(top);
                tbResult.Show();
                pbPoster.Show();
                pbPop1.Hide();
                pbPop2.Hide();
                pbPop3.Hide();
                pbPop4.Hide();
                pbPop5.Hide();
                pbPop6.Hide();
                label1.Hide();
                String result = "";
                result += "Title: " + pop["results"][1]["title"];
                result += "\nRelease Date: " + pop["results"][1]["release_date"];
                result += "\nRating: " + pop["results"][1]["vote_average"];
                result += "\nAdult Category: " + pop["results"][1]["adult"];
                result += "\nSynopsis: " + pop["results"][1]["overview"];
                result += "\nLanguage: " + pop["results"][1]["original_language"];
                pbPoster.ImageLocation = "http://image.tmdb.org/t/p/w154///" + pop["results"][1]["poster_path"];
                tbResult.Text = result;
            }
            catch (WebException)
            {
                MessageBox.Show("Connection Error");
            }
        }
        private void pbPop3_Click(object sender, EventArgs e)
        {
            pnRight.Location = new Point(pnRight.Location.X - 162, pnRight.Location.Y);
            for (int i = -(pnRight.Width); i > 0; i++)
            {
                pnRight.Location = new Point(i, pnRight.Location.Y);
            }
            try {
                var top = a.DownloadString("https://api.themoviedb.org/3/discover/movie?api_key=" + APIKey);
                JObject pop = JObject.Parse(top);
                tbResult.Show();
                pbPoster.Show();
                pbPop1.Hide();
                pbPop2.Hide();
                pbPop3.Hide();
                pbPop4.Hide();
                pbPop5.Hide();
                pbPop6.Hide();
                label1.Hide();
                String result = "";
                result += "Title: " + pop["results"][2]["title"];
                result += "\nRelease Date: " + pop["results"][2]["release_date"];
                result += "\nRating: " + pop["results"][2]["vote_average"];
                result += "\nAdult Category: " + pop["results"][2]["adult"];
                result += "\nSynopsis: " + pop["results"][2]["overview"];
                result += "\nLanguage: " + pop["results"][2]["original_language"];
                pbPoster.ImageLocation = "http://image.tmdb.org/t/p/w154///" + pop["results"][2]["poster_path"];
                tbResult.Text = result;
            }
            catch (WebException)
            {
                MessageBox.Show("Connection Error");
            }
        }
        private void pbPop4_Click(object sender, EventArgs e)
        {
            pnRight.Location = new Point(pnRight.Location.X - 162, pnRight.Location.Y);
            for (int i = -(pnRight.Width); i > 0; i++)
            {
                pnRight.Location = new Point(i, pnRight.Location.Y);
            }
            try {
                var top = a.DownloadString("https://api.themoviedb.org/3/discover/movie?api_key=" + APIKey);
                JObject pop = JObject.Parse(top);
                tbResult.Show();
                pbPoster.Show();
                pbPop1.Hide();
                pbPop2.Hide();
                pbPop3.Hide();
                pbPop4.Hide();
                pbPop5.Hide();
                pbPop6.Hide();
                label1.Hide();
                String result = "";
                result += "Title: " + pop["results"][3]["title"];
                result += "\nRelease Date: " + pop["results"][3]["release_date"];
                result += "\nRating: " + pop["results"][3]["vote_average"];
                result += "\nAdult Category: " + pop["results"][3]["adult"];
                result += "\nSynopsis: " + pop["results"][3]["overview"];
                result += "\nLanguage: " + pop["results"][3]["original_language"];
                pbPoster.ImageLocation = "http://image.tmdb.org/t/p/w154///" + pop["results"][3]["poster_path"];
                tbResult.Text = result;
            }
            catch (WebException)
            {
                MessageBox.Show("Connection Error");
            }
        }
        private void pbPop5_Click(object sender, EventArgs e)
        {
            pnRight.Location = new Point(pnRight.Location.X - 162, pnRight.Location.Y);
            for (int i = -(pnRight.Width); i > 0; i++)
            {
                pnRight.Location = new Point(i, pnRight.Location.Y);
            }
            try {
                var top = a.DownloadString("https://api.themoviedb.org/3/discover/movie?api_key=" + APIKey);
                JObject pop = JObject.Parse(top);
                tbResult.Show();
                pbPoster.Show();
                pbPop1.Hide();
                pbPop2.Hide();
                pbPop3.Hide();
                pbPop4.Hide();
                pbPop5.Hide();
                pbPop6.Hide();
                label1.Hide();
                String result = "";
                result += "Title: " + pop["results"][4]["title"];
                result += "\nRelease Date: " + pop["results"][4]["release_date"];
                result += "\nRating: " + pop["results"][4]["vote_average"];
                result += "\nAdult Category: " + pop["results"][4]["adult"];
                result += "\nSynopsis: " + pop["results"][4]["overview"];
                result += "\nLanguage: " + pop["results"][4]["original_language"];
                pbPoster.ImageLocation = "http://image.tmdb.org/t/p/w154///" + pop["results"][4]["poster_path"];
                tbResult.Text = result;
            }
            catch (WebException)
            {
                MessageBox.Show("Connection Error");
            }
        }
        private void pbPop6_Click(object sender, EventArgs e)
        {
            pnRight.Location = new Point(pnRight.Location.X - 162, pnRight.Location.Y);
            for (int i = -(pnRight.Width); i > 0; i++)
            {
                pnRight.Location = new Point(i, pnRight.Location.Y);
            }
            try {
                var top = a.DownloadString("https://api.themoviedb.org/3/discover/movie?api_key=" + APIKey);
                JObject pop = JObject.Parse(top);
                tbResult.Show();
                pbPoster.Show();
                pbPop1.Hide();
                pbPop2.Hide();
                pbPop3.Hide();
                pbPop4.Hide();
                pbPop5.Hide();
                pbPop6.Hide();
                label1.Hide();
                String result = "";
                result += "Title: " + pop["results"][5]["title"];
                result += "\nRelease Date: " + pop["results"][5]["release_date"];
                result += "\nRating: " + pop["results"][5]["vote_average"];
                result += "\nAdult Category: " + pop["results"][5]["adult"];
                result += "\nSynopsis: " + pop["results"][5]["overview"];
                result += "\nLanguage: " + pop["results"][5]["original_language"];
                pbPoster.ImageLocation = "http://image.tmdb.org/t/p/w154///" + pop["results"][5]["poster_path"];
                tbResult.Text = result;
            }
            catch (WebException)
            {
                MessageBox.Show("Connection Error");
            }
        }

        private void btnSearch_Leave(object sender, EventArgs e)
        {
            tbSearch.Text = null;
        }

        private void btnSearchid_Leave(object sender, EventArgs e)
        {
            tbId.Text = null;
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void pnHandle_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }
    }
    }



