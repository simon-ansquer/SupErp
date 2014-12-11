using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupERP.WPF.Comptabiity.Model
{
    public class TransactionWPFJournal
    {
         public String libelle { get; set; }
        public decimal montant { get; set; }
        public DateTime date { get; set; }
        public bool paye { get; set; }
        public String client { get; set; }
        public String compte { get; set; }

        public TransactionWPFJournal(String _libelle, decimal _montant, DateTime _date, bool _paye, String _client, String _compte)
        {
            libelle = _libelle;
            montant = _montant;
            date = _date;
            paye = _paye;
            client = _client;
            compte = _compte;
        }
    }
}
