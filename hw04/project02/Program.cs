using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project02
{
    class Alarm
    {
        //public delegate void EventHandler(object sender, EventArgs e);
        public event EventHandler<int> Tick;
        public event EventHandler Ring;
        public int NowTime { get; set; }
        public int RingTime { get; set; }
        public Alarm()
        {
            NowTime = 0;
            RingTime = 0;
            //Start();
        }
        
        public void Start()
        {
            while (true){
                System.Threading.Thread.Sleep(1000);
                NowTime++;
                Tick(this,NowTime);
                if (NowTime == RingTime)
                    Ring(this,EventArgs.Empty);
            }
            
        }
        public void SetAlarm(int ringtime)
        {
            RingTime = ringtime;
            Console.WriteLine($"The RingTime is set to {ringtime}");
        }
    }
    class Program
    {
        //使用事件机制，模拟实现一个闹钟功能。闹钟可以有嘀嗒（Tick）事件和响铃（Alarm）两个事件。
        //在闹钟走时时或者响铃时，在控制台显示提示信息。
        static void Main(string[] args)
        {
            Console.WriteLine("hw4-2: Alarm");
            Alarm clock = new Alarm();
            clock.Tick += new EventHandler<int>(Alarm_tick);
            clock.Ring += new EventHandler(Alarm_ring);
            clock.SetAlarm(5);
            clock.Start();
            Console.ReadKey();
        }
        static void Alarm_tick(object sender,int now)
        {
            Console.WriteLine($"Tick... Now is {now}");
        }
        static void Alarm_ring(object sender,EventArgs e)
        {
            Console.WriteLine("RRRRIIIIIIIIIING!!!!!!");
        }
    }
}
