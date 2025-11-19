using UnityEngine;

namespace Preliy.SliderCrank
{
    public class MoveCrank : MonoBehaviour
    {
        // ReSharper disable once InconsistentNaming
        public float LengthAC
        {
            get => _lengthAC;
            set
            {
                _lengthAC = value; 
                Refresh();
            }
        }

        [Header("Control")] 
        [SerializeField]
        private bool _execute;
        [SerializeField]
        // ReSharper disable once InconsistentNaming
        private float _lengthAC;

        [Header("References")]
        [SerializeField]
        private bool _flipSide;
        [SerializeField]
        private Transform _a;
        [SerializeField]
        private Transform _b;
        [SerializeField]
        private Transform _c;
        
        [Header("Gizmos")]
        [SerializeField]
        private bool _showGizmos;

        private void OnValidate()
        {
            Refresh();
        }

        private void Refresh()
        {
            if (!_execute) return;
            
            var a = Vector3.Distance(_b.position, _c.position); // side from B to C
            var b = _lengthAC; // side from C to A
            var c = Vector3.Distance(_a.position, _b.position); // side from A to B
            
            if (a + b <= c || a + c <= b || b + c <= a)
            {
                Debug.LogWarning("Invalid triangle side lengths (triangle inequality violated).");
                return;
            }
            
            // Angle at B (beta) using law of cosines
            // cos(beta) = (c^2 + a^2 - b^2) / (2 * c * a)
            var cosBeta = (c * c + a * a - b * b) / (2f * c * a);
            cosBeta = Mathf.Clamp(cosBeta, -1f, 1f);
            
            var betaRad = Mathf.Acos(cosBeta);
            var betaDeg = betaRad * Mathf.Rad2Deg;
            
            // ReSharper disable once InconsistentNaming
            var directionBA = (_a.position - _b.position).normalized;
            var signedAngle = _flipSide ? -betaDeg : betaDeg;
            
            var rotation = Quaternion.AngleAxis(signedAngle, transform.right);
            var directionBC = rotation * directionBA;
            var positionC = _b.position + directionBC * a;
            _c.position = positionC;
        }

        private void OnDrawGizmos()
        {
            if (!_showGizmos) return;
            if (_a == null) return;
            if (_b == null) return;
            if (_c == null) return;
            
#if UNITY_EDITOR
            UnityEditor.Handles.color = Color.cyan;
            var distance = Vector3.Distance(_b.position, _c.position);
            UnityEditor.Handles.DrawWireDisc(_b.position, transform.rotation * Vector3.right, distance);
#endif 
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(_a.position, 0.02f);
            Gizmos.DrawSphere(_b.position, 0.02f);
            Gizmos.DrawSphere(_c.position, 0.02f);
            
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(_a.position, _b.position);
            Gizmos.DrawLine(_b.position, _c.position);
            
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(_c.position, _a.position);
        }
        
    }
}
