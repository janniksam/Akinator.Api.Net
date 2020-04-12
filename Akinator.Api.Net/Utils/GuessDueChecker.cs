using Akinator.Api.Net.Model;

namespace Akinator.Api.Net.Utils
{
    public static class GuessDueChecker
    {
        public static bool GuessIsDue(AkinatorQuestion currentQuestion, int lastGuessTakenAtStep)
        {
            if (currentQuestion is null)
            {
                return false;
            }

            var stepsTakenSinceLastGuess = currentQuestion.Step - lastGuessTakenAtStep;
            if (NoMoreQuestions() ||
                currentQuestion.Step >= 80)
            {
                return true;
            }

            if (stepsTakenSinceLastGuess < 5)
            {
                return false;
            }

            if (currentQuestion.Step <= 25)
            {
                return !(currentQuestion.Progression <= 97.0f);
            }

            if (currentQuestion.Progression <= 80.0f && stepsTakenSinceLastGuess < 30)
            {
                return false;
            }

            return 80 - currentQuestion.Step > 5;
        }
    
        private static bool NoMoreQuestions()
        {
            //todo
            return false;
        }
    }
}
