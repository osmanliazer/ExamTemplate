using IndigoExam.Models;


namespace IndigoExam.ViewModels.Category
{
    public class CreateCategoryVM
    {
        public string Name { get; set; }
        public IFormFile Img { get; set; }
        public List<Category>? Categories { get; set; } 

    }
}
