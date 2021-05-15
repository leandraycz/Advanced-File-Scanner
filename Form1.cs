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
            //Initialize Compenents
            progressBar1.Value = 0;
            progressBar1.Maximum = 100;
            listBox2.Items.Clear();
            listBox1.Items.Clear();
            listBox1.Items.Add("All Files Count: ");
            listBox1.Items.Add("All Files Size: ");
            listBox1.Items.Add("Picture Files Count: ");
            listBox1.Items.Add("Picture Files Size: ");
            listBox1.Items.Add("Video Files Count: ");
            listBox1.Items.Add("Video Files Size: ");
            listBox1.Items.Add("Music Files Count: ");
            listBox1.Items.Add("Music Files Size: ");
            listBox1.Items.Add("Documents Files Count: ");
            listBox1.Items.Add("Documents Files Size: ");
            listBox1.Items.Add("Other Files Count: ");
            listBox1.Items.Add("Other Files Size: ");
            listBox1.Items.Add("Drive Letter: ");
            listBox1.Items.Add("Date: ");

            //Declare varibles
            int AllFilesCount = 0;
            int AllFilesSize = 0;
            int PictureFilesCount = 0;
            int PictureFilesSize = 0;
            int VideoFilesCount = 0;
            int VideoFilesSize = 0;
            int MusicFilesCount = 0;
            int MusicFilesSize = 0;
            int DocumentsFilesCount = 0;
            int DocumentsFilesSize = 0;
            int OtherFilesCount = 0;
            int OtherFilesSize = 0;
            string DriveLetter = "";
            string Date = "";

            //Get all files count
            AllFilesCount = Directory.GetFiles(textBox1.Text, "*.*", SearchOption.AllDirectories).Length;

            //Initialize progressbar
            int AllProgressbarParts = 0;
            AllProgressbarParts = AllFilesCount;
            progressBar1.Maximum = AllProgressbarParts;

            //Get Files
            DirectoryInfo DI = new DirectoryInfo(textBox1.Text);
            FileInfo[] FI = DI.GetFiles("*", SearchOption.AllDirectories);
            foreach (FileInfo file in FI)
            {
                listBox2.Items.Add(file.Name);
                progressBar1.Value += 1;
            }

            //Get Files count by type
            PictureFilesCount += Directory.GetFiles(textBox1.Text, "*.png", SearchOption.AllDirectories).Length; //Image Files
            VideoFilesCount += Directory.GetFiles(textBox1.Text, "*.mp4", SearchOption.AllDirectories).Length; //Video Files
            MusicFilesCount += Directory.GetFiles(textBox1.Text, "*.mp3", SearchOption.AllDirectories).Length; //Music Files
            DocumentsFilesCount += Directory.GetFiles(textBox1.Text, "*.txt", SearchOption.AllDirectories).Length; //Document Files

            //Get Files size by type
            DirectoryInfo PDI = new DirectoryInfo(textBox1.Text); //Pictures
            FileInfo[] PFI = PDI.GetFiles("*.png", SearchOption.AllDirectories);
            foreach (FileInfo file in PFI)
            {
                PictureFilesSize += Convert.ToInt32(file.Length);
            }

            DirectoryInfo VDI = new DirectoryInfo(textBox1.Text); //Videos
            FileInfo[] VFI = VDI.GetFiles("*.mp4", SearchOption.AllDirectories);
            foreach (FileInfo file in VFI)
            {
                VideoFilesSize += Convert.ToInt32(file.Length);
            }

            DirectoryInfo MDI = new DirectoryInfo(textBox1.Text); //Musics
            FileInfo[] MFI = MDI.GetFiles("*.mp3", SearchOption.AllDirectories);
            foreach (FileInfo file in MFI)
            {
                MusicFilesSize += Convert.ToInt32(file.Length);
            }

            DirectoryInfo DDI = new DirectoryInfo(textBox1.Text); //Documents
            FileInfo[] DFI = DDI.GetFiles("*.txt", SearchOption.AllDirectories);
            
            foreach (FileInfo file in DFI)
            {
                DocumentsFilesSize += Convert.ToInt32(file.Length);
            }

            DirectoryInfo ADI = new DirectoryInfo(textBox1.Text); //All Files
            FileInfo[] AFI = ADI.GetFiles("*.*", SearchOption.AllDirectories);
            foreach (FileInfo file in AFI)
            {
                AllFilesSize += Convert.ToInt32(file.Length);
            }

            //Get Other files count 
            int PVMDFiles = 0;
            PVMDFiles += PictureFilesCount;
            PVMDFiles += VideoFilesCount;
            PVMDFiles += MusicFilesCount;
            PVMDFiles += DocumentsFilesCount;
            OtherFilesCount = AllFilesCount - PVMDFiles;

            //Get Other files Size
            int PVMDSizes = 0;
            PVMDSizes += PictureFilesSize;
            PVMDSizes += VideoFilesSize;
            PVMDSizes += MusicFilesSize;
            PVMDSizes += DocumentsFilesSize;
            OtherFilesSize = AllFilesSize - PVMDSizes;

            //Get Informations
            DirectoryInfo IDI = new DirectoryInfo(textBox1.Text);
            DriveLetter = IDI.Root.ToString();
            Date = DateTime.Now.ToString("M/d/yyyy");

            //Update Summary
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
            listBox1.Items.Add("Date: " + Date);


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
                }
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
                Thread.Sleep(2000);
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
