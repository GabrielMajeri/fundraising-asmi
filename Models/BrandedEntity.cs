namespace Asmi.Fundraising.Models
{
    public abstract class BrandedEntity
    {
        public int? LogoId { get; set; }
        public Image Logo { get; set; }
    }
}
