using System;
using System.Diagnostics;
using System.Threading;
using System.Timers;

namespace ThreadLab
{
    public class Car
    {
        public string Name { get; set; }
        public double DistanceCovered { get; set; }
        public double Speed { get; set; }
        public bool RaceFinished { get; set; }
        private static readonly object lockObject = new object();

        public Car(string name, double speed)
        {
            Name = name;
            Speed = speed;
            RaceFinished = false;
        }

        public void RacingCars()
        {
            Stopwatch stopwatch = new Stopwatch();
            Random random = new Random();
            System.Timers.Timer eventTimer = new System.Timers.Timer();

            // Generating random events every 5 seconds
            eventTimer.Interval = 5000;
            eventTimer.AutoReset = true;
            eventTimer.Elapsed += (sender, e) =>
            {
                int rnd = random.Next(1, 51);

                if (rnd == 10)
                {
                    Thread.Sleep(3000);
                    Speed -= 10;
                    Console.WriteLine($"Oh no! {Name} car ran out of gas. Needs to refuel!");
                    Console.WriteLine($"Speed reduced to {Speed} m/s");
                }

                else if (rnd == 20 || rnd == 50 || rnd == 49)
                {
                    Thread.Sleep(4000);
                    Speed -= 8;
                    Console.WriteLine($"Puncture for {Name}. Needs to change tires!");
                    Console.WriteLine("Slowing down 4 seconds");
                }

                else if (rnd == 30 || rnd == 40 || rnd == 43)
                {
                    Console.WriteLine($"{Name} has a dirty windshield. Needs a carwash!");
                    Console.WriteLine("Slowing down 5 seconds");
                    Thread.Sleep(5000);
                    Speed -= 5;
                }

                else if (rnd == 1 || rnd == 15)
                {
                    Speed -= 5;
                    Console.WriteLine($"{Name} has engine trouble. Speed reduced to {Speed} m/s!");
                }

                // Check if distance is greater than or equal to 10000 after an event
                if (DistanceCovered >= 10000)
                {
                    lock (lockObject)
                    {
                        if (!RaceFinished)
                        {
                            RaceFinished = true;
                            Console.WriteLine($"Race finished! {Name} reached the goal.");
                            eventTimer.Stop();
                            eventTimer.Dispose();
                        }
                    }
                }
            };

            eventTimer.Start();

            while (true)
            {
                if (RaceFinished)
                    break;

                stopwatch.Start();
                TimeSpan elapsed = stopwatch.Elapsed;

                // Check if distance is greater than or equal to 10000 before updating distance
                if (DistanceCovered >= 10000)
                {
                    lock (lockObject)
                    {
                        if (!RaceFinished)
                        {
                            RaceFinished = true;
                            Console.WriteLine($"Race finished! {Name} reached the goal.");
                            eventTimer.Stop();
                            eventTimer.Dispose();
                        }
                    }
                    break;
                }

                // Continuing to update distance
                DistanceCovered += Speed * elapsed.TotalSeconds;

                Console.WriteLine($"{Name} is racing!");
                Console.WriteLine($"Elapsed time: {elapsed}");

                Thread.Sleep(2000);
            }
        }
    }
}