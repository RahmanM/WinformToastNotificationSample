using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToastNotifications
{
    public partial class LongRunningTaskForm2 : Form
    {
        public LongRunningTaskForm2()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var notifier = new Notifier();

            try
            {
                var result = await new MyThreadHelper<int>().Execute(MyLongRunningFunction);
                notifier.ShowSuccess("Task completed", $"Resul was: {result.ToString()}");
            }
            catch (Exception ex)
            {
                notifier.ShowError("Task had error", ex.Message);
            }
        }

        public int MyLongRunningFunction()
        {
            Thread.Sleep(int.Parse(txtHowLong.Text));
            throw new Exception("There was error while completing the task!!");
        }
    }
}
