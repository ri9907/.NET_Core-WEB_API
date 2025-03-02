//using Entities;
//using Microsoft.Data.SqlClient;
//using Microsoft.Extensions.Configuration;
//using Repositories;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Repository
//{
//    public class RatingRepository : IRatingRepository
//    {
//        public IConfiguration _configuration { get; }

//        public RatingRepository(IConfiguration configuration)
//        {
//            _configuration = configuration;
//        }
//        public async Task<int> Add(Rating rating)
//        {

//            string query = "INSERT INTO RATING(HOST,METHOD,PATH,REFERER,USER_AGENT,Record_Date)" + "VALUES(@host,@method,@path,@referer,@user_agent,@record_date)";
//            using (SqlConnection cn = new SqlConnection(_configuration["ConnectionString"]))
//            using (SqlCommand cmd = new SqlCommand(query, cn))
//            {
//                cmd.Parameters.AddWithValue("@HOST", rating.Host);
//                cmd.Parameters.AddWithValue("@METHOD", rating.Method);
//                cmd.Parameters.AddWithValue("@PATH", rating.Path);
//                cmd.Parameters.AddWithValue("@REFERER", rating.Referer);
//                cmd.Parameters.AddWithValue("@USER_AGENT", rating.UserAgent);
//                cmd.Parameters.AddWithValue("@Record_Date", DateTime.Now);

//                cn.Open();
//                int rowsAffected = await cmd.ExecuteNonQueryAsync();
//                cn.Close();

//                return rowsAffected;
//            }

//        }
//    }
//}
using Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Repositories;
using System;
using System.Threading.Tasks;

namespace Repository
{
    public class RatingRepository : IRatingRepository
    {
        public IConfiguration _configuration { get; }

        public RatingRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> Add(Rating rating)
        {
            try
            {
                string query = "INSERT INTO RATING(HOST, METHOD, PATH, REFERER, USER_AGENT, Record_Date) " +
                               "VALUES(@HOST, @METHOD, @PATH, @REFERER, @USER_AGENT, @Record_Date)";

                using (SqlConnection cn = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]))
                {
                    using (SqlCommand cmd = new SqlCommand(query, cn))
                    {
                        cmd.Parameters.AddWithValue("@HOST", rating.Host);
                        cmd.Parameters.AddWithValue("@METHOD", rating.Method);
                        cmd.Parameters.AddWithValue("@PATH", rating.Path);
                        cmd.Parameters.AddWithValue("@REFERER", rating.Referer);
                        cmd.Parameters.AddWithValue("@USER_AGENT", rating.UserAgent);
                        cmd.Parameters.AddWithValue("@Record_Date", DateTime.Now);

                        cn.Open();
                        int rowsAffected = await cmd.ExecuteNonQueryAsync();
                        return rowsAffected;
                    }
                }
            }
            catch (SqlException ex)
            {
                // Handle SQL exception
                Console.WriteLine("SQL Error: " + ex.Message);
                Console.WriteLine("Error Code: " + ex.ErrorCode);
                Console.WriteLine("Stack Trace: " + ex.StackTrace);
                return -1;
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                Console.WriteLine("Error: " + ex.Message);
                Console.WriteLine("Stack Trace: " + ex.StackTrace);
                return -1;
            }
        }
    }
}
