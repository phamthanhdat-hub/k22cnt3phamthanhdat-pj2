using K22CNT3_PhamThanhDat_2210900007_Pj2.Bussiness;
using K22CNT3_PhamThanhDat_2210900007_Pj2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace K22CNT3_PhamThanhDat_2210900007_Pj2.Controllers
{
    public class PTDCartController : Controller
    {
        private const string PTDCartSessionKey = "PTDCartSessionKey";
       K22CNT3_PhamThanhDat_Pj2Entities dbEntities = new K22CNT3_PhamThanhDat_Pj2Entities();

        // GET: PTD_Cart
        private PTD_ShoppingCart GetCart()
        {
            var cart = Session[PTDCartSessionKey] as PTD_ShoppingCart;
            if (cart == null)
            {
                cart = new PTD_ShoppingCart();
                Session[PTDCartSessionKey] = cart;
            }

            return cart;
        }
        // ADD thêm một sản phẩm vào giỏ hàng
        public ActionResult AddToCart(int ID, string TenSanPham, string HinhAnh, int SoLuongMua, float DonGiaMua)
        {
            var cart = GetCart();
            var item = new PTD_CartItem
            {
                ID = ID,
                TenSanPham = TenSanPham,
                HinhAnh = HinhAnh,
                SoLuongMua = SoLuongMua,
                DonGiaMua = DonGiaMua,
                ThanhTien = SoLuongMua * DonGiaMua
            };
            cart.AddToCart(item);
            return RedirectToAction("Index");
        }
        // GET: PTDCart - Hiển thị các sản phẩm trong giỏ hàng
        public ActionResult Index()
        {
            var cart = GetCart();
            return View(cart.Items);
        }
        // Trang thông tin thnah toán 
        public ActionResult ThongTinThanhToan()
        {
            var cart = GetCart();
            ViewBag.TongTriGia = cart.GetTongThanhTien();
            DateTime dt = DateTime.Now;
            var MaHoaDon = "DH-" + dt.ToString("yyyyMMdd-HHmmss");
            ViewBag.MaHoaDon = MaHoaDon;
            return View(cart.Items);
        }
        // Cập nhật và thanh toán
        public ActionResult ThanhToan(FormCollection form)
        {
            var cart = GetCart();
            // Lấy các thông tin trên form để cập nhật bảng hóa đơn
            var HoTenKhachHang = form["HoTenKhachHang"];
            var Email = form["Email"];
            var DienThoai = form["DienThoai"];
            var DiaChi = form["DiaChi"];

            // Thông tin đơn hàng
            DateTime dt = DateTime.Now;
            var MaHoaDon = "DH-" + dt.ToString("yyyyMMdd-HHmmss");
            var NgayNhan = form["NgayNhan"];
            var TriGia = cart.GetTongThanhTien();

            //Thêm mới vào bảng hóa đơn
            var hoaDon = new HoaDon();
            hoaDon.MaHoaDon = MaHoaDon;
            hoaDon.NgayHoaDon = dt;
            hoaDon.NgayNhan = DateTime.Parse(NgayNhan);
            hoaDon.TongTriGia = TriGia;
            hoaDon.HoTenKhachHang = HoTenKhachHang;
            hoaDon.Email = Email;
            hoaDon.DienThoai = DienThoai;
            hoaDon.DiaChi = DiaChi;

            dbEntities.HoaDon.Add(hoaDon);
            dbEntities.SaveChanges();

            //lấy id hóa đơn vừa thêm
            int hoaDonId = dbEntities.HoaDon.Max(x => x.Id);

            //Thêm vào chi tiết hóa đơn
            foreach (var item in cart.Items)
            {
                CTHoaDon ct = new CTHoaDon();
                ct.HoaDonID = hoaDonId;
                ct.SanPhamID = item.ID;
                ct.SoLuongMua = item.SoLuongMua;
                ct.DonGiaMua = item.DonGiaMua;
                ct.ThanhTien = item.ThanhTien;

                dbEntities.CTHoaDon.Add(ct);
                dbEntities.SaveChanges();
            }


            return RedirectToAction("CamOn");
        }
        public ActionResult CamOn()
        {
            return View();
        }
        // Cập nhật giỏ hàng
        public ActionResult UpdateFromCart(FormCollection form)
        {
            var cart = GetCart();
            var ids = form["ID"].Split(',');
            var qtys = form["SoLuongMua"].Split(',');
            for (int i = 0; i < ids.Length; i++)
            {
                int id = int.Parse(ids[i]);
                int qty = int.Parse(qtys[i]);
                cart.UpdateFromCart(id, qty);
            }
            return RedirectToAction("Index");
        }
        // Cap nhat item in cart
        public ActionResult UpdateItemCart(int id, int qty)
        {
            var cart = GetCart();
            cart.UpdateFromCart(id, qty);
            return RedirectToAction("Index");
        }

        // Xoa san pham trong gio hang
        public ActionResult DeleteItemCart(int id)
        {
            var cart = GetCart();
            cart.RemoveCartItem(id);
            return RedirectToAction("Index");
        }

    }
}