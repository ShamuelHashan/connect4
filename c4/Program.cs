using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public struct PlayerDetails
{
	public String Name;
	public char playerID;
};

namespace Connect4
{
	class Program
	{
		static void Main(string[] args)
		{
			PlayerDetails FirstPlayer = new PlayerDetails();
			PlayerDetails SecondPlayer = new PlayerDetails();
			char[,] board = new char[9, 10];
			int counterDrop, win, full, playagain;

			Console.WriteLine("Are you ready to play Connect 4");
			Console.WriteLine("May player 1 please enter their name: ");
			FirstPlayer.Name = Console.ReadLine();
			FirstPlayer.playerID = 'X';
			Console.WriteLine("May player 2 please enter their name: ");
			SecondPlayer.Name = Console.ReadLine();
			SecondPlayer.playerID = 'O';

			full = 0;
			win = 0;
			playagain = 0;
			DisplayBoard(board);
			do
			{
				counterDrop = PlayerDrop(board, FirstPlayer);
				CheckBellow(board, FirstPlayer, counterDrop);
				DisplayBoard(board);
				win = Check4(board, FirstPlayer);
				if (win == 1)
				{
					PlayerWin(FirstPlayer);
					playagain = restart(board);
					if (playagain == 2)
					{
						break;
					}
				}

				counterDrop = PlayerDrop(board, SecondPlayer);
				CheckBellow(board, SecondPlayer, counterDrop);
				DisplayBoard(board);
				win = Check4(board, SecondPlayer);
				if (win == 1)
				{
					PlayerWin(SecondPlayer);
					playagain = restart(board);
					if (playagain == 2)
					{
						break;
					}
				}
				full = BoardFull(board);
				if (full == 7)
				{
					Console.WriteLine("Looks like the board is full. Guess it is a draw");
					playagain = restart(board);
				}

			} while (playagain != 2);
		}
		static int PlayerDrop(char[,] board, PlayerDetails activePlayer)
		{
			int counterDrop;

			Console.WriteLine(activePlayer.Name + "'s. It is your turn");
			do
			{
				Console.WriteLine("Enter a number between 1 and 7 to select a column to drop your counter: ");
				counterDrop = Convert.ToInt32(Console.ReadLine());
			} while (counterDrop < 1 || counterDrop > 7);

			while (board[1, counterDrop] == 'X' || board[1, counterDrop] == 'O')
			{
				Console.WriteLine("That is a full row sorry. Try a different one: ");
				counterDrop = Convert.ToInt32(Console.ReadLine());
			}

			return counterDrop;
		}

		static void CheckBellow(char[,] board, PlayerDetails activePlayer, int counterDrop)
		{
			int length, turn;
			length = 6;
			turn = 0;

			do
			{
				if (board[length, counterDrop] != 'X' && board[length, counterDrop] != 'O')
				{
					board[length, counterDrop] = activePlayer.playerID;
					turn = 1;
				}
				else
					--length;
			} while (turn != 1);


		}

		static void DisplayBoard(char[,] board)
		{
			int rows = 6, columns = 7, i, ix;

			for (i = 1; i <= rows; i++)
			{
				Console.Write("|");
				for (ix = 1; ix <= columns; ix++)
				{
					if (board[i, ix] != 'X' && board[i, ix] != 'O')
						board[i, ix] = '*';

					Console.Write(board[i, ix]);

				}

				Console.Write("| \n");
			}

		}

		static int Check4(char[,] board, PlayerDetails activePlayer)
		{
			char XO;
			int win;

			XO = activePlayer.playerID;
			win = 0;

			for (int i = 8; i >= 1; --i)
			{

				for (int ix = 9; ix >= 1; --ix)
				{

					if (board[i, ix] == XO &&
						board[i - 1, ix - 1] == XO &&
						board[i - 2, ix - 2] == XO &&
						board[i - 3, ix - 3] == XO)
					{
						win = 1;
					}


					if (board[i, ix] == XO &&
						board[i, ix - 1] == XO &&
						board[i, ix - 2] == XO &&
						board[i, ix - 3] == XO)
					{
						win = 1;
					}

					if (board[i, ix] == XO &&
						board[i - 1, ix] == XO &&
						board[i - 2, ix] == XO &&
						board[i - 3, ix] == XO)
					{
						win = 1;
					}

					if (board[i, ix] == XO &&
						board[i - 1, ix + 1] == XO &&
						board[i - 2, ix + 2] == XO &&
						board[i - 3, ix + 3] == XO)
					{
						win = 1;
					}

					if (board[i, ix] == XO &&
						 board[i, ix + 1] == XO &&
						 board[i, ix + 2] == XO &&
						 board[i, ix + 3] == XO)
					{
						win = 1;
					}
				}

			}

			return win;
		}

		static int BoardFull(char[,] board)
		{
			int full;
			full = 0;
			for (int i = 1; i <= 7; ++i)
			{
				if (board[1, i] != '*')
					++full;
			}

			return full;
		}

		static void PlayerWin(PlayerDetails activePlayer)
		{
			Console.WriteLine(activePlayer.Name + " HAS DONE IT, " + activePlayer.Name + " GOT 4 IN A ROW");
		}

		static int restart(char[,] board)
		{
			int restart;

			Console.WriteLine("Wanna play again? Heck yes(1) Heck no(anything else lmao): ");
			restart = Convert.ToInt32(Console.ReadLine());
			if (restart == 1)
			{
				for (int i = 1; i <= 6; i++)
				{
					for (int ix = 1; ix <= 7; ix++)
					{
						board[i, ix] = '*';
					}
				}
			}
			else
				Console.WriteLine("Thanks for playing!");
			return restart;
		}
	}
}