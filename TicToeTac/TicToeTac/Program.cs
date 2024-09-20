using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicToeTac
{
    class Program
    {
        static char[,] Numbers = new char[3, 3] { { ' ', ' ', ' ' }, { ' ', ' ', ' ' }, { ' ', ' ', ' ' } };
        static char[,] Replay = new char[,] { { '1', '2', '3' }, { '4', '5', '6' }, { '7', '8', '9' } };
        static char Player = 'X';
        static Queue<string> Highlights = new Queue<string>();

        static void Main(string[] args)
        {
            StartGame();
        }

        public static void StartGame()
        {
            while (true)
            {
                Console.Clear();
                PrintBoard(Numbers);
                DisplayPosition();
                Console.WriteLine("Enter a Position Where you put " + Player + ". Enter a Choice Between 1 - 9: \n---------Please Check Your Choice are Already Enter---------");

                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input! Please enter a number between 1 and 9.");
                    Console.ReadKey();
                    continue;
                }

                if (GetPosition(choice))
                {
                    if (!IsRunning())
                    {
                        Console.Clear();
                        PrintBoard(Numbers);
                        Console.WriteLine("Player " + Player + " wins!");
                        WatchReplays();
                        return;
                    }
                    else if (IsDraw())
                    {
                        Console.Clear();
                        PrintBoard(Numbers);
                        Console.WriteLine("It's a draw!");
                        return;
                    }
                    else
                    {
                        Player = (Player == 'X') ? 'O' : 'X';
                    }
                }
                else
                {
                    Console.WriteLine("Your Choice is Already Enter! Press Enter Then Press the Choice");
                    Console.ReadKey();
                }
            }
        }

        public static void PrintBoard(char[,] board)
        {
            Console.WriteLine(" " + board[0, 0] + " | " + board[0, 1] + " | " + board[0, 2] + " ");
            Console.WriteLine("---|---|---");
            Console.WriteLine(" " + board[1, 0] + " | " + board[1, 1] + " | " + board[1, 2] + " ");
            Console.WriteLine("---|---|---");
            Console.WriteLine(" " + board[2, 0] + " | " + board[2, 1] + " | " + board[2, 2] + " ");
        }

        public static bool GetPosition(int choice)
        {
            if (choice > 0 && choice < 10)
            {
                Highlights.Enqueue(SetPosition(choice));
                string[] position = SetPosition(choice).Split(',');
                int row = int.Parse(position[0]);
                int col = int.Parse(position[1]);

                if (Numbers[row, col] == ' ')
                {
                    Numbers[row, col] = Player;
                    return true;
                }
            }
            return false;
        }

        public static string SetPosition(int choice)
        {
            switch (choice)
            {
                case 1: return "0,0";
                case 2: return "0,1";
                case 3: return "0,2";
                case 4: return "1,0";
                case 5: return "1,1";
                case 6: return "1,2";
                case 7: return "2,0";
                case 8: return "2,1";
                case 9: return "2,2";
                default: return null;
            }
        }

        public static bool IsRunning()
        {
            for (int i = 0; i < 3; i++)
            {
                if ((Numbers[i, 0] == Player && Numbers[i, 1] == Player && Numbers[i, 2] == Player) ||
                    (Numbers[0, i] == Player && Numbers[1, i] == Player && Numbers[2, i] == Player))
                {
                    return false;
                }
            }

            if ((Numbers[0, 0] == Player && Numbers[1, 1] == Player && Numbers[2, 2] == Player) ||
                (Numbers[0, 2] == Player && Numbers[1, 1] == Player && Numbers[2, 0] == Player))
            {
                return false;
            }
            return true;
        }

        public static void WatchReplays()
        {
            char currentPlayer = 'X';
            int step = 1;

            foreach (var moves in Highlights)
            {
                Console.WriteLine("Press Enter for the next step");
                Console.ReadKey();
                string[] temp = moves.Split(',');
                int row = int.Parse(temp[0]);
                int col = int.Parse(temp[1]);
                Replay[row, col] = currentPlayer;
                Console.WriteLine("Step " + step + ":\n");
                PrintBoard(Replay);
                currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
                step++;
            }
        }

        public static void DisplayPosition()
        {
            Console.WriteLine("Positions:");
            for (int i = 1; i <= 9; i++)
            {
                Console.Write(i + " : [" + SetPosition(i) + "]\t");
                if (i % 3 == 0)
                {
                    Console.WriteLine();
                }
            }
        }

        public static bool IsDraw()
        {
            return CheckForDraw(Numbers);
        }

        public static bool CheckForDraw(char[,] board)
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (board[row, col] == ' ')
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
    