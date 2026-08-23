# StudentAPIw6

API quản lý Sinh viên - Lớp học 

---

## 1. API Endpoints

### Lớp học — `/api/LopHoc`

| Method | Route | Mô tả |
|---|---|---|
| GET | `/api/LopHoc` | Lấy danh sách lớp (phân trang) |
| GET | `/api/LopHoc/{maLop}` | Lấy chi tiết 1 lớp |
| POST | `/api/LopHoc` | Tạo lớp mới |
| PUT | `/api/LopHoc/{maLop}` | Cập nhật lớp |
| DELETE | `/api/LopHoc/{maLop}` | Xoá lớp (chặn nếu lớp còn sinh viên) |
| GET | `/api/LopHoc/thong-ke` | Thống kê số lượng / điểm TB / cao nhất / thấp nhất theo lớp (không phân trang) |

### Sinh viên — `/api/SinhVien`

| Method | Route | Mô tả |
|---|---|---|
| GET | `/api/SinhVien` | Lấy danh sách (tìm kiếm, lọc giới tính/điểm, sắp xếp, phân trang) |
| GET | `/api/SinhVien/{maSV}` | Lấy chi tiết 1 sinh viên |
| POST | `/api/SinhVien` | Tạo sinh viên mới |
| PUT | `/api/SinhVien/{maSV}` | Cập nhật sinh viên |
| DELETE | `/api/SinhVien/{maSV}` | Xoá sinh viên |
### Bộ môn — `/api/BoMon`

| Method | Route | Mô tả |
|---|---|---|
| GET | `/api/BoMon` | Lấy danh sách bộ môn (phân trang) |
| GET | `/api/BoMon/{maBoMon}` | Lấy chi tiết 1 bộ môn |
| POST | `/api/BoMon` | Tạo bộ môn mới |
| PUT | `/api/BoMon/{maBoMon}` | Cập nhật bộ môn |
| DELETE | `/api/BoMon/{maBoMon}` | Xoá bộ môn |

### Dashboard / Báo cáo — `/api/Dashboard`

| Method | Route | Mô tả |
|---|---|---|
| GET | `/api/Dashboard/summary` | Thống kê tổng quan (tổng SV, tổng lớp, tổng bộ môn, điểm TB toàn trường, số SV giỏi) |
| GET | `/api/Dashboard/bao-cao-chi-tiet-sinh-vien` | Báo cáo chi tiết sinh viên (tìm kiếm, lọc theo lớp/bộ môn, phân trang) |
| GET | `/api/Dashboard/bao-cao-thong-ke-theo-lop` | Báo cáo thống kê theo lớp (sĩ số, tỉ lệ nam/nữ, điểm TB/cao nhất/thấp nhất) |
---
---

## Database Objects

### Stored Procedures

| Tên | Chức năng |
|---|---|
| `sp_SinhVien_GetPagedAdvanced` | Lọc/phân trang sinh viên nâng cao (từ khoá, lớp, bộ môn, khoảng điểm), trả kèm `@TotalRecords` output |
| `sp_Dashboard_GetSummaryStats` | Trả về thống kê tổng quan toàn hệ thống cho Dashboard |

### Views

| Tên | Chức năng |
|---|---|
| `vw_BaoCao_ChiTietSinhVien` | Báo cáo chi tiết từng sinh viên: giới tính, tuổi, xếp loại, lớp, bộ môn |
| `vw_BaoCao_ThongKeTheoLop` | Báo cáo thống kê theo từng lớp: sĩ số, tỉ lệ nam/nữ, điểm TB/cao nhất/thấp nhất |

### Indexes

| Tên | Bảng | Cột | Mục đích |
|---|---|---|---|
| `IX_LopHoc_boMonId` | `LopHoc` | `boMonId` | Tối ưu JOIN giữa `LopHoc` và `BoMon` |
| `IX_SinhVien_lopHocId` | `SinhVien` | `lopHocId` | Tối ưu JOIN giữa `SinhVien` và `LopHoc` |
2. Test Plan — Checklist chạy tay (Swagger / Postman)

### 2.1 Sinh viên — Create (`POST /api/SinhVien`)

| Case | Input | Mong đợi | Thực tế |
|---|---|---|---|
| TC-SV-01 | Email đúng định dạng, đầy đủ field hợp lệ | `201 Created`, trả về object có `MaSV` khác `null` | ☐ |
| TC-SV-02 | Email sai định dạng (vd `"abc"`, `"a@b"`) | `400 Bad Request`, `ProblemDetails` chứa lỗi field `Email` | ☐ |
| TC-SV-03 | Email đã tồn tại trong hệ thống | `400 Bad Request`, message `"Email ... đã tồn tại"` | ☐ |
| TC-SV-04 | `HoTen` rỗng | `400 Bad Request`, lỗi field `HoTen` | ☐ |
| TC-SV-05 | `DiemTB = 15` (ngoài khoảng 0-10) | `400 Bad Request`, lỗi field `DiemTB` | ☐ |
| TC-SV-06 | `NgaySinh` là ngày tương lai | `400 Bad Request`, lỗi field `NgaySinh` | ☐ |
| TC-SV-07 | `MaLop` không tồn tại trong hệ thống (nếu đã áp dụng gợi ý #13 ở lượt trao đổi trước) | `404 Not Found` hoặc `400 Bad Request` tuỳ cách bạn implement | ☐ |
| TC-SV-08 | Tạo 2 sinh viên liên tiếp | `MaSV` sinh ra không trùng nhau | ☐ |

### 2.2 Sinh viên — Update / Delete / GetById

| Case | Input | Mong đợi | Thực tế |
|---|---|---|---|
| TC-SV-09 | `PUT /api/SinhVien/{maSV}` với email của **chính nó** | `200 OK` (không bị false-positive báo trùng email) | ☐ |
| TC-SV-10 | `PUT /api/SinhVien/{maSV}` với email của **sinh viên khác** | `400 Bad Request` báo trùng email | ☐ |
| TC-SV-11 | `PUT` với `maSV` không tồn tại | `404 Not Found` | ☐ |
| TC-SV-12 | `GET /api/SinhVien/{maSV}` với mã không tồn tại | `404 Not Found` (không phải `500`) | ☐ |
| TC-SV-13 | `DELETE /api/SinhVien/{maSV}` hợp lệ | `204 No Content`, sinh viên biến mất khỏi `GetAll` | ☐ |

### 2.3 Sinh viên — GetAll (tìm kiếm / lọc / sắp xếp / phân trang)

| Case | Input | Mong đợi | Thực tế |
|---|---|---|---|
| TC-SV-14 | `GET /api/SinhVien` không truyền `SortBy` | `200 OK`, **không** lỗi 500 (trước đây bị `NotImplementedException`) | ☐ |
| TC-SV-15 | `SortBy=diemtb&Descending=true` | Danh sách sắp xếp điểm giảm dần | ☐ |
| TC-SV-16 | `PageNumber=1&PageSize=5` | `Data` trả về đúng **5** phần tử (trước đây bug trả full list) | ☐ |
| TC-SV-17 | `PageNumber=2&PageSize=5` | Trang 2 khác trang 1, không trùng dữ liệu | ☐ |
| TC-SV-18 | `DiemTu=6&DiemDen=8` | Chỉ trả sinh viên có điểm trong khoảng [6,8] (trước đây `DiemDen` bị bỏ qua) | ☐ |
| TC-SV-19 | `Keyword=nguyen` | Trả đúng các sinh viên có họ tên/email/mã chứa từ khoá (không phân biệt hoa thường tuỳ implement) | ☐ |
| TC-SV-20 | `PageNumber=0` hoặc âm | `400 Bad Request` nếu đã gắn `PaginationRequestValidator`, ngược lại kiểm tra không bị lỗi 500 `ArgumentOutOfRangeException` | ☐ |

### 2.4 Lớp học — Create / Update / Delete

| Case | Input | Mong đợi | Thực tế |
|---|---|---|---|
| TC-LH-01 | Tạo lớp với `TenLop` mới | `201 Created`, có `MaLop` | ☐ |
| TC-LH-02 | Tạo lớp với `TenLop` đã tồn tại | `400 Bad Request` | ☐ |
| TC-LH-03 | `TenLop` rỗng hoặc dài quá 100 ký tự | `400 Bad Request` | ☐ |
| TC-LH-04 | `DELETE /api/LopHoc/{maLop}` với lớp **đang có sinh viên** | `400 Bad Request` — bị chặn xoá (trước đây fix #7 bug này không được check) | ☐ |
| TC-LH-05 | `DELETE /api/LopHoc/{maLop}` với lớp **không có sinh viên** | `204 No Content` | ☐ |
| TC-LH-06 | `DELETE /api/LopHoc/{maLop}` (route trước đây thiếu `{maLop}`) | Request tới đúng lớp cần xoá, không bind `null` | ☐ |
| TC-LH-07 | `GET /api/LopHoc/{maLop}` với mã không tồn tại | `404 Not Found` | ☐ |

### 2.5 Thống kê lớp học

| Case | Input | Mong đợi | Thực tế |
|---|---|---|---|
| TC-TK-01 | `GET /api/LopHoc/thong-ke` | `200 OK`, trả `List<ThongKeLopHoc>` (không có `PageNumber`/`PageSize` trong response) | ☐ |
| TC-TK-02 | Kiểm tra từng lớp: `DiemTrungBinh`/`DiemCaoNhat`/`DiemThapNhat` khớp dữ liệu thật | Số liệu khớp tính tay | 
### 2.6 Exception Handling

| Case | Input | Mong đợi | Thực tế |
|---|---|---|---|
| TC-EX-01 | Bất kỳ request nào gây lỗi validate FluentValidation | `400`, body theo `HttpValidationProblemDetails` với `errors` là dictionary theo field | ☐ |
| TC-EX-02 | Bất kỳ `NotFoundException`/subclass | `404`, `ProblemDetails.Title = "Không tìm thấy tài nguyên"` | ☐ |
| TC-EX-03 | Bất kỳ `BadRequestException`/subclass | `400`, `ProblemDetails.Title = "Yêu cầu không hợp lệ"` | ☐ |
| TC-EX-04 | Lỗi hệ thống không xác định (vd ép NullReference giả lập) | `500`, có log `LogError` ghi lại | ☐ |


## 3. Cách chạy dự án

```bash
dotnet restore
dotnet build
dotnet run
```

Sau khi chạy, mở Swagger UI (mặc định `https://localhost:{port}/swagger`) để test các endpoint ở
mục 3, đối chiếu với checklist mục 5.

---
---

## Hướng dẫn Migration

### 1. Migration cho Entity (EF Core)

Dự án dùng EF Core Migration để quản lý schema của các bảng ánh xạ Entity: `SinhVien`, `LopHoc`, `BoMon`.

**Tạo migration mới** (sau khi sửa Entity hoặc `AppDbContext.OnModelCreating`):
```bash
dotnet ef migrations add <TenMigrationMoTaThayDoi>
```
Ví dụ:
```bash
dotnet ef migrations add AddBoMonEntity
dotnet ef migrations add AddIndexOnForeignKeys
```

**Áp dụng migration vào database:**
```bash
dotnet ef database update
```

**Xem lại danh sách migration đã áp dụng:**
```bash
dotnet ef migrations list
```

**Rollback về 1 migration cụ thể** (nếu cần huỷ thay đổi gần nhất):
```bash
dotnet ef database update <TenMigrationTruocDo>
```

**Xoá migration cuối cùng chưa apply vào DB** (chỉ dùng khi chưa chạy `database update`):
```bash
dotnet ef migrations remove
```

### 2. Migration cho Stored Procedure / View / Index (SQL script tay)

Vì EF Core Migration không quản lý tốt Stored Procedure và View phức tạp, các đối tượng này được viết và chạy bằng SQL script tay, **không** thông qua `dotnet ef`. Toàn bộ script được lưu tại: 
```text
test(1908).sql
```
**Cách áp dụng khi setup database mới hoặc pull code có script mới:**
1. Mở SQL Server Management Studio (SSMS), kết nối tới database của project.
2. Chạy lần lượt từng file theo đúng thứ tự thư mục ở trên (Stored Procedures → Views → Indexes), vì View có thể phụ thuộc vào bảng đã tồn tại, Index tạo sau cùng để tránh xung đột khi bảng đang thay đổi cấu trúc.
3. Toàn bộ script dùng `CREATE OR ALTER`, riêng phần `Indexes` dùng `CREATE NONCLUSTERED INDEX` — **chạy lại sẽ báo lỗi trùng tên** nếu index đã tồn tại, cần `DROP INDEX` trước nếu muốn tạo lại.

### 3. Lưu ý khi kết hợp cả 2 (tránh conflict)

Vì Index (`IX_LopHoc_boMonId`, `IX_SinhVien_lopHocId`) được tạo bằng SQL script tay **trước khi** khai báo trong `AppDbContext` qua Fluent API, nếu sau này chạy `dotnet ef migrations add` mà model đã khai báo `HasIndex(...)`, EF Core sẽ tạo ra migration chứa `CreateIndex` — nếu chạy `dotnet ef database update` trên database đã có sẵn index đó, sẽ gặp lỗi:
```text
There is already an object named 'IX_LopHoc_boMonId' in the database.
```
**Cách xử lý:** mở file migration vừa được tạo trong `Context/Migrations/`, xoá hoặc comment đoạn `migrationBuilder.CreateIndex(...)` tương ứng (giữ nguyên các thay đổi khác nếu có), rồi mới chạy `dotnet ef database update`.

### 4. Thứ tự setup database từ đầu (máy mới / clone project lần đầu)

```bash
# 1. Tạo bảng theo Entity qua EF Core Migration
dotnet ef database update

# 2. Chạy các script SQL tay theo đúng thứ tự
#    (mở SSMS, chạy lần lượt các file trong Context/SqlScripts/)
#    StoredProcedures -> Views -> Indexes
```