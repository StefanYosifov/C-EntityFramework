namespace Entity_Relations_Demo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Car
    {
        [Key]
        public int Id { get; set; }

    }
}
