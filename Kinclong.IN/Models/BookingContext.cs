using Npgsql;
using KinclongIN.Helpers;
using System;
using System.Collections.Generic;
using Kinclong.IN.Models;

namespace KinclongIN.Models
{
    public class BookingContext
    {
        private string __constr;
        private string __ErrorMsg;

        public BookingContext(IConfiguration configuration)
        {
            __constr = configuration.GetConnectionString("WebApiDatabase");
        }


        // Mendapatkan semua bookings
        public List<Bookings> ListBookings()
        {
            List<Bookings> list = new List<Bookings>();
            string query = "SELECT id_booking, booking_date, booking_status, created_at, updated_at, IdUser FROM bookings";
            sqlDBHelper db = new sqlDBHelper(this.__constr);

            try
            {
                NpgsqlCommand cmd = db.getNpgsqlCommand(query);
                NpgsqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Bookings
                    {
                        id_booking = Convert.ToInt32(reader["id_booking"]),
                        booking_date = Convert.ToDateTime(reader["booking_date"]),
                        booking_status = reader["booking_status"].ToString(),
                        created_at = Convert.ToDateTime(reader["created_at"]),
                        updated_at = Convert.ToDateTime(reader["updated_at"]),
                        IdUser = Convert.ToInt32(reader["IdUser"])
                    });
                }

                cmd.Dispose();
                db.closeConnection();
            }
            catch (Exception ex)
            {
                __ErrorMsg = ex.Message;
            }

            return list;
        }

        // Mendapatkan booking berdasarkan ID
        public Bookings GetBookingById(int id)
        {
            Bookings booking = null;
            string query = "SELECT id_booking, booking_date, booking_status, created_at, updated_at, IdUser FROM bookings WHERE id_booking = @id";
            sqlDBHelper db = new sqlDBHelper(this.__constr);

            try
            {
                NpgsqlCommand cmd = db.getNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@id", id);
                NpgsqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    booking = new Bookings
                    {
                        id_booking = Convert.ToInt32(reader["id_booking"]),
                        booking_date = Convert.ToDateTime(reader["booking_date"]),
                        booking_status = reader["booking_status"].ToString(),
                        created_at = Convert.ToDateTime(reader["created_at"]),
                        updated_at = Convert.ToDateTime(reader["updated_at"]),
                        IdUser = Convert.ToInt32(reader["IdUser"])
                    };
                }

                cmd.Dispose();
                db.closeConnection();
            }
            catch (Exception ex)
            {
                __ErrorMsg = ex.Message;
            }

            return booking;
        }

        // Menambahkan booking baru
        public bool AddBooking(Bookings booking)
        {
            bool result = false;
            string query = @"INSERT INTO bookings (booking_date, booking_status, created_at, updated_at, IdUser)
                             VALUES (@booking_date, @booking_status, @created_at, @updated_at, @IdUser)";
            sqlDBHelper db = new sqlDBHelper(this.__constr);

            try
            {
                NpgsqlCommand cmd = db.getNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@booking_date", booking.booking_date);
                cmd.Parameters.AddWithValue("@booking_status", booking.booking_status);
                cmd.Parameters.AddWithValue("@created_at", booking.created_at);
                cmd.Parameters.AddWithValue("@updated_at", booking.updated_at);
                cmd.Parameters.AddWithValue("@IdUser", booking.IdUser);

                int rowsAffected = cmd.ExecuteNonQuery();
                result = rowsAffected > 0;

                cmd.Dispose();
                db.closeConnection();
            }
            catch (Exception ex)
            {
                __ErrorMsg = ex.Message;
            }

            return result;
        }

        // Memperbarui status booking
        public bool UpdateBookingStatus(int id, string newStatus)
        {
            bool result = false;
            string query = "UPDATE bookings SET booking_status = @booking_status, updated_at = @updated_at WHERE id_booking = @id";
            sqlDBHelper db = new sqlDBHelper(this.__constr);

            try
            {
                NpgsqlCommand cmd = db.getNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@booking_status", newStatus);
                cmd.Parameters.AddWithValue("@updated_at", DateTime.Now);

                int rowsAffected = cmd.ExecuteNonQuery();
                result = rowsAffected > 0;

                cmd.Dispose();
                db.closeConnection();
            }
            catch (Exception ex)
            {
                __ErrorMsg = ex.Message;
            }

            return result;
        }

        // Menghapus booking berdasarkan ID
        public bool DeleteBooking(int id)
        {
            bool result = false;
            string query = "DELETE FROM bookings WHERE id_booking = @id";
            sqlDBHelper db = new sqlDBHelper(this.__constr);

            try
            {
                NpgsqlCommand cmd = db.getNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@id", id);

                int rowsAffected = cmd.ExecuteNonQuery();
                result = rowsAffected > 0;

                cmd.Dispose();
                db.closeConnection();
            }
            catch (Exception ex)
            {
                __ErrorMsg = ex.Message;
            }

            return result;
        }
    }
}
