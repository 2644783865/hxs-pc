using System;
using SQLiteSugar;

namespace MaSoft.Code.Dao
{
    public class SugarDao
    {
        //禁止实例化
        private SugarDao()
        {

        }
        public static string ConnectionString
        {
            get
            {
                return "DataSource=" + AppDomain.CurrentDomain.BaseDirectory + "Data\\Masoft.Data.db";
            }
        }
        public static SqlSugarClient GetInstance()
        {

            var db = new SqlSugarClient(ConnectionString)
            {
                //启用日志事件
                IsEnableLogEvent = true ,
                LogEventStarting = (sql, par) => { Log.Debug(sql + " " + par + "\r\n"); }
            };
            return db;
        }
    }
}
