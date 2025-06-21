using KinclongIN.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using KinclongIN.Models;
using KinclongIN.DTO;
using Npgsql;


namespace KinclongIN.Models
{
    public class LoginContext
    {
        private string __constr;
        private string __ErrorMsg;
        public string ErrorMessage => __ErrorMsg;

        public LoginContext(string pConstr)
        {
            __constr = pConstr;
        }
        public List<Login> Autentifikasi(string p_username, string p_password, IConfiguration p_config)
        {
            List<Login> list1 = new List<Login>();
            string query = string.Format(@"SELECT ps.id_person, ps.nama, ps.alamat,
                            ps.email, pp.id_peran, p.nama_peran
                            FROM person ps
                            INNER JOIN peran_person pp ON ps.id_person = pp.id_person
                            INNER JOIN peran p ON pp.id_peran = p.id_peran
                            WHERE ps.email = '{0}' AND ps.password = '{1}'", p_username, p_password);
            sqlDBHelper db = new sqlDBHelper(this.__constr);
            try
            {
                NpgsqlCommand cmd = db.getNpgsqlCommand(query);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list1.Add(new Login()
                    {
                        id_person = int.Parse(reader["id_person"].ToString()),
                        nama = reader["nama"].ToString(),
                        alamat = reader["alamat"].ToString(),
                        email = reader["email"].ToString(),
                        id_peran = int.Parse(reader["id_peran"].ToString()),
                        nama_peran = reader["nama_peran"].ToString(),
                        token = GenerateJwtToken(p_username, p_config) // Fixing the method call
                    });
                }
                cmd.Dispose();
                db.closeConnection();
            }
            catch (Exception ex)
            {
                this.__ErrorMsg = ex.Message;
            }
            return list1;
        }

        public bool Register(RegisterDTO registerDto, IConfiguration config)
        {
            bool isRegistered = false;
            string checkUserQuery = "SELECT COUNT(*) FROM person WHERE email = @Email";
            string insertPersonQuery = @"INSERT INTO person (email, password) 
                                         VALUES (@Email, @Password) RETURNING id_person";

            sqlDBHelper db = new sqlDBHelper(this.__constr);
            try
            {
                // Check if user already exists
                NpgsqlCommand checkCmd = db.getNpgsqlCommand(checkUserQuery);
                checkCmd.Parameters.AddWithValue("@Email", registerDto.Email);
                int userCount = Convert.ToInt32(checkCmd.ExecuteScalar());
                checkCmd.Dispose();

                if (userCount > 0)
                {
                    db.closeConnection();
                    return false; // User already exists
                }

                // Insert new user (only email and password)
                NpgsqlCommand insertCmd = db.getNpgsqlCommand(insertPersonQuery);
                insertCmd.Parameters.AddWithValue("@Email", registerDto.Email);
                insertCmd.Parameters.AddWithValue("@Password", registerDto.Password);
                insertCmd.ExecuteScalar();
                insertCmd.Dispose();

                db.closeConnection();
                isRegistered = true;
            }
            catch (Exception ex)
            {
                this.__ErrorMsg = ex.Message;
                Console.WriteLine("Register ERROR: " + ex.Message); // Untuk debug
                db.closeConnection();
                isRegistered = false;
            }
            return isRegistered;
        }

        private string GenerateJwtToken(string username, IConfiguration pConfig) // Fixing the method signature
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(pConfig["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                            new Claim(JwtRegisteredClaimNames.Sub, username),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                        };

            var token = new JwtSecurityToken(
                issuer: pConfig["Jwt:Issuer"],
                audience: pConfig["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
