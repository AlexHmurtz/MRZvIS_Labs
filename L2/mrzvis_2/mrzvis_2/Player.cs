using System;
using System.Collections.Generic;
using System.Drawing;

namespace mrzvis_2
{
    public class Player
    {
        private string playerHand;
        private string enemyHand; 
        bool isFirst = false;

        public void setHand(string symbol, bool first)
        {
            playerHand = symbol;
            isFirst = first;
            if(playerHand == "x")
            {
                enemyHand = "o";
            }
            else
            {
                enemyHand = "x";
                playerHand = "o";
            }
        }

        public void getCoordinates(ref Table table)
        {
            if (isFirst)
            {
                firstMove(ref table);
                isFirst = false;
                return;
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (table.checkCoordinate(i, j, enemyHand))
                    {
                        List<Point> voidCoordinates = new List<Point>();
                        int tempX = i, tempY = j;
                        tempX++;
                        addToList(ref voidCoordinates, tempX, tempY, ref table);
                        tempX--;
                        tempY++;
                        addToList(ref voidCoordinates, tempX, tempY, ref table);
                        tempX++;
                        addToList(ref voidCoordinates, tempX, tempY, ref table);
                        tempX -= 2;
                        tempY--;
                        addToList(ref voidCoordinates, tempX, tempY, ref table);
                        tempX++;
                        tempY--;
                        addToList(ref voidCoordinates, tempX, tempY, ref table);
                        tempX--;
                        addToList(ref voidCoordinates, tempX, tempY, ref table);
                        if(voidCoordinates.Count != 0)
                        {
                            Random rnd = new Random();
                            int result = rnd.Next(0, voidCoordinates.Count);
                            table.fillTheTable(playerHand, voidCoordinates[result].X, voidCoordinates[result].Y);
                            return;
                        }
                    }
                }
            }

        }

        private void addToList(ref List<Point> points, int x, int y, ref Table table)
        {
            if (isExist(x, y))
            {
                if (table.isVoidCoordinate(x, y))
                {
                    Point point = new Point(x, y);
                    points.Add(point);
                }
            }
        }

        private bool isExist(int x, int y)
        {
            if(x < 0 || x > 2)
            {
                return false;
            }
            if(y < 0 || y > 2)
            {
                return false;
            }
            return true;
        }

        private void firstMove(ref Table table)
        {
            Random rnd = new Random();
            int x = rnd.Next(0, 2);
            int y = rnd.Next(0, 2);
            table.fillTheTable(playerHand, x, y);
        }
    }
}
