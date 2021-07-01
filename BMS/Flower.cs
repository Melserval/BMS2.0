using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BMS
{
    // Информация о цветке и его судьбе.
    class Flower
    {
        // максимально возможное количество нектара.
        protected const double MaxNectar = 5;

        // нектар порождаемый по мере роста цветка.
        protected const double NectarAddedPerTurn = 0.01;

        // нектар доступный для сбора за один цикл.
        protected const double NectarGatheredPerTurn = 0.3;

        public Flower(Point location, Random rand)
        {
            Location = location;
            Age = 0;
            Alive = true;
            Nectar = 1.50;
            lifespan = rand.Next(15000, 30000);
            NectarHarvested = 0;
        }

        // расположение цветка.
        public Point Location { get; protected set; }

        // текущий возраст.
        public int Age { get; protected set; }

        // цветок живой.
        public bool Alive { get; protected set; }
        
        // Количество доступного нектара.
        public double Nectar { get; protected set; }

        // Общее количество собранного c цветка нектара.
        public double NectarHarvested { get; protected set; }

        // продолжительность жизни.
        private readonly int lifespan;

        // отдача нектара пчеле.
        public double HarvestNectar() 
        {
            if (this.Nectar >= NectarGatheredPerTurn)
            {
                this.Nectar -= NectarGatheredPerTurn;
                this.NectarHarvested += NectarGatheredPerTurn;
                return NectarGatheredPerTurn;
            }
            return 0;
        }

        // один жизненный цикл цветка.
        public void Go()
        {
            if (this.Alive)
            {
                this.Age++;
                if (this.Age >= this.lifespan) this.Alive = false;
                if (this.Nectar >= MaxNectar) return;
                this.Nectar = (this.Nectar + NectarAddedPerTurn) >= MaxNectar
                    ? MaxNectar
                    : this.Nectar + NectarAddedPerTurn;
                // TODO: Что делать если цветок жив, но нектар достиг максимума?
            }
        }
    }
}
