using System;
using System.Collections.Generic;
using WpfControlLibrarySalaire.Models;

namespace WpfControlLibrarySalaire.ViewModels
{
    public class EmployeeHistoryViewModel
    {
        private List<HistoriqueSalaire> _listHistoriqueSalaire;

        public List<HistoriqueSalaire> ListHistoriqueSalaire
        {
            get { return _listHistoriqueSalaire; }
            set { if(value != null) _listHistoriqueSalaire = value;}
        }
        
        public EmployeeHistoryViewModel()
        {
            RandomizeData();
        }
 
    
    
    private void RandomizeData()
    {
        ListHistoriqueSalaire = new List<HistoriqueSalaire>();

        for (int i = 0; i < 10; i++)
        {
            ListHistoriqueSalaire.Add(new HistoriqueSalaire(DateTime.Now, Convert.ToDouble(i)));
        }

    } 
    }   

}
