using System.ComponentModel.DataAnnotations;

namespace FundsLibrary.InterviewTest.Common
{
    public class Fund
    {
        [Display(Name = "Isin Code")]
        public string IsinCode { get; set; }
        public string FullName { get; set; }
    }
}