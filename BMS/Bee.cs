using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BMS
{
    // Чем сейчас занята пчела.
    enum BeeState: byte
    {
        Idle,            // Свободна
        FlyingToFlower,  // Летит на цветок
        GatheringNectar, // Собирает нектар
        ReturningToHive, // Возвращается в улей
        MakingHoney,     // Производит мед
        Retired          // Завершение работы
    }

    // Пчела.
    class Bee
    {
        protected const double HoneyConsumed = 0.5;
        protected const int MoveRate = 3; 
        // минимально необходимое количество для сбора.
        protected const double MinimumFlowerNectar = 1.5;
        protected const int CareerSpan = 1000;
        
        protected Point location;
        protected Random rand = new Random();
        public BeeState CurrentState { get; protected set; }

        public int Age { get; protected set; } = 0;
        public bool InsideHive { get; protected set; }
        public double NectarCollected { get; protected set; }
        public Point Location { get => location; }

        private int ID;
        private Flower destinationFlower;
        private Hive myHive;
        private World myWorld;
        private Dictionary<BeeState, Action> beeAction;

        public Bee(int id, Point location, World world, Hive hive)
        {
            ID = id;
            Age = 0;
            this.location = location;
            InsideHive = true;
            CurrentState = BeeState.Idle;
            destinationFlower = null;
            NectarCollected = 0;
            myHive = hive;
            myWorld = world;
            beeAction = new Dictionary<BeeState, Action>();
            beeAction.Add(BeeState.Idle, bsIdle);
            beeAction.Add(BeeState.FlyingToFlower, bsFlyingToFlower);
            beeAction.Add(BeeState.GatheringNectar, bsGatheringNectar);
            beeAction.Add(BeeState.ReturningToHive, bsReturningToHive);
            beeAction.Add(BeeState.MakingHoney, bsMakingHoney);
        }

        public void Go()
        {
            Age++;
            this.beeAction[CurrentState]?.Invoke();
        }

        private bool MoveTowardsLocation(Point destination)
        {
            if (Math.Abs(destination.X - Location.X) <= MoveRate &&
                Math.Abs(destination.Y - Location.Y) <= MoveRate)
            {
                return true;
            }

            if (destination.X > Location.X)
            {
                (location).X += MoveRate;
            } 
            else if (destination.X < location.X)
            {
                location.X -= MoveRate;
            }

            if (destination.Y > location.Y)
            {
                location.Y += MoveRate;
            }
            else if (destination.Y < location.Y)
            {
                location.Y -= MoveRate;
            }
            return false;
        }

        // дейсвия для определенных состояний пчелы. из перечня BeeState.
        
        private void bsIdle()
        {
            if (Age > CareerSpan) {
                CurrentState = BeeState.Retired;
            }
            else if (myWorld.flowers.Count > 0 && myHive.ConsumeHoney(HoneyConsumed)) {
                Flower flower = myWorld.flowers[rand.Next(myWorld.flowers.Count)];
                if (flower.Nectar >= MinimumFlowerNectar && flower.Alive) {
                    destinationFlower = flower;
                    CurrentState = BeeState.FlyingToFlower;
                }
            }
        }

        private void bsFlyingToFlower()
        {
            if (!myWorld.flowers.Contains(destinationFlower)) {
                CurrentState = BeeState.ReturningToHive;
            }
            else if ( InsideHive && MoveTowardsLocation(myHive.GetLocation(PlaceName.exit)) ) {
                InsideHive = false;
                location = myHive.GetLocation(PlaceName.entrance);
            }
            else if (MoveTowardsLocation(destinationFlower.Location)) {
                CurrentState = BeeState.GatheringNectar;
            }
        }

        private void bsGatheringNectar() {
            double nectar = destinationFlower.HarvestNectar();
            if (nectar > 0) NectarCollected += nectar;
            else CurrentState = BeeState.ReturningToHive;
        }

        private void bsReturningToHive()
        {
            if (!InsideHive && MoveTowardsLocation(myHive.GetLocation(PlaceName.entrance))) {
                InsideHive = true;
                location = myHive.GetLocation(PlaceName.exit);
            } else if (MoveTowardsLocation(myHive.GetLocation(PlaceName.factory))) {
                 CurrentState = BeeState.MakingHoney;
            }
        }

        private void bsMakingHoney()
        {
            if (NectarCollected < 0.5) {
                NectarCollected = 0;
                CurrentState = BeeState.Idle;
            } else if (myHive.AddHoney(0.5)) {
                    NectarCollected -= 0.5;
            } else {
                 NectarCollected = 0;
            }
        }
    }
}
