using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncurtaLinks.Core.Models
{
    public static class LinkEncurtadoConfigs
    {
        public const string CaracteresPossiveis = 
            "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        public const int MaxCaracteres = 7;
        public const int SegundosValido = 300;
        public const string UrlPadrao = "http://localhost:5229/";
    }
}
