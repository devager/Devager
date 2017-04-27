namespace Devager.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Dynamic;
    public static class Dynamic
    {
        public static dynamic ToDynamic(this object value)
        {
            IDictionary<string, object> expando = new ExpandoObject();

            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(value.GetType()))
                expando.Add(property.Name, property.GetValue(value));

            return expando as ExpandoObject;
        }

        public static void SafeInvoke(this Action action)
        {
            if (action != null) action();
        }

    }
}
