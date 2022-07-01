using System;
using NUnit.Framework;
using TrainingStatistics;
using TrainingStatistics.Exercises.Data;

namespace Tests
{
    public class TrainingTest
    {
        private readonly Training _training0;
        private readonly Training _training0_Copy;
        private readonly Training _training1;

        private readonly TimeSpan _training0_TotalDuration;
        private readonly TimeSpan _training0_ActiveDuration;
        private readonly float _training0_CompletionPercentage;

        public TrainingTest()
        {
            var squatsSpecs = new ExerciseSpecifications(
                "Squats",
                false,
                1.5f,
                3,
                20);

            var plankSpecs = new ExerciseSpecifications(
                "Plank",
                true,
                0.0333f,
                4,
                60);

            var squatsExecution0 = new ExerciseExecutionData
            {
                completeSetsCount = 3,
                completeFractionsPerSet = 10,
                restDurationInSecondsAfterEachSet = 10,
                durationInSecondsPerSet = 30,
                restDurationInSecondsAfterExercise = 30
            };

            var squatsExecution1 = new ExerciseExecutionData
            {
                completeSetsCount = 3,
                completeFractionsPerSet = 10,
                restDurationInSecondsAfterEachSet = 10,
                durationInSecondsPerSet = 30,
                restDurationInSecondsAfterExercise = 29 // changed value
            };

            var plankExecution0 = new ExerciseExecutionData
            {
                completeSetsCount = 2,
                completeFractionsPerSet = 60,
                restDurationInSecondsAfterEachSet = 5
            };

            _training0_TotalDuration = new TimeSpan(0, 0, 3 * 30 + 2 * 10 + 30 + 2 * 60 + 5);
            _training0_ActiveDuration = new TimeSpan(0, 0, 3 * 30 + 2 * 60);
            _training0_CompletionPercentage = 0.5f;

            _training0 = Training.CreateWithExercises((squatsSpecs, squatsExecution0), (plankSpecs, plankExecution0));

            // Same as training0
            _training0_Copy = Training.CreateWithExercises((squatsSpecs, squatsExecution0), (plankSpecs, plankExecution0));

            _training1 = Training.CreateWithExercises((squatsSpecs, squatsExecution1), (plankSpecs, plankExecution0));
        }

        [Test]
        public void EfficienciesOfSameTrainingsAreEqual()
        {
            float efficiency = _training0.Efficiency;
            float efficiencyCopy = _training0_Copy.Efficiency;

            Assert.IsTrue(Math.Abs(efficiency - efficiencyCopy) < float.Epsilon,
                $"Efficiency0 != Efficiency0_copy: {efficiency} != {efficiencyCopy}");
        }

        [Test]
        public void EfficienciesOfDifferentTrainingsAreNotEqual()
        {
            float efficiency0 = _training0.Efficiency;
            float efficiency1 = _training1.Efficiency;

            Assert.IsTrue(Math.Abs(efficiency0 - efficiency1) > float.Epsilon,
                $"Efficiency0 == Efficiency1: {efficiency0} == {efficiency1}");
        }

        [Test]
        public void TotalDurationOfTraining0EqualsValue()
        {
            var duration = _training0.TotalDuration;
            var expectedDuration = _training0_TotalDuration;

            Assert.IsTrue(duration == expectedDuration,
                $"TotalDuration != Expected: {duration} != {expectedDuration}");
        }

        [Test]
        public void ActiveDurationOfTraining0EqualsValue()
        {
            var duration = _training0.ActiveDuration;
            var expectedDuration = _training0_ActiveDuration;

            Assert.IsTrue(duration == expectedDuration,
                $"ActiveDuration != Expected: {duration} != {expectedDuration}");
        }

        [Test]
        public void CompletionPercentageOfTraining0EqualsValue()
        {
            float completionPercentage = _training0.CompletionPercentage;
            float expectedCompletionPercentage = _training0_CompletionPercentage;

            Assert.IsTrue(Math.Abs(completionPercentage - expectedCompletionPercentage) < float.Epsilon,
                $"CompletionPercentage != Expected: {completionPercentage} != {expectedCompletionPercentage}");
        }

        [Test]
        public void CompletionPercentageOfTrainingWithSameExercisesExecutionsAreEqual()
        {
            float completionPercentage0 = _training0.CompletionPercentage;
            float completionPercentage1 = _training1.CompletionPercentage;

            Assert.IsTrue(Math.Abs(completionPercentage0 - completionPercentage1) < float.Epsilon,
                $"CompletionPercentage != Expected: {completionPercentage0} != {completionPercentage1}");
        }
    }
}