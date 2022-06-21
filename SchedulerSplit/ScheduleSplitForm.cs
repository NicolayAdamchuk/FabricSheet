using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ScheduleSplit.CS
{
    public partial class ScheduleSplitForm : Form
    {
        SchedulerData m_schedulerData;
        public ScheduleSplitForm(SchedulerData schedulerData)
        {
            this.m_schedulerData = schedulerData;
            InitializeComponent();
            InitializeComboBox();
        }
        void InitializeComboBox()
        {
            this.comboBoxScheduler.DataSource = m_schedulerData.viewSchedules;
            this.comboBoxScheduler.DisplayMember = "Name";

            this.comboBoxTitle.DataSource = m_schedulerData.fSheets_name;
        }

        private void ComboBoxScheduler_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void ComboBoxTitle_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void ButtonSplit_Click(object sender, EventArgs e)
        {
            m_schedulerData.viewSchedule = (Autodesk.Revit.DB.ViewSchedule)this.comboBoxScheduler.SelectedItem;
            m_schedulerData.fSheet = m_schedulerData.fSheets[this.comboBoxTitle.SelectedIndex];
            m_schedulerData.totalH = (int) this.numericUpDown.Value;
        }
    }
}
