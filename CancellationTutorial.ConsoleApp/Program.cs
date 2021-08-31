using System;
using System.Threading;
using System.Threading.Tasks;

namespace CancellationTutorial.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var token = cancellationTokenSource.Token;

            // Cancelar após um certo período de tempo
            // cancellationTokenSource.CancelAfter(5000);

            // Enquanto não houve cancelamento, continue fazendo algo
            // while (!token.IsCancellationRequested)
            // {
            //     método maroto
            // }

            // Mais comum
            // token.ThrowIfCancellationRequested();

            // Exemplo com o while e com o IsCancellationRequested
            // await ExampleWithLoop(cancellationTokenSource);

            // Exemplo com o while e com o ThrowIfCancellationRequested
            await ExampleWithLoopThrow(cancellationTokenSource);


        }

        public static async Task ExampleWithLoop(CancellationTokenSource cancellationTokenSource)
        {
            Task.Run(() =>
            {
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.C)
                {
                    cancellationTokenSource.Cancel();
                    Console.WriteLine("Cancelando a task");
                }
            });

            while (!cancellationTokenSource.Token.IsCancellationRequested)
            {
                Console.WriteLine("Alguma operação que leva 3 segundos");
                await Task.Delay(3000);
            }

            Console.WriteLine("O token foi devidamente cancelado, saindo do while");

            cancellationTokenSource.Dispose();
        }

        public static async Task ExampleWithLoopThrow(CancellationTokenSource cancellationTokenSource)
        {
            Task.Run(() =>
            {
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.C)
                {
                    cancellationTokenSource.Cancel();
                    Console.WriteLine("Cancelando a task");
                }
            });

            try
            {
                while (true)
                {
                    cancellationTokenSource.Token.ThrowIfCancellationRequested();
                    Console.WriteLine("Alguma operação que leva 3 segundos");
                    await Task.Delay(3000);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.WriteLine("O token foi devidamente cancelado, saindo do while");

            cancellationTokenSource.Dispose();
        }
    }
}
