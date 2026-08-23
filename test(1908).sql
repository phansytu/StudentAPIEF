USE QlSinhVien;


GO
EXECUTE sp_help SinhVien;

EXECUTE sp_help LopHoc;

EXECUTE sp_help BoMon;

SELECT lh.id,
       bm.id
FROM   LopHoc AS lh, BoMon AS bm;

SELECT *
FROM   SinhVien;

SELECT *
FROM   LopHoc;

SELECT *
FROM   BoMon;

ALTER TABLE SinhVien DROP CONSTRAINT FK__SinhVien__maLop__6477ECF3;

ALTER TABLE SinhVien DROP COLUMN maLop;

DROP INDEX SinhVien.IX_SinhVien_MSV_HoTen_MaLop;

ALTER TABLE LopHoc DROP CONSTRAINT FK__LopHoc__maBM__6754599E;

ALTER TABLE LopHoc DROP COLUMN maBM;

ALTER TABLE SinhVien
    ADD lopHocId INT FOREIGN KEY (lopHocId) REFERENCES LopHoc (id);

ALTER TABLE LopHoc
    ADD boMonId INT FOREIGN KEY (boMonId) REFERENCES BoMon (id);

-- alter table SinhVien 
-- SET IDENTITY_INSERT LopHoc OFF;
UPDATE sv
SET    sv.lopHocId = lh.id
FROM   SinhVien AS sv
       INNER JOIN
       LopHoc AS lh
       ON sv.maLop = lh.maLop;

UPDATE lh
SET    lh.boMonId = bm.id
FROM   LopHoc AS lh
       INNER JOIN
       BoMon AS bm
       ON lh.maBM = bm.maBM;

-- tao ma sinh vien tu dong dua tren id sinh vien
UPDATE SinhVien
SET    msv = 'MSV' + CASE WHEN Id < 1000 THEN RIGHT('000' + CAST (id AS VARCHAR (10)), 3) ELSE CAST (id AS VARCHAR (10)) END;

--tao ma lop tu dong du trne id cua ma lop
UPDATE LopHoc
SET    maLop = 'L' + CASE WHEN id < 1000 THEN RIGHT('000' + CAST (id AS VARCHAR (10)), 3) ELSE CAST (id AS VARCHAR (10)) END;

--tao ma lop tu dong du trne id cua ma lop
UPDATE BoMon
SET    maBM = 'BM' + CASE WHEN id < 1000 THEN RIGHT('000' + CAST (id AS VARCHAR (10)), 3) ELSE CAST (id AS VARCHAR (10)) END;

ALTER TABLE LopHoc DROP UQ__LopHoc__261ECAE2D78C5969;

EXECUTE sp_helpconstraint 'LopHoc';

ALTER TABLE LopHoc DROP COLUMN maLop;

ALTER TABLE LopHoc
    ADD MaLop AS ('L' + CASE WHEN Id < 1000 THEN RIGHT('000' + CAST (Id AS VARCHAR (10)), 3) ELSE CAST (Id AS VARCHAR (10)) END);

ALTER TABLE SinhVien DROP UQ__SinhVien__DF50EFBBC8EC12FE;


GO
ALTER TABLE SinhVien DROP COLUMN msv;


GO
ALTER TABLE SinhVien
    ADD msv AS ('MSV' + CASE WHEN Id < 1000 THEN RIGHT('000' + CAST (id AS VARCHAR (10)), 3) ELSE CAST (id AS VARCHAR (10)) END);


GO
ALTER TABLE BoMon DROP UQ_BoMon_maBM;

-- go
-- alter table BoMon
-- DROP INDEX IX_BoMon_MaBM ON BoMon;
ALTER TABLE BoMon DROP COLUMN maBM;


GO
ALTER TABLE BoMon
    ADD maBM AS ('BM' + CASE WHEN id < 1000 THEN RIGHT('000' + CAST (id AS VARCHAR (10)), 3) ELSE CAST (id AS VARCHAR (10)) END);


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
-- Index trên Khóa Ngoại (Tối ưu JOIN giữa các bảng)
CREATE NONCLUSTERED INDEX IX_LopHoc_boMonId
    ON dbo.LopHoc(boMonId);


GO
CREATE NONCLUSTERED INDEX IX_SinhVien_lopHocId
    ON dbo.SinhVien(lopHocId);

DECLARE @Total AS INT;

EXECUTE sp_SinhVien_GetPagedAdvanced @PageIndex = 1, @PageSize = 10, @Keyword = NULL, @LopHocId = NULL, @BoMonId = NULL, @MinDiem = NULL, @MaxDiem = NULL, @TotalRecords = @Total OUTPUT;

SELECT @Total AS TotalRecords;


-- Thog ke lop hoc voi lenh sql tuong ung
-- SELECT lh.Id, lh.TenLop,
--        COUNT(*) AS SoLuongSinhVien,
--        AVG(sv.DiemTb) AS DiemTrungBinh,
--        MAX(sv.DiemTb) AS DiemCaoNhat,
--        MIN(sv.DiemTb) AS DiemThapNhat
-- FROM SinhVien sv
-- JOIN LopHoc lh ON sv.LopHocId = lh.Id
-- GROUP BY lh.Id, lh.TenLop