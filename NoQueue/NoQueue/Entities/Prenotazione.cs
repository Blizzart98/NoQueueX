using System;
using System.Collections.Generic;
using System.Text;

namespace NoQueue.Entities
{
    class Prenotazione
    {

        public string citta { get; set; }
        public string negozio { get; set; }
        public string data { get; set; }
        public int ora { get; set; }
        public int minuti { get; set; }
        public long ts { get; set; }


        public Prenotazione(string citta, string negozio, string data, int ora, int minuti, long ts)
        {
            this.citta = citta;
            this.negozio = negozio;
            this.data = data;
            this.ora = ora;
            this.minuti = minuti;
            this.ts = ts;
        }
    }
}
