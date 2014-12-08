using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupErp.Entities;
using SupErp.IHM.UserServices;

namespace SupErp.IHM.Helpers
{
    public static class WCFManager
    {

        /// <summary>
        /// The user service client
        /// </summary>
        private static UserServices.UserServiceClient userServiceClient;

        /// <summary>
        /// Gets the user service client.
        /// </summary>
        /// <value>
        /// The user service client.
        /// </value>
        public static UserServices.UserServiceClient UserServiceClient
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
