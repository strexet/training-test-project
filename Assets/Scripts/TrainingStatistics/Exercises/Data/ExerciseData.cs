using System.Linq;

namespace TrainingStatistics.Exercises.Data
{
    public readonly struct ExerciseData
    {
        public readonly ExerciseSpecifications specifications;

        public readonly int[] completeFractionsInSets;
        public readonly int[] durationsOfSets;
        public readonly int[] restDurationsBetweenSets;

        public readonly int restDurationAfterExercise;

        public readonly int CompleteFractionsCount => completeFractionsInSets.Length;

        public readonly int ExerciseDurationSeconds =>
            durationsOfSets.Sum() + restDurationsBetweenSets.Sum() + restDurationAfterExercise;

        public float ExerciseCompletionPercentage => (float)CompleteFractionsCount / specifications.AdvisedFractionsCount;

        public ExerciseData(ExerciseSpecifications specifications, ExerciseExecutionData executionData)
        {
            this.specifications = specifications;

            completeFractionsInSets = new int[executionData.completeSetsCount];
            durationsOfSets = new int[executionData.completeSetsCount];
            restDurationsBetweenSets = new int[executionData.completeSetsCount - 1];

            for (int i = 0; i < executionData.completeSetsCount; i++)
            {
                completeFractionsInSets[i] = executionData.completeFractionsPerSet;
                durationsOfSets[i] = GetSetDuration(executionData);
            }

            for (int i = 0; i < executionData.completeSetsCount - 1; i++)
                restDurationsBetweenSets[i] = executionData.restDurationInSecondsAfterEachSet;

            restDurationAfterExercise = executionData.restDurationInSecondsAfterExercise;
        }

        private static int GetSetDuration(ExerciseExecutionData executionDat)
        {
            bool isDynamicSet = executionDat.durationInSecondsPerSet > 0;
            return isDynamicSet ? executionDat.durationInSecondsPerSet : executionDat.completeFractionsPerSet;
        }
    }
}