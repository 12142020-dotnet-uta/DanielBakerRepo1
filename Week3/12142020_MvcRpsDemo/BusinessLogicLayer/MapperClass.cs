using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ModelLayer;
using ModelLayer.ViewModels;

namespace BusinessLogicLayer
{
    public class MapperClass
    {
        public PlayerViewModel ConvertPlayerToPlayerViewModel(Player player)
        {
            PlayerViewModel playerViewModel = new PlayerViewModel()
            {
                PlayerId = player.PlayerId,
                Fname = player.Fname,
                Lname = player.Lname,
                numLosses = player.numLosses,
                numWins = player.numWins,
                JpegStringImage = ConvertByteArrayToJpegString(player.ByteArrayImage)
            };
            return playerViewModel;
        }

        private string ConvertByteArrayToJpegString(byte[] byteImage)
        {
            if (byteImage != null)
            {
                string imageBase64Data = Convert.ToBase64String(byteImage, 0, byteImage.Length);
                string imageDataURL = string.Format($"data:image/jpg;based64,{imageBase64Data}");
                return imageDataURL;
            }
            else return null;
        }

        public byte[] ConvertIformFileToByteArray(IFormFile iformFile)
        {
            using (var ms = new MemoryStream())
            {
                if (ms.Length > 2097152)
                {
                    return null;
                }
                else
                {
                    byte[] a = ms.ToArray();
                    return a;
                }
            }
        }

    }
}
