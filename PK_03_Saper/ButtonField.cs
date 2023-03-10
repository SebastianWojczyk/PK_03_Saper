using System;
using System.Drawing;
using System.Windows.Forms;

namespace PK_03_Saper
{
    internal class ButtonField : Button
    {
        private bool isBomb;
        private bool isCovered;
        private int bombAround;
        public ButtonField()
        {
            IsCovered = true;
            IsBomb = false;
            BombAround = 0;
        }
        public bool IsBomb
        {
            get => isBomb;
            set
            {
                isBomb = value;
                prepareView();
            }
        }
        public bool IsCovered
        {
            get => isCovered;
            set
            {
                isCovered = value;
                if (!isCovered)
                {
                    Enabled = false;
                }
                prepareView();
            }
        }

        public int BombAround
        {
            get => bombAround;
            set
            {
                bombAround = value;
                prepareView();
            }
        }

        private void prepareView()
        {
            if (IsCovered)
            {
                Text = "";
                BackColor = Color.Gray;
            }
            else
            {

                if (IsBomb)
                {
                    Text = "BB";
                    BackColor = Color.Red;
                }
                else if (BombAround > 0)
                {
                    Text = bombAround.ToString();
                    BackColor = Color.White;
                }
                else
                {
                    BackColor = Color.White;
                }
            }
        }
    }
}