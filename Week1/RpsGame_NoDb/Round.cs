using System;

namespace RpsGame_NoDb
{
    class Round
    {
        private Guid roundId = new Guid();
        public Guid RoundId{ get { return roundId; } }
        public RPS Player1Choice { get; set; } //computer
        public RPS Player2Choice { get; set; } //user
        public Player WinningPlayer { get; set; } = null;
    }
}