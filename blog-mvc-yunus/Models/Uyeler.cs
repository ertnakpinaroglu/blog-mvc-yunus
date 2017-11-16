namespace blog_mvc_yunus.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Uyeler")]
    public partial class Uyeler
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Uyeler()
        {
            Yorumlars = new HashSet<Yorumlar>();
        }

        [Key]
        public int UyeId { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage ="L�tfen bu alan� bo� b�rakmay�n�z..")]
        public string UyeAdSoyad { get; set; }

        [Required(ErrorMessage = "L�tfen bir resim se�iniz..")]
        [StringLength(250)]

        public string UyeFoto { get; set; }

        public int? YetkiId { get; set; }

        public DateTime? Tarih { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage ="L�tfen bu alan� bo� b�rakmay�n�z....")]
        public string KullaniciAdi { get; set; }

        public int? FavorilerId { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "L�tfen bu alan� bo� b�rakmay�n�z....")]
        public string Sifre { get; set; }

        public virtual Yetkilendirme Yetkilendirme { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Yorumlar> Yorumlars { get; set; }
    }
}
