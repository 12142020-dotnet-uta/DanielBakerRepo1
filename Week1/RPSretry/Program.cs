using System;
using System.Collections.Generic;

namespace RPSretry
{
    class Program
    {
        static void Main(string[] args)
        {

            //creating collections to store the data locally
            List<Player> players = new List<Player>();


            // creating the computer and some default users
            Player compPlayer = new Player("Max", "Headroom");
            Player player1 = new Player("Max1", "Headroom");
            Player player2 = new Player("Max2", "Headroom");
            Player player3 = new Player("Max3", "Headroom");

            players.Add(compPlayer);
            players.Add(player1);
            players.Add(player2);
            players.Add(player3);


            Console.WriteLine("This is The Official Batch Rock-Paper-Scissors Game");

            Guid onlineId = login( getUserName(), players );

            string[] getUserName()
            {
                Console.WriteLine("Please enter your username:");
                string[] userNameInputArr;
                string userNameInput = Console.ReadLine();
                userNameInputArr = userNameInput.Split(" ");
                return userNameInputArr;
            }


            static Guid login( string[] userName, List<Player> playerList )
            {
                foreach( Player p in playerList )
                {
                    if(userName[0] == p.Fname && userName[1] == p.Lname)
                    {
                        Console.WriteLine($"Welcome back {p.Fname}!");
                        p.IsOnline = true;
                        return p.PlayerID;
                    } else
                    {
                        Player newPlayer = new Player(userName[0], userName[1]);
                        playerList.Add(newPlayer);
                        Console.WriteLine($"Welcome {newPlayer.Fname}!");
                        newPlayer.IsOnline = true;
                        return newPlayer.PlayerID;
                    }
                }
                return Guid.Empty;
            }

            void logout( Guid id, List<Player> playerList )
            {
                foreach( Player p in playerList )
                {
                    if( p.PlayerID == id )
                    {
                        p.IsOnline = false;
                        break;
                    }
                }
                Console.WriteLine("Coolio");
            }

            // userNameArr = getUserName();

            // ID of who is online            

           
            foreach( Player p in players )
            {
                Console.WriteLine($"{p.Fname} {p.IsOnline} {p.PlayerID} : {onlineId}");
            }

            logout(onlineId, players);
            
            foreach( Player p in players )
            {
                Console.WriteLine($"{p.Fname} {p.IsOnline} {p.PlayerID}");
            }

            ///
        }
    }
}
