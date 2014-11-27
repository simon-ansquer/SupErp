using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfControlLibrarySalaire.Models;

namespace WpfControlLibrarySalaire.ViewModels
{
    public class EmployeeHistoryViewModel
    {
        List<HistoriqueSalaire> ListHistoriqueSalaire;
        
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
