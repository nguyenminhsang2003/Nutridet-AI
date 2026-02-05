## NutriDebt AI (bắt đầu: 05/02/2026)

### 1. Product Overview

**NutriDebt AI** là một ứng dụng sức khỏe ứng dụng AI nhằm **thay đổi hành vi ăn uống** bằng cách chuyển dữ liệu dinh dưỡng trừu tượng thành những hình ảnh trực quan và “chi phí vật lý” dễ cảm nhận.  
Thay vì hiển thị các con số khó hiểu như calories hay grams đường, NutriDebt AI quy đổi chúng thành các thông điệp gần gũi hơn, ví dụ: **“Ly trà sữa này tương đương 9 viên đường”** hay **“Bạn cần chạy bộ 38 phút để đốt lượng calories này”**.  
Mục tiêu không chỉ là theo dõi dinh dưỡng, mà là **tạo ra nhận thức tức thời để thúc đẩy thay đổi hành vi**.

### 2. Problem Statement

- **Cognitive Gap – Khoảng cách nhận thức**: người dùng khó hình dung tác động thực tế của các chỉ số dinh dưỡng; 40g đường không tạo cảm xúc, nhưng 8 viên đường thì gây “shock”.
- **Input Friction – Rào cản nhập liệu**: nhập tay từng món ăn khiến phần lớn người dùng bỏ cuộc sau 1–2 tuần vì quá tốn công sức.
- **Hidden Nutritional Risk – Thành phần ẩn**: gần như không thể ước tính đường trong đồ uống pha chế, dầu mỡ trong món ăn ngoài, calories của thực phẩm không nhãn → dễ đánh giá sai mức “tổn hại sức khỏe”.
- **Lack of Consequence Awareness – Thiếu nhận thức về “cái giá phải trả”**: con người ưu tiên khoái cảm ngắn hạn và bỏ qua hậu quả dài hạn; nếu hậu quả được “hiện hình”, hành vi có thể thay đổi ngay.

### 3. Solution

- **Visual Shock Engine**: chuyển đổi dữ liệu dinh dưỡng thành các biểu tượng vật lý:
  - Sugar Cube Equivalent,
  - Fat Stack Visualization,
  - Workout Time Debt,
  - Physical Activity Cost  
  để dữ liệu trở thành thứ người dùng **cảm nhận được**, không chỉ đọc.
- **AI Vision-First Experience**: người dùng chỉ cần chụp ảnh nhãn thực phẩm, đồ uống hoặc món ăn; hệ thống AI đa phương thức sẽ:
  - nhận diện thực phẩm,
  - trích xuất thông tin dinh dưỡng,
  - trả về dữ liệu có cấu trúc, gần như loại bỏ ma sát nhập liệu.
- **Emotional AI Feedback**: dùng giọng văn mang tính cảnh báo nhẹ, hài hước, “phê bình thân thiện” (ví dụ: *“Món này ngon thật… nhưng cơ thể bạn sẽ phải làm việc hơi vất vả đấy.”*) để tăng engagement và retention.

### 4. Product Vision

**NutriDebt AI** hướng tới trở thành một **“Health Reality Mirror” – tấm gương phản chiếu hậu quả thực sự của mỗi lựa chọn thực phẩm.**  
Sản phẩm không cạnh tranh bằng độ chính xác tuyệt đối, mà bằng khả năng:
- tạo nhận thức,
- kích hoạt cảm xúc,
- thúc đẩy thay đổi hành vi.

### 5. Core Features

- **AI Scanner**: phân tích hình ảnh thực phẩm (packaged food, beverages, basic meals).
- **Debt Engine**: thuật toán backend quy đổi dinh dưỡng thành:
  - Sugar Cube Equivalent,
  - Workout Debt,
  - Health Damage Score  
  (AI chỉ extract dữ liệu, business logic nằm ở backend).
- **Health Invoice**: sau mỗi lần scan, người dùng nhận một “hóa đơn sức khỏe” gồm:
  - lượng đường,
  - calories,
  - thời gian vận động cần thiết,
  - điểm tác động sức khỏe.
- **Daily Debt Ledger**: bảng theo dõi tổng “khoản nợ sức khỏe” theo ngày/tuần/tháng, giúp nhìn thấy xu hướng hành vi.
- **Smart Suggestions**: khi phát hiện thực phẩm có rủi ro cao, hệ thống gợi ý lựa chọn thay thế “ít nợ” hơn (ví dụ: *“Bạn có thể giảm 60% lượng đường nếu chọn phiên bản ít ngọt.”*).

### 6. Tổng quan thư mục chính

- **`document/`**: chứa tài liệu ý tưởng sản phẩm (như `document/base/Ý tưởng khởi tạo.txt`), định hướng và checklist các giai đoạn phát triển.  
  → Chi tiết hơn xem tại: [`document/README.md`](document/README.md)
- **`nutridet-ai-api/`**: backend API (ASP.NET Core/.NET) xử lý logic, Debt Engine, lưu lịch sử “nợ sức khỏe”.  
  → Chi tiết hơn xem tại: [`nutridet-ai-api/README.md`](nutridet-ai-api/README.md)
- **`nutridet-ai-web/`**: web client (frontend) – giao diện web cho người dùng (chưa khởi tạo).  
  → Chi tiết hơn xem tại: [`nutridet-ai-web/README.md`](nutridet-ai-web/README.md)
- **`nutridet-ai-mobile/`**: mobile client – app di động tập trung vào trải nghiệm scan bằng camera (chưa khởi tạo).  
  → Chi tiết hơn xem tại: [`nutridet-ai-mobile/README.md`](nutridet-ai-mobile/README.md)
