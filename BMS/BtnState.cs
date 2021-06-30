using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS
{
    // отслеживает состоянии кнопки и соответствующих
    // состояниям меток-надписей отображаемых на кнопке.

    using tsb = System.Windows.Forms.ToolStripButton;
    class BtnState
    {
        private tsb btn;
        private Dictionary<int, string> stateNumAndLabels;
        private int state = -1;
        public int State 
        {
            get 
            { 
                return this.state; 
            }
            set
            {
                if (value >= 0 && value < this.stateNumAndLabels.Count)
                {
                    this.state = value;
                    btn.Text = this.stateNumAndLabels[value];
                }
            }
        }

        public BtnState(tsb btn, params string[] labels)
        {
            this.btn = btn;
            this.stateNumAndLabels = new Dictionary<int, string>();
            for (int i = 0; i < labels.Length; i++)
            {
                this.stateNumAndLabels.Add(i, labels[i]);
            }
            this.State = 0;
        }
    }
}
