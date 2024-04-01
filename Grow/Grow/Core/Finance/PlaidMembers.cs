using System;
using System.Collections.Generic;
using System.Text;

namespace Grow.Core.Finance
{
    public class PlaidMembers
    {
        public const string CLIENT_NAME = "Grow";
        public string[] UnitedStatesCountryCode = { "us" };
        public const string LANUAGE = "en";
        public string[] PRODUCTS = { "transactions", "investments", "liabilities" };
    }

    public struct PlaidAccountTypes
    {
        /// <summary>
        /// Older version of investment
        /// </summary>
        public const string BROKERAGE = "borkerage";

        /// <summary>
        /// Supported products for credit accounts are: Balance, Transactions, Identity, and Liabilities.
        /// </summary>
        public const string CREDIT = "credit";

        /// <summary>
        /// Supported products for depository accounts are: Auth (checking and savings types only), Balance, Transactions, Identity, Payment Initiation, and Assets.
        /// </summary>
        public const string DEPOSITORY = "depository";

        /// <summary>
        /// Supported products for investment accounts are: Balance and Investments. In API versions 2018-05-22 and earlier, this type is called brokerage.
        /// </summary>
        public const string INVESTMENT = "investment";

        /// <summary>
        /// Supported products for loan accounts are: Balance, Liabilities, and Transactions.
        /// </summary>
        public const string LOAN = "loan";

        /// <summary>
        /// Other or unknown account type. Supported products for other accounts are: Balance, Transactions, Identity, and Assets.
        /// </summary>
        public const string OTHER = "other";
    }
}
