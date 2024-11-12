using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace K22CNT3_PhamThanhDat_2210900007_Pj2.Models
{
    public class PTD_CartItem
    {
        public int ID { get; set; }
        public string TenSanPham { get; set; }
        public string HinhAnh { get; set; }
        public int SoLuongMua { get; set; }
        public float DonGiaMua { get; set; }
        public float ThanhTien { get; set; }
    }
}