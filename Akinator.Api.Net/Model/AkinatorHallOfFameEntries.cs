namespace Akinator.Api.Net.Model
{
    public class AkinatorHallOfFameEntries
    {
        public AkinatorHallOfFameEntries(string awardId, string characterName, string description, string type, string winnerName, string delai, string pos)
        {
            AwardId = awardId;
            CharacterName = characterName;
            Description = description;
            Type = type;
            WinnerName = winnerName;
            Delai = delai;
            Pos = pos;
        }

        public string AwardId { get; }
        public string CharacterName { get; }
        public string Description { get; }
        public string Type { get; }
        public string WinnerName { get; }
        public string Delai { get; }
        public string Pos { get; }
    }
}