# Adventure-of-Ball
Fizik tabanlı bir 3d platformer oyunu. 
Daha çok bir oyun nasıl yapılır onu öğrenmek amacıyla yaptım.
Sizlerin de faydalanabilmesi için paylaşıyorum.
# Oyun İçeriği
Top kontrolleri, kamera sistemi, coin toplama, hareketli platformlar, geriye doğru akan zaman, dinamik ui.

Menu, kaydedilebilir post process ve ses ayarları.

Sqlite tabanlı skor menüsü, level bittikten sonra tabloya kayıt etme.

Kontroller: WASD, Boşluk ve Mouse kullanılıyor. Mouse Scroll’ u ile FOV ayarı yapılabilir.
# Build Almak İçin Gereken Ayarlar
Veritabanı sadece 64 bit ve Windows ile uyumlu olduğu için sadece 64 bitte çalışır.

![alt text](http://www.utkugurler.com/wp-content/uploads/2017/09/resim6-300x293.png)

Player Settings’ e girip Other Settings kısmında aşağıdaki kırmızı alandaki seçimleri yapın. Bu ayarda Sqlite’ in düzgün çalışması için gerekli.

![alt text](http://www.utkugurler.com/wp-content/uploads/2017/09/resim7.png)
# Ek Bilgiler
Veritabanına ulaşmak için Ana Proje dizinin içerisinde CubeWorld var onu açarak bakabilirsiniz.
Sqlite veritabanı çalıştırmak için vereceğim linkteki Mozilla Firefox eklentisini kullanabilirsiniz : https://addons.mozilla.org/en-US/firefox/addon/sqlite-manager/

