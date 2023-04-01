using System;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;

namespace Problem4
{
    class Program
    {
        static void Main(string[] args)
        {
            FileStream inFile = new FileStream("In0304.txt", FileMode.Open, FileAccess.Read);
            FileStream outFile = new FileStream("Out0304.txt", FileMode.Truncate, FileAccess.Write);
            using var writer = new StreamWriter(outFile);
            using var reader = new StreamReader(inFile);

            int n;
            n = int.Parse(reader.ReadLine());

            string[] file = new string[n];
            int[,] tab = new int[n, n * n]; // tablica, gdzie przechowujemy wartości z całego pliku
            int[,] graph = new int[n, n]; // tablica wag
            int[] lTab = new int[n]; // tablica, gdzie przechowujemy długości dla poszczególnych kolumn

            for (int i = 0; i < n; i++)
            {
                file[i] = reader.ReadLine();
            }

            for (int i = 0; i < n; i++)
            {
                string[] variables = file[i].Split(' ');

                for (int j = 0; j < variables.Length; j++)
                {
                    lTab[i] = variables.Length;
                    tab[i, j] = int.Parse(variables[j]);
                    //Console.Write(tab[i, j] + " ");
                }
                //Console.WriteLine();
            }


            //zerowanie całej tabeli wag
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    graph[i, j] = 0;
                }
            }

            int help2 = 0, maxW = 0;
            for (int i = 0; i < n; i++)
            {

                for (int j = 0; j < lTab[i]; j += 2)
                {
                    help2 = tab[i, j];
                    graph[i, help2 - 1] = tab[i, j + 1];
                    if (maxW < tab[i, j + 1])
                    {
                        maxW = tab[i, j + 1];
                    }
                }
            }

            /*for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(graph[i, j] + " ");
                }
                Console.WriteLine();
            }*/


            int suma = 0;
            int[,] odp = new int[n, 3]; // odpowiedzi
            int[,] options = new int[1, 2]; // możliwości z wierzcholka gdzie można
            int[] tops = new int[n]; // co z czym połączone
            bool[] used = new bool[n]; // połączone wierzchołki

            for (int i = 0; i < n; i++)
            {
                tops[i] = 0;
            }

            tops[0] = -1;
            used[0] = true;

            int help = 0, minCost = int.MaxValue;
            while (help < n - 1)
            {
                minCost = int.MaxValue;
                for (int i = 0; i < n; i++)
                {
                    if (used[i] == true)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            if (graph[i, j] < minCost && graph[i, j] != 0 && used[j] == false)
                            {
                                minCost = graph[i, j];
                                options[0, 0] = j;
                                options[0, 1] = graph[i, j];
                            }
                        }
                    }
                }

                for (int x = 0; x < n; x++)
                {
                    if (used[x] == true)
                    {
                        for (int y = 0; y < n; y++)
                        {
                            if (graph[x, y] == minCost && used[y] == false && y == options[0, 0])
                            {
                                odp[help, 0] = x + 1;
                                odp[help, 1] = y + 1;
                                odp[help, 2] = minCost;

                                suma += minCost;

                                Console.WriteLine("{0} {1} [{2}], ", odp[help, 0], odp[help, 1], odp[help, 2]);
                                writer.Write("{0} {1} [{2}], ", odp[help, 0], odp[help, 1], odp[help, 2]);

                                help++;
                                used[y] = true;
                                tops[y] = x;
                            }
                        }

                    }

                }

            }

            Console.WriteLine(suma);
            writer.WriteLine();
            writer.WriteLine(suma);

        }
    }
}
