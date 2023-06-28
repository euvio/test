using System.Collections;
using System.IO.Ports;

namespace 串口通信2
{
    public class EvenNumbersGenerator : IEnumerable<int>
    {

        static async IAsyncEnumerable<int> GetData()
        {
            for (int i = 0; i < 1004; i++)
            {
                yield return i;
                await Task.Delay(10);
            }
        }



        IEnumerator<int> ProduceEvenNumbers()
        {
            var numbers = new int[] { 2, 4, 8, 6, 3, 5 };
            foreach (var number in numbers)
            {
                if(number % 2 == 0)
                {
                    yield return number;
                }
                else
                {
                    yield break;
                }
            }
        }


        private readonly int[] numbers;

        public EvenNumbersGenerator()
        {
            numbers = new int[] { 2, 4, 8, 6, 3, 5 };
        }

        public IEnumerator<int> GetEnumerator()
        {
            return new EvenNumbersGeneratorEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public class EvenNumbersGeneratorEnumerator : IEnumerator<int>
        {
            private readonly int[] numbers;
            private int index = -1;

            public EvenNumbersGeneratorEnumerator(EvenNumbersGenerator generator)
            {
                numbers = generator.numbers;
            }

            public int Current => numbers[index];

            object IEnumerator.Current => Current;

            public void Dispose()
            {
                return;
            }

            public bool MoveNext()
            {
                if (index++ >= numbers.Length)
                {
                    return false;
                }

                return Current % 2 == 0;
            }

            public void Reset()
            {
                index = -1;
            }
        }
    }



    internal class Program
    {
        private static void Main(string[] args)
        {
            SerialPort sp = new SerialPort();
            sp.PortName = "COM2";
            sp.ReadBufferSize = 2024 * 10000;

            sp.Open();


            while (true)
            {
                if (sp.BytesToRead > 0)
                {
                    File.AppendAllText("D:\\sp.txt", sp.ReadExisting());
                    Thread.Sleep(20);
                }
            }

            Console.ReadLine();

        }
    }
}