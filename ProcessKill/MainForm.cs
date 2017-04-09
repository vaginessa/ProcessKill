using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace ProcessKill
{
    public partial class MainForm : Form
    {
        private Process[] processes = null;

        public MainForm()
        {
            InitializeComponent();
        }

        private void FetchAllProcesses()
        {
            this.processes = Process.GetProcesses();
        }

        private void KillProcess()
        {
            if (listBox_Processes.Items.Count == 0)
            {
                return;
            }
            try
            {
                foreach (Process process in this.processes)
                {
                    if (process.ProcessName.Equals(listBox_Processes.SelectedItem.ToString()))
                    {
                        process.Kill();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddContentToListBox()
        {
            List<string> list = new List<string>();
            foreach (Process process in this.processes)
            {
                list.Add(process.ProcessName);
            }
            list = list.Distinct().ToList();
            list.Sort();
            listBox_Processes.DataSource = list;
        }

        private void AutoFilter()
        {
            string str = textBox_Search.Text.ToUpper();
            int length = textBox_Search.Text.Length;
            if (length == 0)
            {
                this.AddContentToListBox();
                return;
            }
            List<string> list = new List<string>();
            foreach (Process process in this.processes)
            {
                if (process.ProcessName.Length > length && process.ProcessName.Substring(0, length).ToUpper().Equals(str))
                {
                    list.Add(process.ProcessName);
                }
            }
            list = list.Distinct().ToList();
            list.Sort();
            listBox_Processes.DataSource = list;
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            this.FetchAllProcesses();
            this.AddContentToListBox();
        }

        private void button_Refrsh_Click(object sender, System.EventArgs e)
        {
            this.FetchAllProcesses();
            this.AddContentToListBox();
            textBox_Search.Clear();
        }

        private void button_Kill_Click(object sender, System.EventArgs e)
        {
            this.KillProcess();
            this.FetchAllProcesses();
            this.AddContentToListBox();
            textBox_Search.Clear();
        }

        private void textBox_Search_TextChanged(object sender, System.EventArgs e)
        {
            this.AutoFilter();
        }
    }
}
