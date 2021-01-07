using System;
using ModelLayer;
using ModelLayer.ViewModels;
using RepositoryLayer;

namespace BusinessLogicLayer
{
    public class BusinessLogicClass
    {
        private readonly Repository _repo;
        private readonly MapperClass _mapper;
        public BusinessLogicClass(Repository repo, MapperClass mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// Takes a LoginPlayerViewModel instance and returns a PlayerViewModel
        /// </summary>
        /// <returns></returns>
        public PlayerViewModel LoginPlayer(LoginPlayerViewModel loginPlayerViewModel)
        {
            Player player = new Player()
            {
                Fname = loginPlayerViewModel.Fname,
                Lname = loginPlayerViewModel.Lname
            };

            Player player1 = _repo.LoginPlayer(player);

            PlayerViewModel playerViewModel = _mapper.ConvertPlayerToPlayerViewModel(player1);
            
            return playerViewModel;
        }

        public PlayerViewModel EditPlayer(Guid playerId)
        {
            Player player = _repo.GetPlayerById(playerId);

            PlayerViewModel playerView = _mapper.ConvertPlayerToPlayerViewModel(player);

            return playerView;
        }

        public PlayerViewModel EditedPlayer(PlayerViewModel playerViewModel)
        {
            Player player = _repo.GetPlayerById(playerViewModel.PlayerId);

            player.Fname = playerViewModel.Fname;
            player.Lname = playerViewModel.Lname;
            player.numLosses = playerViewModel.numLosses;
            player.numWins = playerViewModel.numWins;
            player.ByteArrayImage = _mapper.ConvertIformFileToByteArray(playerViewModel.IformFileImage);

            Player editedPlayer = _repo.EditPlayer(player);

            PlayerViewModel editedPlayerViewModel = _mapper.ConvertPlayerToPlayerViewModel(editedPlayer);

            return editedPlayerViewModel;
            
        }
    }
}
