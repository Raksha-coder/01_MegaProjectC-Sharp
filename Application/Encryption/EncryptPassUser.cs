﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Encryption
{
    public class EncryptPassUser
    {
        //The hash algorithm used is SHA256.
        /*
         The hash result in the byte array is converted to a base 64 string,
        which is then re-entered as a password 
        parameter into the ComputeHash() function until 
        the iteration process is complete.
         
         */
        public  string ComputeHash(string password, string salt, string pepper, int iteration)
        {
            if (iteration <= 0) return password;

            using var sha256 = SHA256.Create();
            //The process of combining password, salt, and pepper
            var passwordSaltPepper = $"{password}{salt}{pepper}";
            var byteValue = Encoding.UTF8.GetBytes(passwordSaltPepper);
            var byteHash = sha256.ComputeHash(byteValue);
            var hash = Convert.ToBase64String(byteHash);
            return ComputeHash(hash, salt, pepper, iteration - 1);
        }


        //GenerateSalt() is a function to generate a salt from random bytes
        //and convert it as a base 64 string
        public  string GenerateSalt()
        {
            using var rng = RandomNumberGenerator.Create();
            var byteSalt = new byte[16];
            rng.GetBytes(byteSalt);
            var salt = Convert.ToBase64String(byteSalt);
            return salt;
        }

    }
}
