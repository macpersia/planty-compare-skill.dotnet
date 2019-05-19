using System;
using System.Collections.Generic;

namespace planty_compare_skill.Data
{
    public partial class PurchasingPower
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public string City { get; set; }
        public string Category { get; set; }
        public decimal Value { get; set; }
    }
}
