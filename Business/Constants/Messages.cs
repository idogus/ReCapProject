using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        public static string BrandAdded = "Marka eklendi";
        public static string BrandExists = "Marka önceden mevcut";
        public static string NoCarAvailable = "Seçilen araç müsait değil";
        public static string CarRented = "Araç kiralama başarılı";
        public static string SuccessRentUpdate = "Kira güncelleme başarılı";
        public static string ErrorRentUpdate = "Kira güncelleme başarısız";
        public static string CarImageAdded = "Araç fotoğrafı eklendi";
        public static string CarImageLimitExceded = "Maksimum fotoğraf sayısına ulaşıldı";
        public static string CarImageDeleted = "Araç fotoğrafı silindi";
        public static string CarImageUpdated = "Araç fotoğrafı güncellendi";
        public static string UserNotFound = "Kullanıcı bulunamadı";
        public static string PasswordError = "Şifre hatalı";
        public static string SuccessfullLogin = "Sisteme giriş başarılı.";
        public static string UserAlreadyExists="Bu kullanıcı zaten mevcut.";
        public static string UserRegistered="Kullanıcı başarıyla kaydedildi";
        public static string AccessTokenCreated ="Access token başarıyla oluşturuldu.";
    }
}
