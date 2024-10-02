using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    public class PaperProperty
    {
        [Column("paper_id")]
        public int PaperId { get; set; }
    
        [ForeignKey("PaperId")]
        public Paper Paper { get; set; }

        [Column("property_id")]
        public int PropertyId { get; set; }

        [ForeignKey("PropertyId")]
        public Property Property { get; set; }
    
    }
    
}