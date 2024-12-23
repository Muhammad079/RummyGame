using UnityEngine;

public class SpinPrizeSection : MonoBehaviour
{
    [SerializeField] private RewardItems reward = null;
   public void SetReward(RewardItems rewards)
    {
        reward = rewards;
    }
    public RewardItems PassReward()
    {
        return reward;
    }
}
