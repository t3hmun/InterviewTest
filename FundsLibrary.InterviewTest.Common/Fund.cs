using System.ComponentModel.DataAnnotations;

namespace FundsLibrary.InterviewTest.Common
{
    public class Fund
    {
        [Display(Name = "IA Sector")]
        public string IASector { get; set; }
        [Display(Name = "Benchmark Description")]
        public string BenchmarkDescription { get; set; }
        [Display(Name = "ISIN Code")]
        public string IsinCode { get; set; }
        public string Objectives { get; set; }
        public string FullName { get; set; }
    }
}