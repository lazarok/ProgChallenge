using System;
using System.Collections.Generic;
using System.Text;

namespace ProgChallenge.Application.Interfaces
{
    public interface IDateTimeService
    {
        DateTime NowUtc { get; }
    }
}
