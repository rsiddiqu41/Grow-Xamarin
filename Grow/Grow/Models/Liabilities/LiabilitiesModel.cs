using System;
using System.Collections.Generic;
using System.Text;

namespace Grow.Models.Liabilities
{
    public class LiabilitiesModel
    {
        /// <summary>
        /// 
        /// Stores a dictionary of all the investment transactions associated with a user in key-value pairs for fast lookup times
        /// 
        /// Key: AccountId - The Id of the account that the liability belongs to
        /// 
        /// Value: The liability object associated with the account id
        /// 
        /// </summary>
        public Dictionary<string, List<LiabilitiesCreditModel>> CreditLiabilitiesDict { get; set; }
        public Dictionary<string, List<LiabilitiesMortgageModel>> MortgageLiabilitiesDict { get; set; }
        public Dictionary<string, List<LiabilitiesStudentModel>> StudentLiabilitiesDict { get; set; }

        public LiabilitiesModel()
        {
            CreditLiabilitiesDict = new Dictionary<string, List<LiabilitiesCreditModel>>();
            MortgageLiabilitiesDict = new Dictionary<string, List<LiabilitiesMortgageModel>>();
            StudentLiabilitiesDict = new Dictionary<string, List<LiabilitiesStudentModel>>();
        }

    }
}
