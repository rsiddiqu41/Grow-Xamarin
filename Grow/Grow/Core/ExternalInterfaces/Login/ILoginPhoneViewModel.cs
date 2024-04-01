using System;
using System.Collections.Generic;
using System.Text;

namespace Grow.Core.ExternalInterfaces.Login
{
    public interface ILoginPhoneViewModel
    {
        void UpdatePhoneValidationLengthPrompt(bool isValid);

        void UpdatePhoneValidationCharacterPrompt(bool isValid);
    }
}
