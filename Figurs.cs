using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris
{
    public class Figurs
    {
        static Random rnd = new Random();
        int[,] figur_1 = new int[2, 2] { { 1, 1 }, { 1, 1 } };
        int[,] figur_2 = new int[1, 4] { { 1, 1, 1, 1 } };
        int[,] figur_3 = new int[2, 3] { { 0, 1, 0 }, { 1, 1, 1 } };
        int[,] figur_4 = new int[2, 3] { { 1, 0, 0 }, { 1, 1, 1 } };
        int[,] figur_5 = new int[2, 3] { { 0, 0, 1 }, { 1, 1, 1 } };
        int[,] figur_6 = new int[2, 3] { { 0, 1, 1 }, { 1, 1, 0 } };
        int[,] figur_7 = new int[2, 3] { { 1, 1, 0 }, { 0, 1, 1 } };

        public int[,] get_random()
        {
            switch (rnd.Next(1, 8))
            {
                case 1:
                    return figur_1;
                case 2:
                    return figur_2;
                case 3:
                    return figur_3;
                case 4:
                    return figur_4;
                case 5:
                    return figur_5;
                case 6:
                    return figur_6;
                case 7:
                    return figur_7;

                default: return figur_1;
            }
        }

        public void color_rnd(ref int[,] obj)
        {
            int obj_color = rnd.Next(1, 8);

            for (int i = 0; i < obj.GetLength(0); i++)
                for (int j = 0; j < obj.GetLength(1); j++)
                    if (obj[i, j] == 1)
                        obj[i, j] = obj_color;
        }
        public int rand_J(int[,] obj, int[,] matr)
        {
            return rnd.Next(0, matr.GetLength(1) - obj.GetLength(1) + 1);
        }
    }
}
