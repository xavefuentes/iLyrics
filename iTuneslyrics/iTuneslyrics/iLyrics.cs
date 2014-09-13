using System;
using System.Windows.Forms;
using iTunesLib;
using iTuneslyrics.Interface;
using iTuneslyrics.LyricService;

namespace iTuneslyrics
{
    public partial class iLyrics : Form
    {
        readonly IiTunes _iTunesApp;
        ILyricService _lyricService;

        public iLyrics()
        {
            InitializeComponent();
            _iTunesApp = new iTunesAppClass();
            _iTunesApp.BrowserWindow.Visible = true;
            _iTunesApp.BrowserWindow.Minimized = false;
        }

        private void iLyrics_Load(object sender, EventArgs e)
        {
            cbService.Text = "LyricWiki";
            cbService.Items.Insert(0, "LyricWiki");
            cbService.Items.Insert(1, "Leo's Lyrics");
            cbService.SelectedIndex = 0;
        }

        private void btnAlbums_Click(object sender, EventArgs e)
        {
            IITTrackCollection selectedTracks = _iTunesApp.SelectedTracks;
            if ((selectedTracks == null))
            {
                MessageBox.Show("You must first load iTunes and select some songs in iTunes");
                return;
            }

            if (cbService.SelectedItem.ToString() == "LyricWiki")
            {
                _lyricService = new LyricWiki();
            }
            else if (cbService.SelectedItem.ToString() == "Leo's Lyrics")
            {
                _lyricService = new LeosLyricService(txtLeosAuthId.Text);
            }
            else
            {
                MessageBox.Show("Please select a lyric service first.");
                return;
            }

            if (chkAuto.Checked)
            {
                FrmResult fr = new FrmResult(selectedTracks, _lyricService, chkOverwrite.Checked);
                fr.ShowDialog();
            }
            else
            {
                int updatedSongsCount = 0;
                for (int i = 1; i <= selectedTracks.Count; i++)
                {
                    var currentTrack = (IITFileOrCDTrack)selectedTracks[i];
                    //if (CurrentTrack.Lyrics != null)
                    //    continue;

                    updatedSongsCount++;
                    var ab = new ManualUpdate { CurrentTrack = currentTrack, LyricService = _lyricService };

                    DialogResult dr = ab.ShowDialog();
                    if (dr == DialogResult.Abort) break;

                }
                if (updatedSongsCount == 0)
                    MessageBox.Show("All selected songs seems to have lyrics");
                else
                    MessageBox.Show("Update completed");
            }
        }


        private void cbService_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbService.SelectedItem.ToString() == "Leo's Lyrics")
            {
                pnlLeosSettings.Visible = true;
            }
            else
            {
                pnlLeosSettings.Visible = false;
            }
        }
    }
}