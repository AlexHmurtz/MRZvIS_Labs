using System.IO;

namespace mrzvis_2
{
    public class Table
    {
        public string[, ] matrix = new string[3, 3];
        private string path = @"D:\studying\MRZvIS\L2\results.txt";

        public Table()
        {
            for (int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    matrix[i, j] = "-";
                }
            }
        }

        public bool isVoidCoordinate(int x, int y)
        {
            if(matrix[x,y] == "-")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void fillTheFile()
        {
            for (int i = 0; i < 3; i++)
            {
                string temp = "";
                for (int j = 0; j < 3; j++)
                {
                    temp += matrix[i, j];
                }
                using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
                {
                    sw.WriteLine(temp);
                }
            }
            using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
            {
                sw.WriteLine("\n");
            }

        }

        public bool checkCoordinate(int x, int y, string symbol)
        {
            if (matrix[x,y] == symbol)
            {
                return true;
            }
            return false;
        }

        public void fillTheTable(string symbol, int x, int y)
        {
            matrix[x, y] = symbol;
        }

        public string getResults()
        {
            for(int i = 0; i < 3; i++)
            {
                if (matrix[i, 0] == matrix[i, 1] && matrix[i, 2] == matrix[i, 1])
                {
                    return matrix[i, 0];
                }
                else if(matrix[0,i] == matrix[1,i] && matrix[2,i] == matrix[1, i])
                {
                    return matrix[0, i];
                }
            }

            if(matrix[0,0] == matrix[1,1] && matrix[1,1] == matrix[2, 2])
            {
                return matrix[0, 0];
            }
            if(matrix[0,2] == matrix[1,1] && matrix[1,1] == matrix[2, 0])
            {
                return matrix[0, 2];
            }
            return "nobody";
        }
    }
}
