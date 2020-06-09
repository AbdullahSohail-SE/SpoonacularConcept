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
            using (var cmd=new SqlCommand("AddToCart", _sqlConn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
               
          
                cmd.Parameters.Add("@RecipeId", System.Data.SqlDbType.Int).Value = Info.recipe.recipeId;
                cmd.Parameters.Add("@UserId", System.Data.SqlDbType.TinyInt).Value = Info.userId;
                cmd.Parameters.Add("@Image", System.Data.SqlDbType.NVarChar).Value = Info.recipe.Image;
                cmd.Parameters.Add("@Summary", System.Data.SqlDbType.VarChar).Value = Info.recipe.Summary;
                cmd.Parameters.Add("@Title", System.Data.SqlDbType.VarChar).Value = Info.recipe.Title;
                cmd.Parameters.Add("@Servings", System.Data.SqlDbType.SmallInt).Value = Info.recipe.Servings;
                cmd.Parameters.Add("@Price", System.Data.SqlDbType.SmallInt).Value = Info.recipe.Price;
                cmd.Parameters.Add("@Score", System.Data.SqlDbType.TinyInt).Value = Info.recipe.Score;
                cmd.Parameters.Add("@Time", System.Data.SqlDbType.VarChar).Value = Info.recipe.Time;

                cmd.Parameters.Add("@cartId", System.Data.SqlDbType.SmallInt).Direction= System.Data.ParameterDirection.Output;
                _sqlConn.Open();
                cmd.ExecuteNonQuery();
                if (cmd.Parameters["@cartId"].Value == DBNull.Value)
                    return -1;
                var cartId = Convert.ToInt32(cmd.Parameters["@cartId"].Value);
                _sqlConn.Close();

                return cartId;

            }
        }
        public void UnmarkFavourite(int recipeId,int userId)
        {
            using (var cmd = new SqlCommand("RemoveFromCart", _sqlConn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@RecipeId", System.Data.SqlDbType.Int).Value = recipeId;
                cmd.Parameters.Add("@UserId", System.Data.SqlDbType.TinyInt).Value = userId;

                _sqlConn.Open();
                cmd.ExecuteNonQuery();
                _sqlConn.Close();
            }
        }
        public int GetLikeCount(int userId)
        {
            using (var cmd = new SqlCommand("GetLikeCount", _sqlConn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@userId", System.Data.SqlDbType.TinyInt).Value = userId;
                cmd.Parameters.Add("@likeCount", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.Output;

                _sqlConn.Open();
                cmd.ExecuteNonQuery();
                var likesCount = Convert.ToInt32(cmd.Parameters["@likeCount"].Value);
                _sqlConn.Close();
                return likesCount;
            }

        }

        public int PurchaseIngredients(List<Ingredient> ingredientsList,int userId)
        {
            using (var cmd = new SqlCommand("PurchaseIngredients", _sqlConn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;




                _sqlConn.Open();
                foreach (var ingredient in ingredientsList)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("@cartId", System.Data.SqlDbType.SmallInt).Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add("@UserId", System.Data.SqlDbType.TinyInt).Value = userId;
                    cmd.Parameters.Add("@IngredientId", System.Data.SqlDbType.Int).Value = ingredient.IngredientId;
                    cmd.Parameters.Add("@Name", System.Data.SqlDbType.VarChar).Value = ingredient.Name;
                    cmd.Parameters.Add("@Image", System.Data.SqlDbType.VarChar).Value = ingredient.Image;
                    cmd.Parameters.Add("@Unit", System.Data.SqlDbType.VarChar).Value = ingredient.Unit;
                    cmd.Parameters.Add("@Amount", System.Data.SqlDbType.SmallInt).Value = ingredient.Amount;

                    cmd.ExecuteNonQuery();
                }
                _sqlConn.Close();

                return 0;

            }

        }
        
    }
}