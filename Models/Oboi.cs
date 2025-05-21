namespace ekzVar.Models
{
    public class Oboi
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int OsnovaMaterialId { get; set; }
        public virtual Materials? OsnovaMaterial { get; set; }
        public int PokritieMaterialId { get; set; }
        public virtual Materials? PokritieMaterial { get; set; }
    }
}
