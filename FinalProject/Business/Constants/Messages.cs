using Core.Entities.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Ürün eklendi";
        public static string ProductNameInvalid = "Ürün ismi geçersiz";
        public static string MaintenanceTime = "Sistem bakımda";
        public static string ProductListed = "Ürünler listelendi.";

        public static string ProductCountCategoryError = "En fazla 10 Ürün getirebilirsiniz";
        public static string ProductNameAlreadyExists = "Bu isimde zaten başka bir Ürün var";

        public static string CategoryLimitExceded = "Category Limiti aşıldığı için ürün eklenemiyor";

        public static string AuthorizationDenied = "Yetkiniz yok";

        public static string UserRegistered { get; internal set; }
        public static User UserNotFound { get; internal set; }
        public static string AccessTokenCreated { get; internal set; }
        public static string UserAlreadyExists { get; internal set; }
        public static string SuccessfulLogin { get; internal set; }
        public static User PasswordError { get; internal set; }
    }
}
 