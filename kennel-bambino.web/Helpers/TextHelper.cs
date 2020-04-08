using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kennel_bambino.web.Helpers
{
    public static class TextHelper
    {
        public static string TextTransform(this string name) => name.Trim().ToLower();
        
    }
}
