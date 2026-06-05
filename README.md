# TaskBoard - Dinamik Görev ve Süreç Yönetim Panosu 🚀

[cite_start]TaskBoard, bireysel kullanıcıların veya yazılım/donanım ekiplerinin proje geliştirme süreçlerini, günlük iş akışlarını ve görev dağılımlarını tek bir merkezden organize etmelerini sağlayan, masaüstü tabanlı modern bir süreç yönetim uygulamasıdır[cite: 7]. [cite_start]Çevik (Agile) geliştirme yaklaşımlarından biri olan Kanban metodolojisi, bu projenin temel mimarisini oluşturmaktadır[cite: 8].

## 👥 Proje Ekibi
* [cite_start]**EMİNE TABAN (032490060)** - UI (Kullanıcı Arayüzü) ve Form Tasarımı [cite: 5]
* [cite_start]**MUSTAFA PEKTAŞ (032390009)** - Kanban İş Mantığı ve Sürükle-Bırak Algoritmaları [cite: 5]
* [cite_start]**EMRECAN KUTLU (032390027)** - Veri Katmanı (Veritabanı) ve Yetkilendirme (Auth) [cite: 5]

## ✨ Öne Çıkan Özellikler
* [cite_start]**Dinamik Görev Yönetimi:** Kullanıcılar atanan panolarda dinamik görev kartları oluşturabilir; her kart başlık, açıklama, öncelik ve teslim tarihi (Due Date) içerir[cite: 9, 10].
* [cite_start]**Alt Görevler (Checklist):** Büyük görevleri parçalara bölmek için kartlara alt kontrol listeleri eklenebilir[cite: 11].
* [cite_start]**Sürükle ve Bırak (Drag & Drop):** Kullanıcılara fiziksel bir tahtada post-it taşıyormuş hissi vermek için özel kodlanmış sürükle-bırak mimarisi[cite: 12].
* [cite_start]**Modern Arayüz:** Koyu Tema (Dark Mode) renk kodajları ve şık Dashboard istatistik paneli[cite: 22, 23].

## 🛠️ Kullanılan Teknolojiler ve Mimari
* [cite_start]**Arayüz (UI):** Windows Forms (.NET) ve C# [cite: 7, 25]
* [cite_start]**Veritabanı ve ORM:** SQL Server, Entity Framework Core (Code-First Yaklaşımı) [cite: 15, 29]
* [cite_start]**Güvenlik:** BCrypt ile "Salt & Hash" şifreleme ve Session tabanlı rol yetkilendirmesi (Admin/Standart)[cite: 30, 31].

## 🗄️ Veritabanı Yapısı
[cite_start]Sistem Code-First yaklaşımıyla geliştirilmiş olup aşağıdaki temel sınıfları barındırır[cite: 15]:
* [cite_start]**Users:** Kullanıcı verileri ve hash'lenmiş parolalar[cite: 17].
* [cite_start]**Projects:** Projelerin temel bilgileri (N:N -> Users)[cite: 17].
* [cite_start]**BoardColumns:** Kanban aşamaları (1:N -> Projects)[cite: 17].
* [cite_start]**TaskCards:** Görev gövdeleri (1:N -> BoardColumns)[cite: 17].
* [cite_start]**ChecklistItems:** Görev alt listeleri (1:N -> TaskCards)[cite: 18].

---
[cite_start]*Not: Bu proje Bursa Uludağ Üniversitesi Görsel Programlama dersi kapsamında geliştirilmiştir[cite: 1, 4].*
