using System;

namespace RPSretry
{
    public class Player
    {
        private Guid playerID = Guid.NewGuid();
        public Guid PlayerID
        {
            get { return playerID; }
            set { playerID = value; }
        }

        private bool isOnline = false;
        public bool IsOnline
        {
            get { return isOnline; }
            set { isOnline = value; }
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
                    throw new Exception("The name you sent is not valid");
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
                    throw new Exception("The name you sent is not valid");
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
            numLosses--;
        }

        public int[] getWinLossRecord()
        {
            int[] record = new int[2];

            record[0] = numWins;
            record[1] = numLosses;

            return record;
        }



        // constructors
        public Player()
        {

        }
        
        public Player( string fname, string lname)
        {
            this.Fname = fname;
            this.Lname = lname;
        }
        
        
    }
}