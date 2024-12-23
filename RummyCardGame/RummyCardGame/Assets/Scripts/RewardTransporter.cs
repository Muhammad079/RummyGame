using DG.Tweening;
using UnityEngine;

public class RewardTransporter : MonoBehaviour
{
    bool towardsTarget = false;
    Transform target = null;
    Vector3[] wayPoints = { Vector3.zero, Vector3.zero, Vector3.zero };
    public void MoveNow(Transform targetTransform)
    {
        target = targetTransform;
        transform.parent = target;
        this.gameObject.AddComponent<Canvas>();
        GetComponent<Canvas>().overrideSorting = true;
        GetComponent<Canvas>().sortingOrder = 10;
        Vector3 pos = new Vector3(transform.position.x + 2, transform.position.y + 2, transform.position.z);
        wayPoints[0] = (transform.position);
        wayPoints[1] = (pos);
        wayPoints[2] = (target.position);
        transform.DOPath(wayPoints, 0.7f, PathType.CatmullRom).SetEase(Ease.Linear).OnComplete(Transported);
    }
    private void Transported()
    {
        Destroy(this.gameObject);
    }
}
