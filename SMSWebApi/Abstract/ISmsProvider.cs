using System;
using System.Collections.Generic;
using System.Text;

namespace Abstract
{
    public interface ISmsProvider
    {
        object SendSms(dynamic payload);
    }
}
