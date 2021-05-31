using System;
using System.Windows.Forms;
using System.Threading;

namespace mrzvis_2
{
    public partial class MRZvIS_2 : Form
    {
        public MRZvIS_2()
        {
            InitializeComponent();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            
            int firstPlWin = 0, secPlWin = 0;
            if(tbNumOfGames.Text == "")
            {
                return;
            }
            int num;
            if(!int.TryParse(tbNumOfGames.Text, out num))
            {
                tbNumOfGames.Text = "";
                return;
            }

            for(int count = 0; count < num; count++)
            {
                Player firstPlayer = new Player();
                Player secondPlayer = new Player();
                Table table = new Table();
                firstPlayer.setHand("x", true);
                secondPlayer.setHand("o", false);
                SimpleThread fPlForThread = new SimpleThread(ref firstPlayer, ref table, 5);
                SimpleThread sPlForThread = new SimpleThread(ref secondPlayer, ref table, 4);
                Thread fThread = new Thread(new ThreadStart(fPlForThread.WorkProcess));
                Thread sThread = new Thread(new ThreadStart(sPlForThread.WorkProcess));

                fThread.Start();
                sThread.Start();

                sThread.Join();
                fThread.Join();

                table.fillTheFile();
                if (table.getResults() == "x")
                {
                    firstPlWin++;
                }
                else if (table.getResults() == "o")
                {
                    secPlWin++;
                }
            }
            string results = firstPlWin.ToString() + " : " + secPlWin;
            MessageBox.Show(results, "Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        

        public class SimpleThread
        {
            Player player;
            Table table;
            int numOfIter;
            static Mutex cycleMutex = new Mutex();
            public SimpleThread(ref Player pl, ref Table tb, int count)
            {
                player = pl;
                table = tb;
                numOfIter = count;
            }
            public void WorkProcess()
            {
                for (int i = 0; i < numOfIter; i++)
                {
                    cycleMutex.WaitOne();
                    player.getCoordinates(ref table);
                    Thread.Sleep(1);
                    cycleMutex.ReleaseMutex();
                }
            }
        }
    }
}

