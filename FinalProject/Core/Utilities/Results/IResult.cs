using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    //temel voidleri içi başlangıç
    public interface IResult
    {
        bool Success { get; }
        string Message { get; }
    }
}
