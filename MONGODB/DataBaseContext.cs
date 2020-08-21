using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;

namespace PDK.DB.MONGODB
{
    public abstract class DataBaseContext
    {
        public string DataBaseName { get; set; }
        public DataBaseContext()
        {
            AttributeControl(GetType().GetCustomAttributes(true), out SpecialNameAttribute specialNameAttribute);
            if (specialNameAttribute != null)
                DataBaseName = specialNameAttribute.SpecialName;
            else
            {
                var subclassType = Assembly
                   .GetAssembly(typeof(DataBaseContext))
                   .GetTypes()
                   .Where(t => t.IsSubclassOf(typeof(DataBaseContext)))
                   .FirstOrDefault();
                if (subclassType != null)
                    DataBaseName = subclassType.Name;
                else
                    throw new Exception($@"Have to use {typeof(SpecialNameAttribute).FullName} or Inherint {typeof(DataBaseContext).FullName}");
            }
        }
        private void AttributeControl<Type>(object[] attributes, out Type attribute) where Type : Attribute => attribute = attributes.FirstOrDefault(attr => attr != null && attr is Type tempAttribute && tempAttribute != null) as Type;
    }
}
