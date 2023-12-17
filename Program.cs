using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ThreadLab
{
    public class Program
    {
        // Program with cars (threads) competing with each other
        public static void Main(string[] args)
        {
            Car car1 = new Car("Volvo", 30);
            Car car2 = new Car("Audi", 30);
            Car car3 = new Car("Tesla", 30);

            Thread car1Thread = new Thread(car1.RacingCars);
            Thread car2Thread = new Thread(car2.RacingCars);
            Thread car3Thread = new Thread(car3.RacingCars);

            car1Thread.Start();
            car2Thread.Start();
            car3Thread.Start();

            bool raceFinished = false;

            Console.WriteLine("Press enter to check status of cars!");

            while (!raceFinished)
            {
                if (Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Status:");
                    Console.WriteLine($"{car1.Name} has driven {car1.DistanceCovered} m and speed is {car1.Speed} m/s");
                    Console.WriteLine($"{car2.Name} has driven {car2.DistanceCovered} m and speed is {car2.Speed} m/s");
                    Console.WriteLine($"{car3.Name} has driven {car3.DistanceCovered} m and speed is {car3.Speed} m/s");
                    Console.WriteLine();

                    // Checking if any car is finished
                    raceFinished = car1.RaceFinished || car2.RaceFinished || car3.RaceFinished;

                    if (raceFinished)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Race finished!");

                        if (car1.RaceFinished)
                        {
                            Console.WriteLine($"{car1.Name} won the race!");
                            break;
                        }

                        else if (car2.RaceFinished)
                        {
                            Console.WriteLine();
                            Console.WriteLine($"{car2.Name} won the race!");
                            break;
                        }

                        else
                        {
                            Console.WriteLine($"{car3.Name} won the race!");
                            break;
                        }
                    }
                }
            }
        }
    }
}
