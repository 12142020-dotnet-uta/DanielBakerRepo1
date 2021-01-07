using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModelLayer;

namespace RepositoryLayer
{
    public class Repository
    {
        private readonly DbContextClass _context;

        DbSet<Player> players; 
        DbSet<Match> matches; 
        DbSet<Round> rounds; 
      
        public Repository(DbContextClass dbContextClass)
        {
            _context = dbContextClass;
            players = _context.players;
            matches = _context.matches;
            rounds = _context.rounds;
        }

        public Player LoginPlayer(Player player)
        {
            Player p1 = players.FirstOrDefault(x => x.Fname == player.Fname && x.Lname == player.Lname);

            if (p1 == null)
            {
                p1 = new Player()
                {
                    Fname = player.Fname,
                    Lname = player.Lname
                };
                players.Add(p1);
                _context.SaveChanges();
                Player p2 = players.FirstOrDefault(x => x.PlayerId == p1.PlayerId);
                return p2;
            }

            return p1;
        }

        public Player GetPlayerById(Guid id)
        {
            Player player = players.FirstOrDefault(x => x.PlayerId == id);
            return player;
        }

        public Player EditPlayer(Player p)
        {
            Player p1 = GetPlayerById(p.PlayerId);
            p1.Fname = p.Fname;
            p1.Lname = p.Lname;
            p1.numLosses = p.numLosses;
            p1.numWins = p.numWins;
            p1.ByteArrayImage = p.ByteArrayImage;

            _context.SaveChanges();

            Player p2 = GetPlayerById(p.PlayerId);

            return p2;
        }


    }
}
