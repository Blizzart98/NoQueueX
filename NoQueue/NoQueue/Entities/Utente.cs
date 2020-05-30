using System;
using System.Collections.Generic;
using System.Text;

namespace NoQueue.Entities
{
    public class Utente
    {
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public Utente(string nome, string cognome, string email, string password)
        {
            this.Nome = nome;
            this.Cognome = cognome;
            this.Email = email;
            this.Password = password;
        }

    }
}
