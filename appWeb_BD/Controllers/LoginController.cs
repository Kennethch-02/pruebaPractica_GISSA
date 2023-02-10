using Microsoft.AspNetCore.Mvc;
using appWeb_BD.Data;
using System.Data.SqlClient;
using System.Text;
using System.Security.Cryptography;
using appWeb_BD.Models;
using System.Data;
using static System.Net.Mime.MediaTypeNames;

namespace appWeb_BD.Controllers
{
    public class LoginController : Controller
    {
        private readonly Context _context;
        public LoginController(Context context)
        {
            _context = context;
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(LoginUserModel login)
        {
            var (resultado, tipoUsuario) = await VerificarInicioSesion(login.CorreoElectronico,login.Clave);
            Response.Cookies.Append("clave", Convert.ToString(resultado));
            Response.Cookies.Append("claveee", Convert.ToString(login.Clave));
            if (resultado)
            {
                Response.Cookies.Append("logged", tipoUsuario);
                if (tipoUsuario=="Administrador")
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Bienvenido", "Home");
                }
            }
            return View();
        }
        public async Task<(bool, string)> VerificarInicioSesion(string correo, string clave)
        {
            
            using (var connection = new SqlConnection(_context.Valor))
            {
                await connection.OpenAsync();

                var encryptedPass = EncryptPassword(clave);
                using (var command = new SqlCommand("test_verificarInicioSesion", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@correo", correo);
                    command.Parameters.AddWithValue("@clave", encryptedPass);

                    var resultado = command.Parameters.Add("@resultado", SqlDbType.Bit);
                    resultado.Direction = ParameterDirection.Output;

                    var tipoUsuarioParam = command.Parameters.Add("@tipo_usuario", SqlDbType.VarChar, 50);
                    tipoUsuarioParam.Direction = ParameterDirection.Output;

                    await command.ExecuteNonQueryAsync();

                    return ((bool)resultado.Value, (tipoUsuarioParam.Value != DBNull.Value) ? (string)tipoUsuarioParam.Value : null);
                }
            }
        }
        public ActionResult Logout()
        {
            Response.Cookies.Delete("user");
            return RedirectToAction("Login", "Login");
        }
        private static string EncryptPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
