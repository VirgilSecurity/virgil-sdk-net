using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var sss = new Model1 { Title = "aasdeasdasdasdsdas" };
            var context = new ValidationContext(sss);

            var validationResults = new List<ValidationResult>();

            var vc = new ValidationContext(sss, null, null);
            var isValid = Validator.TryValidateObject
                    (sss, vc, validationResults, true);
            
            Console.WriteLine("Errors: " + isValid);

            Console.ReadKey();
        }
    }

    public class Model1
    {
        [Required]
        public DateTime? StartDate { get; set; }

        [StringLength(10)]
        public string Title { get; set; }
    }
}
