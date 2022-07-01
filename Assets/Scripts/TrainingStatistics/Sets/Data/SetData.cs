namespace TrainingStatistics.Sets.Data
{
    public class SetData
    {
        private const float AccuracyValue = 0.7f;

        public readonly int advisedFractionsCount;
        public readonly int completeFractionsCount;
        public readonly int durationSeconds;
        public readonly float caloriesPerFraction;
        public readonly float[] accuracyInFractions;

        private SetData(int advisedFractionsCount, int completeFractionsCount, int durationSeconds, float caloriesPerFraction,
            float accuracy)
        {
            this.advisedFractionsCount = advisedFractionsCount;
            this.completeFractionsCount = completeFractionsCount;
            this.durationSeconds = durationSeconds;
            this.caloriesPerFraction = caloriesPerFraction;

            accuracyInFractions = new float[completeFractionsCount];

            for (int i = 0; i < completeFractionsCount; i++)
                accuracyInFractions[i] = accuracy;
        }

        public static SetData CreateStatic(int advisedDurationSeconds, int completeDurationSeconds, float caloriesPerSecond) =>
            new SetData(advisedDurationSeconds, completeDurationSeconds, completeDurationSeconds, caloriesPerSecond, AccuracyValue);

        public static SetData CreateDynamic(int advisedCount, int completeCount, int durationSeconds, float caloriesPerRepetition) =>
            new SetData(advisedCount, completeCount, durationSeconds, caloriesPerRepetition, AccuracyValue);
    }
}