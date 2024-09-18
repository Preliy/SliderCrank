using UnityEngine;

public class DemoRotation : MonoBehaviour
{
    [SerializeField]
    private Vector3 _axis = Vector3.right;
    [SerializeField]
    private float _speed;

    private void Update()
    {
        transform.Rotate(_axis, _speed); 
    }
}
