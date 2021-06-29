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
        public BeeState CurrentState { get; protected set; }

        public int Age { get; protected set; } = 0;
        public bool InsideHive { get; protected set; }
        public double NectarCollected { get; protected set; }
        public Point Location { get => location; }

        private int ID;
        private Flower destinationFlower;

        public Bee(int id, Point location)
        {
            ID = id;
            Age = 0;
            this.location = location;
            InsideHive = true;
            CurrentState = BeeState.Idle;
            destinationFlower = null;
            NectarCollected = 0;
        }

        public void Go()
        {
            Age++;
            switch (CurrentState)
            {
                case BeeState.Idle:
                    if (Age > CareerSpan) CurrentState = BeeState.Retired;
                    else /**/ ;
                    break;

                case BeeState.FlyingToFlower:
                    break;

                case BeeState.GatheringNectar:
                    double nectar = destinationFlower.HarvestNectar();
                    if (nectar > 0) NectarCollected += nectar;
                    else CurrentState = BeeState.ReturningToHive;
                    break;

                case BeeState.ReturningToHive:
                    if (!InsideHive) /**/;
                    else /**/;
                    break;

                case BeeState.MakingHoney:
                    if (NectarCollected < 0.5) {
                        NectarCollected = 0;
                        CurrentState = BeeState.Idle;
                    } else {
                        /**/
                    }
                    break;

                default:
                    break;
            }
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

    }
}
