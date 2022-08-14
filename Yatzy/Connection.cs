using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FireSharp;
using FireSharp.Config;

namespace Yatzy
{
    internal class Connection
    {
        public static FirebaseClient Client { get; set; }

        public static bool Setup()
        {
            var config = new FirebaseConfig
            {
                AuthSecret = "kRXrNBhkUeuCs2AhSpTyhYcshczcg993GJEhFqYh",
                BasePath = "https://maxi-yatzy-maxi-default-rtdb.europe-west1.firebasedatabase.app/"
            };

            Client = new FireSharp.FirebaseClient(config);

            return Client != null;
        }
    }
}
