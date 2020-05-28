using System;
using System.Collections.Generic;
using System.Text;

namespace NoQueue.Entities
{
    public class Utente
    {
        public string UserId { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public Utente()
        {

        }
    }
}
