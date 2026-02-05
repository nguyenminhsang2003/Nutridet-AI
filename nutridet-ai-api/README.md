## nutridet-ai-api – Backend API cho NutriDebt AI

### 1. Mục đích

- Cung cấp các API cho toàn bộ hệ thống NutriDebt AI:
  - Nhận dữ liệu về món ăn/đồ uống (ảnh hoặc metadata).
  - Xử lý và quy đổi dữ liệu dinh dưỡng thành “nợ sức khỏe”.
  - Lưu và trả về lịch sử “nợ sức khỏe” cho người dùng.

### 2. Nhiệm vụ chính

- Đóng vai trò “bộ não” xử lý logic:
  - Kết nối với AI để extract dữ liệu dinh dưỡng.
  - Chạy Debt Engine (sugar cubes, workout debt, health score).
  - Lưu trữ và tổng hợp dữ liệu cho các màn hình Daily/Weekly/Monthly Debt trên web/mobile.
