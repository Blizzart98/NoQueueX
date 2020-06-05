using System;
using System.Collections.Generic;
using System.Text;

namespace NoQueue.Entities
{
    public class Prenotazione
    {
        public string citta { get; set; }
        public string negozio { get; set; }
        public int ora { get; set; }
        public int minuti { get; set; }
        public string data { get; set; }
        public long ts { get; set; }

        public Prenotazione(string citta, string negozio, int ora, int minuti, string data, long ts)
        {
            this.citta = citta;
            this.negozio = negozio;
            this.ora = ora;
            this.minuti = minuti;
            this.data = data;
            this.ts = ts;
        }
        public Prenotazione()
        {
 
        }

        public string GetCitta() { return citta; }
        public string GetNegozio() { return negozio; }
        public int GetOra() { return ora; }
        public int GetMinuti() { return minuti; }
        public string GetData() { return data; }
    }
}
