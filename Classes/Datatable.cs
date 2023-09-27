using e_commerce.Classes.datatable;

namespace e_commerce.Classes
{
    public class Datatable
    {

        public List<Column> columns { get; set; }
        public List<Order> order { get; set; }
        public int start { get; set; }
        public int draw { get; set; }
        public int length { get; set; }
        public Search search { get; set; }
    }
}




