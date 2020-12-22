using System;
using System.Collections.Generic;

namespace RpsGame_NoDb
{
    class Match
    {
        private Guid matchId = new Guid();
        public Guid MatchId{ get { return matchId; } }

        public Player Player1 { get; set; } //computer
        public Player Player2 { get; set; } //user

        List<Round> Rounds = new List<Round>();

        private int p1RoundWins { get; set; } //how many rounds has player won?
        private int p2RoundWins { get; set; }
        private int ties { get; set; }


        //Methods
        public void roundWinner(Player p = null)
        {
            if (p.PlayerId == Player1.PlayerId)
            {
                p1RoundWins++;
            }
            else if(p.PlayerId == Player2.PlayerId)
            {
                p2RoundWins++;
            }
            else
            {
                ties++;
            }
        }

        public Player matchWinner()
        {
            if(p1RoundWins == 2)
            {
                return Player1;
            }
            else if(p2RoundWins == 2)
            {
                return Player2;
            }
            else
            {
                return null;
            }
        }

    }
}