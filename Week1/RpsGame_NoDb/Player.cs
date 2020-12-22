using System;

namespace RpsGame_NoDb
{
    class Player
    {
        //propfull
        private Guid playerId = new Guid();
        public Guid PlayerId
        {
            get { return playerId; }
        }

        private string fName;
        public string Fname
        {
            get { return fName; }
            set
            {
                if(value is string && value.Length < 20 && value.Length > 0)
                {
                    fName = value; 
                }
                else
                {
                    throw new Exception("The player name you sent is not valid");
                }
            }
        }

        private string lName;
        public string Lname
        {
            get { return lName; }
            set
            {
                if(value is string && value.Length < 20 && value.Length > 0)
                {
                    lName = value; 
                }
                else
                {
                    throw new Exception("The player name you sent is not valid");
                }
            }
        }
        

        private int numWins;
        private int numLosses;


        public void addWin()
        {
            numWins++;
        }

        public void addLoss()
        {
            numLosses++;
        }

        public int[] getWinLossRecord()
        {
            int[] winsAndLosses = new int [2];

            winsAndLosses[0] = numWins;
            winsAndLosses[1] = numLosses;

            return winsAndLosses;
        }
        
        
    }
}