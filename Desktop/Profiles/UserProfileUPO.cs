
using PurchaseData.DataModel;

using PurchaseDesktop.Helpers;

namespace PurchaseDesktop.Profiles
{
    public class UserProfileUPO : HFunctions
    {
        private readonly PurchaseManagerContext rContext;
        public Users CurrentUser { get; set; }

        public UserProfileUPO(PurchaseManagerContext rContext)
        {
            this.rContext = rContext;
        }
    }
}
