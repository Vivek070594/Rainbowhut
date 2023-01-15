using System;
using System.Runtime.CompilerServices;

namespace Rainbowhut1._0.Persistances
{
    public class IncludeInOrderByAttribute: Attribute
    {
        public string EntityPropertyName { get; set; }

        public IncludeInOrderByAttribute(string entityPropertyName = null, [CallerMemberName] string propertyName = null)
        {
            EntityPropertyName = entityPropertyName;
            if (entityPropertyName == null)
                EntityPropertyName = propertyName;
        }
    }
}
