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
        private const int fieldSize = 40;
        private Random generator;
        private ButtonField[,] board;
        private int bombCount;
        private int uncoveredCount;
        public PanelGame(int width, int height, int bombCount)
        {
            this.generator = new Random();
            this.board = new ButtonField[width, height];
            this.bombCount = bombCount;
            this.uncoveredCount = 0;

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

                    board[x, y].Click += ButtonField_Click;
                    // to tylko koloruje ... nie blokuje odkrywania
                    board[x, y].MouseDown += ButtonField_MouseDown;
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

        private void ButtonField_MouseDown(object sender, MouseEventArgs e)
        {
            // to tylko koloruje ... nie blokuje odkrywania
            if (e.Button == MouseButtons.Right)
            {
                (sender as Button).BackColor = Color.Coral;
            }
        }

        private void ButtonField_Click(object sender, EventArgs e)
        {
            if (sender is ButtonField)
            {
                ButtonField bf = (sender as ButtonField);
                //if (bf != null) // zwaraca null gdy błąd

                if (bf.IsBomb)
                {
                    UncoverAll();
                    MessageBox.Show("Przegarłeś!");
                }
                else
                {
                    Uncover(bf);
                    if (board.GetLength(0) * board.GetLength(1) - uncoveredCount == bombCount)
                    {
                        UncoverAll();
                        MessageBox.Show("Wygrałeś!");
                    }
                }
            }

        }

        private void UncoverAll()
        {
            for (int x = 0; x < board.GetLength(0); x++)
            {
                for (int y = 0; y < board.GetLength(1); y++)
                {
                    board[x, y].IsCovered = false;
                }
            }
        }

        private void Uncover(ButtonField bf)
        {
            if (bf.IsCovered)
            {
                bf.IsCovered = false;
                uncoveredCount++;
                if (bf.BombAround == 0)
                {
                    foreach (ButtonField b in ButtonFieldAround(bf))
                    {
                        Uncover(b);
                    }
                }
            }
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
