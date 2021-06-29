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

        // TODO: После тестирование перенести создание координат в конструктор.
        private Dictionary<string, Point> places = new Dictionary<string, Point>
        {
            { "entrance", new Point(600, 100) },
            {"nursery", new Point(95, 174) },
            {"factory", new Point(157, 98) },
            {"exit", new Point(194, 213) }
        };

        public void InitializeLocations(Point entrance, Point nursery, Point factory, Point exit) { 
        
        }

        public Point GetLocation(string placeName)
        {
            if (places.ContainsKey(placeName)) return places[placeName];
            throw new ArgumentException($"Unknow location: {placeName}");
        }

        public Hive()
        {
            Honey = initHoney;
            for (int i = 0; i < initBeeCount; i++) AddBee();
        }

        public bool AddHoney(double nectar) => true;
        public bool ConsumeHoney(double amount) => true;
        private void AddBee() { }
        public void Go() { }
    }
}
