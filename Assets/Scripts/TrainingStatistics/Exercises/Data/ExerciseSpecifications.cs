namespace TrainingStatistics.Exercises.Data
{
    public readonly struct ExerciseSpecifications
    {
        public readonly string name;
        public readonly bool isStaticExercise;

        public readonly float caloriesPerFraction;
        public readonly int[] advisedFractionsInSets;

        public int AdvisedFractionsCount => advisedFractionsInSets.Length;

        public ExerciseSpecifications(string name, bool isStaticExercise, float caloriesPerFraction, int advisedSetsCount,
            int advisedFractionsPerSet)
        {
            this.name = name;
            this.isStaticExercise = isStaticExercise;
            this.caloriesPerFraction = caloriesPerFraction;

            advisedFractionsInSets = new int[advisedSetsCount];

            for (int i = 0; i < advisedSetsCount; i++)
                advisedFractionsInSets[i] = advisedFractionsPerSet;
        }
    }
}