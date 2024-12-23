using UnityEngine;

public class SpinDetector : MonoBehaviour
{
    [SerializeField] private Prize_Display prize_Display = null;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SpinPrizeSection"))
        {
            Debug.Log(collision.gameObject.name);
            prize_Display.UpdateValues(collision.GetComponent<SpinPrizeSection>().PassReward());
        }
    }
}
