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
        // количество собранного нектара, при сборе которого было
        // опылено достаточное количество цветов для рождения нового.
        private const double nectarHarvestPerNewFlower = 50.0;
        
        // координаты границ цветочного поля.
        private const int field_minX = 15;
        private const int field_maxX = 177;
        private const int field_minY = 290;
        private const int field_maxY = 690;

        public Hive hive;
        public List<Bee> bees;
        public List<Flower> flowers;
        private Random rand = new Random();
        private double totalNectarHarvested = 0;

        public World()
        {
            bees = new List<Bee>();
            flowers = new List<Flower>();
            hive = new Hive(this);
            for (int i = 0; i < 10; i++) AddFlower();
        }

        public void Go()
        {
            double all_nectar_harvested = 0;
            this.hive.Go();
            // попутно избавляемся от больше не работающих пчел...
            this.bees = this.bees.Where(bee =>
            {
                bee.Go();
                return bee.CurrentState != BeeState.Retired;
            }).ToList<Bee>();
            // и увядших цветов. 
            this.flowers = this.flowers.Where(flower => {
                flower.Go();
                all_nectar_harvested += flower.NectarHarvested;
                return flower.Alive;
            }).ToList<Flower>();

            // если разница между количеством собранного со всех цветов нектара от последней записи
            // составляет разницу достаточную для появления нового цветка.
            if ( (all_nectar_harvested - this.totalNectarHarvested) >= nectarHarvestPerNewFlower)
            {
                this.totalNectarHarvested = all_nectar_harvested;
                AddFlower();
            }
        }

        private void AddFlower()
        {
            int x = rand.Next(field_minX, field_maxX);
            int y = rand.Next(field_minY, field_maxY);
            flowers.Add(new Flower(new Point(x, y), rand));
        }
    }
}
