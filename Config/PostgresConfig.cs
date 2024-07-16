namespace HouseInv.Config
{
    public class PostgresConfig
    {
        public required string Host { get; set; }
        public required string Port { get; set; }
        public required string Database { get; set; }
        public required string User { get; set; }
        public required string Password { get; set; }

        public string ConnectionString { 
            get
            {
                return $"Host={Host}; Port={Port}; Database={Database}; Username={User}; Password={Password}";
            } 
        }
    }
}