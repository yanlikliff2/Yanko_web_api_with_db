﻿namespace Yanko_web3_v2.Helpers
{
    public class AppSettings
    {
        // Ключ шифрования токена
        public string Secret { get; set; }

        // рефреш токен для обновления, неактивные токены будут
        // автоматически удалены после указанного времени
        public int RefreshTokenTTL { get; set; }

        // Данные для отправки email
        public string EmailFrom { get; set; }
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUser { get; set; }
        public string SmtpPass { get; set; }
    }
}
