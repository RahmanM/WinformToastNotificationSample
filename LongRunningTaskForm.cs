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
    public partial class LongRunningTaskForm : Form
    {
        public LongRunningTaskForm()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var notifier = new Notifier();

            try
            {
               var result = await  new MyThreadHelper<int>().Execute(MyLongRunningFunction);
                notifier.ShowSuccess("Task completed", $"Result was: {result.ToString()}");
            }
            catch (Exception ex)
            {
                notifier.ShowError("Task had error" , ex.Message);
            }
        }

        public int MyLongRunningFunction()
        {
            Thread.Sleep(int.Parse(txtHowLong.Text));
            return 10;
        }
    }
}
