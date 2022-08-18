﻿using System;
using System.Collections.Generic;
using board;

namespace Console_Chess
{
    class View
    {
        public static void printBoard(Board board)
        {
            for (int i = 0; i < board.Lines; i++)
            {
                for (int j = 0; j < board.Columns; j++)
                {
                    if (board.piece(i, j) == null)
                        Console.Write("- ");
                    else
                        Console.Write($"{board.piece(i, j)} ");
                }
                Console.WriteLine();
            }
            
        }
       
    }
}