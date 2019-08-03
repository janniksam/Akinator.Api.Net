using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Akinator.Api.Net.Enumerations;
using Akinator.Api.Net.Model;
using Akinator.Api.Net.Utils;

namespace Akinator.Api.Net.Example
{
    public class Program
    {
        static void Main(string[] args)
        {
            var p = new Program();
            p.Start().Wait();
        }

        private async Task Start()
        {
            Console.WriteLine("Starting session...");

            using (var client = new AkinatorClient(Language.German, ServerType.Person))
            {
                var question = await client.StartNewGame();
                var answer = GetAnswerFor(question);
                do
                {
                    question = await client.Answer(answer);
                    if (client.GuessIsDue(question))
                    {
                        var guess = await client.GetGuess();
                        if (VerifyGuess(guess))
                        {
                            break;
                        }
                    }

                    answer = GetAnswerFor(question);
                } while (true);
            }

            Console.WriteLine("Ending session...");
            Console.ReadLine();
        }

        private static bool VerifyGuess(AkinatorGuess[] guess)
        {
            int yesNoVal;
            do
            {
                Console.WriteLine("My guess is:");
                Console.WriteLine($"{guess[0].Name} - {guess[0].Description}");
                Console.WriteLine("Is this correct? (0 = no; 1 = yes)");
            } while (!int.TryParse(Console.ReadLine(), out yesNoVal) && !(yesNoVal == 0 || yesNoVal == 1));

            if (yesNoVal == 1)
            {
                return true;
            }

            return false;
        }

        private static AnswerOptions GetAnswerFor(AkinatorQuestion question)
        {
            string line;
            AnswerOptions answer;
            do
            {
                Console.WriteLine($"Question {question.Step + 1} ({question.Progression}): {question.Text}");
                Console.WriteLine("Possible answers:");
                var options = GetOptions();
                foreach (var option in options)
                {
                    Console.WriteLine($"{option.Key}: {option.Value}");
                }

                line = Console.ReadLine();
            } while (!Enum.TryParse(line, out answer));

            return answer;
        }

        public static Dictionary<int, string> GetOptions()
        {
            var myDic = new Dictionary<int, string>();
            foreach (AnswerOptions foo in Enum.GetValues(typeof(AnswerOptions)))
            {
                myDic.Add((int)foo, foo.ToString());
            }

            return myDic;

            return myDic;
        }
    }
}
