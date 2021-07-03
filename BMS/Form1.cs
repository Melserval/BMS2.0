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
            this.timer_frame.Interval = 50;
            UpdateStats(new TimeSpan());
            this.btnRun = new BtnState(this.toolStripButton_start, "Запуск", "Пауза", "Продолжить");
        }

        // Обновление статистики на дисплее.
        private void UpdateStats(TimeSpan ts)
        {

            label_beeCount.Text = world.bees.Count.ToString();
            label_flowerCount.Text = world.flowers.Count.ToString();
            label_honeyCount.Text = $"{world.hive.Honey}";
            label_nectarOnFieldCount.Text = world.flowers.Aggregate(0.0, (total, flower) => total + flower.Nectar).ToString();
            label_frameRun.Text = framesRun.ToString();

            label_frameRate.Text = ts.TotalMilliseconds != 0.0 
                ? String.Format("{0:f0} ({1:f1}ms)", 1000 / ts.TotalMilliseconds, ts.TotalMilliseconds)
                : "N/A";
        }

        private void RunFrame()
        {
            this.framesRun += 1;
            world.Go();
            end = DateTime.Now;
            UpdateStats((end - start));
            start = end;
        }

        private void timer_frame_Tick(object sender, EventArgs e)
        {
            RunFrame();
        }

        private void toolStripButton_start_Click(object sender, EventArgs e)
        {
            switch(this.btnRun.State)
            {
                case 0:
                    this.timer_frame.Start();
                    this.btnRun.State = 1; 
                    return;
                case 1: 
                    this.btnRun.State = 2;
                    this.timer_frame.Stop();
                    return;
                case 2: 
                    this.btnRun.State = 1;
                    this.timer_frame.Start();
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
        }
    }
}
