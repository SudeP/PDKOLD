using System;
using System.Collections.Generic;
using System.Text;

namespace PDK.DB.MONGODB
{
    public class SpecialNameAttribute : Attribute
    {
        public string SpecialName { get; set; }
        public SpecialNameAttribute(string SpecialName) => this.SpecialName = SpecialName;
    }
}
