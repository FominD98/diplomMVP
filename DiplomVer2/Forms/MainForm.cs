using System;
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
    interface IMainForm
    {
        string ListBoxSelectedItem { get; set; }
        ListBox ListBox { get; }
        Label LabelDBName { get; }

        UC.UCResult UCResult { get; }
        UC.UCChange UCChange { get; }
        UC.UCHome UCHome { get; }

        event EventHandler ButtonOpenClick;
        event EventHandler ButtonAddClick;
        event EventHandler ButtonChooseDBClick;
        event EventHandler ButtonDeleteClick;
        event EventHandler ButtonChangeClick;
        event EventHandler ButtonHomeClick;
    }

    public partial class MainForm : Form, IMainForm
    {
        DataBaseWorker data = new DataBaseWorker();

        public MainForm()
        {
            InitializeComponent();
            buttonAdd.Click += ButtonAdd_Click;
            buttonOpen.Click += ButtonOpen_Click;
            buttonDelete.Click += ButtonDelete_Click;
            buttonChooseDB.Click += ButtonChooseDB_Click;
            buttonChange.Click += ButtonChange_Click;
            buttonHome.Click += ButtonHome_Click;
        }



        #region Проброс событий

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            ButtonAddClick?.Invoke(this, EventArgs.Empty);
        }

        private void ButtonOpen_Click(object sender, EventArgs e)
        {
            ButtonOpenClick?.Invoke(this, EventArgs.Empty);
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            ButtonDeleteClick?.Invoke(this, EventArgs.Empty);
        }

        private void ButtonChooseDB_Click(object sender, EventArgs e)
        {
            ButtonChooseDBClick?.Invoke(this, EventArgs.Empty);
        }

        private void ButtonChange_Click(object sender, EventArgs e)
        {
            ButtonChangeClick?.Invoke(this, EventArgs.Empty);
        }

        private void ButtonHome_Click(object sender, EventArgs e)
        {
            ButtonHomeClick?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region Реализация интерфейса

        public string ListBoxSelectedItem
        {
            get { return listBox1.SelectedItem.ToString(); }
            set { value = listBox1.SelectedItem.ToString();  }
        }

        public UC.UCResult UCResult
        {
            get { return ucResult1; }
        }

        public UC.UCChange UCChange
        {
            get { return ucChange1; }
        }

        public UC.UCHome UCHome
        {
            get { return ucHome1; }
        }

        public ListBox ListBox
        {
            get { return listBox1; }
        }

        public Label LabelDBName
        {
            get { return labelDBName; }
        }

        #endregion

        public event EventHandler ButtonOpenClick;
        public event EventHandler ButtonAddClick;
        public event EventHandler ButtonChooseDBClick;
        public event EventHandler ButtonDeleteClick;
        public event EventHandler ButtonChangeClick;
        public event EventHandler ButtonHomeClick;
    }

    static class ProjectName
    {
        public static string projectName { get; set; }
    }
}
