using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PK_03_Saper
{
    class PanelGame : Panel
    {
        const int fieldSize = 40;
        Random generator;
        ButtonField[,] board;
        public PanelGame(int width, int height, int bombCount)
        {
            generator = new Random();
            board = new ButtonField[width, height];

            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    board[x, y] = new ButtonField();
                    board[x, y].Size = new Size(fieldSize, fieldSize);
                    board[x, y].Location = new Point(x * fieldSize, y * fieldSize);
                    this.Controls.Add(board[x, y]);
                }
            }

            do
            {
                int x = generator.Next(width);
                int y = generator.Next(height);
                if (!board[x, y].IsBomb)
                {
                    bombCount--;
                    board[x, y].IsBomb = true;

                    foreach (ButtonField bf in ButtonFieldAround(board[x, y]))
                    {
                        bf.BombAround++;
                    }

                }
            } while (bombCount > 0);
        }

        private List<ButtonField> ButtonFieldAround(ButtonField buttonField)
        {
            List<ButtonField> list = new List<ButtonField>();

            Point loc = GetLocation(buttonField);

            for (int x = loc.X - 1; x <= loc.X + 1; x++)
            {
                for (int y = loc.Y - 1; y <= loc.Y + 1; y++)
                {
                    if (x >= 0 && y >= 0 && x < board.GetLength(0) && y < board.GetLength(1))
                    {
                        list.Add(board[x, y]);
                    }
                }
            }
            return list;
        }

        private Point GetLocation(ButtonField buttonField)
        {
            for (int x = 0; x < board.GetLength(0); x++)
            {
                for (int y = 0; y < board.GetLength(1); y++)
                {
                    if (board[x, y] == buttonField)
                    {
                        return new Point(x, y);
                    }
                }
            }

            return Point.Empty;
        }
    }
}
