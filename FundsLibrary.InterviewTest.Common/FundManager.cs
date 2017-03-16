using System;
using System.Collections.Generic;

namespace FundsLibrary.InterviewTest.Common
{
    public class FundManager
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime ManagedSince { get; set; }
        public string Biography { get; set; }
        public Location Location { get; set; }
        public IEnumerable<Fund> Funds { get; set; }
    }
}