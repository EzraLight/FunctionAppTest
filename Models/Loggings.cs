using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionAppTest.Models
{
    public class Loggings
    {
        [Key]
        public int Id { get; set; }
        public DateTime StartedTime { get; set; }
        public DateTime? FinishedTime { get; set; }
        public double? ExcutionTime { get; set; }
        public bool? IsCompleted { get; set; }
        public string FunctionName { get; set; }
    }
}
