using e_commerce.models;

namespace e_commerce.viewModels
{
    public class productViewModel : Product
    {
        public List<models.Image> images { get; set; }
    }
}
