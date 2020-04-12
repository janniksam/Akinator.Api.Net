using Akinator.Api.Net.Model;
using Akinator.Api.Net.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Akinator.Api.Net.Tests
{
    [TestClass]
    public class GuessDueCheckerTests
    {
        [TestMethod]
        public void GuessIsDue_ReturnsTrueWhenProgressionAtleast97()
        {
            const int currentProgression = 97;
            const int currentlyAt = 5;
            const int lastGuessAt = 0;
            var guessIsDue = GuessDueChecker.GuessIsDue(new AkinatorQuestion(string.Empty, currentProgression, currentlyAt), lastGuessAt);
            Assert.IsFalse(guessIsDue);
        }

        [TestMethod]
        public void GuessIsDue_ReturnsFalseWhenTooCloseTo80Steps()
        {
            const int currentProgression = 97;
            const int currentlyAt = 79;
            const int lastGuessAt = 70;
            var guessIsDue = GuessDueChecker.GuessIsDue(new AkinatorQuestion(string.Empty, currentProgression, currentlyAt), lastGuessAt);
            Assert.IsFalse(guessIsDue);
        }

        [TestMethod]
        public void GuessIsDue_ReturnsTrueWhenMoreThan80Steps()
        {
            const int currentProgression = 0;
            const int currentlyAt = 80;
            const int lastGuessAt = 79;
            var guessIsDue = GuessDueChecker.GuessIsDue(new AkinatorQuestion(string.Empty, currentProgression, currentlyAt), lastGuessAt);
            Assert.IsTrue(guessIsDue);
        }

        [TestMethod]
        public void GuessIsDue_ReturnsFalseWhenLastGuessWasLessThan5StepsAgo()
        {
            const int currentProgression = 99;
            const int currentlyAt = 29;
            const int lastGuessAt = 25;
            var guessIsDue = GuessDueChecker.GuessIsDue(new AkinatorQuestion(string.Empty, currentProgression, currentlyAt), lastGuessAt);
            Assert.IsFalse(guessIsDue);

            const int currentlyAt2 = 30;
            guessIsDue = GuessDueChecker.GuessIsDue(new AkinatorQuestion(string.Empty, currentProgression, currentlyAt2), lastGuessAt);
            Assert.IsTrue(guessIsDue);
        }

        [TestMethod]
        public void GuessIsDue_ReturnsTrueWhenLastGuessWasMoreOrEqual30StepsAgo()
        {
            const int currentProgression = 0;
            const int currentlyAt = 61;
            const int lastGuessAt = 32;
            var guessIsDue = GuessDueChecker.GuessIsDue(new AkinatorQuestion(string.Empty, currentProgression, currentlyAt), lastGuessAt);
            Assert.IsFalse(guessIsDue);

            const int currentlyAt2 = 62;
            guessIsDue = GuessDueChecker.GuessIsDue(new AkinatorQuestion(string.Empty, currentProgression, currentlyAt2), lastGuessAt);
            Assert.IsTrue(guessIsDue);
        }

        [TestMethod]
        public void GuessIsDue_ReturnsFalseWhenCurrentQuestionNull()
        {
            var guessIsDue = GuessDueChecker.GuessIsDue(null, 0);
            Assert.IsFalse(guessIsDue);
        }
    }
}
