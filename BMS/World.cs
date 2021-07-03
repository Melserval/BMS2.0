using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BMS
{
    // Основной класс контейнер управляющий остальными компонентами.
    class World
    {
        public static Random rand;
        // количество собранного нектара, при сборе которого было
        // опылено достаточное количество цветов для рождения нового.
        private const double NECTAR_HARVEST_FP_FLOWER = 50.0;

        // координаты границ цветочного поля.
        private const int FIELD_MIN_X = 15;
        private const int FIELD_MIN_Y = 177;
        private const int FIELD_MAX_X = 690;
        private const int FIELD_MAX_Y = 290;


        public Hive hive;
        public List<Bee> bees;
        public List<Flower> flowers;


        public World()
        {
            if (rand == null)
            {
                throw new ArgumentNullException($"Неустановлен Rand в {nameof(World)}");
            }
            bees = new List<Bee>();
            flowers = new List<Flower>();
            hive = new Hive(this);
            for (int i = 0; i < 10; i++)
            {
                AddFlower();
            }

        }

        public void Go()
        {
            this.hive.Go();
            // обратная нумераци для корректного смещения индекса при удалении элемента.
            for (int i = this.bees.Count - 1; i >= 0; i--)
            {
                Bee b = this.bees[i];
                b.Go();
                if (b.IsRetired)
                {
                    this.bees.RemoveAt(i);
                }
            }
            double totalNectarHarvested = 0;
            for (int i = this.flowers.Count - 1; i >= 0; i--)
            {
                Flower f = this.flowers[i];
                f.Go();
                totalNectarHarvested += f.NectarHarvested;
                if(f.Alive == false)
                {
                    this.flowers.RemoveAt(i);
                }
            }

            // возможно, было собрано количество нектара, при сборе которого
            // было опылено достаточное количество цветов, что бы родился новый.
            if (totalNectarHarvested > NECTAR_HARVEST_FP_FLOWER)
            {
                this.AddFlower();
                this.flowers.ForEach(f => f.NectarHarvested = 0);
            }
        }

        private void AddFlower()
        {
            Point bornp = new Point(rand.Next(FIELD_MIN_X, FIELD_MAX_X),
                                    rand.Next(FIELD_MIN_Y, FIELD_MAX_Y));
            flowers.Add(new Flower(bornp));
        }
    }
}
