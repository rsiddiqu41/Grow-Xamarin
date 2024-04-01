using System;
using System.Collections.Generic;
using System.Text;

namespace Grow.Core.ExternalInterfaces.Login
{
    public interface ILoginEmailViewModel
    {
        void UpdatePasswordValidationLengthPrompt(bool isValid);
        void UpdatePasswordValidationCharacterPrompt(bool isValid);
    }
}
