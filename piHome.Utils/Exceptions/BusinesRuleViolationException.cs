using System;
using System.Collections.Generic;
using System.Linq;

namespace piHome.Utils.Exceptions
{
    public class BusinesRuleViolationException : Exception
    {
        public IEnumerable<string> Errors { get; }

        public BusinesRuleViolationException(IEnumerable<string> errors)
        {
            Errors = errors;
        }
    }

    public static class ValidationExtentions
    {
        public static void ThrowIfAny(this List<string> errors)
        {
            if (errors != null && errors.Any())
            {
                throw new BusinesRuleViolationException(errors);
            }
        }
    }
}