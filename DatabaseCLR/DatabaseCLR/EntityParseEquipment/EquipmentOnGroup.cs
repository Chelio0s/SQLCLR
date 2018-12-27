namespace DatabaseCLR
{
    public class EquipmentOnGroup
    {
        public int Site_ID { get; set; }
        public int KOB { get; set; }
        public string KOBTitle { get; set; }
        public int KOBCount { get; set; }

        public override bool Equals(object obj)
        {
            var group = obj as EquipmentOnGroup;
            return group != null &&
                   Site_ID == group.Site_ID &&
                   KOB == group.KOB &&
                   KOBTitle == group.KOBTitle &&
                   KOBCount == group.KOBCount;
        }

        public override int GetHashCode()
        {
            var hashCode = -1105953208;
            hashCode = hashCode * -1521134295 + Site_ID.GetHashCode();
            hashCode = hashCode * -1521134295 + KOB.GetHashCode();
            hashCode = hashCode * -1521134295 + KOBTitle.GetHashCode();
            hashCode = hashCode * -1521134295 + KOBCount.GetHashCode();
            return hashCode;
        }
    }
}