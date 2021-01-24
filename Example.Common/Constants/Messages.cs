namespace Example.Common.Constants
{
    public static class Messages
    {
        public static string AuthorizationDenied = "Bu işlemi yapmaya yetkiniz yoktur !";
        public static string UserRegistered = "Kullanıcı başarıyla kaydedildi";
        public static string UserNotFound = "Kullanıcı bulunamadı";
        public static string PasswordError = "Şifre hatalı";
        public static string SuccessfulLogin = "Sisteme giriş başarılı";
        public static string AccessTokenCreated = "Access token başarıyla oluşturuldu";
        public static string AccessTokenNotCreated = "Access token oluşturulamadı !";

        public static string CacheAddErrorMessage =
            "CacheAttribute Error : \"duration\" parametresi \"Cache.AddOrGet\" ile kullanılır !";

        public static string CacheClearErrorMessage =
            "CacheAttribute Error : \"keyOrPattern\" parametresi \"Cache.Remove\" ile kullanılır !";
    }
}