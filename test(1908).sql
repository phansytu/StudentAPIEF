use QlSinhVien
go
exec sp_help  SinhVien
exec sp_help  LopHoc
exec sp_help  BoMon
select lh.id, bm.id from LopHoc lh, BoMon bm

select * from SinhVien
select * from LopHoc
select * from BoMon

ALTER TABLE SinhVien 
DROP CONSTRAINT FK__SinhVien__maLop__6477ECF3


ALTER TABLE SinhVien 
DROP column maLop

DROP INDEX SinhVien.IX_SinhVien_MSV_HoTen_MaLop;

alter table LopHoc
drop  constraint FK__LopHoc__maBM__6754599E
alter table LopHoc
drop column maBM

alter table SinhVien add  lopHocId int foreign key (lopHocId) references LopHoc(id)

alter table LopHoc add  boMonId int foreign key (boMonId) references BoMon(id)

alter table SinhVien 
SET IDENTITY_INSERT LopHoc OFF;

UPDATE sv
SET sv.lopHocId = lh.id
FROM SinhVien sv
INNER JOIN LopHoc lh ON sv.maLop = lh.maLop;

UPDATE lh
SET lh.boMonId = bm.id 
FROM LopHoc lh
INNER JOIN  BoMon bm ON lh.maBM = bm.maBM

-- tao ma sinh vien tu dong dua tren id sinh vien
update SinhVien
set msv  =
    'MSV' +
    CASE
        WHEN Id < 1000
            THEN RIGHT('000' + CAST(id AS VARCHAR(10)), 3)
        ELSE
            CAST(id AS VARCHAR(10))
    END
	;
--tao ma lop tu dong du trne id cua ma lop

update LopHoc
set maLop  =
    'L' +
    CASE
        WHEN id < 1000
            THEN RIGHT('000' + CAST(id AS VARCHAR(10)), 3)
        ELSE
            CAST(id AS VARCHAR(10))
    END
	;

--tao ma lop tu dong du trne id cua ma lop

update BoMon
set maBM  =
    'BM' +
    CASE
        WHEN id < 1000
            THEN RIGHT('000' + CAST(id AS VARCHAR(10)), 3)
        ELSE
            CAST(id AS VARCHAR(10))
    END
	;
alter table LopHoc
drop UQ__LopHoc__261ECAE2D78C5969

EXEC sp_helpconstraint 'LopHoc';
ALTER TABLE LopHoc
DROP COLUMN maLop;
ALTER TABLE LopHoc
ADD MaLop AS
(
    'L' +
    CASE
        WHEN Id < 1000
            THEN RIGHT('000' + CAST(Id AS VARCHAR(10)), 3)
        ELSE
            CAST(Id AS VARCHAR(10))
    END
);

alter table SinhVien
drop UQ__SinhVien__DF50EFBBC8EC12FE
go

alter table SinhVien
drop column msv

go
alter table SinhVien
add msv  
as(
    'MSV' +
    CASE
        WHEN Id < 1000
            THEN RIGHT('000' + CAST(id AS VARCHAR(10)), 3)
        ELSE
            CAST(id AS VARCHAR(10))
    END
	);
go
alter table BoMon
drop UQ_BoMon_maBM

alter table BoMon
DROP INDEX IX_BoMon_MaBM ON BoMon;


alter table BoMon
drop column maBM
go

alter table  BoMon
add  maBM  as(
    'BM' +
    CASE
        WHEN id < 1000
            THEN RIGHT('000' + CAST(id AS VARCHAR(10)), 3)
        ELSE
            CAST(id AS VARCHAR(10))
    END
	);
