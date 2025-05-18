using praktika_desktop_dotNET7.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Windows.Media.Imaging;

namespace praktika_desktop_dotNET7.Models.Entities
{
    public class Service : EntityBase
    {
        [Display(Name = "Выберете категорию, к которой относится услуга")]
        public int? ServiceCategoryId { get; set; }
        public ServiceCategory? ServiceCategory { get; set; }

        [Display(Name = "Краткое описание"), MaxLength(3_000)]
        public string? DescriptionShort { get; set; }

        [Display(Name = "Описание"), MaxLength(100_000)]
        public string? Description { get; set; }

        [Display(Name = "Титульная картинка"), MaxLength(300)]
        public string? Photo { get; set; }

        public string PhotoUrl => $"https://localhost:7088/img/{Photo}";

        [NotMapped]
        public BitmapImage? ServiceImage
        {
            get
            {
                // Если есть прямое подключенное фото (ServicePhoto)
                if (ServicePhoto != null && ServicePhoto.Data != null)
                {
                    var image = new BitmapImage();
                    using (var stream = new MemoryStream(ServicePhoto.Data))
                    {
                        image.BeginInit();
                        image.CacheOption = BitmapCacheOption.OnLoad;
                        image.StreamSource = stream;
                        image.EndInit();
                    }
                    return image;
                }

                // Если фото нет, возвращаем null
                return null;
            }
            set { }
        }

        [Display(Name = "Тип услуги")]
        public ServiceTypeEnum? Type { get; set; }

        public int? ServicePhotoId { get; set; }
        public ServicePhoto? ServicePhoto { get; set; }
    }
}