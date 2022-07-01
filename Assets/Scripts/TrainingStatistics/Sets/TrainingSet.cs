using System;
using System.Collections.Generic;
using System.Linq;
using TrainingStatistics.Activities;
using TrainingStatistics.Sets.Data;

namespace TrainingStatistics.Sets
{
    public class TrainingSet : IContinuousActivity
    {
        private readonly IReadOnlyList<IActivity> _completeSetFractions;
        private readonly int _advisedFractionsCount;

        public TrainingSet(SetData data)
        {
            int completeCount = data.completeFractionsCount;
            var completeActivities = new IActivity[completeCount];

            for (int i = 0; i < completeCount; i++)
                completeActivities[i] = new SetFraction(data.accuracyInFractions[i], data.caloriesPerFraction);

            _completeSetFractions = completeActivities;
            _advisedFractionsCount = data.advisedFractionsCount;

            ActiveDuration = new TimeSpan(0, 0, data.durationSeconds);
        }

        public float Accuracy
        {
            get
            {
                float accuracySum = _completeSetFractions.Sum(fraction => fraction.Accuracy);
                int count = _completeSetFractions.Count;

                return accuracySum / count;
            }
        }

        public float CaloriesBurnt => _completeSetFractions.Sum(fraction => fraction.CaloriesBurnt);

        public TimeSpan ActiveDuration { get; }

        public TimeSpan TotalDuration => ActiveDuration;

        public float CompletionPercentage
        {
            get
            {
                int count = _completeSetFractions.Count;
                return (float)count / _advisedFractionsCount;
            }
        }
    }
}