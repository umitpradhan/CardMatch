namespace CardMatch.Core.Domain
{
    public sealed class ComboTracker
    {
        public int CurrentCombo { get; private set; }

        public void RegisterMatch()
        {
            CurrentCombo++;
        }

        public void RegisterMismatch()
        {
            CurrentCombo = 0;
        }

        public void Reset()
        {
            CurrentCombo = 0;
        }
    }
}