using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using SpoonacularConcept.Models;
using System.Configuration;
using SpoonacularConcept.Models.ViewModels;

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
        public int MarkFavourite(dynamic Info)
        {
            using (var cmd=new SqlCommand("markFavourite", _sqlConn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                var recipe = new AddToLikeCart();
          
                cmd.Parameters.Add("@RecipeId", System.Data.SqlDbType.SmallInt).Value = Info.recipe.RecipeId;
                cmd.Parameters.Add("@UserId", System.Data.SqlDbType.TinyInt).Value = Info.userId;
                cmd.Parameters.Add("@Image", System.Data.SqlDbType.VarChar).Value = Info.Image;
                cmd.Parameters.Add("@Summary", System.Data.SqlDbType.VarChar).Value = Info.Summary;
                cmd.Parameters.Add("@Title", System.Data.SqlDbType.VarChar).Value = Info.Title;
                cmd.Parameters.Add("@Servings", System.Data.SqlDbType.SmallInt).Value = Info.Servings;
                cmd.Parameters.Add("@Price", System.Data.SqlDbType.SmallInt).Value = Info.Price;
                cmd.Parameters.Add("@Score", System.Data.SqlDbType.TinyInt).Value = Info.Score;
                cmd.Parameters.Add("@Time", System.Data.SqlDbType.Time).Value = Info.Time;

                cmd.Parameters.Add("@cartId", System.Data.SqlDbType.SmallInt).Direction= System.Data.ParameterDirection.Output;
                _sqlConn.Open();
                cmd.ExecuteNonQuery();
                var cartId = Convert.ToInt32(cmd.Parameters["@cartId"].Value);
                _sqlConn.Close();

                return cartId;

            }
        }
        
    }
}