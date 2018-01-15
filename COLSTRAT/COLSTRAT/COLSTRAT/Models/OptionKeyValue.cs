using System;

namespace COLSTRAT.Models
{
    public class OptionKeyValue
    {
        public string key { get; private set; }
        public Object val { get; set; }

        public OptionKeyValue(String key, Object val)
        {
            this.key = key;
            this.val = val;
        }

        public override string ToString()
        {
            return (String)val;
        }
    }
}
