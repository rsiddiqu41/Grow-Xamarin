using System;
using System.Collections.Generic;
using System.Text;

using Grow.Core.SystemConfiguration;

namespace Grow.Models
{
    public interface ISystemLayoutModel
    {
        void ReadConfigFileForStartupScreen();

        SystemStartScreen SystemStartLayoutScreen { get; }

        void ReadConfigFileForFinancialQuotesView();

        FinancialQuote FinanceQuoteLayoutScreen { get; }

    }
}
