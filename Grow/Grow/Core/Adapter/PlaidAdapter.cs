using Acklann.Plaid.Entity;
using Grow.Models;
using Grow.Models.Liabilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grow.Core.Adapter
{
    class PlaidAdapter : IPlaidAdapter
    {
        public TransactionModel ConvertTransactionsResponseToModel(Acklann.Plaid.Entity.Transaction InPlaidTransaction)
        {
            TransactionModel returnModel = new TransactionModel
            {
                AccountId = InPlaidTransaction.AccountId,
                AccountOwner = InPlaidTransaction.AccountOwner,
                Amount = InPlaidTransaction.Amount,
                Pending = InPlaidTransaction.Pending,
                PendingTransactionId = InPlaidTransaction.PendingTransactionId,
                TransactionId = InPlaidTransaction.TransactionId,
                Name = InPlaidTransaction.Name,
                Address = InPlaidTransaction.Location.Address,
                City = InPlaidTransaction.Location.City,
                State = InPlaidTransaction.Location.State,
                Zip = InPlaidTransaction.Location.Zip,
                Country = InPlaidTransaction.Location.Country,
                Latitude = InPlaidTransaction.Location.Latitude,
                Longitude = InPlaidTransaction.Location.Longitude,
                StoreNumber = InPlaidTransaction.Location.StoreNumber,
                AuthorizedDate = InPlaidTransaction.AuthorizedDate,
                Date = InPlaidTransaction.Date,
                Categories = InPlaidTransaction.Categories,
                CategoryId = InPlaidTransaction.CategoryId,
                PayeeName = InPlaidTransaction.Payment.PayeeName,
                Payer = InPlaidTransaction.Payment.Payer,
                PaymentChannel = InPlaidTransaction.PaymentChannel,
                PaymentProcessor = InPlaidTransaction.Payment.PaymentProcessor,
                PPDId = InPlaidTransaction.Payment.PPDId,
                Reason = InPlaidTransaction.Payment.Reason,
                ReferenceNumber = InPlaidTransaction.Payment.ReferenceNumber,
                ByOrderOf = InPlaidTransaction.Payment.ByOrderOf,
                CurrencyCode = InPlaidTransaction.CurrencyCode,
                IsoCurrencyCode = InPlaidTransaction.IsoCurrencyCode,
                UnofficialCurrencyCode = InPlaidTransaction.UnofficialCurrencyCode
            };

            return returnModel;
        }

        public InvestmentHoldingModel ConvertInvestmentHoldingsResponseToModel(Acklann.Plaid.Entity.Holding InPlaidInvestmentHolding)
        {
            InvestmentHoldingModel returnInvestmentHolding = new InvestmentHoldingModel
            {
                AccountId = InPlaidInvestmentHolding.AccountId,
                CostBasis = InPlaidInvestmentHolding.CostBasis,
                CurrencyCode = InPlaidInvestmentHolding.CurrencyCode,
                InstitutionPrice = InPlaidInvestmentHolding.InstitutionPrice,
                InstitutionPriceAsOf = InPlaidInvestmentHolding.InstitutionPriceAsOf,
                InstitutionValue = InPlaidInvestmentHolding.InstitutionValue,
                IsoCurrencyCode = InPlaidInvestmentHolding.IsoCurrencyCode,
                Quantity = InPlaidInvestmentHolding.Quantity,
                SecurityId = InPlaidInvestmentHolding.SecurityId,
                UnofficialCurrencyCode = InPlaidInvestmentHolding.UnofficialCurrencyCode
            };

            return returnInvestmentHolding;
        }

        public InvestmentSecuritiesModel ConvertInvestmentSecuritiesResponseToModel(Acklann.Plaid.Entity.Security InPlaidInvestmentSecurity)
        {
            InvestmentSecuritiesModel returnSecuritiesModel = new InvestmentSecuritiesModel
            {
                ClosePrice = InPlaidInvestmentSecurity.ClosePrice,
                ClosePriceAsOf = InPlaidInvestmentSecurity.ClosePriceAsOf,
                CurrencyCode = InPlaidInvestmentSecurity.CurrencyCode,
                Cusip = InPlaidInvestmentSecurity.Cusip,
                InstitutionId = InPlaidInvestmentSecurity.InstitutionId,
                InstitutionSecurityId = InPlaidInvestmentSecurity.InstitutionSecurityId,
                IsCashEquivalent = InPlaidInvestmentSecurity.IsCashEquivalent,
                Isin = InPlaidInvestmentSecurity.Isin,
                IsoCurrencyCode = InPlaidInvestmentSecurity.IsoCurrencyCode,
                Name = InPlaidInvestmentSecurity.Name,
                ProxySecurityId = InPlaidInvestmentSecurity.ProxySecurityId,
                SecurityId = InPlaidInvestmentSecurity.SecurityId,
                Sedol = InPlaidInvestmentSecurity.Sedol,
                TickerSymbol = InPlaidInvestmentSecurity.TickerSymbol,
                UnofficialCurrencyCode = InPlaidInvestmentSecurity.UnofficialCurrencyCode
            };

            switch (InPlaidInvestmentSecurity.Type)
            {
                case Acklann.Plaid.Entity.SecurityType.Cash:
                    {
                        returnSecuritiesModel.Type = Models.SecurityType.Cash;
                        break;
                    }

                case Acklann.Plaid.Entity.SecurityType.Derivative:
                    {
                        returnSecuritiesModel.Type = Models.SecurityType.Derivative;
                        break;
                    }

                case Acklann.Plaid.Entity.SecurityType.Equity:
                    {
                        returnSecuritiesModel.Type = Models.SecurityType.Equity;
                        break;
                    }

                case Acklann.Plaid.Entity.SecurityType.Etf:
                    {
                        returnSecuritiesModel.Type = Models.SecurityType.Etf;
                        break;
                    }

                case Acklann.Plaid.Entity.SecurityType.FixedIncome:
                    {
                        returnSecuritiesModel.Type = Models.SecurityType.FixedIncome;
                        break;
                    }

                case Acklann.Plaid.Entity.SecurityType.Loan:
                    {
                        returnSecuritiesModel.Type = Models.SecurityType.Loan;
                        break;
                    }

                case Acklann.Plaid.Entity.SecurityType.MutualFund:
                    {
                        returnSecuritiesModel.Type = Models.SecurityType.MutualFund;
                        break;
                    }

                default:
                    returnSecuritiesModel.Type = Models.SecurityType.Other;
                    break;
            }

            return returnSecuritiesModel;
        }

        public InvestmentTransactionModel ConvertInvestmentTransactionsResponseToModel(Acklann.Plaid.Entity.InvestmentTransaction InPlaidInvestmentTransaction)
        {
            InvestmentTransactionModel returnInvestmentTransaction = new InvestmentTransactionModel
            {
                AccountId = InPlaidInvestmentTransaction.AccountId,
                Amount = InPlaidInvestmentTransaction.Amount,
                CurrencyCode = InPlaidInvestmentTransaction.CurrencyCode,
                Date = InPlaidInvestmentTransaction.Date,
                Fees = InPlaidInvestmentTransaction.Fees,
                InvestmentTransactionId = InPlaidInvestmentTransaction.InvestmentTransactionId,
                IsoCurrencyCode = InPlaidInvestmentTransaction.IsoCurrencyCode,
                Name = InPlaidInvestmentTransaction.Name,
                Price = InPlaidInvestmentTransaction.Price,
                Quantity = InPlaidInvestmentTransaction.Quantity,
                SecurityId = InPlaidInvestmentTransaction.SecurityId,
                UnofficialCurrencyCode = InPlaidInvestmentTransaction.UnofficialCurrencyCode
            };

            switch (InPlaidInvestmentTransaction.TransactionType)
            {
                case InvestmentTransactionType.Buy:
                    {
                        returnInvestmentTransaction.TransactionType = "Buy";
                        break;
                    }

                case InvestmentTransactionType.Cancel:
                    {
                        returnInvestmentTransaction.TransactionType = "Cancel";
                        break;
                    }

                case InvestmentTransactionType.Cash:
                    {
                        returnInvestmentTransaction.TransactionType = "Cash";
                        break;
                    }

                case InvestmentTransactionType.Fee:
                    {
                        returnInvestmentTransaction.TransactionType = "Fee";
                        break;
                    }

                case InvestmentTransactionType.Sell:
                    {
                        returnInvestmentTransaction.TransactionType = "Sell";
                        break;
                    }

                case InvestmentTransactionType.Transfer:
                    {
                        returnInvestmentTransaction.TransactionType = "Transfer";
                        break;
                    }

                default:
                    returnInvestmentTransaction.TransactionType = "None";
                    break;
            }

            switch (InPlaidInvestmentTransaction.TransactionSubType)
            {
                case InvestmentTransactionSubType.AccountFee:
                    {
                        returnInvestmentTransaction.TransactionSubType = "Account Fee";
                        break;
                    }

                case InvestmentTransactionSubType.Assignment:
                    {
                        returnInvestmentTransaction.TransactionSubType = "Assignment";
                        break;
                    }

                case InvestmentTransactionSubType.Buy:
                    {
                        returnInvestmentTransaction.TransactionSubType = "Buy";
                        break;
                    }

                case InvestmentTransactionSubType.BuyToCover:
                    {
                        returnInvestmentTransaction.TransactionSubType = "Buy To Cover";
                        break;
                    }

                case InvestmentTransactionSubType.Contribution:
                    {
                        returnInvestmentTransaction.TransactionSubType = "Contribution";
                        break;
                    }

                case InvestmentTransactionSubType.Deposit:
                    {
                        returnInvestmentTransaction.TransactionSubType = "Deposit";
                        break;
                    }

                case InvestmentTransactionSubType.Distribution:
                    {
                        returnInvestmentTransaction.TransactionSubType = "Distribution";
                        break;
                    }

                case InvestmentTransactionSubType.Dividend:
                    {
                        returnInvestmentTransaction.TransactionSubType = "Dividend";
                        break;
                    }

                case InvestmentTransactionSubType.DividendReinvestment:
                    {
                        returnInvestmentTransaction.TransactionSubType = "Dividend Reinvestment";
                        break;
                    }

                case InvestmentTransactionSubType.Exercise:
                    {
                        returnInvestmentTransaction.TransactionSubType = "Exercise";
                        break;
                    }

                case InvestmentTransactionSubType.Expire:
                    {
                        returnInvestmentTransaction.TransactionSubType = "Expire";
                        break;
                    }

                case InvestmentTransactionSubType.FundFee:
                    {
                        returnInvestmentTransaction.TransactionSubType = "Fund Fee";
                        break;
                    }

                case InvestmentTransactionSubType.Interest:
                    {
                        returnInvestmentTransaction.TransactionSubType = "Interest";
                        break;
                    }

                case InvestmentTransactionSubType.InterestReceivable:
                    {
                        returnInvestmentTransaction.TransactionSubType = "Interest Receivable";
                        break;
                    }

                case InvestmentTransactionSubType.InterestReinvestment:
                    {
                        returnInvestmentTransaction.TransactionSubType = "Interest Reinvestment";
                        break;
                    }

                case InvestmentTransactionSubType.LegalFee:
                    {
                        returnInvestmentTransaction.TransactionSubType = "Legal Fee";
                        break;
                    }

                case InvestmentTransactionSubType.LoanPayment:
                    {
                        returnInvestmentTransaction.TransactionSubType = "Loan Payment";
                        break;
                    }

                case InvestmentTransactionSubType.LongTermCapitalGain:
                    {
                        returnInvestmentTransaction.TransactionSubType = "Long Term Capital Gain";
                        break;
                    }

                case InvestmentTransactionSubType.LongTermCapitalGainReinvestment:
                    {
                        returnInvestmentTransaction.TransactionSubType = "Long Term Capital Gain Reinvestment";
                        break;
                    }

                case InvestmentTransactionSubType.ManagementFee:
                    {
                        returnInvestmentTransaction.TransactionSubType = "Management Fee";
                        break;
                    }

                case InvestmentTransactionSubType.MarginExpense:
                    {
                        returnInvestmentTransaction.TransactionSubType = "Margin Expense";
                        break;
                    }

                case InvestmentTransactionSubType.Merger:
                    {
                        returnInvestmentTransaction.TransactionSubType = "Merger";
                        break;
                    }

                case InvestmentTransactionSubType.MiscellaneousFee:
                    {
                        returnInvestmentTransaction.TransactionSubType = "Miscellaneous Fee";
                        break;
                    }

                case InvestmentTransactionSubType.NonQualifiedDividend:
                    {
                        returnInvestmentTransaction.TransactionSubType = "Non Qualified Dividend";
                        break;
                    }

                case InvestmentTransactionSubType.NonResidentTax:
                    {
                        returnInvestmentTransaction.TransactionSubType = "Non Resident Tax";
                        break;
                    }

                case InvestmentTransactionSubType.PendingCredit:
                    {
                        returnInvestmentTransaction.TransactionSubType = "Pending Credit";
                        break;
                    }

                case InvestmentTransactionSubType.PendingDebit:
                    {
                        returnInvestmentTransaction.TransactionSubType = "Pending Debit";
                        break;
                    }

                case InvestmentTransactionSubType.QualifiedDividend:
                    {
                        returnInvestmentTransaction.TransactionSubType = "Qualified Dividend";
                        break;
                    }

                case InvestmentTransactionSubType.Rebalance:
                    {
                        returnInvestmentTransaction.TransactionSubType = "Rebalance";
                        break;
                    }

                case InvestmentTransactionSubType.ReturnOfPrincipal:
                    {
                        returnInvestmentTransaction.TransactionSubType = "Return Of Principal";
                        break;
                    }

                case InvestmentTransactionSubType.Sell:
                    {
                        returnInvestmentTransaction.TransactionSubType = "Sell";
                        break;
                    }

                case InvestmentTransactionSubType.SellShort:
                    {
                        returnInvestmentTransaction.TransactionSubType = "Sell Short";
                        break;
                    }

                case InvestmentTransactionSubType.ShortTermCapitalGain:
                    {
                        returnInvestmentTransaction.TransactionSubType = "Short Term Capital Gain";
                        break;
                    }

                case InvestmentTransactionSubType.ShortTermCapitalGainReinvestment:
                    {
                        returnInvestmentTransaction.TransactionSubType = "Short Term Capital Gain Reinvestment";
                        break;
                    }

                case InvestmentTransactionSubType.SpinOff:
                    {
                        returnInvestmentTransaction.TransactionSubType = "Spin Off";
                        break;
                    }

                case InvestmentTransactionSubType.Split:
                    {
                        returnInvestmentTransaction.TransactionSubType = "Split";
                        break;
                    }

                case InvestmentTransactionSubType.StockDistribution:
                    {
                        returnInvestmentTransaction.TransactionSubType = "Stock Distribution";
                        break;
                    }

                case InvestmentTransactionSubType.Tax:
                    {
                        returnInvestmentTransaction.TransactionSubType = "Tax";
                        break;
                    }

                case InvestmentTransactionSubType.TaxWithheld:
                    {
                        returnInvestmentTransaction.TransactionSubType = "Tax Withheld";
                        break;
                    }

                case InvestmentTransactionSubType.Transfer:
                    {
                        returnInvestmentTransaction.TransactionSubType = "Transfer";
                        break;
                    }

                case InvestmentTransactionSubType.TransferFee:
                    {
                        returnInvestmentTransaction.TransactionSubType = "Transfer Fee";
                        break;
                    }

                case InvestmentTransactionSubType.TrustFee:
                    {
                        returnInvestmentTransaction.TransactionSubType = "Trust Fee";
                        break;
                    }

                case InvestmentTransactionSubType.UnqualifiedGain:
                    {
                        returnInvestmentTransaction.TransactionSubType = "Unqualified Gain";
                        break;
                    }

                case InvestmentTransactionSubType.Withdrawal:
                    {
                        returnInvestmentTransaction.TransactionSubType = "Withdrawal";
                        break;
                    }

                default:
                    {
                        returnInvestmentTransaction.TransactionSubType = "None";
                        break;
                    }
            }

            return returnInvestmentTransaction;
        }



        public LiabilitiesCreditModel ConvertLiabilitiesCreditResponseToModel(Acklann.Plaid.Entity.Credit InPlaidLiabilitiesCredit)
        {
            // NEEDS TO BE DONE, CONVERT APR RATE
            //LiabilitiesCreditModel.Apr[] temp = new LiabilitiesCreditModel.Apr[25]();

            LiabilitiesCreditModel returnCreditModel = new LiabilitiesCreditModel
            {
                AccountId = InPlaidLiabilitiesCredit.AccountId,
                LastPaymentAmount = InPlaidLiabilitiesCredit.LastPaymentAmount,
                LastPaymentDate = InPlaidLiabilitiesCredit.LastPaymentDate,
                LastStatementBalance = InPlaidLiabilitiesCredit.LastStatementBalance,
                LastStatementIssueDate = InPlaidLiabilitiesCredit.LastStatementIssueDate,
                MinimumPaymentAmount = InPlaidLiabilitiesCredit.MinimumPaymentAmount,
                IsOverdue = InPlaidLiabilitiesCredit.IsOverdue,
                NextPaymentDueDate = InPlaidLiabilitiesCredit.NextPaymentDueDate
            };

            return returnCreditModel;
        }

        public LiabilitiesMortgageModel ConvertLiabilitiesMortgageResponseToModel(Acklann.Plaid.Entity.Mortgage InPlaidLiabilitiesMortgage)
        {
            LiabilitiesMortgageModel returnMortgageModel = new LiabilitiesMortgageModel
            {
                AccountId = InPlaidLiabilitiesMortgage.AccountId,
                AccountNumber = InPlaidLiabilitiesMortgage.AccountNumber,
                CurrentLateFee = InPlaidLiabilitiesMortgage.CurrentLateFee,
                EscrowBalance = InPlaidLiabilitiesMortgage.EscrowBalance,
                HasPmi = InPlaidLiabilitiesMortgage.HasPmi,
                HasPrepaymentPenalty = InPlaidLiabilitiesMortgage.HasPrepaymentPenalty,
                InterestRatePercentage = InPlaidLiabilitiesMortgage.InterestRate.Percentage,
                InterestRateType = InPlaidLiabilitiesMortgage.InterestRate.Type,
                LastPaymentAmount = InPlaidLiabilitiesMortgage.LastPaymentAmount,
                LastPaymentDate = InPlaidLiabilitiesMortgage.LastPaymentDate,
                LoanTerm = InPlaidLiabilitiesMortgage.LoanTerm,
                LoanTypeDescription = InPlaidLiabilitiesMortgage.LoanTypeDescription,
                MaturityDate = InPlaidLiabilitiesMortgage.MaturityDate,
                NextMonthlyPayment = InPlaidLiabilitiesMortgage.NextMonthlyPayment,
                NextPaymentDueDate = InPlaidLiabilitiesMortgage.NextPaymentDueDate,
                OriginationDate = InPlaidLiabilitiesMortgage.OriginationDate,
                OriginationPrincipalAmount = InPlaidLiabilitiesMortgage.OriginationPrincipalAmount,
                PastDueAmount = InPlaidLiabilitiesMortgage.PastDueAmount,
                PropertyAddressCity = InPlaidLiabilitiesMortgage.PropertyAddress.City,
                PropertyAddressCountry = InPlaidLiabilitiesMortgage.PropertyAddress.Country,
                PropertyAddressPostalCode = InPlaidLiabilitiesMortgage.PropertyAddress.PostalCode,
                PropertyAddressRegion = InPlaidLiabilitiesMortgage.PropertyAddress.Region,
                PropertyAddressStreet = InPlaidLiabilitiesMortgage.PropertyAddress.Street,
                YtdInterestPaid = InPlaidLiabilitiesMortgage.YtdInterestPaid,
                YtdPrincipalPaid = InPlaidLiabilitiesMortgage.YtdPrincipalPaid
            };

            return returnMortgageModel;
        }

        public LiabilitiesStudentModel ConvertLiabilitiesStudentResponseToModel(Student InPlaidLiabilitiesStudent)
        {
            LiabilitiesStudentModel returnStudentModel = new LiabilitiesStudentModel
            {
                AccountId = InPlaidLiabilitiesStudent.AccountId,
                AccountNumber = InPlaidLiabilitiesStudent.AccountNumber,
                DisbursementDates = InPlaidLiabilitiesStudent.DisbursementDates,
                ExpectedPayoffDate = InPlaidLiabilitiesStudent.ExpectedPayoffDate,
                Guarantor = InPlaidLiabilitiesStudent.Guarantor,
                InterestRatePercentage = InPlaidLiabilitiesStudent.InterestRatePercentage,
                IsOverdue = InPlaidLiabilitiesStudent.IsOverdue,
                LastPaymentAmount = InPlaidLiabilitiesStudent.LastPaymentAmount,
                LastPaymentDate = InPlaidLiabilitiesStudent.LastPaymentDate,
                LastStatementBalance = InPlaidLiabilitiesStudent.LastStatementBalance,
                LastStatementIssueDate = InPlaidLiabilitiesStudent.LastStatementIssueDate,
                LoanName = InPlaidLiabilitiesStudent.LoanName,
                LoanStatusEndDate = InPlaidLiabilitiesStudent.LoanStatus.EndDate,
                LoanStatusType = InPlaidLiabilitiesStudent.LoanStatus.Type,
                MinimumPaymentAmount = InPlaidLiabilitiesStudent.MinimumPaymentAmount,
                NextPaymentDueDate = InPlaidLiabilitiesStudent.NextPaymentDueDate,
                OriginationDate = InPlaidLiabilitiesStudent.OriginationDate,
                OriginationPrincipalAmount = InPlaidLiabilitiesStudent.OriginationPrincipalAmount,
                OutstandingInterestAmount = InPlaidLiabilitiesStudent.OutstandingInterestAmount,
                PaymentReferenceNumber = InPlaidLiabilitiesStudent.PaymentReferenceNumber,
                PslfStatusEstimatedEligibilityDate = InPlaidLiabilitiesStudent.PslfStatus.EstimatedEligibilityDate,
                PslfStatusPaymentsMade = InPlaidLiabilitiesStudent.PslfStatus.PaymentsMade,
                PslfStatusPaymentsRemaining = InPlaidLiabilitiesStudent.PslfStatus.PaymentsRemaining,
                RepaymentPlanDescription = InPlaidLiabilitiesStudent.RepaymentPlan.Description,
                RepaymentPlanType = InPlaidLiabilitiesStudent.RepaymentPlan.Type,
                SequenceNumber = InPlaidLiabilitiesStudent.SequenceNumber,
                ServicerAddressCity = InPlaidLiabilitiesStudent.ServicerAddress.City,
                ServicerAddressCountry = InPlaidLiabilitiesStudent.ServicerAddress.Country,
                ServicerAddressPostalCode = InPlaidLiabilitiesStudent.ServicerAddress.PostalCode,
                ServicerAddressRegion = InPlaidLiabilitiesStudent.ServicerAddress.Region,
                ServicerAddressStreet = InPlaidLiabilitiesStudent.ServicerAddress.Street,
                YtdInterestPaid = InPlaidLiabilitiesStudent.YtdInterestPaid,
                YtdPrincipalPaid = InPlaidLiabilitiesStudent.YtdPrincipalPaid
            };

            return returnStudentModel;
        }
    }
}
