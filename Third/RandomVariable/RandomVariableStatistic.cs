namespace RandomVariable
{
    using System.Collections.Generic;
    public class RandomVariableStatistic
    {
        public double? ExpectedValue { get; set; }
        public double? Variance { get; set; }
        public Dictionary<double, double> ProbabilityDistribution { get; set; }
    }
}