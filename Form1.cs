using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace Advanced_file_scanner
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Program error: 0x0001", "AFS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                backgroundWorker1.RunWorkerAsync();
                button1.Enabled = false;
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            #region Declaration of variables
            int AllFilesCount = Directory.GetFiles(textBox1.Text, "*.*", SearchOption.AllDirectories).Length;
            long AllFilesSize = 0;
            int PictureFilesCount = 0;
            long PictureFilesSize = 0;
            int VideoFilesCount = 0;
            long VideoFilesSize = 0;
            int MusicFilesCount = 0;
            long MusicFilesSize = 0;
            int DocumentsFilesCount = 0;
            long DocumentsFilesSize = 0;
            int OtherFilesCount = 0;
            long OtherFilesSize = 0;
            string DriveLetter = "";
            #endregion

            //Initialize progressbar
            progressBar1.Value = 0;
            progressBar1.Maximum = int.Parse(AllFilesCount.ToString());

            #region Get all files and display them in listBox
            DirectoryInfo DI = new DirectoryInfo(textBox1.Text);
            FileInfo[] FI = DI.GetFiles("*", SearchOption.AllDirectories);
            foreach (FileInfo file in FI)
            {
                listBox2.Items.Add(file.Name);
            }
            #endregion

            #region Get number of files by type
            PictureFilesCount += Directory.GetFiles(textBox1.Text, "*.png", SearchOption.AllDirectories).Length; //Image Files
            VideoFilesCount += Directory.GetFiles(textBox1.Text, "*.mp4", SearchOption.AllDirectories).Length; //Video Files
            MusicFilesCount += Directory.GetFiles(textBox1.Text, "*.mp3", SearchOption.AllDirectories).Length; //Music Files
            DocumentsFilesCount += Directory.GetFiles(textBox1.Text, "*.txt", SearchOption.AllDirectories).Length; //Document Files
            #endregion

            #region Get files size by type
            DirectoryInfo PDI = new DirectoryInfo(textBox1.Text); //Pictures
            FileInfo[] PFI = PDI.GetFiles("*.png", SearchOption.AllDirectories);
            foreach (FileInfo file in PFI)
            {
                PictureFilesSize += file.Length;
                progressBar1.Value += 1;
            }

            DirectoryInfo VDI = new DirectoryInfo(textBox1.Text); //Videos
            FileInfo[] VFI = VDI.GetFiles("*.mp4", SearchOption.AllDirectories);
            foreach (FileInfo file in VFI)
            {
                VideoFilesSize += file.Length;
                progressBar1.Value += 1;
            }

            DirectoryInfo MDI = new DirectoryInfo(textBox1.Text); //Musics
            FileInfo[] MFI = MDI.GetFiles("*.mp3", SearchOption.AllDirectories);
            foreach (FileInfo file in MFI)
            {
                MusicFilesSize += file.Length;
                progressBar1.Value += 1;
            }

            DirectoryInfo DDI = new DirectoryInfo(textBox1.Text); //Documents
            FileInfo[] DFI = DDI.GetFiles("*.txt", SearchOption.AllDirectories);
            
            foreach (FileInfo file in DFI)
            {
                DocumentsFilesSize += file.Length;
                progressBar1.Value += 1;
            }

            DirectoryInfo ADI = new DirectoryInfo(textBox1.Text); //All Files
            FileInfo[] AFI = ADI.GetFiles("*.*", SearchOption.AllDirectories);
            foreach (FileInfo file in AFI)
            {
                AllFilesSize += file.Length;
            }
            #endregion

            #region Get number of other files
            int PVMDFiles = 0;
            PVMDFiles += PictureFilesCount;
            PVMDFiles += VideoFilesCount;
            PVMDFiles += MusicFilesCount;
            PVMDFiles += DocumentsFilesCount;
            OtherFilesCount = AllFilesCount - PVMDFiles;
            progressBar1.Value += OtherFilesCount;
            #endregion

            #region Get size of other files
            long PVMDSizes = 0;
            PVMDSizes += PictureFilesSize;
            PVMDSizes += VideoFilesSize;
            PVMDSizes += MusicFilesSize;
            PVMDSizes += DocumentsFilesSize;
            OtherFilesSize = AllFilesSize - PVMDSizes;
            #endregion

            //Get Informations
            DirectoryInfo IDI = new DirectoryInfo(textBox1.Text);
            DriveLetter = IDI.Root.ToString();

            #region Display summary in listBox
            listBox1.Items.Clear();
            listBox1.Items.Add("All Files Count: " + AllFilesCount.ToString());
            listBox1.Items.Add("All Files Size: " + AllFilesSize.ToString());
            listBox1.Items.Add("Picture Files Count: " + PictureFilesCount.ToString());
            listBox1.Items.Add("Picture Files Size: " + PictureFilesSize.ToString());
            listBox1.Items.Add("Video Files Count: " + VideoFilesCount.ToString());
            listBox1.Items.Add("Video Files Size: " + VideoFilesSize.ToString());
            listBox1.Items.Add("Music Files Count: " + MusicFilesCount.ToString());
            listBox1.Items.Add("Music Files Size: " + MusicFilesSize.ToString());
            listBox1.Items.Add("Documents Files Count: " + DocumentsFilesCount.ToString());
            listBox1.Items.Add("Documents Files Size: " + DocumentsFilesSize.ToString());
            listBox1.Items.Add("Other Files Count: " + OtherFilesCount.ToString());
            listBox1.Items.Add("Other Files Size: " + OtherFilesSize.ToString());
            listBox1.Items.Add("Drive Letter: " + DriveLetter);
            listBox1.Items.Add("Date: " + DateTime.Now.ToString("M/d/yyyy"));
            #endregion

            button1.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;
            textBox1.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog FDB = new FolderBrowserDialog())
            {
                if (FDB.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = FDB.SelectedPath;
                    button1.Enabled = true;
                    listBox2.Items.Clear();
                }
            }
        }
        private void startScanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog SFD = new SaveFileDialog())
            {
                SFD.Filter = "Text file|*.txt";
                if (SFD.ShowDialog() == DialogResult.OK)
                {
                    List<string> DataToExport = new List<string>();
                    DataToExport.Clear();

                    //Generate Data to Export
                    DataToExport.Add("Machine name: " + System.Environment.MachineName);
                    DataToExport.Add("-----------");
                    DataToExport.Add("Summary: ");
                    foreach (var item in listBox1.Items)
                    {
                        DataToExport.Add(item.ToString());
                    }
                    DataToExport.Add("-----------");
                    DataToExport.Add("Files: ");
                    foreach (var item in listBox2.Items)
                    {
                        DataToExport.Add(item.ToString());
                    }

                    ExportList.Export(DataToExport, SFD.FileName);
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(1);
        }

        private void websiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://advanced-file-scanner.webnode.cz");
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox ab = new AboutBox();
            ab.ShowDialog();
        }
    }
}
