namespace Tulpar.Core.Const
{
    public static class ProjectSettings
    {
        public const string Project_Name = "TulparYazilimCore_";

        #region Product Modules
        public const string Product_RabbitMq_QName = "";

        public const string Product_RedisCache_Name = Project_Name + "Product_Module:";
        public const string Product_RedisCache_Host = "127.0.0.1";
        public const int Product_RedisCache_Port = 6379;
        public const string Product_RedisCache_Password = "";
        public const long Product_RedisCache_DatabaseId = 0;
        #endregion

    }
}
