namespace blog_mvc_yunus.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Kategoriler")]
    public partial class Kategoriler
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Kategoriler()
        {
            Makalelers = new HashSet<Makaleler>();
        }

        [Key]
        public int KategoriId { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Lütfen bu alanı boş bırakmayınız..")]
        public string KategoriAdi { get; set; }

        [StringLength(250)]
        [Required(ErrorMessage = "Lütfen bu alanı boş bırakmayınız..")]
        public string Tanım { get; set; }

        [StringLength(250)]
      
        public string Resim { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Makaleler> Makalelers { get; set; }
    }
}
