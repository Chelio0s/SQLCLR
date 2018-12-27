using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseCLR
{
    class UniqueKOB
    {
        public int ID_uniqKOB { get; set; }
        public int Site_ID { get; set; }
        public int KOB { get; set; }
        public string Title { get; set; }

        public override bool Equals(object obj)
        {
            var kOB = obj as UniqueKOB;
            return kOB != null &&
                   ID_uniqKOB == kOB.ID_uniqKOB &&
                   Site_ID == kOB.Site_ID &&
                   KOB == kOB.KOB &&
                   Title == kOB.Title;
        }

        public override int GetHashCode()
        {
            var hashCode = 357266222;
            hashCode = hashCode * -1521134295 + ID_uniqKOB.GetHashCode();
            hashCode = hashCode * -1521134295 + Site_ID.GetHashCode();
            hashCode = hashCode * -1521134295 + KOB.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Title);
            return hashCode;
        }
    }
}
