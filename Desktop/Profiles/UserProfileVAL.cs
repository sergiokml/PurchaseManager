
using PurchaseData.DataModel;

using PurchaseDesktop.Helpers;

namespace PurchaseDesktop.Profiles
{

    public class UserProfileVAL : HFunctions
    {
        private readonly PurchaseManagerEntities rContext;
        public Users CurrentUser { get; set; }

        public UserProfileVAL(PurchaseManagerEntities rContext)
        {
            this.rContext = rContext;
        }
    }
}
