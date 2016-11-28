namespace Comm.Cloud.RDS.DTO
{
    public class PublicCloudRdsConfig
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public string Uid { get; set; }
        public string Pwd { get; set; }
        public int Port { get; set; }

        public string GetDbConnectString()
        {
            return string.Format("Server={0};Database={1};Uid={2};Pwd={3};charset=utf8;",
                Server, Database, Uid, Pwd);
        }
        public string GetSqlServerConnectString()
        {
            return string.Format("Server={0};Database={1};Uid={2};Pwd={3};",
                Server, Database, Uid, Pwd);
        }
        public string GetMySqlConnectString()
        {
            return string.Format("Server={0};Port={4};Database={1};Uid={2};Pwd={3};charset=utf8;Max Pool Size=512;default command timeout=30;",
                Server, Database, Uid, Pwd,Port);
        }
        //Server=myServerAddress; Port=1234; Database=myDataBase; Uid=myUsername; Pwd=myPassword;
        //public string GetMySqlConnectString()
        //{
        //    return string.Format("Server={0};Database={1};Uid={2};Pwd={3};charset=utf8;",
        //        Server, Database, Uid, Pwd);
        //}
    }
}
