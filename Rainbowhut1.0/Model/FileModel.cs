using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rainbowhut1._0.Model
{
    public class FileModel
    {
        public ProfileModel profilemodel { get; set; }
        public List<SlideShowModel> slideshowmodel { get; set; }
        public List<GalleryModel> gallerymodel { get; set; }
    }
    [Table("PROD_PROFILE_DATA")]
    public class ProfileModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string ContentType { get; set; }
        public string Data { get; set; }
    }
    [Table("PROD_SLIDESHOW_DATA")]
    public class SlideShowModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string ContentType { get; set; }
        public string Data { get; set; }
    }
    [Table("PROD_QRCODE_DATA")]
    public class QrCodeModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public string Data { get; set; }
        public Guid KeyID { get; set; }
    }
    public class QrCodeViewModel
    {
        public string FileName { get; set; }
        public string Data { get; set; }
    }
    [Table("PROD_GALLERY_DATA")]
    public class GalleryModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string ContentType { get; set; }
        public string Data { get; set; }

        public string GalleryType { get; set; }
    }
    //public class GalleryViewModel
    //{
    //    public string Data { get; set; }
    //    public string GalleryType { get; set; }
    //}
    //public class ProfileViewModel
    //{
    //    public string Data { get; set; }
    //}
    //public class SlideShowViewModel
    //{
    //    public string Data { get; set; }
    //}
}
