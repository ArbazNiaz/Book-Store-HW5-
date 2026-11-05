namespace Book_Store.Models
{
    public class Book
    {
        public int Id
        { set;get; }
        public int AuthorId {  set;get; }
        public string Title { set;get; }
        public decimal Price { set; get; }

    }
}
