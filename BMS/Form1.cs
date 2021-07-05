using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class Form1 : Form
    {
        private World world;
        private DateTime start = DateTime.Now;
        private DateTime end;
        private int framesRun = 0;
        private BtnState btnRun;


        public Form1()
        {
            InitializeComponent();
            Random rand = new Random();
            World.rand = rand;
            Hive.rand = rand;
            Bee.rand = rand;
            Flower.rand = rand;
            world = new World();
            this.btnRun = new BtnState(this.toolStripButton_start, "Запуск", "Пауза", "Продолжить");
            toolStripStatusLabel_info.Text = "Ожидание запуска";
            this.timer_frame.Interval = 50;
            this.timer_frame.Tick += new EventHandler(RunFrame);
            Bee.changeBeeState += (int id, string message) => this.toolStripStatus_beeStateInfo.Text = $"Bee#{id} {message}";
            Bee.changeBeeState += (int id, string message) => this.listBox_beeStateInfo.Items.Add($"Bee#{id} {message}");
            UpdateStats(new TimeSpan());
        }


        // Обновление статистики на дисплее.
        private void UpdateStats(TimeSpan ts)
        {

            label_beeCount.Text = world.bees.Count.ToString();
            label_flowerCount.Text = world.flowers.Count.ToString();
            label_honeyCount.Text = $"{world.hive.Honey:f3}";
            label_nectarOnFieldCount.Text = world.flowers
                .Aggregate(0.0, (total, flower) => total + flower.Nectar)
                .ToString("f3");
            label_frameRun.Text = framesRun.ToString();

            label_frameRate.Text = ts.TotalMilliseconds != 0.0 
                ? String.Format("{0:f0} ({1:f1}ms)", 1000 / ts.TotalMilliseconds, ts.TotalMilliseconds)
                : "N/A";
        }

        private void RunFrame(object sender, EventArgs eventer)
        {
            this.framesRun += 1;
            world.Go();
            end = DateTime.Now;
            UpdateStats((end - this.start));
            start = end;
        }


        private void toolStripButton_start_Click(object sender, EventArgs e)
        {
            switch(this.btnRun.State)
            {
                case 0:
                    this.timer_frame.Start();
                    this.btnRun.State = 1;
                    toolStripStatusLabel_info.Text = "Работаю.";
                    return;
                case 1: 
                    this.btnRun.State = 2;
                    this.timer_frame.Stop();
                    toolStripStatusLabel_info.Text = "Приостановлен.";
                    return;
                case 2: 
                    this.btnRun.State = 1;
                    this.timer_frame.Start();
                    toolStripStatusLabel_info.Text = "Работаю.";
                    return;
                default: 
                    this.btnRun.State = 0; 
                    return;
            }
        }

        private void toolStripButton_reset_Click(object sender, EventArgs e)
        {
            this.timer_frame.Stop();
            this.framesRun = 0;
            this.world = new World();
            this.UpdateStats(new TimeSpan());
            this.btnRun.State = 0;
            toolStripStatusLabel_info.Text = "Ожидание запуска(2)";
        }
    }
}
