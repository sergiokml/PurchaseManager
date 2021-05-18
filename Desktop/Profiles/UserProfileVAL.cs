
using PurchaseData.DataModel;

using PurchaseDesktop.Helpers;

namespace PurchaseDesktop.Profiles
{

    public class UserProfileVAL : HFunctions
    {
        private readonly PurchaseManagerContext rContext;
        public Users CurrentUser { get; set; }

        public UserProfileVAL(PurchaseManagerContext rContext)
        {
            this.rContext = rContext;
        }
    }
}
