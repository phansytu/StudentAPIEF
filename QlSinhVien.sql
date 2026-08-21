IF EXISTS (SELECT *
           FROM   sys.databases
           WHERE  name = 'QlSinhVien')
    DROP DATABASE QlSinhVien;


GO
CREATE DATABASE QlSinhVien;


GO
USE QlSinhVien;


GO
CREATE TABLE BoMon (
    id     INT           IDENTITY (1, 1) PRIMARY KEY,
    maBM   VARCHAR (10)  UNIQUE NOT NULL,
    tenMon NVARCHAR (50) NOT NULL
);


GO
CREATE TABLE LopHoc (
    id          INT           IDENTITY (1, 1) PRIMARY KEY,
    maLop       VARCHAR (10)  UNIQUE NOT NULL,
    tenLop      VARCHAR (10)  NOT NULL,
    chuyenNganh NVARCHAR (50),
    boMonId     INT          ,
    CONSTRAINT FK_LopHoc_BoMon FOREIGN KEY (boMonId) REFERENCES BoMon (id) ON DELETE SET NULL
);


GO
CREATE TABLE SinhVien (
    id       INT           IDENTITY (1, 1) PRIMARY KEY,
    msv      VARCHAR (10)  UNIQUE NOT NULL,
    hoTen    NVARCHAR (50) NOT NULL,
    gioiTinh BIT          , -- 1: Nam, 0: Nữ
    ngaySinh DATE         ,
    email    VARCHAR (100),
    diemTb   FLOAT        ,
    lopHocId INT          ,
    CONSTRAINT FK_SinhVien_LopHoc FOREIGN KEY (lopHocId) REFERENCES LopHoc (id) ON DELETE SET NULL
);


GO
INSERT  INTO dbo.BoMon (
    maBM,
    tenMon
)
VALUES                ('BM001', N'Toán giải tích'),
('BM002', N'Tin văn phòng'),
('BM003', N'Tin cơ sở'),
('BM004', N'Học máy');


GO
INSERT  INTO dbo.LopHoc (
    maLop,
    tenLop,
    chuyenNganh,
    boMonId
)
VALUES                 ('L0001', 'P01', N'CNTT 1', (SELECT id
                                                    FROM   BoMon
                                                    WHERE  maBM = 'BM001')),
('L0002', 'P02', N'CNTT 2', (SELECT id
                             FROM   BoMon
                             WHERE  maBM = 'BM001')),
('L0003', 'P03', N'CNTT 3', (SELECT id
                             FROM   BoMon
                             WHERE  maBM = 'BM002')),
('L0004', 'P04', N'CNTT 4', (SELECT id
                             FROM   BoMon
                             WHERE  maBM = 'BM003')),
('L0005', 'P05', N'Khoa học dữ liệu', (SELECT id
                                       FROM   BoMon
                                       WHERE  maBM = 'BM004'));


GO
INSERT  INTO dbo.SinhVien (
    msv,
    hoTen,
    gioiTinh,
    ngaySinh,
    email,
    diemTb,
    lopHocId
)
VALUES                   ('MSV01', N'Phan Sỹ Tú', 1, '2005-08-24', 'phansytu02@gmail.com', 8.55, (SELECT id
                                                                                                  FROM   LopHoc
                                                                                                  WHERE  maLop = 'L0001')),
('MSV02', N'Nguyễn Văn A', 0, '2005-08-24', 'phansytu03@gmail.com', 8.65, (SELECT id
                                                                           FROM   LopHoc
                                                                           WHERE  maLop = 'L0002')),
('MSV03', N'Nguyễn Trang Lăng', 0, '2005-05-20', NULL, NULL, NULL),
('MSV04', N'Lê Văn C', 1, '2005-03-20', 'levanc@gmail.com', 6.95, (SELECT id
                                                                   FROM   LopHoc
                                                                   WHERE  maLop = 'L0004')),
('MSV05', N'Phạm Thị D', 0, '2005-07-12', 'phamthid@gmail.com', 9.10, (SELECT id
                                                                       FROM   LopHoc
                                                                       WHERE  maLop = 'L0005')),
('MSV06', N'Hoàng Văn E', 1, '2005-01-30', 'hoangvane@gmail.com', 7.45, (SELECT id
                                                                         FROM   LopHoc
                                                                         WHERE  maLop = 'L0001')),
('MSV07', N'Võ Thị F', 0, '2005-11-05', 'vothif@gmail.com', 8.25, (SELECT id
                                                                   FROM   LopHoc
                                                                   WHERE  maLop = 'L0002')),
('MSV08', N'Đặng Văn G', 1, '2005-06-18', 'dangvang@gmail.com', 6.75, (SELECT id
                                                                       FROM   LopHoc
                                                                       WHERE  maLop = 'L0003')),
('MSV09', N'Bùi Thị H', 0, '2005-09-22', 'buithih@gmail.com', 9.35, (SELECT id
                                                                     FROM   LopHoc
                                                                     WHERE  maLop = 'L0004')),
('MSV10', N'Nguyễn Văn I', 1, '2005-04-10', 'nguyenvani@gmail.com', 7.90, (SELECT id
                                                                           FROM   LopHoc
                                                                           WHERE  maLop = 'L0005')),
('MSV11', N'Phạm Văn K', 1, '2005-02-14', 'phamvank@gmail.com', 8.05, (SELECT id
                                                                       FROM   LopHoc
                                                                       WHERE  maLop = 'L0001')),
('MSV12', N'Nguyễn Thị L', 0, '2005-12-01', 'nguyenthil@gmail.com', 8.75, (SELECT id
                                                                           FROM   LopHoc
                                                                           WHERE  maLop = 'L0002')),
('MSV13', N'Trần Văn M', 1, '2005-05-25', 'tranvanm@gmail.com', 7.25, (SELECT id
                                                                       FROM   LopHoc
                                                                       WHERE  maLop = 'L0003')),
('MSV14', N'Lê Thị N', 0, '2005-08-08', 'lethin@gmail.com', 9.00, (SELECT id
                                                                   FROM   LopHoc
                                                                   WHERE  maLop = 'L0004')),
('MSV15', N'Hoàng Thị O', 0, '2005-10-28', 'hoangthio@gmail.com', 8.45, (SELECT id
                                                                         FROM   LopHoc
                                                                         WHERE  maLop = 'L0005'));


GO
UPDATE dbo.SinhVien
SET    email    = '123@gmail.com',
       diemTb   = 9.08,
       lopHocId = (SELECT id
                   FROM   LopHoc
                   WHERE  maLop = 'L0001')
WHERE  msv = 'MSV03';


GO
--  SP thêm mới
CREATE OR ALTER PROCEDURE sp_SinhVien_Create
@msv VARCHAR (10), @hoTen NVARCHAR (50), @gioiTinh BIT, @ngaySinh DATE, @email VARCHAR (100)=NULL, @diemTb FLOAT=NULL, @lopHocId INT=NULL
AS
BEGIN
    --  để ngăn SQL Server gửi thông báo về số lượng dòng (hàng) bị ảnh hưởng bởi câu lệnh T-SQL (như INSERT, UPDATE, DELETE) về cho client.
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1
               FROM   dbo.SinhVien
               WHERE  msv = @msv)
        BEGIN
            RAISERROR (N'Mã sinh viên này đã tồn tại!', 16, 1);
            RETURN;
        END
    IF @lopHocId IS NOT NULL
       AND NOT EXISTS (SELECT 1
                       FROM   dbo.LopHoc
                       WHERE  id = @lopHocId)
        BEGIN
            RAISERROR (N'ID Lớp học không tồn tại!', 16, 1);
            RETURN;
        END
    INSERT  INTO dbo.SinhVien (
        msv,
        hoTen,
        gioiTinh,
        ngaySinh,
        email,
        diemTb,
        lopHocId
    )
    VALUES                   (@msv, @hoTen, @gioiTinh, @ngaySinh, @email, @diemTb, @lopHocId);
END


GO
--SP lấy chi tiết sinh viên theo id
CREATE OR ALTER PROCEDURE sp_SinhVien_GetById
@id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT sv.id,
           sv.msv,
           sv.hoTen,
           sv.gioiTinh,
           sv.ngaySinh,
           sv.email,
           sv.diemTb,
           sv.lopHocId,
           lh.tenLop,
           lh.chuyenNganh,
           bm.id AS boMonId,
           bm.tenMon
    FROM   dbo.SinhVien AS sv
           LEFT OUTER JOIN
           dbo.LopHoc AS lh
           ON sv.lopHocId = lh.id
           LEFT OUTER JOIN
           dbo.BoMon AS bm
           ON lh.boMonId = bm.id
    WHERE  sv.id = @id;
END


GO
-- cập nhật thông tin sinh viên
CREATE OR ALTER PROCEDURE sp_SinhVien_Update
@id INT, @hoTen NVARCHAR (50), @gioiTinh BIT, @ngaySinh DATE, @email VARCHAR (100), @diemTb FLOAT, @lopHocId INT
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1
                   FROM   dbo.SinhVien
                   WHERE  id = @id)
        BEGIN
            RAISERROR (N'Không tìm thấy sinh viên cần cập nhật!', 16, 1);
            RETURN;
        END
    IF @lopHocId IS NOT NULL
       AND NOT EXISTS (SELECT 1
                       FROM   dbo.LopHoc
                       WHERE  id = @lopHocId)
        BEGIN
            RAISERROR (N'Lớp học không tồn tại!', 16, 1);
            RETURN;
        END
    UPDATE dbo.SinhVien
    SET    hoTen    = @hoTen,
           gioiTinh = @gioiTinh,
           ngaySinh = @ngaySinh,
           email    = @email,
           diemTb   = @diemTb,
           lopHocId = @lopHocId
    WHERE  id = @id;
END


GO
-- Xóa sinh viên
CREATE OR ALTER PROCEDURE sp_SinhVien_Delete
@id INT
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1
                   FROM   dbo.SinhVien
                   WHERE  id = @id)
        BEGIN
            RAISERROR (N'Không tìm thấy sinh viên để xóa!', 16, 1);
            RETURN;
        END
    DELETE dbo.SinhVien
    WHERE  id = @id;
END


GO
--Thêm bộ lọc theo Lớp, Bộ môn, Điểm số ngoài Từ khóa tìm kiếm
CREATE OR ALTER PROCEDURE sp_SinhVien_GetPagedAdvanced
@PageIndex INT=1, @PageSize INT=10, @Keyword NVARCHAR (50)=NULL, @LopHocId INT=NULL, @BoMonId INT=NULL, @MinDiem FLOAT=NULL, @MaxDiem FLOAT=NULL, @TotalRecords INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    -- Đếm tổng bản ghi thỏa mãn bộ lọc
    SELECT @TotalRecords = COUNT(*)
    FROM   dbo.SinhVien AS sv
           LEFT OUTER JOIN
           dbo.LopHoc AS lh
           ON sv.lopHocId = lh.id
    WHERE  (@Keyword IS NULL
            OR sv.hoTen LIKE N'%' + @Keyword + '%'
            OR sv.msv LIKE '%' + @Keyword + '%')
           AND (@LopHocId IS NULL
                OR sv.lopHocId = @LopHocId)
           AND (@BoMonId IS NULL
                OR lh.boMonId = @BoMonId)
           AND (@MinDiem IS NULL
                OR sv.diemTb >= @MinDiem)
           AND (@MaxDiem IS NULL
                OR sv.diemTb <= @MaxDiem);
    -- Trả về dữ liệu trang hiện tại
    SELECT   sv.id,
             sv.msv,
             sv.hoTen,
             sv.gioiTinh,
             sv.ngaySinh,
             sv.email,
             sv.diemTb,
             ISNULL(lh.tenLop, N'Chưa xếp lớp') AS tenLop,
             ISNULL(bm.tenMon, N'N/A') AS tenMon
    FROM     dbo.SinhVien AS sv
             LEFT OUTER JOIN
             dbo.LopHoc AS lh
             ON sv.lopHocId = lh.id
             LEFT OUTER JOIN
             dbo.BoMon AS bm
             ON lh.boMonId = bm.id
    WHERE    (@Keyword IS NULL
              OR sv.hoTen LIKE N'%' + @Keyword + '%'
              OR sv.msv LIKE '%' + @Keyword + '%')
             AND (@LopHocId IS NULL
                  OR sv.lopHocId = @LopHocId)
             AND (@BoMonId IS NULL
                  OR lh.boMonId = @BoMonId)
             AND (@MinDiem IS NULL
                  OR sv.diemTb >= @MinDiem)
             AND (@MaxDiem IS NULL
                  OR sv.diemTb <= @MaxDiem)
    ORDER BY sv.id DESC
    OFFSET (@PageIndex - 1) * @PageSize ROWS FETCH NEXT @PageSize ROWS ONLY;
END


GO
--Lấy dữ liệu Tổng quan cho báo cáo
CREATE OR ALTER PROCEDURE sp_Dashboard_GetSummaryStats
AS
BEGIN
    SET NOCOUNT ON;
    SELECT (SELECT COUNT(*)
            FROM   dbo.SinhVien) AS TongSoSinhVien,
           (SELECT COUNT(*)
            FROM   dbo.LopHoc) AS TongSoLop,
           (SELECT COUNT(*)
            FROM   dbo.BoMon) AS TongSoBoMon,
           (SELECT ISNULL(ROUND(AVG(diemTb), 2), 0)
            FROM   dbo.SinhVien) AS DiemTrungBinhToanTruong,
           (SELECT COUNT(*)
            FROM   dbo.SinhVien
            WHERE  diemTb >= 8.0) AS SoSinhVienGioi;
END


GO
--VIEW BÁO CÁO CHI TIẾT SINH VIÊN
CREATE OR ALTER VIEW dbo.vw_BaoCao_ChiTietSinhVien
AS
SELECT sv.id AS SinhVienId,
       sv.msv AS MaSinhVien,
       sv.hoTen AS HoTen,
       sv.gioiTinh AS GioiTinh,
       CASE WHEN sv.gioiTinh = 1 THEN N'Nam' ELSE N'Nữ' END AS GioiTinhText,
       sv.ngaySinh AS NgaySinh,
       DATEDIFF(YEAR, sv.ngaySinh, GETDATE()) - CASE WHEN DATEADD(YEAR, DATEDIFF(YEAR, sv.ngaySinh, GETDATE()), sv.ngaySinh) > GETDATE() THEN 1 ELSE 0 END AS Tuoi,
       ISNULL(sv.email, N'N/A') AS Email,
       ISNULL(sv.diemTb, 0) AS DiemTB,
       CASE WHEN sv.diemTb >= 9.0 THEN N'Xuất sắc' WHEN sv.diemTb >= 8.0 THEN N'Giỏi' WHEN sv.diemTb >= 6.5 THEN N'Khá' WHEN sv.diemTb >= 5.0 THEN N'Trung bình' WHEN sv.diemTb IS NOT NULL THEN N'Yếu' ELSE N'Chưa có điểm' END AS XepLoai,
       lh.id AS LopHocId,
       ISNULL(lh.maLop, N'N/A') AS MaLop,
       ISNULL(lh.tenLop, N'Chưa xếp lớp') AS TenLop,
       ISNULL(lh.chuyenNganh, N'N/A') AS ChuyenNganh,
       bm.id AS BoMonId,
       ISNULL(bm.tenMon, N'N/A') AS TenBoMon
FROM   dbo.SinhVien AS sv
       LEFT OUTER JOIN
       dbo.LopHoc AS lh
       ON sv.lopHocId = lh.id
       LEFT OUTER JOIN
       dbo.BoMon AS bm
       ON lh.boMonId = bm.id;


GO
-- VIEW BÁO CÁO THỐNG KÊ THEO LỚP
CREATE OR ALTER VIEW dbo.vw_BaoCao_ThongKeTheoLop
AS
SELECT   lh.id AS LopHocId,
         lh.maLop AS MaLop,
         lh.tenLop AS TenLop,
         ISNULL(lh.chuyenNganh, N'Chưa xác định') AS ChuyenNganh,
         ISNULL(bm.tenMon, N'Chưa phân môn') AS TenBoMon,
         COUNT(sv.id) AS TongSoSinhVien,
         SUM(CASE WHEN sv.gioiTinh = 1 THEN 1 ELSE 0 END) AS SoNam,
         SUM(CASE WHEN sv.gioiTinh = 0 THEN 1 ELSE 0 END) AS SoNu,
         ISNULL(ROUND(AVG(sv.diemTb), 2), 0) AS DiemTrungBinhLop,
         ISNULL(MAX(sv.diemTb), 0) AS DiemCaoNhat,
         ISNULL(MIN(sv.diemTb), 0) AS DiemThapNhat
FROM     dbo.LopHoc AS lh
         LEFT OUTER JOIN
         dbo.BoMon AS bm
         ON lh.boMonId = bm.id
         LEFT OUTER JOIN
         dbo.SinhVien AS sv
         ON lh.id = sv.lopHocId
GROUP BY lh.id, lh.maLop, lh.tenLop, lh.chuyenNganh, bm.tenMon;


GO
USE QlSinhVien;


GO
-- 1. Index trên Khóa Ngoại (Tối ưu JOIN giữa các bảng)
CREATE NONCLUSTERED INDEX IX_LopHoc_boMonId
    ON dbo.LopHoc(boMonId);


GO
CREATE NONCLUSTERED INDEX IX_SinhVien_lopHocId
    ON dbo.SinhVien(lopHocId);


GO
--Index cho chức năng Tìm kiếm tên & Lọc điểm (Tối ưu WHERE & ORDER BY)
CREATE NONCLUSTERED INDEX IX_SinhVien_hoTen
    ON dbo.SinhVien(hoTen);


GO
-- Index kèm INCLUDE giúp phủ truy vấn (Covering Index) cho báo cáo điểm số
CREATE NONCLUSTERED INDEX IX_SinhVien_diemTb
    ON dbo.SinhVien(diemTb DESC)
    INCLUDE(hoTen, msv, lopHocId);