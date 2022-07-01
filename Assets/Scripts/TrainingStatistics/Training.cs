using System;
using System.Collections.Generic;
using System.Linq;
using TrainingStatistics.Activities;
using TrainingStatistics.Exercises;
using TrainingStatistics.Exercises.Data;

namespace TrainingStatistics
{
    public class Training : IContinuousActivity
    {
        private readonly IReadOnlyList<IContinuousActivity> _activities;

        public TimeSpan TotalDuration
        {
            get
            {
                var duration = new TimeSpan();

                for (int i = 0; i < _activities.Count; i++)
                    duration = duration.Add(_activities[i].TotalDuration);

                return duration;
            }
        }

        public Training(IReadOnlyList<IContinuousActivity> activities) => _activities = activities;

        public static Training CreateWithExercises(
            params (ExerciseSpecifications specifications, ExerciseExecutionData executionData)[] exercisesData)
        {
            var activities = new IContinuousActivity[exercisesData.Length];

            for (int i = 0; i < exercisesData.Length; i++)
            {
                (ExerciseSpecifications, ExerciseExecutionData) exerciseData = exercisesData[i];
                activities[i] = Exercise.Create(exerciseData.Item1, exerciseData.Item2);
            }

            return new Training(activities);
        }

        public float Accuracy
        {
            get
            {
                float accuracySum = _activities.Sum(activity => activity.Accuracy);
                int count = _activities.Count;

                return accuracySum / count;
            }
        }

        public float CaloriesBurnt => _activities.Sum(activity => activity.CaloriesBurnt);

        public TimeSpan ActiveDuration
        {
            get
            {
                var duration = new TimeSpan();

                foreach (var fraction in _activities)
                    duration = duration.Add(fraction.ActiveDuration);

                return duration;
            }
        }

        public float CompletionPercentage
        {
            get
            {
                float completionPercentageSum = _activities.Sum(activity => activity.CompletionPercentage);
                int count = _activities.Count;

                return completionPercentageSum / count;
            }
        }

        public float Efficiency => Accuracy * CompletionPercentage * CaloriesBurnt / TotalDuration.Seconds;
    }
}