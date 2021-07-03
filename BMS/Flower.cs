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
        // максимальное содержание нектара (вместимость).
        protected const double MAX_NECTAR = 5.0;
        // нектар порождаемый за один цикл.
        protected const double NECTAR_ADDED_PER_TURN = 0.01;
        // нектар доступный для сбора за один цикл.
        protected const double NECTAR_GATHERED_PER_TURN = 0.3;
        // минимальная продолжительность жизни.
        protected const int LIFE_SPAN_MIN = 15_000;
        // максимальная продолжительность жизни.
        protected const int LIFE_SPANT_MAX = 30_000;
        // начальное количество нектара.
        protected const double INITIAL_NECTAR = 1.50;


        // расположение цветка.
        public Point Location { get; protected set; }
        // текущий возраст.
        public int Age { get; protected set; }
        // цветок живой.
        public bool Alive { get; protected set; }
        // Количество доступного нектара.
        public double Nectar { get; protected set; }
        // Общее количество собранного c цветка нектара.
        public double NectarHarvested { get; set; }
        // продолжительность жизни.
        protected readonly int lifespan;


        public Flower(Point location, Random rand)
        {
            Location = location;
            Age = 0;
            Alive = true;
            Nectar = INITIAL_NECTAR;
            lifespan = rand.Next(LIFE_SPAN_MIN, LIFE_SPANT_MAX);
            NectarHarvested = 0;
        }


        // отдача нектара пчеле.
        public double HarvestNectar() 
        {
            if (NECTAR_GATHERED_PER_TURN > this.Nectar)
            {
                return 0;
            } else
            {
                this.Nectar -= NECTAR_GATHERED_PER_TURN;
                this.NectarHarvested += NECTAR_GATHERED_PER_TURN;
                return NECTAR_GATHERED_PER_TURN;
            }
        }

        // один жизненный цикл цветка.
        public void Go()
        {
            if (this.Alive)
            {
                if ((this.Age += 1) == this.lifespan)
                {
                    this.Alive = false;
                }
                if (this.Nectar < MAX_NECTAR)
                {
                    this.Nectar =
                        (this.Nectar + NECTAR_ADDED_PER_TURN) >= MAX_NECTAR ? 
                        MAX_NECTAR : this.Nectar + NECTAR_ADDED_PER_TURN;
                }
            }
        }
    }
}
