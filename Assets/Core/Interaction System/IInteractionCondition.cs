public interface IInteractionCondition
{
    bool IsMet(PlayerController player);
    string GetFailMessage();
}
