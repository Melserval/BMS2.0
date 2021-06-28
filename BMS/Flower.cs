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
        public Flower(Point location, Random rand)
        {
            Location = location;
            Age = 0;
            Alive = true;
            Nectar = InitialNextar;
            lifespan = rand.Next(LifeSpanMin, LifeSpanMax + 1);
            NectarHarvested = 0;
        }

        // минимальная продолжительность жизни.
        protected const int LifeSpanMin = 15_000;

        // максимальная продолжительность жизни.
        protected const int LifeSpanMax = 30_000;

        // начальное количество нектара.
        protected double InitialNextar = 1.50;

        // максимально возможное количество нектара.
        protected double MaxNectar = 5;

        // нектар добавляемый по мере роста цветка.
        protected double NectarAddedPerTurn = 0.01;

        // нектар собираемый с цветка за один цикл.
        protected double NectarGatheredPerTurn = 0.3;

        // расположение цветка.
        public Point Location { get; protected set; }

        // текущий возраст.
        public int Age { get; protected set; }

        // цветок живой.
        public bool Alive { get; protected set; }
        
        // Количество доступного нектара.
        public double Nectar { get; protected set; }

        // Общее количество собранного нектара.
        public double NectarHarvested { get; protected set; }

        // продолжительность жизни.
        private readonly int lifespan;

        // отдача нектара пчеле.
        public double HarvestNectar() 
        {
            if (NectarGatheredPerTurn > Nectar) return 0;
            Nectar -= NectarGatheredPerTurn;
            return NectarHarvested += NectarGatheredPerTurn;
        }

        // один жизненный цикл цветка.
        public void Go()
        {
            if (Age < lifespan)
            {
                if (Nectar < MaxNectar)
                {
                    if ((Nectar + NectarAddedPerTurn) > MaxNectar)
                        Nectar = MaxNectar;
                    else
                        Nectar += NectarAddedPerTurn;
                    // TODO: Что делать если цветок жив, но нектар достиг максимума?
                }
                Age++;
                if (Age == lifespan) Alive = false;
            }
        }
    }
}
