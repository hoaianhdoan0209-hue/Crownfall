namespace Crownfall.Core
{
    public interface IRandomService
    {
        int Range(int minInclusive, int maxExclusive);
        float Value();
    }
}
