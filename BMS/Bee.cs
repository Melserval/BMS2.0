using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BMS
{
    // TODO: Придумать расход меда пчелой покадрово. 
    // что бы не расходовался мед оптом, для каждого полета к цветку.

    // Состояния И/ИЛИ ДИРЕКТИВЫ пчелы.    
    enum BeeState : byte
    {
        Idle=1,            // Свободна
        FlyingToFlower,  // Летит на цветок
        GatheringNectar, // Собирает нектар
        ReturningToHive, // Возвращается в улей
        MakingHoney,     // Производит мед
        Retired          // Не трудоспособна.
    }

    // смена состояния пчелы <beeID, текущее, предыдущее>.
    delegate void ChangeBeeState(int beeID, BeeState current, BeeState previus);

    // Пчела.
    class Bee
    {
        public static ChangeBeeState changeBeeState; 
        public static Random rand;

        // потребление меда пчелой.
        protected const double HONEY_CONSUMED = 0.3;
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
        // описания статусов, при показе статистики.
        public static Dictionary<BeeState, string> DescripeBeeState = new Dictionary<BeeState, string>
        {
            {BeeState.Idle,  "Простаивает" },
            {BeeState.FlyingToFlower, "Летит к цветку" },
            {BeeState.GatheringNectar, "Собирает нектар" },
            {BeeState.ReturningToHive, "Летит в улей" },
            {BeeState.MakingHoney, "Производит мед" },
            {BeeState.Retired, "Ресурс исчерпан" }
        };
        
        // состояние пчелы.
        protected BeeState currentState;
        protected BeeState previousState;
        protected BeeState CurrentState 
        {   
            get
            {
                return this.currentState;
            }
            set
            {
                if (this.currentState != value) { 
                    this.previousState = this.currentState;
                    this.currentState = value;
                    changeBeeState?.Invoke(this.ID, this.currentState, this.previousState);
                }
            }
        }
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
            beeAction = new Dictionary<BeeState, Action>() {
                {BeeState.Idle,            bsIdle },
                {BeeState.FlyingToFlower,  bsFlyingToFlower },
                {BeeState.GatheringNectar, bsGatheringNectar },
                {BeeState.MakingHoney,     bsMakingHoney },
                {BeeState.ReturningToHive, bsReturningToHive },
                {BeeState.Retired,         bsRetired }
            };
            ID = id;
            Age = 0;
            location = startLocation;
            InsideHive = true;
            destinationFlower = null;
            NectarCollected = 0;
            CurrentState = BeeState.Idle; // TODO: Разобраться почему при первичном запуске все нормально. А при резете - this.descripeBeeState[value] = НУЛЛ!
            myHive = hive;
            myWorld = world;
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
        
        // протокол : нет выполняемых задач.
        protected void bsIdle()
        {
            if (Age > CARIER_SPAN) 
            {
                CurrentState = BeeState.Retired;
            }
            // Если есть доступные цветы и мед в улье для пропитания, пчела отправляется к цветку. 
            else if (myWorld.flowers.Count > 0 && myHive.ConsumeHoney(HONEY_CONSUMED)) 
            {
                Flower flower = myWorld.flowers[rand.Next(myWorld.flowers.Count)];

                // если цветок не подходящий - на следующем цикле ищется новый.
                if (flower.Nectar >= MIN_FLOWER_NECTAR && flower.Alive) 
                {
                    this.destinationFlower = flower;
                    CurrentState = BeeState.FlyingToFlower;

                    // TODO: проверка логичности задания...
                    if (this.myHive.StoreLimit < 1)
                    {
                        System.Windows.Forms.MessageBox.Show(
                            "Зачем отправлять пчелу собирать нектар,\n" +
                            "если мед уже негде хранить?!"
                        );
                    }
                } 
            }
        }

        // протокол : полет к цветку.
        protected void bsFlyingToFlower()
        {
            // цель (цветок) перестает существовать, пока пчела летит. 
            if (!myWorld.flowers.Contains(this.destinationFlower)) {
                CurrentState = (InsideHive == true)
                    ? BeeState.Idle
                    : BeeState.ReturningToHive;
                Console.WriteLine("Flower RIP. new state: {0}", CurrentState);
            } 
            // внутри улья, сначала перемещение к выходу наружу.
            else if (InsideHive == true) {
                if ( MoveTo(myHive.GetLocation(PlaceName.exit)) ) { 
                    InsideHive = false;
                    // установка разположение относительно внешних координат. 
                    location = myHive.GetLocation(PlaceName.entrance);
                }
            }
            else if (MoveTo(this.destinationFlower.Location)) {
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
                // Нужна программа дейсвий по сбору нектара с окрестных 
                // цветов и определение лимита сбора, после которого стоит
                // возвращаться в улей с собранным нектаром.
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
            if (this.NectarCollected >= 0.5 && this.myHive.AddHoney(0.5)) 
            {
                this.NectarCollected -= 0.5;
            } 
            // если нектара меньше чем нужно или улей полон меда - нектар просто выбрасывается.
            else 
            {
                this.NectarCollected = 0;
                this.CurrentState = BeeState.Idle;
                // TODO: Что делать пчеле если улей полон меда?
                // зачем ей лететь снова собирать нектар?
                // нужны дополнитеьные задания, помимо сбора нектара.
            }
        }

        protected void bsRetired()
        {
            Console.WriteLine($"{this.ID}: All Done!") ;
        }

    }
}
