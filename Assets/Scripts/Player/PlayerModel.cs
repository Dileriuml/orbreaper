namespace OrbReaper.Player
{
    public class PlayerModel
    {
        public PlayerModel(LivingBeingModel livingModel, CharacterModel character)
        {
            LivingModel = livingModel;
            Character = character;
        }

        public LivingBeingModel LivingModel { get; }

        public CharacterModel Character { get; }
    }
}