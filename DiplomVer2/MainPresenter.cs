using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DiplomVerMVP
{
    public class MainPresenter
    {
        private readonly IMainForm view;
        private readonly ICalculateEMS calculateEMS;
        private readonly IDataBaseWorker dataBaseWorker;
        private readonly IMessageService messageService;

        public MainPresenter(IMainForm view, IDataBaseWorker dataBaseWorker, ICalculateEMS calculateEMS, IMessageService messageService)
        {
            this.view = view;
            this.dataBaseWorker = dataBaseWorker;
            this.messageService = messageService;
            this.calculateEMS = calculateEMS;


            view.ButtonAddClick += new EventHandler(view_ButtonAddClick);
            view.ButtonOpenClick += new EventHandler(view_ButtonOpenClick);
            view.ButtonChangeClick += new EventHandler(view_ButtonChangeClick);
            view.ButtonChooseDBClick += new EventHandler(view_ButtonChooseDBClick);
            view.ButtonDeleteClick += new EventHandler(view_ButtonDeleteClick);
            view.ButtonHomeClick += new EventHandler(view_ButtonHomeClick);

        }

        private void view_ButtonOpenClick(object sender, EventArgs e)
        {
            try
            {
                ProjectName.projectName = view.ListBoxSelectedItem;

                view.UCResult.label1.Text = ProjectName.projectName;

                dataBaseWorker.ConstructorFillFromDB("ValueEMS", ProjectName.projectName);

                dataBaseWorker.CreateTableResult(DataBaseWorker.calculate.ProjectName, DataBaseWorker.calculate.TimeValueElecticShortVoltage(10000, 500));

                dataBaseWorker.ChartsLoad(view.UCResult.cartesianChart1, DataBaseWorker.calculate.ProjectName, "Напряженность электрического поля в ближней зоне излучения");

                dataBaseWorker.DeleteTableRusult(DataBaseWorker.calculate.ProjectName);

                dataBaseWorker.CreateTableResult(DataBaseWorker.calculate.ProjectName, DataBaseWorker.calculate.TimeValueElecticLongVoltage(100, 5));

                dataBaseWorker.ChartsLoad(view.UCResult.cartesianChart2, DataBaseWorker.calculate.ProjectName, "Напряженность электрического поля в дальней зоне излучения");

                dataBaseWorker.DeleteTableRusult(DataBaseWorker.calculate.ProjectName);

                dataBaseWorker.CreateTableResult(DataBaseWorker.calculate.ProjectName, DataBaseWorker.calculate.TimeValueMagneticShortVoltage(100, 5));

                dataBaseWorker.ChartsLoad(view.UCResult.cartesianChart3, DataBaseWorker.calculate.ProjectName, "Напряженность магнитного поля в ближней зоне излучения");

                dataBaseWorker.DeleteTableRusult(DataBaseWorker.calculate.ProjectName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            view.UCResult.Visible = true;
            view.UCChange.Visible = false;
            view.UCHome.Visible = false;
        }

        private void view_ButtonAddClick(object sender, EventArgs e)
        {
            var form = new FormAddProject(view.ListBox);
            form.Show();
            dataBaseWorker.ListBoxRefresh(view.ListBox);
        }

        private void view_ButtonDeleteClick(object sender, EventArgs e)
        {
            dataBaseWorker.ButtonDelete(view.ListBox.SelectedItem);

            dataBaseWorker.ListBoxRefresh(view.ListBox);
        }

        private void view_ButtonChooseDBClick(object sender, EventArgs e)
        {

            var open = new OpenFileDialog();
            open.Filter = "База данных (*.mdf)|*.mdf" +
                "" +
                "|Все файлы (*.*)|*.*";
            if (open.ShowDialog() == DialogResult.OK)
            {
                view.LabelDBName.Text = open.SafeFileName; //имя.расширение
                view.LabelDBName.ForeColor = Color.White;
                view.LabelDBName.Image = null;
                //labelDBName.Image = Image.FromFile(@"C:\Users\Dan\Desktop\icons\icons8-database-view-32.png");

                DataBaseWorker.ConnectionDataBase(open.FileName);
                dataBaseWorker.ListBoxItemsAdd(view.ListBox);
                dataBaseWorker.ListBoxRefresh(view.ListBox);
            }
        }

        private void view_ButtonChangeClick(object sender, EventArgs e)
        {
            try
            {
                string text = view.ListBox.SelectedItem.ToString();
                ProjectName.projectName = text;
                view.UCChange.label1.Text = text;

                view.UCChange.textBoxWireLength.Text = dataBaseWorker.GetValueFromDB("wireLength ", text);
                view.UCChange.textBoxResZ1.Text = dataBaseWorker.GetValueFromDB("resistanceZ1", text);
                view.UCChange.textBoxRezZ2.Text = dataBaseWorker.GetValueFromDB("resistanceZ2", text);
                view.UCChange.textBoxDistanceBeetweenWire.Text = dataBaseWorker.GetValueFromDB("distanceBetweenWire", text);
                view.UCChange.textBoxRadiusWire.Text = dataBaseWorker.GetValueFromDB("radiusWire", text);
                view.UCChange.textBoxElectricAntennaWite.Text = dataBaseWorker.GetValueFromDB("electricAntennaWire", text);
                view.UCChange.textBoxLengthAntennaWire.Text = dataBaseWorker.GetValueFromDB("lengthAntennfWire", text);
                view.UCChange.textBoxDielectricConstant.Text = dataBaseWorker.GetValueFromDB("dielectricConstant", text);
                view.UCChange.textBoxCirculatFrequency.Text = dataBaseWorker.GetValueFromDB("circularFrequency", text);
                view.UCChange.textBoxWaveLengthOfField.Text = dataBaseWorker.GetValueFromDB("wavelengthOfField", text);
                view.UCChange.textBoxMediumPower.Text = dataBaseWorker.GetValueFromDB("mediumPower", text);
                view.UCChange.textBoxCoeffAntenn.Text = dataBaseWorker.GetValueFromDB("сoeffAntenn", text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            view.UCResult.Visible = false;
            view.UCChange.Visible = true;
            view.UCHome.Visible = false;
        }

        private void view_ButtonHomeClick(object sender, EventArgs e)
        {
            view.UCResult.Visible = false;
            view.UCHome.Visible = true;
            view.UCChange.Visible = false;
        }

    }
}
