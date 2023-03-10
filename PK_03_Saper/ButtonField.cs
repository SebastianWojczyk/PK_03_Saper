using System;
using System.Windows.Forms;

namespace PK_03_Saper
{
    internal class ButtonField : Button
    {
        private bool isBomb;
        private int bombAround;
        public bool IsBomb
        {
            get => isBomb;
            set
            {
                isBomb = value;
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
            if (IsBomb)
            {
                Text = "BB";
            }
            else if (BombAround > 0)
            {
                Text = bombAround.ToString();
            }
        }
    }
}