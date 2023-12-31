-- Dữ liệu cho bảng EMPLOYEE
INSERT INTO EMPLOYEE (EMP_DISPLAYNAME, EMP_ADDRESS, EMP_PHONE, EMP_CCCD, EMP_SALARY, EMP_ROLE)
VALUES
  (N'Nguyễn Văn A', N'123 Đường ABC, Quận XYZ, Thành phố Hồ Chí Minh', N'0901234567', N'123456789', 5000000, N'Quản lý'),
  (N'Trần Thị B', N'456 Đường XYZ, Quận ABC, Thành phố Hà Nội', N'0987654321', N'987654321', 6000000, N'Nhân viên'),
  (N'Lê Văn C', N'789 Đường LMN, Quận DEF, Thành phố Đà Nẵng', N'0123456789', N'987654321', 7000000, N'Nhân viên'),
  (N'Hồ Thị D', N'321 Đường KLM, Quận GHI, Thành phố Hải Phòng', N'0123456789', N'123456789', 5500000, N'Tư vấn'),
  (N'Phạm Văn E', N'555 Đường UVW, Quận XYZ, Thành phố Cần Thơ', N'0909876543', N'123456789', 8000000, N'Bảo vệ');

-- Dữ liệu cho bảng CUSTOMER
INSERT INTO CUSTOMER (CUS_NAME, CUS_PHONE, CUS_EMAIL, CUS_SEX)
VALUES
  (N'Nguyễn Thị A', N'0901111111', N'kh1@example.com', N'Nữ'),
  (N'Trần Văn B', N'0912222222', N'kh2@example.com', N'Nam'),
  (N'Lê Thị C', N'0923333333', N'kh3@example.com', N'Nữ'),
  (N'Hồ Văn D', N'0934444444', N'kh4@example.com', N'Nam'),
  (N'Phạm Thị E', N'0945555555', N'kh5@example.com', N'Nữ');

-- Dữ liệu cho bảng PRODUCT
INSERT INTO PRODUCT (PRO_NAME, PRO_IMG, PRO_URL, PRICE)
VALUES
  (N'Kem dưỡng', 'https://www.shiseido.com.vn/on/demandware.static/-/Sites-itemmaster_shiseido/default/dw5abf09dc/images/products/18285/18285_S_01.jpg', 'https://www.shiseido.com.vn/vi/essential-energy-kem-d%C6%B0%E1%BB%A1ng-%E1%BA%A9m-essential-energy-hydrating-cream-1011828510.html', 100000),
  (N'Bông tẩy trang', 'https://ipek.vn/Uploads/images/anh_web1/bong-tay-trang-ipek-sieu-mem-min-100-30-mieng.jpg','https://ipek.vn/bong-tay-trang-ipek-tho-nhi-ky-100-30-mieng', 40000),
  (N'Nước tẩy trang', 'https://vonpreen.com/wp-content/uploads/2018/12/nuoc-tay-trang-Loreal-Micellar-Water-tuoi-mat.jpg', 'https://vonpreen.com/product/nuoc-tay-trang-tuoi-mat-loreal-micellar-water-400ml-lam-sach-sau-giu-am-va-duong-mem-da', 200000),
  (N'Kem chống nắng', 'https://storage.beautyfulls.com/uploads-1/avatar/product/1200x/2022/05/12/figure-1652344045386.png', 'https://www.beautyfulls.com/kem-chong-nang/cell-fusion-c-mau-vang', 150000),
  (N'Chấm mụn', 'https://bizweb.dktcdn.net/100/318/244/products/71wbvqxp6jl-sl1500.jpg?v=1637856857803', 'https://lingbeau.com/cham-mun-mario', 400000);

-- Dữ liệu cho bảng SERVICESS
INSERT INTO SERVICESS (SER_NAME, PRICE)
VALUES
  (N'Nặn mụn', 200000),
  (N'Triệt lông', 150000),
  (N'Tẩy tế bào chết', 300000),
  (N'Gội đầu', 50000),
  (N'Massage', 200000);

-- Dữ liệu cho bảng BOOKING
INSERT INTO BOOKING (C_ID, E_ID, S_ID, START_TIME, END_TIME, IS_EDITED)
VALUES
  (1, 1, 1, '2023-01-01 10:00:00', '2023-01-01 12:00:00', 0),
  (2, 2, 2, '2023-02-01 14:00:00', '2023-02-01 16:00:00', 1),
  (3, 3, 3, '2023-03-01 09:00:00', '2023-03-01 11:00:00', 0),
  (4, 4, 4, '2023-04-01 13:00:00', '2023-04-01 15:00:00', 1),
  (5, 5, 5, '2023-05-01 11:00:00', '2023-05-01 13:00:00', 0);

-- Dữ liệu cho bảng PAYMENT
INSERT INTO PAYMENT (C_ID, PRICE, DAYTIME)
VALUES
  (1, 850000, '2022-07-12 15:30:00'),
  (2, 580000, '2022-08-16 18:45:00'),
  (3, 500000, '2022-11-22 12:00:00'),
  (4, 350000, '2022-12-08 16:20:00'),
  (5, 400000, '2023-01-30 14:10:00'),
  (1, 500000, '2023-02-12 15:30:00'),
  (2, 300000, '2023-04-16 18:45:00'),
  (1, 850000, '2023-05-10 15:30:00'),
  (2, 580000, '2023-05-11 18:45:00'),
  (3, 500000, '2023-05-12 12:00:00'),
  (4, 350000, '2023-05-13 16:20:00'),
  (5, 400000, '2023-05-14 14:10:00'),
  (1, 500000, '2023-05-15 15:30:00'),
  (2, 300000, '2023-05-16 18:45:00');

  

-- Dữ liệu cho bảng PAYMENT_DETAIL_SERVICE
INSERT INTO PAYMENT_DETAIL_SERVICE (PMT_ID, S_ID, QUANTITY)
VALUES
  (1, 1, 1),
  (1, 4, 1),

  (2, 3, 1),

  (3, 5, 1),

  (4, 2, 1),
  (4, 4, 1),

  (5, 1, 1),

  (6, 3, 1),
  (7, 1, 1),
   (8, 1, 1),
  (8, 4, 1),

  (9, 3, 1),

  (10, 5, 1),

  (11, 2, 1),
  (11, 4, 1),

  (12, 1, 1),

  (13, 3, 1),
  (14, 1, 1);

-- Dữ liệu cho bảng PAYMENT_DETAIL_PRODUCT
INSERT INTO PAYMENT_DETAIL_PRODUCT (PMT_ID, P_ID, QUANTITY)
VALUES
  (1, 1, 2),
  (1, 5, 1),

  (2, 2, 2),
  (2, 3, 1),

  (3, 4, 2),

  (4, 4, 1),


  (5, 2, 1),
  (5, 3, 1),

  (6, 3, 1),
  (7, 1, 1),
    (8, 1, 2),
  (8, 5, 1),

  (9, 2, 2),
  (9, 3, 1),

  (10, 4, 2),

  (11, 4, 1),


  (12, 2, 1),
  (12, 3, 1),

  (13, 3, 1),
  (14, 1, 1);