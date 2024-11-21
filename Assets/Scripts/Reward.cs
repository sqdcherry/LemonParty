[System.Serializable]
public class Reward
{
    public enum RewardType
    { 
        COIN,
        CRYSTAL
    }

    public RewardType type;
    public int Value;
}