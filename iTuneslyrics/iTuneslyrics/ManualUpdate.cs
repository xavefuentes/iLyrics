using System;
using System.Windows.Forms;
using System.Reflection;
using iTunesLib;
using iTuneslyrics.Interface;


namespace iTuneslyrics {
    partial class ManualUpdate : Form {
        public ILyricService LyricService;

        public IITFileOrCDTrack CurrentTrack;
        public ManualUpdate() {
            InitializeComponent();

            //  Initialize the AboutBox to display the product information from the assembly information.
            //  Change assembly information settings for your application through either:
            //  - Project->Properties->Application->Assembly Information
            //  - AssemblyInfo.cs
            //this.Text = String.Format("About {0}", AssemblyTitle);
            //this.lblSong.Text = AssemblyProduct;
            //this.lblArtist.Text = String.Format("Version {0}", AssemblyVersion);
            //this.labelCopyright.Text = AssemblyCopyright;
            //this.labelCompanyName.Text = AssemblyCompany;
            //this.textBoxDescription.Text = AssemblyDescription;
        }

        #region Assembly Attribute Accessors

        public string AssemblyTitle {
            get {
                // Get all Title attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                // If there is at least one Title attribute
                if (attributes.Length > 0) {
                    // Select the first one
                    var titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    // If it is not an empty string, return it
                    if (titleAttribute.Title != "")
                        return titleAttribute.Title;
                }
                // If there was no Title attribute, or if the Title attribute was the empty string, return the .exe name
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion {
            get {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription {
            get {
                // Get all Description attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                // If there aren't any Description attributes, return an empty string
                if (attributes.Length == 0)
                    return string.Empty;
                // If there is a Description attribute, return its value
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct {
            get {
                // Get all Product attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                // If there aren't any Product attributes, return an empty string
                if (attributes.Length == 0)
                    return String.Empty;
                // If there is a Product attribute, return its value
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright {
            get {
                // Get all Copyright attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                // If there aren't any Copyright attributes, return an empty string
                if (attributes.Length == 0)
                    return String.Empty;
                // If there is a Copyright attribute, return its value
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany {
            get {
                // Get all Company attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                // If there aren't any Company attributes, return an empty string
                if (attributes.Length == 0)
                    return String.Empty;
                // If there is a Company attribute, return its value
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        private void AboutBox1_Load(object sender, EventArgs e) {
            try {
                String tempPath = String.Empty;
                String artist = CurrentTrack.Artist;
                String song = CurrentTrack.Name;

                lblArtist.Text = artist;
                lblSong.Text = song;
                if (CurrentTrack.Artwork[1] != null) {
                    tempPath = System.IO.Path.GetTempPath() + "\\itunesart";
                    CurrentTrack.Artwork[1].SaveArtworkToFile(tempPath);
                }
                artPictureBox.ImageLocation = tempPath;
                string lyrics;

                var result = FetchLyrics(artist, song, out lyrics);

                if (result == SearchResult.NotFound) {
                    //remove parenthesis and retry
                    var parenStartIndex = song.IndexOf('(');
                    var parenEndIndex = song.IndexOf(')');

                    if (parenStartIndex > -1 && parenEndIndex > -1) {
                        song = song.Remove(parenStartIndex, parenEndIndex - parenStartIndex).Trim();

                        result = FetchLyrics(artist, song, out lyrics);
                    }
                }

                if (result == SearchResult.Found) {
                    lyricsBox.Text = lyrics;
                }
                else {
                    lyricsBox.Text = "No Result";
                }

                btnUpdate.Enabled = result == SearchResult.Found;
            }
            catch (Exception ex) {
                lyricsBox.Text = "Error:\r\n" + ex.Message;
                btnUpdate.Enabled = false;
            }
        }

        private SearchResult FetchLyrics(string artist, string song, out string lyrics) {
            if (LyricService.Exists(artist, song) == SearchResult.NotFound) {
                lyrics = string.Empty;
                return SearchResult.NotFound;
            }
            return LyricService.GetLyrics(artist, song, out lyrics);

        }

        private void btnUpdate_Click(object sender, EventArgs e) {
            CurrentTrack.Lyrics = lyricsBox.Text;
            Close();
            Dispose();
        }

        private void button1_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Abort;
            Close();
            Dispose();
        }
    }
}