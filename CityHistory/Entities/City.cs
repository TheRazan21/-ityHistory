using System.ComponentModel.DataAnnotations;
using Common.Entities;
// Переконайтесь, що цей namespace правильний (див. MyResources.Designer.cs)
using CityHistory.Resources;

namespace CityHistory.Entities
{
    public class City : Entity
    {
        public override string Key => Id.ToString();

        [Display(Name = "City_Name", ResourceType = typeof(MyResources))]
        [Required(ErrorMessageResourceType = typeof(MyResources), ErrorMessageResourceName = "Error_Required")]
        [StringLength(50, MinimumLength = 3, ErrorMessageResourceType = typeof(MyResources), ErrorMessageResourceName = "Error_StringLength")]
        public string Name { get; set; }

        [Display(Name = "City_Country", ResourceType = typeof(MyResources))]
        [Required(ErrorMessageResourceType = typeof(MyResources), ErrorMessageResourceName = "Error_Required")]
        public string Country { get; set; }

        [Display(Name = "City_Area", ResourceType = typeof(MyResources))]
        [Required(ErrorMessageResourceType = typeof(MyResources), ErrorMessageResourceName = "Error_Required")]
        [Range(0.1, 100000, ErrorMessageResourceType = typeof(MyResources), ErrorMessageResourceName = "Error_Range")]
        public double Area { get; set; }

        [Display(Name = "City_Population", ResourceType = typeof(MyResources))]
        [Required(ErrorMessageResourceType = typeof(MyResources), ErrorMessageResourceName = "Error_Required")]
        [Range(1, 50000000, ErrorMessageResourceType = typeof(MyResources), ErrorMessageResourceName = "Error_Range")]
        public int Population { get; set; }

        [Display(Name = "City_Description", ResourceType = typeof(MyResources))]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}