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
        // начальное количество пчел.
        private const int initBeeCount = 6;

        // начальное количество меда.
        private const double initHoney = 3.2;

        // максимальное количество меда.
        private const double maxHoney = 15.0;

        // коффициент переработки нектара в мед.
        private const double nectarHoneyRatio = 0.25;

        // максимальное количество пчел в улье.
        private const int maxBeePopulation = 18;

        // минимальное наличие меда для увеличения популяции.
        private const double minHoneyForUpPopula = 4;

        private static Random rand = new Random();

        // количество меда в улье.
        public double Honey { get; private set; }
        private Point location;
        private int beeCount;
        private World myWorld;

        // TODO: После тестирование перенести создание координат в конструктор.
        private Dictionary<PlaceName, Point> places = new Dictionary<PlaceName, Point>
        {
            { PlaceName.entrance, new Point(600, 100) },
            { PlaceName.nursery, new Point(95, 174) },
            { PlaceName.factory, new Point(157, 98) },
            { PlaceName.exit, new Point(194, 213) }
        };

        public void InitializeLocations(Point entrance, Point nursery, Point factory, Point exit) { 
        
        }

        public Point GetLocation(PlaceName placeName)
        {
            if (places.ContainsKey(placeName)) return places[placeName];
            throw new ArgumentException($"Unknow location: {placeName}");
        }

        public Hive(World world)
        {
            Honey = initHoney;
            myWorld = world;
            for (int i = 0; i < initBeeCount; i++) AddBee();
        }

        // переработка меда из нектара.
        public bool AddHoney(double nectar)
        {
            double honeyToAdd = nectar * nectarHoneyRatio;
            if (honeyToAdd + this.Honey > maxHoney) return false;
            this.Honey += honeyToAdd;
            return true;
        }

        // Выдача меда из улья.
        public bool ConsumeHoney(double amount)
        {
            if (amount > this.Honey) return false;
            this.Honey -= amount;
            return true;
        }

        // Порождение новой пчелы.
        private void AddBee() 
        {
            if (! (this.beeCount < maxBeePopulation))
            {
                throw new Exception("Превышен лимит на количество пчел!");
            }

            this.beeCount++;
            Point beeStartPoint = new Point(
                places[PlaceName.nursery].X + rand.Next(100) - 50,// 50 - на столько единиц может отстоять
                places[PlaceName.nursery].Y + rand.Next(100) - 50 // точка от питомника по осям X и Y.
            );
            Bee newBee = new Bee(beeCount, beeStartPoint, myWorld, this);
            // TODO: добавить пчелу в систему. Нужно коллецию чел привязать к улью. Или как то учитывать их количество.
            this.myWorld.bees.Add(newBee);
        }

        
        public void Go() 
        { 
            if (this.Honey > minHoneyForUpPopula)
            {
                // если повезет может родиться новая пчела...
                if (rand.Next(10) == 1) AddBee();
            }
        }
    }
}
