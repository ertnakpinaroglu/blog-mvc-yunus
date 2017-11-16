namespace blog_mvc_yunus.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("Makaleler")]
    public partial class Makaleler
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Makaleler()
        {
            Yorumlars = new HashSet<Yorumlar>();
        }

        [Key]
        public int MakaleId { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Lütfen bu alaný boþ býrakmayýnýz..")]
        public string MakaleBasligi { get; set; }

        [Required(ErrorMessage = "Lütfen bu alaný boþ býrakmayýnýz..")]
        [UIHint("tinymce_full_compressed")]
        [AllowHtml]
        public string MakaleIcerik { get; set; }

        [StringLength(250)]
        
        public string MakaleFoto { get; set; }

        public DateTime? MakaleTarih { get; set; }

        public int? KategoriId { get; set; }

        public int? OkunmaSayisi { get; set; }

        public virtual Kategoriler Kategoriler { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Yorumlar> Yorumlars { get; set; }
    }
}
