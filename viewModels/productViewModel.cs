using e_commerce.models;

namespace e_commerce.viewModels
{
    public class productViewModel : Product
    {
        public List<Image> images { get; set; }
    }
}
