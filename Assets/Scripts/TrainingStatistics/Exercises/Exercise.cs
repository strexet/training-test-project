using System;
using System.Collections.Generic;
using System.Linq;
using TrainingStatistics.Activities;
using TrainingStatistics.Exercises.Data;
using TrainingStatistics.Sets;
using TrainingStatistics.Sets.Data;

namespace TrainingStatistics.Exercises
{
    public class Exercise : IContinuousActivity
    {
        private readonly IReadOnlyList<IContinuousActivity> _completeExerciseFractions;
        private readonly int _advisedFractionsCount;

        private Exercise(ExerciseData data)
        {
            _completeExerciseFractions = CreateSets(data);
            _advisedFractionsCount = data.specifications.AdvisedFractionsCount;

            TotalDuration = new TimeSpan(0, 0, data.ExerciseDurationSeconds);
        }

        public static Exercise Create(ExerciseSpecifications specifications, ExerciseExecutionData executionData)
        {
            var data = new ExerciseData(specifications, executionData);
            return new Exercise(data);
        }

        private static TrainingSet[] CreateSets(ExerciseData data)
        {
            int completeFractionsCount = data.CompleteFractionsCount;
            var completeFractions = new TrainingSet[completeFractionsCount];

            for (int i = 0; i < completeFractionsCount; i++)
            {
                var trainingSetData = CreateDataForSetWithIndex(i, data);
                completeFractions[i] = new TrainingSet(trainingSetData);
            }

            return completeFractions;
        }

        private static SetData CreateDataForSetWithIndex(int i, ExerciseData data) =>
            data.specifications.isStaticExercise
                ? SetData.CreateStatic(
                    data.specifications.advisedFractionsInSets[i],
                    data.completeFractionsInSets[i],
                    data.specifications.caloriesPerFraction)
                : SetData.CreateDynamic(
                    data.specifications.advisedFractionsInSets[i],
                    data.completeFractionsInSets[i],
                    data.durationsOfSets[i],
                    data.specifications.caloriesPerFraction);

        public float Accuracy
        {
            get
            {
                float accuracySum = _completeExerciseFractions.Sum(fraction => fraction.Accuracy);
                int count = _completeExerciseFractions.Count;
                float averageAccuracy = accuracySum / count;

                return averageAccuracy;
            }
        }

        public float CaloriesBurnt => _completeExerciseFractions.Sum(fraction => fraction.CaloriesBurnt);

        public TimeSpan ActiveDuration
        {
            get
            {
                var duration = new TimeSpan();

                foreach (var fraction in _completeExerciseFractions)
                    duration = duration.Add(fraction.ActiveDuration);

                return duration;
            }
        }

        public TimeSpan TotalDuration { get; }

        public float CompletionPercentage
        {
            get
            {
                float completionPercentageSum = _completeExerciseFractions.Sum(fraction => fraction.CompletionPercentage);
                return completionPercentageSum / _advisedFractionsCount;
            }
        }
    }
}