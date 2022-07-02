using System;
using System.Collections.Generic;

namespace Weather.API.Attributes
{
    public class PollyExceptionAttribute
    {
        public Dictionary<Type, int> ExceptionDictTypes { get; set; }
        public PollyExceptionAttribute()
        {
            ExceptionDictTypes = new Dictionary<Type, int>();
        }

        public PollyExceptionAttribute(Type exceptionType, int retryCnt = 3)
        {
            ExceptionDictTypes.Add(exceptionType, retryCnt);
        }

        public PollyExceptionAttribute(Dictionary<Type, int> dictExceptionsCounts)
        {
            ExceptionDictTypes = dictExceptionsCounts;
        }
    }
}
