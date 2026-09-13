using System;
using System.Security.Cryptography;

namespace AnaYAntonio_ProyectoInmobiliaria.Models
{
    public static class PasswordHelper
    {
        private const int SaltSize = 16;

        private const int KeySize = 32;

        private const int Iterations = 100000;


        public static string HashPassword(string password)
        {
            // Generamos una sal aleatoria
            byte[] salt = new byte[SaltSize];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }


            // Generamos el hash usando PBKDF2 + SHA256
            byte[] hash;

            using (var pbkdf2 = new Rfc2898DeriveBytes(
                password,
                salt,
                Iterations,
                HashAlgorithmName.SHA256))
            {
                hash = pbkdf2.GetBytes(KeySize);
            }


            // Guardamos:
            // Iteraciones.Salt.Hash
            return Iterations + "." +
                   Convert.ToBase64String(salt) + "." +
                   Convert.ToBase64String(hash);
        }


        public static bool VerifyPassword(
            string password,
            string storedPassword)
        {
            try
            {
                string[] partes =
                    storedPassword.Split('.');


                if (partes.Length != 3)
                {
                    return false;
                }


                int iterations =
                    int.Parse(partes[0]);


                byte[] salt =
                    Convert.FromBase64String(partes[1]);


                byte[] storedHash =
                    Convert.FromBase64String(partes[2]);


                byte[] hash;

                // Generamos nuevamente el hash
                // usando la misma contraseña y la misma sal
                using (var pbkdf2 = new Rfc2898DeriveBytes(
                    password,
                    salt,
                    iterations,
                    HashAlgorithmName.SHA256))
                {
                    hash =
                        pbkdf2.GetBytes(
                            storedHash.Length
                        );
                }


                // Comparación segura
                return CryptographicOperations.FixedTimeEquals(
                    hash,
                    storedHash
                );
            }
            catch
            {
                return false;
            }
        }
    }
}