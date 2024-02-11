using System.ComponentModel.DataAnnotations;

namespace BlazorExpenseTracker.Model
{
    public class Category
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings =false, ErrorMessage ="Category Name is required")]
        public string Name { get; set; }
    }
}
