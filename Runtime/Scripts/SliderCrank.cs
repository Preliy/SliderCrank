using JetBrains.Annotations;
using UnityEngine;

namespace Preliy.SliderCrank
{
    [ExecuteInEditMode]
    public class SliderCrank : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField]
        private float _length = 1f;
        [SerializeField]
        private bool _useNegativeSolution;
        
        [Header("References")] 
        [SerializeField] 
        private Transform _pin;
        [SerializeField] 
        private Transform _slider;

        [Header("Settings")]
        [SerializeField]
        private UpdateType _updateType;
        [SerializeField]
        private bool _executeInEditor;
        [SerializeField] 
        private bool _showGizmos;
        
        private void Update()
        {
            if (_updateType != UpdateType.Update) return;
            ApplySliderPosition();
        }

        private void FixedUpdate()
        {
            if (_updateType != UpdateType.FixedUpdate) return;
            ApplySliderPosition();
        }
        
        private void LateUpdate()
        {
            if (_updateType != UpdateType.LateUpdate) return;
            ApplySliderPosition();
        }

        private void OnValidate()
        {
            _length = Mathf.Clamp(_length, 0f, Mathf.Infinity);
        }

        private void ApplySliderPosition()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying && !_executeInEditor) return;
#endif

            if (_slider == null) return;
            if (_pin == null) return;

            var p1 = new Vector2(0, _length * 10);
            var p2 = new Vector2(0, -_length * 10);
            var pin = transform.InverseTransformPoint(_pin.position);

            var intersections = GetLineCircleIntersections(p1, p2, new Vector2(pin.y, pin.z), _length);

            if (intersections == null) return;

            _slider.localPosition = _useNegativeSolution ? new Vector3(0, intersections[1].x, intersections[1].y) : new Vector3(0, intersections[0].x, intersections[0].y);
        }

        [CanBeNull]
        private Vector2[] GetLineCircleIntersections(Vector2 p1, Vector2 p2, Vector2 center, float radius)
        {
            var sliderNormal = p2 - p1;
            var a = Vector2.Dot(sliderNormal, sliderNormal);
            var b = 2 * Vector2.Dot(sliderNormal, p1 - center);
            var c = Vector2.Dot(center, center) - 2 * Vector2.Dot(center, p1) + Vector2.Dot(p1, p1) - radius * radius;
            var d = b * b - 4 * a * c;

            if (d < 0 || Mathf.Abs(a) < float.Epsilon)
            {
                return null;
            }

            var sqrtD = Mathf.Sqrt(d);
            var doubleA = a * 2;
            
            var positive = (-b - sqrtD) / doubleA;
            var negative = (-b + sqrtD) / doubleA;
            
            var result = new Vector2[2];
            result[0] = p1 + positive * sliderNormal;
            result[1] = p1 + negative * sliderNormal;
            return result;
        }

        private enum UpdateType
        {
            Update,
            FixedUpdate,
            LateUpdate
        }

        private void OnDrawGizmos()
        {
            if (!_showGizmos) return;
            if (_slider == null) return;
            if (_pin == null) return;
#if UNITY_EDITOR
            UnityEditor.Handles.color = Color.cyan;
            UnityEditor.Handles.DrawWireDisc(_pin.position, _slider.rotation * Vector3.right, _length);
#endif

            Gizmos.color = Color.red;
            Gizmos.DrawSphere(_pin.position, 0.02f);
            Gizmos.DrawSphere(_slider.position, 0.02f);
        }
    }
}



