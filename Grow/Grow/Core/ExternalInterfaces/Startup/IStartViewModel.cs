using Grow.Core.SystemConfiguration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grow.Core.ExternalInterfaces
{
    public interface IStartViewModel
    {
        List<SystemStartLayoutRecords> LoadItemsForStart();
    }
}
