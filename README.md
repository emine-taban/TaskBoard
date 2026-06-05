# TaskBoard - Dinamik Görev ve Süreç Yönetim Panosu 🚀

TaskBoard, bireysel kullanıcıların veya yazılım/donanım ekiplerinin proje geliştirme süreçlerini, günlük iş akışlarını ve görev dağılımlarını tek bir merkezden organize etmelerini sağlayan, masaüstü tabanlı modern bir süreç yönetim uygulamasıdır. Çevik (Agile) geliştirme yaklaşımlarından biri olan Kanban metodolojisi, bu projenin temel mimarisini oluşturmaktadır.

## 👥 Proje Ekibi
* **EMİNE TABAN (032490060)** - UI (Kullanıcı Arayüzü) ve Form Tasarımı
* **MUSTAFA PEKTAŞ (032390009)** - Kanban İş Mantığı ve Sürükle-Bırak Algoritmaları
* **EMRECAN KUTLU (032390027)** - Veri Katmanı (Veritabanı) ve Yetkilendirme (Auth)

## ✨ Öne Çıkan Özellikler
* **Dinamik Görev Yönetimi:** Kullanıcılar atanan panolarda dinamik görev kartları oluşturabilir; her kart başlık, açıklama, öncelik ve teslim tarihi (Due Date) içerir.
* **Alt Görevler (Checklist):** Büyük görevleri parçalara bölmek için kartlara alt kontrol listeleri eklenebilir.
* **Sürükle ve Bırak (Drag & Drop):** Kullanıcılara fiziksel bir tahtada post-it taşıyormuş hissi vermek için özel kodlanmış sürükle-bırak mimarisi.
* **Modern Arayüz:** Koyu Tema (Dark Mode) renk kodajları ve şık Dashboard istatistik paneli.

## 🛠️ Kullanılan Teknolojiler ve Mimari
* **Arayüz (UI):** Windows Forms (.NET) ve C#
* **Veritabanı ve ORM:** SQL Server, Entity Framework Core (Code-First Yaklaşımı)
* **Güvenlik:** BCrypt ile "Salt & Hash" şifreleme ve Session tabanlı rol yetkilendirmesi (Admin/Standart).

## 🗄️ Veritabanı Yapısı
Sistem Code-First yaklaşımıyla geliştirilmiş olup aşağıdaki temel sınıfları barındırır:
* **Users:** Kullanıcı verileri ve hash'lenmiş parolalar.
* **Projects:** Projelerin temel bilgileri (N:N -> Users).
* **BoardColumns:** Kanban aşamaları (1:N -> Projects).
* **TaskCards:** Görev gövdeleri (1:N -> BoardColumns).
* **ChecklistItems:** Görev alt listeleri (1:N -> TaskCards).

---
*Not: Bu proje Bursa Uludağ Üniversitesi Görsel Programlama dersi kapsamında geliştirilmiştir.*
