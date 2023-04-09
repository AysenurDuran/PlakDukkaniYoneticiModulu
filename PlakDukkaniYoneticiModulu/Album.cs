using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlakDukkaniYoneticiModulu
{
    public class Album
    {
        [Key]
        public int AlbumId { get; set; }
        public string AlbumAdi { get; set; }
        public string AlbumSanatcisi { get; set; }
        public DateTime AlbumCikisTarihi { get; set; }
        public decimal AlbumFiyati { get; set; }
        public double İndirimOrani { get; set; }
        public string SatisDevami { get; set; }
    }
}
