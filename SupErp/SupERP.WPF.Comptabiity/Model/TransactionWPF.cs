using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupERP.WPF.Comptabiity.Model
{
    public class TransactionWPF
    {
        public String libelle { get; set; }
        public float montant { get; set; }
        public DateTime date { get; set; }
        public bool paye { get; set; }
        public String client { get; set; }

        public TransactionWPF(String _libelle, float _montant, DateTime _date, bool _paye, String _client)
        {
            libelle = _libelle;
            montant = _montant;
            date = _date;
            paye = _paye;
            client = _client;
        }
    }
}
