using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace MassFileRemover
{
    public partial class Form1 : Form
    {
        private string path;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.folderBrowserDialog1.ShowDialog();
            string path = folderBrowserDialog1.SelectedPath;
            this.label2.Text = folderBrowserDialog1.SelectedPath;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
                
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            switch (MessageBox.Show("Waring all file in "+ folderBrowserDialog1.SelectedPath + " with extension ." + this.textBox1.Text + " will be deleted! This can't be undone. Are you sure you want to delete those files?", "MassFileRemover", MessageBoxButtons.YesNoCancel))
            {
                case DialogResult.Yes:
                    this.progressBar1.Value = 10;
                    string lines = "@Echo Off\r\ncd " + this.folderBrowserDialog1.SelectedPath + "\r\ndel /s *." + this.textBox1.Text;
                    System.IO.StreamWriter file = new System.IO.StreamWriter("delfiles.bat");
                    file.WriteLine(lines);

                    file.Close();
                    this.progressBar1.Value = 25;

                    Process p = new Process();
                    ProcessStartInfo pi = new ProcessStartInfo();
                    pi.UseShellExecute = true;
                    pi.FileName = @"delfiles.bat";
                    p.StartInfo = pi;
                    this.progressBar1.Value = 50;
                    try
                    {
                        p.Start();
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message);
                    }
                    this.progressBar1.Value = 100;
                    MessageBox.Show("The file(s) in " + this.folderBrowserDialog1.SelectedPath + " with extension ." + this.textBox1.Text + " have been deleted!");
                    File.Delete(@"delfiles.bat");
                    this.progressBar1.Value = 0;
                    Application.Restart();
                    break;

                case DialogResult.No: MessageBox.Show("Your files won't be deleted!"); break;
                case DialogResult.Cancel: MessageBox.Show("Your files won't be deleted!"); break;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/DimitriTheFox");
        }
    }
}
