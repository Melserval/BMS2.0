using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BMS
{
    // Среда обитания пчел - УЛЕЙ
    class Hive
    {
        public static Random rand;
        // начальное количество пчел.
        private const int INIT_BEE_COUNT = 6;
        // начальное количество меда.
        private const double INIT_HONEY = 3.20;
        // максимум меда вмещаемого ульем.
        private const double HONEY_MAX_COUNT = 15.0;
        // коффициент переработки нектара в мед.
        private const double NECTAR_TO_HONEY_RATIO = 0.25;
        // максимальное количество пчел в улье.
        private const int BEE_MAX_POPULATION = 8;
        // минимальное наличие меда для увеличения популяции.
        private const double HONEY_MIN_FOR_INC_POPULA = 4.0;


        // количество меда в улье.
        public double Honey { get; private set; }
        // сколько еще меда поместится в улей.
        public double StoreLimit => HONEY_MAX_COUNT - this.Honey;
        // количество проживающих пчел.
        private int beeCount;
        // расположение объектов улья ВНУТРИ него.
        private readonly Dictionary<PlaceName, Point> locations;
        // ссылка на мир в котором существует улей.
        private readonly World myWorld;


        public Hive(World world)
        {
            if (rand == null)
            {
                throw new ArgumentNullException($"Неустановлен Rand в {nameof(Hive)}");
            }
            Honey = 3.20;
            myWorld = world;
            Bee.changeBeeState += this.ObserveBeeState;

            // координаты относительно внутреннего пространства улья.
            locations = new Dictionary<PlaceName, Point>
            {
                { PlaceName.entrance, new Point(600, 100) },
                { PlaceName.nursery,  new Point(95, 174)  },
                { PlaceName.factory,  new Point(157, 98)  },
                { PlaceName.exit,     new Point(194, 213) }
            };

            for (int i = 0; i < INIT_BEE_COUNT && this.beeCount < BEE_MAX_POPULATION; i++)
            {
                AddBee();
            }
        }

        // выдает координаты внутреннего объекта улья.
        public Point GetLocation(PlaceName placeName)
        {
            if (this.locations.ContainsKey(placeName))
            {
                return this.locations[placeName];
            }
            throw new ArgumentException($"Unknow location: {placeName}");
        }


        // переработка меда из нектара.
        public bool AddHoney(double nectar)
        {
            double additionHoney = nectar * NECTAR_TO_HONEY_RATIO;
            if ((this.Honey + additionHoney) <= HONEY_MAX_COUNT)
            {
                this.Honey += additionHoney;
                return true;
            }
            return false;
        }

        // Выдача меда потребителям из запасов улья.
        public bool ConsumeHoney(double amount)
        {
            if (amount <= this.Honey)
            {
                this.Honey -= amount;
                return true;
            }
            return false;
        }

        // Порождение новой пчелы.
        private void AddBee() 
        {
            Point nursery = GetLocation(PlaceName.nursery);
            // точка рождения (появления) пчелы может отличаться от точки питомника на +- 50 единиц.
            Point bornp = new Point(nursery.X + rand.Next(100) - 50, nursery.Y + rand.Next(100) - 50);
            Bee newBee = new Bee(beeCount, bornp, myWorld, this);
            this.beeCount++;

            // TODO: добавить пчелу в систему. Нужно коллецию чел привязать к улью. Или как то учитывать их количество.
            this.myWorld.bees.Add(newBee);

        }

        private void ObserveBeeState(BeeStateInfo binfo)
        {
            // Обработка убывания пчел
            if (binfo.current == BeeState.Retired)
            {   // TODO: Добавить проверки принадлежности пчелы к конкредному улью. Что бы учитывать только своих пчел.
                this.beeCount--;
            }

        }

        
        public void Go() 
        { 
            if (this.Honey > HONEY_MIN_FOR_INC_POPULA && this.beeCount < BEE_MAX_POPULATION)
            {
                // если повезет может родиться новая пчела...
                // TODO: Заглушка пока не привели пчелиную королеву.
                if (rand.Next(7) == 1)
                {
                    AddBee();
                }
            }
        }
    }
}
