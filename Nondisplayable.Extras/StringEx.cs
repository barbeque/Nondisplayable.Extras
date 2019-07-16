using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nondisplayable.Extras
{
    public static class StringEx
    {
        public static bool LooseEquals(string a, string b)
        {
            Validate.NotNull(a);
            Validate.NotNull(b);

            var trimmedA = a.Trim();
            var trimmedB = b.Trim();

            return string.Equals(trimmedA, trimmedB, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
