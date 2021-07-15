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
        private Dictionary<BeeState, int> beeStatesStats;


        public Form1()
        {
            InitializeComponent();
            Random rand = new Random();
            World.rand = rand;
            Hive.rand = rand;
            Bee.rand = rand;
            Flower.rand = rand;
            this.btnRun = new BtnState(this.toolStripButton_start, "Запуск", "Пауза", "Продолжить");
            toolStripStatusLabel_info.Text = "Ожидание запуска";
            this.timer_frame.Interval = 50;
            this.timer_frame.Tick += new EventHandler(RunFrame);

            this.beeStatesStats = new Dictionary<BeeState, int>
            {
                {BeeState.Idle,  0 },
                {BeeState.FlyingToFlower, 0 },
                {BeeState.GatheringNectar, 0 },
                {BeeState.ReturningToHive, 0 },
                {BeeState.MakingHoney, 0 },
                {BeeState.Retired, 0 }
            };
            Bee.changeBeeState += (int id, BeeState bsNew, BeeState bsPrev) =>
            {
                this.toolStripStatus_beeStateInfo.Text = $"Bee#{id} {bsNew}";
                if (this.beeStatesStats.ContainsKey(bsNew)) this.beeStatesStats[bsNew]++;
                if (this.beeStatesStats.ContainsKey(bsPrev)) this.beeStatesStats[bsPrev]--;
            };
            world = new World();
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

            this.listBox_beeStateInfo.Items.Clear();
            foreach (BeeState stKey in this.beeStatesStats.Keys)
            {
                this.listBox_beeStateInfo.Items.Add( $"{stKey, -20} {this.beeStatesStats[stKey]}");
            }
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
            Array.ForEach(Enum.GetValues(typeof(BeeState)) as BeeState[], (state) => this.beeStatesStats[state] = 0);
            this.world = new World();
            this.UpdateStats(new TimeSpan());
            this.btnRun.State = 0;
            toolStripStatusLabel_info.Text = "Ожидание запуска(2)";
        }
    }
}
