using APIformbuilder.Domains.DTOs;
using APIformbuilder.Models;
using APIformbuilder.Service.Interface;
using Microsoft.IdentityModel.Tokens;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APIformbuilder.Service.Concrete
{
    public class AutenticacionService: IAutenticacionService
    {
        private readonly string cadenaSQL;
        private readonly string secretKey;
        public AutenticacionService(IConfiguration config)
        {
            secretKey = config.GetSection("settings").GetSection("secretkey").ToString();
            cadenaSQL = config.GetConnectionString("CadenaSQL");
        }
        public async Task<ValidarDTO> validar(LogUsuario request)
        {
            ValidarDTO validar = new ValidarDTO();
            try
            {

                int id_user; // Declarar la variable id_user
                bool usuarioValido = ValidarUsuario(request.Username, request.Password, out id_user, out int roleId); // Pasar id_user como parámetro
                if (usuarioValido)
                {
                    var keyBytes = Encoding.ASCII.GetBytes(secretKey);
                    var claims = new ClaimsIdentity();
                    claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, request.Username));
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = claims,
                        Expires = DateTime.UtcNow.AddMinutes(30),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);
                    string tokenCreado = tokenHandler.WriteToken(tokenConfig);

                    List<int> idPermisos = ObtenerIdPermisos(id_user);

                    var NombreUsuario = request.Username;
                    validar.token = tokenCreado;
                    validar.usuario = NombreUsuario;
                    validar.id_usuario = id_user;
                    validar.permisos = idPermisos;
                    validar.rol = roleId;

                }
                else
                {
                    throw new Exception($"Ususario incorrecto o no existe");

                    //StatusCode(StatusCodes.Status401Unauthorized, new { mensaje = "El usuario es incorrecto o no existe" });
                }
            }
            catch (Exception ex)
            {
                validar.mensaje = ex.Message;
            }
            return validar;
        }
        private bool ValidarUsuario(string username, string password, out int id_user, out int roleId) // Agregar el parámetro out para obtener id_user
        {
            using (var conexion = new SqlConnection(cadenaSQL))
            {
                conexion.Open();

                // Modifica la consulta SQL para obtener el id_user
                string query = "SELECT Username, Password, UserID, RoleID FROM Users WHERE Username = @Username AND fecha_eliminacion IS NULL";
                using (var command = new SqlCommand(query, conexion))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string storedPasswordHash = reader["Password"].ToString(); // Suponemos que la contraseña se almacena como hash
                            id_user = Convert.ToInt32(reader["UserID"]); // Obtiene el id usuario
                            roleId = Convert.ToInt32(reader["RoleID"]);

                            // Aquí debes verificar si la contraseña proporcionada coincide con el hash almacenado
                            // Utiliza la función VerifyPassword que mencioné anteriormente para comparar la contraseña proporcionada con el hash almacenado
                            if (VerifyPassword(password, storedPasswordHash))
                            {
                                return true; // La autenticación fue exitosa
                            }
                        }
                    }
                }
            }
            id_user = -1; // Establece id_user en un valor predeterminado si la autenticación falla
            roleId = -1;
            return false; // El usuario no existe o la contraseña no coincide
        }
        private bool VerifyPassword(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
        private List<int> ObtenerIdPermisos(int id_user)
        {
            List<int> idPermisos = new List<int>();

            using (var conexion = new SqlConnection(cadenaSQL))
            {
                conexion.Open();

                string query = "SELECT permisoId FROM permisos WHERE usuarioId = @IdUser";
                using (var command = new SqlCommand(query, conexion))
                {
                    command.Parameters.AddWithValue("@IdUser", id_user);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            idPermisos.Add(Convert.ToInt32(reader["permisoId"]));
                        }
                    }
                }
            }

            return idPermisos;
        }
    }

}
