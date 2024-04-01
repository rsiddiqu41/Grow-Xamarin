using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Threading.Tasks;

namespace Grow.Core.ExternalInterfaces.Finance
{
    public interface IPlaidUIViewModel
    {
        Task<bool> OnSuccessCallback(Uri InUri);
        void ReturnToOverviewPage();
    }
}
