namespace EntityFrameworkNet6.Domain
{
    // Team har en one-to-one relation med coach, hvor coach er child

    public class Coach : BaseDomainObject
    {
        public string Name { get; set; }
        public int? TeamId{ get; set; }
        public virtual Team Team { get; set; }
    }
}
