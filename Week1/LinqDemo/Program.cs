using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Player> players = new List<Player>();

            for(int x = 0; x < 10; x++)
            {
                players.Add(new Player()
                {
                    Fname = "Test-" + x,
                    Lname = "" + (x - (x * 2))
                });
            }

            foreach(Player p in players){
                Console.WriteLine($"{p.Fname} {p.Lname}");
            }

            var result = players.Where(x => x.Fname == "Test-4").FirstOrDefault(); //ToList() would return a list if you want to see more than one

            if (result != null)
            {
                Console.WriteLine($"The players Fname is {result.Fname} and their last name is {result.Lname}");
            } else
            {
                throw new Exception("The player wasn't found");
            }        

            int count = players.Count;

            // .Sort can be useful

            players.Remove(result);

            result = players.Where(x => x.Fname == "Test-4").FirstOrDefault(); //ToList() would return a list if you want to see more than one

            if (result != null)
            {
                Console.WriteLine($"The players Fname is {result.Fname} and their last name is {result.Lname}");
            } else
            {
                throw new Exception("The player wasn't found");
            }  





        }
    }
}
