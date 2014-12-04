using System;

namespace WpfControlLibrarySalaire.Models
{
    public class HistoriqueSalaire
    {
        private readonly  DateTime _dateSalaire;

        public DateTime DateSalaire
        {
            get { return _dateSalaire; }
        }

        private readonly Double _salaire;

        public Double Salaire
        {
            get { return _salaire; }
        }

        public HistoriqueSalaire()
        {

        }

        public HistoriqueSalaire(DateTime dateSalaire, Double salaire)
        {
            _dateSalaire = dateSalaire;
            _salaire = salaire;
        }

    }
}
