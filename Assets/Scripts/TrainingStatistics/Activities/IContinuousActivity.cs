using System;

namespace TrainingStatistics.Activities
{
    public interface IContinuousActivity : IActivity
    {
        TimeSpan ActiveDuration { get; }
        TimeSpan TotalDuration { get; }
        float CompletionPercentage { get; }
    }
}