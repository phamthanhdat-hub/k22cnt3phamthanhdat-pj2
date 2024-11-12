using K22CNT3_PhamThanhDat_2210900007_Pj2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace K22CNT3_PhamThanhDat_2210900007_Pj2.Bussiness
{
    public class PTD_ShoppingCart
    {
        public List<PTD_CartItem> Items { get; set; }
        public PTD_ShoppingCart()
        {
            Items = new List<PTD_CartItem>();
        }
        //chức năng thêm sản phẩm vào giỏ hàng 
        public void AddToCart(PTD_CartItem item)
        {
            var existingItem = Items.FirstOrDefault(x => x.ID == item.ID);
            if (existingItem != null)
            {
                existingItem.SoLuongMua += item.SoLuongMua;
            }
            else
            {
                Items.Add(item);
            }
        }
        // Xóa sản phấm trong gió hàng
        public void RemoveCartItem(int id)
        {
            var itemToRemove = Items.FirstOrDefault(x => x.ID == id);
            if (itemToRemove != null)
            {
                Items.Remove(itemToRemove);
            }

        }

        // Tính tổng trị giá của hóa đơn
        public float GetTongThanhTien()
        {
            return Items.Sum(x => x.SoLuongMua * x.DonGiaMua);
        }

        // cập nhật giỏ hàng (Shopping Cart)
        public void UpdateFromCart(int id, int qty)
        {
            var existingItem = Items.FirstOrDefault(x => x.ID == id);
            if (existingItem != null)
            {
                existingItem.SoLuongMua = qty;
            }
        }
    }
}