using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerticTest.Model
{
    class DynamicObjectModel : DynamicObject
    {

        // En privat dictionary til at gemme egenskaber og deres værdier.
        private Dictionary<string, object> properties = new Dictionary<string, object>();

        // En metode, der overrider TryGetMember-metoden fra DynamicObject.
        // Dette gør det muligt at få adgang til egenskaber dynamisk.
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            /*string propertyName = binder.Name;*/  // Navnet på den egenskab, der forsøges at få adgang til.
            return properties.TryGetValue(binder.Name, out result);  // Forsøger at hente værdien for den angivne egenskab.
        }


        // En metode, der overrider TrySetMember-metoden fra DynamicObject.
        // Dette gør det muligt at sætte egenskaber dynamisk.
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            string propertyName = binder.Name;  // Navnet på den egenskab, der forsøges at sættes.
            properties[propertyName] = value; // Sætter værdien for den angivne egenskab.
            return true;
        }
    }
}
