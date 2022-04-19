using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform ray;

    private void Start()
    {
    }

    public void Fire()
    {
        Debug.DrawRay(ray.transform.position, ray.transform.forward);

        Debug.Log("fire");
        RaycastHit hit;
        if (Physics.Raycast(ray.transform.position, ray.transform.forward, out hit, Mathf.Infinity))
        {
            Debug.DrawRay(ray.transform.position, ray.transform.forward * 10, Color.red, 5.0f);
            Debug.Log("rayray");
        }
    }
}