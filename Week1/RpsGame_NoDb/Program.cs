using System;
using System.Collections.Generic;

namespace RpsGame_NoDb
{

    class Program
    {
        static void Main(string[] args)
        {
            List<Player> players = new List<Player>();
            List<Match> matches = new List<Match>();
            List<Round> rounds = new List<Round>();

            Player p1 = new Player()
            {
                Fname = "Max",
                Lname = "Headroom"
            };

            players.Add(p1);
            
            // login or create new player
            Console.WriteLine("Please enter your first name\n If you enter unique first and last name I will create new player");
            string userName = Console.ReadLine();
            string [] names = userName.Split(' ');


            Player p2 = new Player()
            {
                Fname = names[0],
                Lname = names[1]
            };
            players.Add(p2);

            Match match = new Match()
            {
                Player1 = p1,
                Player2 = p2
            };

            Round round = new Round();



            Console.WriteLine($"Welcome {p2.Fname}");

            // User will chose rock paper or scissors.
            static RPS getUserResponse()
            {
                RPS userChoice;
                bool userResponseParsed = false;
                Console.WriteLine($"Please choose Rock, Paper, of Scissors by typing 1, 2, or 3 and hitting enter.");
                Console.WriteLine("\t1. Rock \n\t2. Paper \n\t3. Scissors ");
                string userResponse = Console.ReadLine();   
                userResponseParsed = RPS.TryParse(userResponse, out userChoice);
                
                if(!userResponseParsed || (int)userChoice > 3 || (int)userChoice < 1)
                {
                    Console.Clear();
                    Console.WriteLine("Please enter a valid response\n");
                    return getUserResponse();
                }

                return userChoice;
            }

            static int getCompResponse()
            {
                Random randomNumber = new Random();
                int computerChoice = randomNumber.Next(1,4);
                return computerChoice;
            }

            round.Player1Choice = (RPS)getCompResponse();
            round.Player2Choice = getUserResponse();

            Console.WriteLine($"You used {round.Player1Choice}"); 
                Console.WriteLine($"Computer used {round.Player2Choice}"); 

                switch ((int)round.Player1Choice - (int)round.Player2Choice)
                {
                    case 0:
                        Console.WriteLine("Tied.");
                        match.roundWinner();
                        break;
                    case -2:
                    case 1:
                        Console.WriteLine("You win");
                        round.WinningPlayer = p2;
                        match.roundWinner(p2);
                        Console.WriteLine(round.WinningPlayer.Fname);
                        Console.WriteLine(round);
                        break;
                    default:
                        Console.WriteLine("The computer wins.");
                        round.WinningPlayer = p1;
                        Console.WriteLine(round.WinningPlayer.Fname);
                        Console.WriteLine(round);
                        match.roundWinner(p1);
                        break;
                }
            

            // static void findWinner( RPS uinput, int cinput)
            // {
            //     RPS userChoice = (RPS)uinput;
            //     RPS compChoice = (RPS)cinput;
            //     Console.WriteLine($"You used {userChoice}"); 
            //     Console.WriteLine($"Computer used {compChoice}"); 

            //     switch ((int)uinput - cinput)
            //     {
            //         case 0:
            //             Console.WriteLine("Tied.");
            //             match.roundWinner();
            //             break;
            //         case -2:
            //         case 1:
            //             Console.WriteLine("You win");
            //             round.WinningPlayer(p2);
            //             break;
            //         default:
            //             Console.WriteLine("The computer wins.");
            //             round.WinningPlayer(p1);
            //             break;
            //     }
            // }

            // static void playGame()
            // {
            //     RPS userInput = getUserResponse();
            //     int compInput = getCompResponse();
            //     findWinner(userInput, compInput);
            // }
 
            // playGame();

        }
    }


}

// Console.WriteLine("This is The Official Batch Rock-Paper-Scissors Game");
        
            // Console.WriteLine(userResponse);

            // try
            // {
            //     int userResponseInt = int.Parse(userResponse);
            // }
            // catch(FormatException e)
            // {
            //     Console.WriteLine("There was a problem with the input");
            // }

            // int userChoice = 0;
            // bool userResponseParsed = false;

            // do
            // {
            //     Console.WriteLine("Please choose Rock, Paper, of Scissors by typing 1, 2, or 3 and hitting enter.");
            //     Console.WriteLine("\t1. Rock \n\t2. Paper \n\t3. Scissors ");

            //     string userResponse = Console.ReadLine();                       

            //     userResponseParsed = int.TryParse(userResponse, out userChoice);
            //     if(!userResponseParsed || userChoice > 3 || userChoice < 1)
            //     {
            //         Console.WriteLine("Your response is invalid");
            //     }
            // } while(!userResponseParsed || userChoice > 3 || userChoice < 1);

            // Console.WriteLine($"Congrats you entered a number. It is {userChoice}.");
            
            // creating a random number for the computer and assinging it
            // Random randomNumber = new Random();
            // int computerChoice = randomNumber.Next(1,4);
            // Console.WriteLine(computerChoice);


            // if((userChoice == 2 && computerChoice == 1) ||
            // (userChoice == 3 && computerChoice == 2) ||
            // (userChoice == 1 && computerChoice == 3))  
            // {
            //     Console.WriteLine("Congrats you won!");
            // } else if(userChoice == computerChoice)
            // {
            //     Console.WriteLine("This game was a tie");
            // } else {
            //     Console.WriteLine("The computer won");
            // }
