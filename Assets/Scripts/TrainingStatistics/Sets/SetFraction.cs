using TrainingStatistics.Activities;

namespace TrainingStatistics.Sets
{
    public class SetFraction : IActivity
    {
        public float Accuracy { get; }
        public float CaloriesBurnt { get; }

        public SetFraction(float accuracy, float caloriesBurnt)
        {
            Accuracy = accuracy;
            CaloriesBurnt = caloriesBurnt;
        }
    }
}