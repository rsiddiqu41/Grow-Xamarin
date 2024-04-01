using System;
using System.Collections.Generic;
using System.Text;

namespace Grow.Core.ExternalInterfaces.Registration
{
    public interface IRegistrationPhoneViewModel
    {
        void UpdatePhoneValidationLengthPrompt(bool isValid);

        void UpdatePhoneValidationCharacterPrompt(bool isValid);
    }
}
