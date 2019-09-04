using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris
{
    public class Item : Figurs
    {
        public int[,] obj;
        public int obj_I = 0;
        public int obj_J = 0;
        public Item(int[,] matr)
        {
            this.obj = get_random();
            color_rnd(ref this.obj);
            this.obj_J = rand_J(this.obj, matr);
        }
        public void transform(int[,] matr)
        {

            int[,] arr = new int[obj.GetLength(1), obj.GetLength(0)];
            int alternativ_J = obj_J;

            if (obj.GetLength(0) + obj_J > matr.GetLength(1))
            {
                alternativ_J += obj.GetLength(1) - obj.GetLength(0);

            }

            if (obj.GetLength(1) + obj_I > matr.GetLength(0))
                return;
            for (int i = 0; i < obj.GetLength(0); i++)
                for (int j = 0; j < obj.GetLength(1); j++)
                {
                    if (matr[obj_I + j, alternativ_J + obj.GetLength(0) - 1 - i] != 0)
                        return;
                    arr[j, obj.GetLength(0) - 1 - i] = obj[i, j];
                }
            obj_J = alternativ_J;
            obj = arr;
        }
        public void clear(int[,] matr)
        {
            for (int i = 0; i < obj.GetLength(0); i++)
                for (int j = 0; j < obj.GetLength(1); j++)
                {
                    if (obj[i, j] != 0)
                        matr[obj_I + i, obj_J + j] = 0;
                }
        }
        public void draw(int[,] matr)
        {
            for (int i = 0; i < obj.GetLength(0); i++)
                for (int j = 0; j < obj.GetLength(1); j++)
                {
                    if (obj[i, j] != 0)
                        matr[obj_I + i, obj_J + j] = obj[i, j];
                }
        }
        public bool control(int[,] matr, int i_plas = 0, int j_plas = 0, bool tst = true)
        {
            if (obj_I + obj.GetLength(0) > matr.GetLength(0) - 1 && tst)
                return false;
            for (int i = 0; i < obj.GetLength(0); i++)
                for (int j = 0; j < obj.GetLength(1); j++)
                    if ((matr[obj_I + i_plas + i, obj_J + j_plas + j] != 0 && obj[i, j] != 0))
                        return false;

            return true;
        }
        
    }
}
