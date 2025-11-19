using UnityEngine;
using UnityEngine.Events;

public class DemoMotion : MonoBehaviour
{
    [SerializeField]
    private Vector2 _range = new (0, 1);
    [SerializeField]
    private float _speed = 0.2f;
    
    public UnityEvent<float> OnValueChanged;

    private void Update()
    {
        var result = Mathf.Lerp(_range.x, _range.y, Mathf.PingPong(Time.time * _speed, 1));
        OnValueChanged?.Invoke(result);
    }
}
