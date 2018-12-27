using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseCLR
{
    class FinalEquip
    {
        public int ID { get; set; }
        public int KOB { get; set; }
        public int Group_ID { get; set; }
        public string Title { get; set; }

        public override bool Equals(object obj)
        {
            var equip = obj as FinalEquip;
            return equip != null &&
                   ID == equip.ID &&
                   KOB == equip.KOB &&
                   Group_ID == equip.Group_ID &&
                   Title == equip.Title;
        }

        public override int GetHashCode()
        {
            var hashCode = -442842356;
            hashCode = hashCode * -1521134295 + ID.GetHashCode();
            hashCode = hashCode * -1521134295 + KOB.GetHashCode();
            hashCode = hashCode * -1521134295 + Group_ID.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Title);
            return hashCode;
        }
    }
}

