# CMCU-PhatTrienGame

## Mathematics with Unity

Project Unity mang tính học tập, nhằm hỗ trợ tìm hiểu và thực hành các khái niệm toán học cơ bản trong Unity, đặc biệt là việc làm việc với hệ tọa độ và không gian hiển thị trong game engine.

---

### Nội dung chính

- **WorldToScreen.cs**  
  Script minh họa quá trình chuyển đổi tọa độ từ *World Space* sang *Screen Space* trong Unity.

---

### Yêu cầu

- Unity phiên bản **2022 trở lên**
- .NET Framework tương thích với Unity

---

## Báo cáo SFF – Tóm tắt

### 1. Thông tin chung

- **Tên dự án:** CMCU – Phát Triển Game  
- **Công nghệ:** Unity  
- **Scene demo:** `SFF/Assets/Scenes/Demo.unity`

---

### 2. Mục tiêu

Scene `Demo.unity` được xây dựng nhằm minh họa các khái niệm toán học cơ bản trong Unity, đặc biệt là việc xử lý không gian và chuyển đổi tọa độ giữa *World Space* và *Screen Space*.

---

### 3. Chức năng chính

- Hiển thị các **GameObject** trong không gian Unity  
- Chuyển đổi tọa độ từ *World Space* sang *Screen Space* thông qua script  
- Scene có thể chạy trực tiếp và ổn định trong Unity Editor  

---

### 4. Yêu cầu phi chức năng

- Giao diện đơn giản, dễ quan sát  
- Scene chạy mượt, không phát sinh lỗi nghiêm trọng  
- Dễ mở rộng thêm các nội dung toán học khác  

---

### 5. Kiểm thử

- Scene `Demo.unity` chạy bình thường khi nhấn Play  
- Đối tượng hiển thị đúng vị trí  
- Kết quả chuyển đổi tọa độ chính xác  

**Kết quả:** Pass

---

## LAB THỰC HÀNH – CHƯƠNG 3: UNITY SCRIPTING

### 1. Mục tiêu
Mục tiêu của các bài lab trong Chương 3 là giúp sinh viên:
- Hiểu rõ vòng đời `MonoBehaviour`
- Sử dụng Vector, Quaternion và Rotation API trong Unity
- Áp dụng `SignedAngle` cho game 2D / Top-down
- Nắm vững Observer Pattern bằng C# Event và UnityEvent
- Kết hợp các kiến thức vào một mini project hoàn chỉnh

---

### 2. Môi trường phát triển
- Unity Hub
- Unity Editor: 2021 LTS hoặc 2022 LTS
- Ngôn ngữ: C#
- Template: 3D Core
- IDE: Visual Studio / Rider

---

### 3. Lab 1 – Component Lifecycle Debugger

#### Demo
![Lab 1 - Lifecycle](LAB_C3/Unity_Lab_Chuong3/Gif/Lab1.gif)

---

### 4. Lab 2 – Vector Movement & Gizmos

#### Demo
![Lab 2 - Vector Movement](LAB_C3/Unity_Lab_Chuong3/Gif/Lab2.gif)

---

### 5. Lab 3 – Quaternion Rotation (Turret xoay target)

#### Demo
![Lab 3 - Quaternion Rotation](LAB_C3/Unity_Lab_Chuong3/Gif/Lab3.gif)

---

### 6. Lab 4 – Signed Angle (Top-down / 2D)

#### Image

![Lab 4 - Signed Angle](LAB_C3/Unity_Lab_Chuong3/Images/Lab4.png)

#### Demo
##### Theo chuột
![Lab 4 - Signed Angle](LAB_C3/Unity_Lab_Chuong3/Gif/Lab4.gif)

##### Theo Target
![Lab 4 - Signed Angle](LAB_C3/Unity_Lab_Chuong3/Gif/Lab4_Target.gif)

---

### 7. Lab 5 – Observer Pattern (C# Event)

#### Demo
![Lab 5 – Observer Pattern](LAB_C3/Unity_Lab_Chuong3/Gif/Lab5.gif)

---

### 8. Lab 6 – Observer Pattern (UnityEvent)

#### Image
![Lab 6 - Binding Image](LAB_C3/Unity_Lab_Chuong3/Images/Lab6.png)

#### Demo

![Lab 6 – Observer Pattern](LAB_C3/Unity_Lab_Chuong3/Gif/Lab6.gif)

---

### 9. Mini Project – Turret Defense Dummy
#### Demo
![Mini Project – Turret Defense Dummy](LAB_C3/Unity_Lab_Chuong3/Gif/MiniProject.gif)

---

## BÁO CÁO THỰC HÀNH CHƯƠNG 4: AUDIO & VIDEO (UNITY)
#### Các demo video được để ở Asset/Video

---

### 1. Mục tiêu

- Nắm vững hệ thống Audio: AudioSource, AudioListener, AudioClip
- Hiểu và áp dụng Spatial Audio (2D/3D)
- Làm việc với VideoPlayer và events
- Xây dựng intro cutscene hoàn chỉnh

---

### 2. Kết quả các Labs

#### Lab 1: AudioSource Cơ Bản
**Kết quả:** 
- ✓ Tạo AudioSource với điều khiển bằng phím (Space: Play, S: Stop)
- ✓ Play On Awake = OFF hoạt động đúng
- ✓ Script AudioController chạy ổn định

**Ứng dụng:** Sound effects trong game (tiếng súng, bước chân, interaction)

#### Lab 2: Audio 2D vs 3D
**Kết quả:**
- ✓ Audio 2D (Spatial Blend = 0): Âm lượng không đổi theo vị trí
- ✓ Audio 3D (Spatial Blend = 1): Âm lượng thay đổi theo khoảng cách
- ✓ Cấu hình Min/Max Distance ảnh hưởng rõ rệt

**Kết luận:**
- **2D Audio:** Background music, UI sounds, narration
- **3D Audio:** Tiếng bước chân, môi trường, NPC voices

#### Lab 3: Điều Khiển Audio Toàn Cục
**Kết quả:**
- ✓ Phím M: Mute/Unmute (AudioListener.volume)
- ✓ Phím P: Pause/Resume (AudioListener.pause)
- ✓ Áp dụng được cho Settings Menu

#### Lab 4: AudioClip Optimization
**Cấu hình tối ưu:**

| Loại Audio | Load Type | Compression | Lý do |
|------------|-----------|-------------|-------|
| BGM dài | Streaming | Vorbis | Tiết kiệm RAM |
| SFX ngắn | Decompress On Load | PCM | Phát nhanh |
| SFX trung bình | Compressed In Memory | ADPCM | Cân bằng |

**Kết quả:** Giảm ~60% file size, performance tốt

#### Lab 5: VideoPlayer Cơ Bản
**Kết quả:**
- ✓ Import video .mp4 thành công
- ✓ Phím V để play video
- ✓ 60 FPS ổn định, không desync

#### Lab 6: Video Render Target
**Phương pháp thực hiện:**
- **Method 1:** RenderTexture + UI RawImage → Phù hợp cutscene fullscreen
- **Method 2:** Material Override trên 3D Object → Phù hợp TV screen in-game

**Kết quả:** Cả 2 phương pháp hoạt động tốt

#### Lab 7: Video Events & Control
**Events đã implement:**
- `prepareCompleted`: Video sẵn sàng → Auto play
- `loopPointReached`: Video kết thúc → Hiện UI + chuyển scene
- `errorReceived`: Xử lý lỗi

**Kết quả:**
- ✓ Events kích hoạt đúng
- ✓ Scene transition mượt mà
- ✓ Không memory leak

---

### 3. Mini Project: Intro Cutsence

#### Tính năng đã hoàn thành:
✓ **Video Intro:** Tự động phát, RenderTexture fullscreen, 60 FPS  
✓ **BGM:** Đồng bộ với video, fade out khi kết thúc  
✓ **Skip Button:** Click hoặc Space/ESC để bỏ qua  
✓ **Fade Effects:** Fade in 1s, fade out mượt mà  
✓ **Auto Transition:** Tự chuyển scene khi video hết  

#### Code highlights:
```csharp
// IntroCutsceneManager.cs
- Event-driven: prepareCompleted, loopPointReached
- Coroutine fade effects
- Proper cleanup (unregister events)
- Error handling & validation
```

#### Testing:
- ✓ Video auto-play: PASS
- ✓ BGM sync: PASS
- ✓ Skip functionality: PASS
- ✓ Fade smooth: PASS
- ✓ Performance: 60 FPS, ~150MB RAM

---

### 4. Nhận xét

#### Điểm mạnh:
✓ Hiểu rõ 2D/3D Audio và ứng dụng  
✓ Làm chủ VideoPlayer events  
✓ Code clean, có comments đầy đủ  
✓ Mini project chạy ổn định  

#### Điểm cần cải thiện:
- Chưa có AudioMixer groups
- Chưa implement subtitle system
- Chưa optimize cho mobile

#### Kiến thức học được:
- **Audio:** Spatial Audio, optimization, global control
- **Video:** Events, RenderTexture, optimization
- **Coding:** Event-driven, coroutines, error handling
- **Workflow:** Testing, debugging, documentation

#### Ứng dụng thực tế:
- Intro/outro cutscenes
- Tutorial videos in-game
- TV screens, security cameras
- Training simulations

---

## 5. Kết luận

Qua bài lab, đã nắm vững được Audio & Video system trong Unity từ cơ bản đến nâng cao. Mini project intro cutscene hoạt động professional với đầy đủ features: video, audio, UI, transitions.

**Thành tựu:**
- ✓ Hiểu sâu AudioSource, AudioListener, Spatial Audio
- ✓ Làm chủ VideoPlayer và events
- ✓ Xây dựng sản phẩm chất lượng tốt

**Hướng phát triển:**
- Thêm AudioMixer cho audio groups
- Implement subtitle system
- Optimize cho mobile
- Adaptive music system
