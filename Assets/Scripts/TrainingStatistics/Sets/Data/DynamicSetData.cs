namespace TrainingStatistics.Sets.Data
{
    public struct DynamicSetData
    {
        public readonly int advisedCount;
        public readonly int durationSeconds;
        public readonly float caloriesPerRepetition;

        public float completionPercentage;
        public float averageAccuracy;

        public DynamicSetData(int advisedCount, int durationSeconds, float caloriesPerRepetition)
        {
            this.advisedCount = advisedCount;
            this.durationSeconds = durationSeconds;
            this.caloriesPerRepetition = caloriesPerRepetition;

            completionPercentage = 1f;
            averageAccuracy = 0.7f;
        }

        public DynamicSetData(int advisedCount, int durationSeconds, float caloriesPerRepetition, float completionPercentage,
            float averageAccuracy)
            : this(advisedCount, durationSeconds, caloriesPerRepetition)
        {
            this.completionPercentage = completionPercentage;
            this.averageAccuracy = averageAccuracy;
        }
    }
}