using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using iTunesLib;
using iTuneslyrics.Interface;

namespace iTuneslyrics {
    public delegate int AddRowDelegate(String[] row);

    public delegate void UpdateRowDelegate(int index, String result);

    public partial class FrmResult : Form {
        private const string STR_TRUE = "true";
        private const string STR_FALSE = "false";
        private const string MSG_UPDATED = "Updated";
        private const string MSG_NOT_FOUND = "Not Found";
        private const string MSG_NO_SONG_FOUND = "No mathcing song found";

        private readonly ILyricService _lyricService;
        private readonly IITTrackCollection _selectedTracks;

        private readonly Boolean _overwrite;

        // Delegate instances used to call user interface
        // functions from worker thread:
        public AddRowDelegate RowAdded;
        public UpdateRowDelegate RowUpdated;

        public FrmResult(IITTrackCollection selectedTracks, ILyricService lyricsService, Boolean overwrite)
            : this() {
            _selectedTracks = selectedTracks;
            _lyricService = lyricsService;
            _overwrite = overwrite;
        }

        public FrmResult() {
            InitializeComponent();

            // initialize delegates
            RowAdded = new AddRowDelegate(AddRow);
            RowUpdated = new UpdateRowDelegate(UpdateRow);
        }

        private void frmResult_Load(object sender, EventArgs e) {
            var lu = new LyricsUpdater(_selectedTracks, _lyricService, _overwrite, this);

            for (int i = 0; i < dataGridView1.Columns.Count - 1; i++) {
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.Programmatic;
            }

            ThreadStart threadDelegate = lu.UpdateLyrics;

            var newThread = new Thread(threadDelegate);
            newThread.Start();
        }

        private int AddRow(String[] row) {
            int index = dataGridView1.Rows.Add(row);
            if (chkAutoScroll.Checked) {
                dataGridView1.FirstDisplayedScrollingRowIndex = index;
            }
            return index;
        }

        private void UpdateRow(int index, String result) {
            if (result == STR_TRUE) {
                dataGridView1.Rows[index].Cells[2].Value = MSG_UPDATED;
            }
            else if (result == STR_FALSE) {
                dataGridView1.Rows[index].Cells[2].Value = MSG_NOT_FOUND;
                dataGridView1.Rows[index].ErrorText = MSG_NO_SONG_FOUND;
            }
            else {
                dataGridView1.Rows[index].Cells[2].Value = result;
                dataGridView1.Rows[index].ErrorText = result;
                dataGridView1.Rows[index].DefaultCellStyle.BackColor = Color.YellowGreen;
            }

            if (chkAutoScroll.Checked) {
                dataGridView1.FirstDisplayedScrollingRowIndex = index;
            }
        }

    }
}