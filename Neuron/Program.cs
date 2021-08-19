using System;

namespace Neuron
{
    class Program
    {
        public class Neuron
        {
            private decimal weight = 0.5m;
            public decimal LastError { get; private set; }
            public decimal Smoothing { get; set; } = 0.0000001m;
            public decimal ProcessInputData(decimal input)
            {
                return input * weight;
            }
            public decimal RestoreInputData(decimal output)
            {
                return output / weight;
            }
            public void Train (decimal input,decimal expectedResult)
            {
                var actualResult = input * weight;
                LastError = expectedResult - actualResult;
                var correction = (LastError / actualResult)*Smoothing ;
                weight += correction;
            }
        }

        static void Main(string[] args)
        {
            decimal km = 100;
            decimal miles = 62.1371m;

            Neuron neuron = new Neuron();
            int i = 0;
            do
            {
                i++;
                    neuron.Train(km, miles);
                if (i % 100000 == 0)
                {
                    Console.WriteLine($"Итерация {i}\t Ошибка:\t{neuron.LastError}");
                }
            } while (neuron.LastError > neuron.Smoothing || neuron.LastError < -neuron.Smoothing);
            Console.WriteLine("Обучение завершено");

            Console.WriteLine($"{neuron.ProcessInputData(100)}миль в {100} km");
            Console.WriteLine($"{neuron.ProcessInputData(542)}миль в {542} km");
            Console.WriteLine($"{neuron.ProcessInputData(1000)}миль в {1000} km");
        }
    }
}
