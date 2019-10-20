using UnityEngine;

public class Diamond : MonoBehaviour, IPickUpable
{
    [SerializeField] private GameObject _particles;


    public void PickUp()
    {
        Instantiate( _particles, transform.position, transform.rotation );
        Destroy( gameObject );
    }
}