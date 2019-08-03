namespace Akinator.Api.Net.Model
{
    public class AkinatorQuestion
    {
        public AkinatorQuestion(string text, double progression, int step)
        {
            Text = text;
            Progression = progression;
            Step = step;
        }

        public string Text { get; }
        
        public double Progression { get; }

        public int Step { get; }
    }
}