using System.Data;
using Npgsql;
using KinclongIN.Models;
using KinclongIN.DTO;

namespace KinclongIN.Models
{
    public class ReviewContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public ReviewContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("WebApiDatabase");
        }

        public bool AddReview(Review review)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                string query = @"INSERT INTO review (id_user, id_order, rating, comment, created_at)
                                 VALUES (@id_user, @id_order, @rating, @comment, @created_at)";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id_user", review.IdUser);
                    command.Parameters.AddWithValue("@id_order", review.IdOrder);
                    command.Parameters.AddWithValue("@rating", review.Rating);
                    command.Parameters.AddWithValue("@comment", review.Comment ?? "");
                    command.Parameters.AddWithValue("@created_at", DateTime.Now);

                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    return result > 0;
                }
            }
        }

        public bool DeleteReview(int reviewId, int userId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                string query = @"DELETE FROM review 
                                 WHERE id_review = @id_review AND id_user = @id_user";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id_review", reviewId);
                    command.Parameters.AddWithValue("@id_user", userId);

                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    return result > 0;
                }
            }
        }
    }
}
