using System.ComponentModel.DataAnnotations;

namespace SQLiteData
{
    public partial class TestClass
    {
        public TestClass()
        {
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}