using Akinator.Api.Net.Enumerations;
using Akinator.Api.Net.Model;

namespace Akinator.Api.Net.Utils
{
    /// <summary>
    /// Checks, whether or not Akinator is ready to guess
    /// </summary>
    public static class GuessDueChecker
    {
        public static bool GuessIsDue(AkinatorQuestion currentQuestion, int lastGuessTakenAtStep, Platform platform = Platform.Android)
        {
            if (currentQuestion is null)
            {
                return false;
            }

            switch (platform)
            {
                default:
                case Platform.Android:
                    {
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
                case Platform.Android_Modified:
                    {
                        var step = currentQuestion.Step;
                        var variance = step - lastGuessTakenAtStep;
                        var progress = currentQuestion.Progression;
                        if (NoMoreQuestions() || step == 80)
                        {
                            return true;
                        }
                        if (variance < 5)
                        {
                            return false;
                        }
                        if (step <= 25)
                        {
                            if (progress > 97.0)
                            {
                                return true;
                            }
                        }
                        else if (progress >= 80.0 || variance >= 30)
                        {
                            if (variance >= 10)
                            {
                                return true;
                            }
                        }
                        return false;
                    }
                case Platform.Windows_Phone:
                    {
                        return currentQuestion.Step >= 79 || (currentQuestion.Step - lastGuessTakenAtStep >= 5 && (currentQuestion.Progression >= 97f || currentQuestion.Step - lastGuessTakenAtStep == 25) && currentQuestion.Step != 75);
                    }
            }
        }

        private static bool NoMoreQuestions()
        {
            //todo
            return false;
        }
    }
}
