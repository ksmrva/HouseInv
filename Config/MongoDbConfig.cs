namespace HouseInv.Config
{
    public class MongoDbConfig
    {
        public required string Host { get; set; }
        public required int Port { get; set; }
        public required string User { get; set; }
        public required string Password { get; set; }

        public string ConnectionString
        {
            get
            {
                return $"mongodb://{User}:{Password}@{Host}:{Port}";
            }
        }
    }
}