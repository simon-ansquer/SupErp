using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfControlLibrarySalaire.Models
{
    class HistoriqueSalaire
    {
        DateTime DateSalaire;
        Double Salaire;

        public HistoriqueSalaire()
        {

        }

        public HistoriqueSalaire(DateTime _dateSalaire, Double _Salaire)
        {
            DateSalaire = _dateSalaire;
            Salaire = _Salaire;
        }

    }
}
