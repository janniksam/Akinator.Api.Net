using System;

namespace Akinator.Api.Net.Model
{
    public class AkinatorGuess
    {
        public AkinatorGuess(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; }

        public string Description { get; }

        public float Probabilty { get; set; }

        public Uri PhotoPath { get; set; }
    }
}