using praktika_desktop_dotNET7.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        [Display(Name = "Тип услуги")]
        public ServiceTypeEnum? Type { get; set; }
    }
}
