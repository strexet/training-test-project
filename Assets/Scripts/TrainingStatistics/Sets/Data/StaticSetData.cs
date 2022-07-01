namespace TrainingStatistics.Sets.Data
{
    public struct StaticSetData
    {
        public readonly int advisedDurationSeconds;
        public readonly int durationSeconds;
        public readonly float caloriesPerSecond;

        public float completionPercentage;
        public float averageAccuracy;

        public StaticSetData(int advisedDurationSeconds, int durationSeconds, float caloriesPerSecond)
        {
            this.advisedDurationSeconds = advisedDurationSeconds;
            this.durationSeconds = durationSeconds;
            this.caloriesPerSecond = caloriesPerSecond;

            completionPercentage = 1f;
            averageAccuracy = 0.7f;
        }

        public StaticSetData(int advisedDurationSeconds, int durationSeconds, float caloriesPerSecond, float completionPercentage,
            float averageAccuracy)
            : this(advisedDurationSeconds, durationSeconds, caloriesPerSecond)
        {
            this.completionPercentage = completionPercentage;
            this.averageAccuracy = averageAccuracy;
        }
    }
}