using appWeb_BD.Data;
using appWeb_BD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;
using System.Security.Cryptography;
namespace appWeb_BD.Controllers
{
    public class RegisterController : Controller
    {
        private readonly Context _context;
        public RegisterController(Context context)
        {
            _context = context;
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(TestUsuario u)
        {
            string encryptedPass = EncryptPassword(u.Clave);
            try
            {
                using (SqlConnection connection = new SqlConnection(_context.Valor))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("test_agregarUsuario", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@tipo_usuario", u.TipoUsuario);
                        command.Parameters.AddWithValue("@tipo_identificacion", u.TipoIdentificacion);
                        command.Parameters.AddWithValue("@numero_identificacion", u.NumIdentificacion);
                        command.Parameters.AddWithValue("@nombre_completo", u.NombreCompleto);
                        command.Parameters.AddWithValue("@nombre_usuario", u.NombreUsuario);
                        command.Parameters.AddWithValue("@clave", encryptedPass);
                        command.Parameters.AddWithValue("@correo_electronico", u.CorreoElectronico);
                        command.Parameters.AddWithValue("@telefonos", u.Telefono);
                        command.Parameters.AddWithValue("@habilidades_blandas", u.HabilidadBlanda);
                        command.ExecuteNonQuery();
                    }
                }
                return RedirectToAction("Register", "Register");
            }
            catch (Exception) 
            {
                ViewData["error"] = "Error al registrar el usuario.";
                return View();
            }
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
