using System.Collections.Generic;

namespace DatabaseCLR
{
    class NeedEquip
    {
        public int ID_Group { get; set; }
        public int KOB { get; set; }
        public string TitleGroup { get; set; }
        public int Site_ID { get; set; }

        public override bool Equals(object obj)
        {
            var equip = obj as NeedEquip;
            return equip != null &&
                   ID_Group == equip.ID_Group &&
                   KOB == equip.KOB &&
                   TitleGroup == equip.TitleGroup &&
                   Site_ID == equip.Site_ID;
        }

        public override int GetHashCode()
        {
            var hashCode = -1107924263;
            hashCode = hashCode * -1521134295 + ID_Group.GetHashCode();
            hashCode = hashCode * -1521134295 + KOB.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TitleGroup);
            hashCode = hashCode * -1521134295 + Site_ID.GetHashCode();
            return hashCode;
        }
    }
}
