using System.ComponentModel;

namespace MvcApplication1.Models
{
    public class Person
    {
        [DisplayName("What's your name")]
        public string Name { get; set; }
    }
}