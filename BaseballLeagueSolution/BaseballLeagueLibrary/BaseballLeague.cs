using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace BaseballLeagueLibrary
{
    //Name : Banuka Kumara AMbegoda
    //Date : 2022-06-23
    //Lab 45
    public class BaseballLeague
    {
        private IDbConnection db;

        public BaseballLeague(string connStr)
        {
            db = new SqlConnection(connStr);
        }
        public List<Player> GetPlayerData(string playerLastName, string playerBattingAvgLow, string playerBattingAvgHigh)
        {
            string sql = "usp_PlayerSelectByID";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@playerLastName", playerLastName);
            parameters.Add("@playerBattingAvgLow", playerBattingAvgLow);
            parameters.Add("@playerBattingAvgHigh", playerBattingAvgHigh);

            try
            {
                List<Player> list = db.Query<Player>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
                return list;
            }
            catch(Exception ex)
            {
                throw ex;
            }
                
        }
        public Player InsertDepartment(Player player)
        {
            string sql = "INSERT INTO PLAYER (playerFirstName, playerLastName, playerBattingAverage, playerRunsScored) " +
                            "VALUES (@playerFirstName, @playerLastName, @playerBattingAverage, @playerRunsScored) " +
                            "SELECT @@ROWCOUNT";        

            int rowsAffected = db.Query<int>(sql, player).SingleOrDefault();
            if (rowsAffected == 0) throw new Exception("Insert Failed");
            return player;
        }
        public void DeletePlayer(int playerID)
        {
            string sql = "DELETE FROM PLAYER WHERE playerID = @playerID";

            int rowsAffected = db.Execute(sql, new { playerID });

            if (rowsAffected == 0) throw new Exception("Delete failed");
        }

    }
}
