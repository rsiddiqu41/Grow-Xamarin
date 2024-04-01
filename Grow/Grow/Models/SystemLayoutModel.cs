using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using Xamarin.Essentials;

using Grow.Core.SystemConfiguration;
using Grow.Core.Mediator;

namespace Grow.Models
{
    public class SystemLayoutModel : ISystemLayoutModel
    {
        protected SystemStartScreen _SystemStartTypes;
        protected FinancialQuote _FinanceQuotesTypes;

        string SYSTEMSTARTLAYOUT = "SystemStartLayout.xml";
        string FINANCEQUOTESFILE = "FinancialQuotes.xml";

        public SystemLayoutModel()
        {
            _SystemStartTypes = new SystemStartScreen();
            _FinanceQuotesTypes = new FinancialQuote();
        }

        public async void ReadConfigFileForStartupScreen()
        {
            try
            {
                using (var stream = await FileSystem.OpenAppPackageFileAsync(SYSTEMSTARTLAYOUT))
                {
                    using (var Reader = new StreamReader(stream))
                    {
                        XmlSerializer Deserializer = new XmlSerializer(typeof(SystemStartScreen));

                        object obj = Deserializer.Deserialize(Reader);
                        _SystemStartTypes = (SystemStartScreen)obj;
                    }
                }
            }
            catch (Exception E)
            {
                string ErrorMessage = E.Message;
                System.Console.WriteLine(ErrorMessage);
            }
        }

        public async void ReadConfigFileForFinancialQuotesView()
        {
            try
            {
                using (var stream = await FileSystem.OpenAppPackageFileAsync(FINANCEQUOTESFILE))
                {
                    using (var Reader = new StreamReader(stream))
                    {
                        XmlSerializer Deserializer = new XmlSerializer(typeof(FinancialQuote));

                        object obj = Deserializer.Deserialize(Reader);
                        _FinanceQuotesTypes = (FinancialQuote)obj;
                    }
                }
            }
            catch (Exception E)
            {
                string ErrorMessage = E.Message;
                System.Console.WriteLine(ErrorMessage);
            }
        }

        public SystemStartScreen SystemStartLayoutScreen
        {
            get { return _SystemStartTypes; }
        }

        public FinancialQuote FinanceQuoteLayoutScreen
        {
            get { return _FinanceQuotesTypes; }
        }
    }
}
