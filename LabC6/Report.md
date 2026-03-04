# Báo Cáo Kết Quả – Lab Chương 6: Sprites & Animations (Unity)

### Demo & Kết Quả ở: LabC6/ Results

## Kết Quả Từng Lab

### Lab 1 – Sprite Creator & Sprite Renderer
- Tạo 3 placeholder sprite (Square / Circle / Triangle) bằng **Sprite Creator**
- Thiết lập 3 **Sorting Layer**: `Background` → `Player` → `FX`
- Gán **Order in Layer** khác nhau, kiểm chứng thứ tự render bằng cách chồng sprite lên nhau
- Thay placeholder bằng sprite thật bằng cách drag vào ô **Sprite** của Sprite Renderer

### Lab 2 – Import Texture & Pixels Per Unit
- Import 1 sprite đơn và 1 sprite sheet, cấu hình `Texture Type = Sprite (2D and UI)`
- So sánh **PPU = 32** vs **PPU = 100**:
  - PPU = 32 → sprite hiển thị lớn hơn ~3 lần trong Scene
  - PPU = 100 → kích thước chuẩn theo thiết kế
- Công thức: `Kích thước Unity = Pixel Width ÷ PPU`

### Lab 3 – Sprite Editor: Manual Slicing
- Set `Sprite Mode = Multiple` trên sprite sheet
- Trong Sprite Editor: **kéo tay** để vẽ 6 rectangle selection (thay cho tùy chọn Manual đã bị loại bỏ trong Unity 2022.3)
- Sử dụng **Trim** để khít theo vùng không transparent
- Đặt Pivot: `Center` cho sprite thường, `Bottom` cho sprite nhân vật

### Lab 4 – Auto/Grid Slicing & Edit Outline
- Dùng **Grid By Cell Size (16×16)** để slice tự động toàn bộ sprite sheet
- Dùng **Custom Outline** trong Sprite Editor để chỉnh outline ôm sát nhân vật, loại bỏ vùng transparent thừa
- **Nhận xét:** Edit outline cần thiết khi sprite có nhiều vùng trong suốt lớn → giảm overdraw → tăng hiệu năng render khi có nhiều sprite trên màn hình

### Lab 5 – Transparency Sort Mode
- Tạo 5 sprite đặt lệch nhau theo trục Z
- Thử nghiệm các mode tại `Edit → Project Settings → Graphics`:

| Mode | Kết quả quan sát |
|------|-----------------|
| Default | Sắp xếp theo Sorting Layer và Order in Layer |
| Orthographic | Sprite có Z nhỏ hơn hiển thị phía trên |
| Perspective | Sprite gần camera hơn hiển thị phía trên |
| Custom Axis (Y=1) | Sprite có Y nhỏ hơn hiển thị phía trước – phù hợp game isometric |

### Lab 6 – Animation Clip: Record vs Preview Mode
- Tạo `Idle.anim` với keyframe tại frame 0 / 30 / 60 (Position Y dao động nhẹ)
- **Record Mode** : mọi thay đổi property trong Inspector được tự động ghi thành keyframe
- **Preview Mode** : thay đổi chỉ để xem trước, phải tự nhấn **Add Key** hoặc chuột phải → Add Key để tạo keyframe thủ công

### Lab 7 – Animator Controller: States, Transitions, Parameters
- Tạo 3 Animation Clip: `Idle`, `Run`, `Attack`
- Cấu hình Animator Controller với state machine:

```
[Entry] ──→ [Idle] ⇄ [Run]
[Any State] ──→ [Attack] ──→ [Idle]
```

- Parameters:
  - `Speed` (Float): điều khiển chuyển Idle ↔ Run
  - `Attack` (Trigger): kích hoạt Attack từ bất kỳ state nào
- Transition đều tắt `Has Exit Time` (trừ Attack → Idle) để chuyển state ngay lập tức

### Lab 8 – Animator API & Tối Ưu
- Sử dụng `Animator.SetFloat()` và `Animator.SetTrigger()`
- Tối ưu bằng `Animator.StringToHash()` – tính hash 1 lần, tránh convert string mỗi frame
- Tối ưu bằng cách chỉ gọi `SetFloat` khi giá trị thực sự thay đổi (dùng `Mathf.Approximately`)

---

## Mini Project – 2D Character Showcase

### Mô tả
Scene 2D hoàn chỉnh với nhân vật có 3 trạng thái animation, sorting layer đúng thứ tự và hiệu ứng tấn công.

### Workflow Thực Hiện

```
1. PNG (tilemap.png)
        ↓
2. Import Settings
   Texture Type = Sprite (2D and UI)
   Sprite Mode  = Multiple
   PPU          = 16
   Filter Mode  = Point (no filter)
        ↓
3. Sprite Editor
   Grid By Cell Size (16×16)
   Pivot = Bottom (nhân vật)
        ↓
4. Sorting Layers
   Background → Player → FX
        ↓
5. Animation Clips
   Idle.anim  – Position bob nhẹ lên xuống
   Run.anim   – Sprite swap các frame chạy
   Attack.anim – Scale to ra rồi về
        ↓
6. Animator Controller
   States: Idle, Run, Attack
   Transitions + Parameters (Speed, Attack)
        ↓
7. Script (PlayerController.cs)
   SetFloat(SpeedHash, ...)
   SetTrigger(AttackHash)
   flipX để lật sprite theo hướng di chuyển
```

### Điều Khiển

| Phím | Hành động |
|------|-----------|
| `A` / `D` | Di chuyển trái / phải |
| `Space` | Tấn công + hiện FX |

### Sorting Layer Setup

| GameObject | Sorting Layer | Order in Layer |
|------------|--------------|----------------|
| Background | Background | 0 |
| Player | Player | 0 |
| FX_Attack | FX | 1 |

---

## Vấn Đề Gặp Phải & Cách Xử Lý

| Vấn đề | Nguyên nhân | Cách fix |
|--------|-------------|----------|
| Sprite bị mờ/nhòe | Filter Mode mặc định là Bilinear | Đổi `Filter Mode = Point (no filter)` trong Import Settings |
| Không tìm thấy tùy chọn "Manual" trong Slice | Đã bị loại bỏ trong Unity 2022.3 | Kéo tay trực tiếp trong Sprite Editor để vẽ vùng slice |
| Animation không chuyển state | `Has Exit Time` đang bật | Bỏ tick `Has Exit Time` trong Transition |
| FX hiện sai vị trí | FX không phải child của Player | Drag `FX_Attack` vào trong `Player` trong Hierarchy |
| Lỗi NullReferenceException | Quên GetComponent hoặc chưa gán reference | Kiểm tra `Start()` và kéo object vào ô Inspector |

---

## 📚 Tài Nguyên Sử Dụng

- Asset: [Kenney – Tiny Dungeon](https://kenney.nl/assets/tiny-dungeon) *(CC0, miễn phí)*
