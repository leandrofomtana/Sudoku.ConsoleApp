using System;
using System.IO;

namespace Sudoku.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string sudoku = @"1 3 2 5 7 9 4 6 8
                              4 9 8 2 6 1 3 7 5
                              7 5 6 3 8 4 2 1 9
                              6 4 3 1 5 8 7 9 2
                              5 2 1 7 9 3 8 4 6
                              9 8 7 4 2 6 5 3 1
                              2 1 4 9 3 5 6 8 7
                              3 6 5 8 1 7 9 2 4
                              8 7 9 6 4 2 1 5 3";
            int[,] linhasSudoku = new int[9, 9];

            using (StringReader sudokuReader = new StringReader(sudoku))
            {
                string linhaSudoku = "";
                for (int x = 0; x < 9; x++)
                {
                    linhaSudoku = sudokuReader.ReadLine();
                    string[] valores = linhaSudoku.Trim().Split();
                    for (int y = 0; y<9; y++)
                    {
                        linhasSudoku[x, y] = Convert.ToInt32(valores[y]);
                    }
                }
            }
//array bool unico é usado para checar os números em cada linha/coluna/matriz3x3.
//quando o numero é encontrado pela primeira vez, por ex é encontrado o n 1, o valor de bool[1] se
////torna true indicando que ja foi encontrado.
            bool[] unico = new bool[10]; 
            if (solucaoValida(linhasSudoku))
            {
                Console.WriteLine("SIM");
            }
            else
            {
                Console.WriteLine("NÃO");
            }
                Console.ReadLine();




            bool solucaoValida(int[,] sudoku)
            {
                if (!checarLinhaColuna(sudoku, "linha"))
                {
                    return false;
                }
                if (!checarLinhaColuna(sudoku, "coluna"))
                {
                    return false;
                }
                if (!checarminiMatrizes(sudoku))
                {
                    return false;
                }
                
                return true;
            }
//funcao checarminimatrizes checa cada matriz 3x3, usando 4 fors. Os 2 primeiros, com indice i e j,
//servem para identifcar a primeira linha e coluna respectivamente de cada matriz 3x3.
//entao com os 2 ultimos for, com indice m e n, cada matriz 3x3 é percorrida, e usando o vetor unico,
//se descobre se o número já apareceu ou não.
            bool checarminiMatrizes(int[,] sudoku)
            {
               for (int i = 0; i < 9; i += 3) 
                {
                    for (int j = 0; j < 9; j += 3)
                    {
                        Array.Fill(unico, false); 

                        for(int m = 0; m < 3; m++)
                        {
                            for(int n = 0; n < 3; n++)
                            {
                                int X = i + m; 
                                int Y = j + n;
                                int Z = sudoku[X, Y];
                                if (unico[Z])
                                {
                                    return false;
                                }
                                unico[Z] = true;
                            }
                        }
                    }
                }
                return true;
            }
//funcao checarlinhacoluna checa cada linha ou cada coluna usando o array unico.           
            bool checarLinhaColuna(int[,] sudoku, string id)
            {
                int N;
                for (int i = 0; i < 9; i++)
                {
                    Array.Fill(unico, false);
                    for (int j = 0; j < 9; j++)
                    {
                        if (id == "linha")
                        {
                            N =sudoku[i, j];
                        }
                        else
                        {
                            N = sudoku[j, i];
                        }
                        if (unico[N])
                        {
                            return false;
                        }
                        unico[N] = true;
                    }
                }
                return true;
            }
        }
    }
}
