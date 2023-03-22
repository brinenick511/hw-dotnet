using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project02
{
    class ClockTime
    {
        public int Hour { get; set; }
        public int Min { get; set; }
        public int Sec { get; set; }
        public ClockTime()
        {
            Hour = Min = Sec = 0;
        }
        public ClockTime(int h,int m,int s)
        {
            Hour = h;
            Min = m;
            Sec = s;
        }
        public void SetNow()
        {
            DateTime curentTime = DateTime.Now;
            Hour = curentTime.Hour;
            Min = curentTime.Minute;
            Sec = curentTime.Second;
        }
        public ClockTime PlusSec(int t)
        {
            ClockTime ans = new ClockTime(Hour, Min, Sec);
            ans.Sec += t;
            if (ans.Sec >= 60)
            {
                ans.Sec -= 60;
                ans.Min += 1;
                if (ans.Min >= 60)
                {
                    ans.Min -= 60;
                    ans.Hour -= 1;
                }
            }
            return ans;
        }
        public bool Equal(ClockTime c)
        {
            return (Hour == c.Hour) && (Min == c.Min) && (Sec == c.Sec);
        }
        public void Print()
        {
            Console.WriteLine(Hour + ":" + Min + ":" + Sec);
        }
    }
    class Alarm
    {
        //public delegate void EventHandler(object sender, EventArgs e);
        public event EventHandler<ClockTime> Tick;
        public event EventHandler Ring;
        public ClockTime NowTime { get; set; }
        public ClockTime RingTime { get; set; }
        public Alarm()
        {
            NowTime = new ClockTime();
            RingTime = new ClockTime();
            //Start();
        }
        
        public void Start()
        {
            while (true){
                System.Threading.Thread.Sleep(1000);
                NowTime=NowTime.PlusSec(1);
                Tick(this,NowTime);
                if (NowTime.Equal(RingTime))
                    Ring(this,EventArgs.Empty);
            }
            
        }
        public void SetAlarm(ClockTime ringtime)
        {
            RingTime.Hour = ringtime.Hour;
            RingTime.Min = ringtime.Min;
            RingTime.Sec = ringtime.Sec;
            Console.WriteLine($"The RingTime is set to {ringtime.Hour}:{ringtime.Min}:{ringtime.Sec}");
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
            clock.Tick += new EventHandler<ClockTime>(Alarm_tick);
            clock.Ring += new EventHandler(Alarm_ring);
            ClockTime now = new ClockTime();
            now.SetNow();
            clock.NowTime = now;
            clock.SetAlarm(now.PlusSec(5));
            //clock.NowTime.Print();
            //clock.RingTime.Print();
            clock.Start();
            Console.ReadKey();
        }
        static void Alarm_tick(object sender,ClockTime now)
        {
            Console.WriteLine($"Tick... Now is {now.Hour}:{now.Min}:{now.Sec}");
        }
        static void Alarm_ring(object sender,EventArgs e)
        {
            Console.WriteLine("RRRRIIIIIIIIIING!!!!!!");
        }
    }
}
