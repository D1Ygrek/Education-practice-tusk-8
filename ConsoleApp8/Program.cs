using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp8
{

    class Program
    {
        public static string printedword = "";
        public static bool ko = false;
        public static int[,] answer;
        static void recur(string currentword, int allnumbers, int maximalnumbers,int[,] aray1,int[,]aray2, params int[] numbersused)
        {
            if (!ko)
            {
                for (int i = 0; i < allnumbers; i++)
                {
                    bool ok = true; ;
                    for (int j = 0; j < numbersused.Length; j++)
                    {
                        if (numbersused[j] == i)
                        {
                            ok = false;
                        }
                    }
                    if ((ok) | (numbersused.Length == maximalnumbers))
                    {
                        if (numbersused.Length < maximalnumbers)
                        {
                            int[] numbersused1 = new int[numbersused.Length + 1];
                            for (int k = 0; k < numbersused.Length; k++)
                            {
                                numbersused1[k] = numbersused[k];
                            }
                            numbersused1[numbersused.Length] = i;
                            recur(currentword + i + " ", allnumbers, maximalnumbers, aray1, aray2, numbersused1);
                        }
                        else
                        {
                            if ((printedword == "") | (printedword != currentword))
                            {
                                printedword = currentword;
                                string[] slova = currentword.Split(' ');
                                int[,] arsr = new int[maximalnumbers, maximalnumbers];
                                for (int k = 0; k < maximalnumbers; k++)
                                {
                                    arsr[k, int.Parse(slova[k])] = 1;
                                }
                                int[,] res = new int[maximalnumbers, maximalnumbers];
                                int[,] arsrtr = new int[maximalnumbers, maximalnumbers];
                                for (int a1 = 0; a1 < maximalnumbers; a1++)
                                {
                                    for (int a2 = 0; a2 < maximalnumbers; a2++)
                                    {
                                        arsrtr[a1, a2] = arsr[a1, a2];
                                    }
                                }
                                for (int a1 = 0; a1 < maximalnumbers; a1++)
                                {
                                    for (int a2 = 0; a2 < a1; a2++)
                                    {
                                        int tmp = arsrtr[a1, a2];
                                        arsrtr[a1, a2] = arsrtr[a2, a1];
                                        arsrtr[a2, a1] = tmp;
                                    }
                                }
                                res = Multiplication(Multiplication(arsr, aray2), arsrtr);
                                ko = true;
                                for (int a1 = 0; a1 < maximalnumbers; a1++)
                                {
                                    for (int a2 = 0; a2 < a1; a2++)
                                    {
                                        if (res[a1, a2] != aray1[a1, a2]) ko = false;
                                    }
                                }
                                if (ko)
                                {
                                    answer = arsr;
                                }
                            }
                        }

                    }
                }
            }
            
        }
        static void Main(string[] args)
        {
            string filename= @"C:\Users\Demid\Desktop\курсовая\практика\8.txt";  //адрес файла с матрицами
            int arrstep=0;
            int[,] arr1 = new int[0,0];
            int[,] arr2 = new int[0,0];
            using (StreamReader reader = new StreamReader(File.Open(filename, FileMode.Open)))
            {
                arrstep = int.Parse(reader.ReadLine());
                arr1 = new int[arrstep,arrstep];
                arr2 = new int[arrstep, arrstep];
                for (int i = 0; i < arrstep; i++)
                {
                    string s = reader.ReadLine();
                    string[] ss = s.Split(' ');
                    for(int j = 0; j < arrstep; j++)
                    {
                        arr1[i, j] = int.Parse(ss[j]);
                    }
                }
                for (int i = 0; i < arrstep; i++)
                {
                    string s = reader.ReadLine();
                    string[] ss = s.Split(' ');
                    for (int j = 0; j < arrstep; j++)
                    {
                        arr2[i, j] = int.Parse(ss[j]);
                    }
                }
            }
            answer = new int[arrstep, arrstep];
            Console.WriteLine("Матрица графа1:");
            for (int i = 0; i < arrstep; i++)
            {
                for (int j = 0; j < arrstep; j++)
                {
                    Console.Write(arr1[i, j] + " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("Матрица графа2:");
            for (int i = 0; i < arrstep; i++)
            {
                for (int j = 0; j < arrstep; j++)
                {
                    Console.Write(arr2[i, j] + " ");
                }
                Console.WriteLine();
            }
            recur("", arrstep, arrstep,arr1,arr2, new int[0]);
            Console.WriteLine();
            Console.WriteLine("Соответствие вершин:");
            for(int i = 0; i < arrstep; i++)
            {
                for(int j = 0; j < arrstep; j++)
                {
                    if (answer[i, j] == 1) Console.WriteLine("{0}={1}", i, j);
                }
            }
            Console.ReadLine();
        }
        static int[,] Multiplication(int[,] a, int[,] b)
        {
            if (a.GetLength(1) != b.GetLength(0)) throw new Exception("Матрицы нельзя перемножить");
            int[,] r = new int[a.GetLength(0), b.GetLength(1)];
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < b.GetLength(1); j++)
                {
                    for (int k = 0; k < b.GetLength(0); k++)
                    {
                        r[i, j] += a[i, k] * b[k, j];
                    }
                }
            }
            return r;
        }
    }
}
