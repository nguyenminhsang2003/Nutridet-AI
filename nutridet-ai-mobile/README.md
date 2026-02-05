## nutridet-ai-mobile – Mobile App cho NutriDebt AI

### 1. Mục đích

- Mang trải nghiệm NutriDebt AI lên thiết bị di động với trọng tâm:
  - **Scan nhanh bằng camera** (chụp món ăn/đồ uống).
  - Xem “hóa đơn sức khỏe” ngay sau khi chụp.
  - Theo dõi nợ sức khỏe hằng ngày ở mọi nơi.

### 2. Trạng thái hiện tại

- Chưa khởi tạo project mobile (chưa có code ứng dụng).
- Đây là nơi sẽ chứa source cho app iOS/Android trong tương lai.

### 3. Định hướng công nghệ (dự kiến)

- **Lựa chọn 1**: React Native (dễ chia sẻ logic với web nếu dùng React).
- **Lựa chọn 2**: Flutter (UI đẹp, đa nền tảng).

### 4. Gợi ý bước tiếp theo

- Chọn stack (React Native hoặc Flutter).
- Khởi tạo project mobile bên trong thư mục này.
- Tạo luồng cơ bản:
  - Màn hình mở camera/chọn ảnh.
  - Gửi ảnh tới `nutridet-ai-api`.
  - Màn hình hiển thị Health Invoice.
  - Màn hình lịch sử / Daily Health Debt (phiên bản tối giản).
