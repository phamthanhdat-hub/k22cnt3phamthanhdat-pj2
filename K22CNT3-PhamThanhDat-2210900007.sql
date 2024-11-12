CREATE DATABASE K22CNT3_PhamThanhDat_Pj2;
GO
USE K22CNT3_PhamThanhDat_Pj2;
GO 

CREATE TABLE QuanTri(
  Id INT PRIMARY KEY,
  TaiKhoan VARCHAR(25) UNIQUE,
  Matkhau varchar(255),
  TrangThai TINYINT
);
GO
CREATE TABLE LoaiSanPham(
  Id INT PRIMARY KEY IDENTITY,
  MaLoai VARCHAR(255) UNIQUE,
  TenLoai NVARCHAR(255),
  TrangThai TINYINT
);
GO

CREATE TABLE SanPham(
  Id INT PRIMARY KEY IDENTITY,
  MaSanPham VARCHAR(255) UNIQUE,
  TenSanPham NVARCHAR(255),
  HinhAnh NVARCHAR(255),
  SoLuong INT,
  DonGia FLOAT,
  MaLoai INT REFERENCES LoaiSanPham(Id),
  TrangThai TINYINT
);
GO

CREATE TABLE KhachHang(
  Id INT PRIMARY KEY IDENTITY,
  MaKhachHang VARCHAR(255) UNIQUE,
  HoTenKhachHang NVARCHAR(255), 
  Email VARCHAR(255) UNIQUE,
  MatKhau VARCHAR(255),
  DienThoai VARCHAR(10) UNIQUE,
  DiaChi NVARCHAR(255),
  NgayDangKy DATETIME,
  TrangThai TINYINT
);
GO
CREATE TABLE HoaDon(
  Id INT PRIMARY KEY IDENTITY,
  MaHoaDon VARCHAR(255) UNIQUE,
  MaKhachHang INT REFERENCES KhachHang(Id),
  NgayHoaDon DATETIME,
  NgayNhan DATETIME,
  HoTenKhachHang NVARCHAR(255),
  Email VARCHAR(255),
  DienThoai VARCHAR(255),
  DiaChi NVARCHAR(255),
  TongTriGia FLOAT,
  TrangThai TINYINT
);
GO
CREATE TABLE CTHoaDon(
  Id INT PRIMARY KEY IDENTITY,
  HoaDonID INT REFERENCES HoaDon(Id),
  SanPhamID INT REFERENCES SanPham(Id),
  SoLuongMua INT,
  DonGiaMua FLOAT,
  ThanhTien FLOAT,
  TrangThai TINYINT
);
INSERT INTO QuanTri (Id, TaiKhoan,  Matkhau, TrangThai)
VALUES
(1, 'admin', 'password123', 1),
(2, 'manager', 'password456', 1);

INSERT INTO LoaiSanPham (MaLoai, TenLoai, TrangThai)
VALUES
('SP001', 'Áo Thun', 1),
('SP002', 'Quần Jean', 1),
('SP003', 'Giày', 1);
INSERT INTO SanPham (MaSanPham, TenSanPham, HinhAnh, SoLuong, DonGia, MaLoai, TrangThai)
VALUES
('SP001001', 'Áo Thun ', 'aothun.jpg', 100, 200000, 1, 1),
('SP001002', 'Áo Vest', 'aovest.jpg', 50, 250000, 1, 1),
('SP002001', 'Quần Âu', 'quanau.jpg', 30, 350000, 2, 1),
('SP003001', 'Quần Đùi', 'quandui.jpg', 70, 500000, 3, 1);
('SP004005', 'Đầm Ngắn ', 'dam.jpg', 100, 200000, 1, 1),
('SP005002', 'Áo Sơ Mi', 'somi.jpg', 50, 250000, 1, 1),
('SP006001', 'Giày Nike', 'giay.jpg', 30, 350000, 2, 1),
('SP007001', 'Áo Lỡ Tay', 'ngantay.jpg', 70, 500000, 3, 1);
INSERT INTO KhachHang (MaKhachHang, HoTenKhachHang, Email, MatKhau, DienThoai, DiaChi, NgayDangKy, TrangThai)
VALUES
('KH001', 'Pham Thanh Dat', 'thanhdat@gmail.com', 'password789', '0912345678', 'Nam Định ', '2024-11-01', 1),
('KH002', 'Tran Thi B', 'transang@gmail.com', 'password101', '0987654321', 'Hà Nội', '2024-11-05', 1);
INSERT INTO CTHoaDon (HoaDonID, SanPhamID, SoLuongMua, DonGiaMua, ThanhTien, TrangThai)
VALUES
(1, 1, 2, 200000, 400000, 1),  
(1, 3, 1, 350000, 350000, 1),  
(2, 2, 3, 250000, 750000, 1);
select * from SanPham
update SanPham set HinhAnh = 'cotent/'+ HinhAnh