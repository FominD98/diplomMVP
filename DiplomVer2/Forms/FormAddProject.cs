﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiplomVerMVP
{
    public partial class FormAddProject : Form
    {
        DataBaseWorker data = new DataBaseWorker();

        public FormAddProject()
        {
            InitializeComponent();
        }

        private ListBox listBox;

        public FormAddProject(ListBox listBox)
        {
            this.listBox = listBox;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonAddProject_Click(object sender, EventArgs e)
        {
            if (TextBoxProjectName.Text != "")
            {
                data.ButtonAdd(TextBoxProjectName.Text);
                data.ListBoxRefresh(listBox);
                Close();
            }
            else
            {
                label3.Visible = true;
            }
        }
    }
}
