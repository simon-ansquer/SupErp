using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupErp.IHM.ServiceReference1;

namespace SupErp.IHM.Helpers
{
    public static class WCFManager
    {

        /// <summary>
        /// The user service client
        /// </summary>
        private static ServiceReference1.UserServiceClient userServiceClient;

        /// <summary>
        /// Gets the user service client.
        /// </summary>
        /// <value>
        /// The user service client.
        /// </value>
        public static ServiceReference1.UserServiceClient UserServiceClient
        {
            get
            {
                if(userServiceClient != null)
                    return UserServiceClient;

                return  new UserServiceClient();
            }
        }
    }
}
