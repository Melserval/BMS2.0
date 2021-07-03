using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BMS
{


    // Пчела.
    class Bee
    {
        public static Random rand;
        // потребление меда пчелой.
        protected const double HONEY_CONSUMED = 0.5;
        // единиц перемещения за цикл.
        protected const int MOVE_RATE = 3;
        // минимальное содержание нектара в цветке для сбора.
        protected const double MIN_FLOWER_NECTAR = 1.5;
        // продолжительность существования пчелы.
        protected const int CARIER_SPAN = 1000;
        // общее количество созданных пчел.
        protected static int beeBorned = 0;


        // возраст пчелы.
        public int Age { get; protected set; } = 0;
        // пчела внутри улья.
        public bool InsideHive { get; protected set; }
        // количество нектара у пчелы.
        public double NectarCollected { get; protected set; }
        // место нахождения пчелы.
        protected Point location;
        public Point Location { get => location; }
        // идентификатор пчелы.
        protected int ID;
        // цветок для сбора нектара.
        protected Flower destinationFlower;

        // Состояния И/ИЛИ ДИРЕКТИВЫ пчелы.    
        protected enum BeeState : byte
        {
            Idle,            // Свободна
            FlyingToFlower,  // Летит на цветок
            GatheringNectar, // Собирает нектар
            ReturningToHive, // Возвращается в улей
            MakingHoney,     // Производит мед
            Retired          // Завершение работы
        }
        // текущее состояние пчелы.
        protected BeeState CurrentState { get; set; }
        // выработала ли свой ресурс пчела.
        public bool IsRetired => this.CurrentState == BeeState.Retired;

        protected Hive myHive;
        protected World myWorld;
        protected Dictionary<BeeState, Action> beeAction;

        public Bee(int id, Point startLocation, World world, Hive hive)
        {
            if (rand == null)
            {
                throw new ArgumentNullException($"Неустановлен Rand в {nameof(Bee)}");
            }
            ID = id;
            Age = 0;
            this.location = startLocation;
            InsideHive = true;
            destinationFlower = null;
            NectarCollected = 0;
            CurrentState = BeeState.Idle;
            myHive = hive;
            myWorld = world;

            beeAction = new Dictionary<BeeState, Action>() {
                {BeeState.Idle,            bsIdle },
                {BeeState.FlyingToFlower,  bsFlyingToFlower },
                {BeeState.GatheringNectar, bsGatheringNectar },
                {BeeState.MakingHoney,     bsMakingHoney },
                {BeeState.ReturningToHive, bsReturningToHive },
                {BeeState.Retired,         bsRetired }
            };
        }


        public void Go()
        {
            Age++;
            this.beeAction[this.CurrentState]?.Invoke();
        }

        // перемещение в указанную точку. 
        // за цикл - одно смещение на растояние "MoveRate".
        protected bool MoveTo(Point destination)
        {
            // вычисление разности между текущим и заданным местоположениями.
            bool xComplete = (Math.Abs(destination.X - location.X) <= MOVE_RATE);
            bool yComplete = (Math.Abs(destination.Y - location.Y) <= MOVE_RATE);

            if (!xComplete)
            {
                if (destination.X > Location.X)
                {
                    location.X += MOVE_RATE;
                }
                else
                {
                    location.X -= MOVE_RATE;
                }
            }

            if (!yComplete)
            {
                if (destination.Y > location.Y)
                {
                    location.Y += MOVE_RATE;
                }
                else
                {
                    location.Y -= MOVE_RATE;
                }
            }
            return xComplete && yComplete;
        }

        ///--- дейсвия для пчелы при выполнении директив BeeState. ---///
        
        protected void bsIdle()
        {
            if (Age > CARIER_SPAN) {
                CurrentState = BeeState.Retired;
                return;
            }
            // Если есть доступные цветы и мед в улье для пропитания, пчелы должна опылять цветы.
            if (myWorld.flowers.Count > 0 && myHive.ConsumeHoney(HONEY_CONSUMED)) {
                Flower flower = myWorld.flowers[rand.Next(myWorld.flowers.Count)];
                if (flower.Nectar >= MIN_FLOWER_NECTAR && flower.Alive) {
                    this.destinationFlower = flower;
                    CurrentState = BeeState.FlyingToFlower; // директива.
                }
            }
            // TODO: Что делать если нет цветов или улей не выдал мед?!
        }

        protected void bsFlyingToFlower()
        {
            // цветок не существует - возврат в улей.
            if (!myWorld.flowers.Contains(destinationFlower)) {
                CurrentState = BeeState.ReturningToHive;
                return;
            }

            if (InsideHive == true) {
                // когда внутри улья, сначала перемещение к выходу наружу.
                if ( MoveTo(myHive.GetLocation(PlaceName.exit)) ) { 
                    InsideHive = false;
                    location = myHive.GetLocation(PlaceName.entrance);
                }
            }
            else if (MoveTo(destinationFlower.Location)) {
                CurrentState = BeeState.GatheringNectar;
            }
        }

        protected void bsGatheringNectar() {
            double nectar = destinationFlower.HarvestNectar();
            if (nectar > 0)
            {
                NectarCollected += nectar;
            }
            else
            {   // TODO: зачем возвращаться в улей, вместо смены цветка?!
                CurrentState = BeeState.ReturningToHive;
            }
        }

        protected void bsReturningToHive()
        {
            if (!InsideHive) {
                // лезть в улей!
                if (MoveTo(myHive.GetLocation(PlaceName.entrance)))
                {
                    InsideHive = true;
                    location = myHive.GetLocation(PlaceName.exit);

                }
            } else {
                // что делать внутри.
                if (MoveTo(myHive.GetLocation(PlaceName.factory)))
                {
                    CurrentState = BeeState.MakingHoney;
                }
            }
        }

        protected void bsMakingHoney()
        {
            // переработка требует минимум 0.5 нектара.
            if (this.NectarCollected >= 0.5) 
            {

            } 
            else // если нектара меньше - он просто выбрасывается.
            {
                this.NectarCollected = 0;
                this.CurrentState = BeeState.Idle;
            }
        }

        protected void bsRetired()
        {
            Console.WriteLine($"{this.ID}: All Done!") ;
        }

    }
}
