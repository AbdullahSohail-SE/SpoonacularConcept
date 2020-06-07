using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using SpoonacularConcept.Models;
using System.Configuration;

namespace SpoonacularConcept.Models
{
    
    public class DBManager
    {
        private SqlConnection _sqlConn;
        public DBManager(string conn)
        {
            var connectionString = ConfigurationManager.ConnectionStrings[conn].ConnectionString;
            _sqlConn = new SqlConnection(connectionString);
            
        }
        public AuthResult authenticateUser(User user)
        {
            using (var cmd = new SqlCommand("authenticateUser", _sqlConn))
            {

                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@email", System.Data.SqlDbType.VarChar).Value=user.Email;
                cmd.Parameters.Add("@password", System.Data.SqlDbType.VarChar).Value=user.Password;
                cmd.Parameters.Add("@status", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.Add("@userId", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

                _sqlConn.Open();
                cmd.ExecuteNonQuery();
                var status = Convert.ToInt32(cmd.Parameters["@status"].Value);
                var userId = Convert.IsDBNull(cmd.Parameters["@userId"].Value)? 0 : Convert.ToInt32(cmd.Parameters["@userId"].Value);
                _sqlConn.Close();

                return new AuthResult() { UserId = userId, Status = status };
            }
        }
        public int RegisterUser(User user)
        {
            using (var cmd=new SqlCommand("registerUser",_sqlConn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@name", System.Data.SqlDbType.VarChar).Value = user.Name;
                cmd.Parameters.Add("@email", System.Data.SqlDbType.VarChar).Value = user.Email;
                cmd.Parameters.Add("@password", System.Data.SqlDbType.VarChar).Value = user.Password;
                cmd.Parameters.Add("@userId", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;


                _sqlConn.Open();
                cmd.ExecuteNonQuery();
                var userId = Convert.ToInt32(cmd.Parameters["@userId"].Value);
                _sqlConn.Close();

                return userId;
            }
        }
    }
}