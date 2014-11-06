using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SupERP.Comptability.Front.Contract
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" à la fois dans le code et le fichier de configuration.
    public class Service1 : IService1
    {
        public string GetData ( int value )
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract ( CompositeType composite )
        {
            if ( composite == null )
            {
                throw new ArgumentNullException("composite");
            }
            if ( composite.BoolValue )
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
