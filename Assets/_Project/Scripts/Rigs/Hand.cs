using ElasticSea.Framework.Scripts.Extensions;
using UnityEngine;
using UnityEngine.XR;

namespace Rigs
{
    public class Hand : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private SphereCollider grabVolume;
        
        [Header("Properties")]
        [SerializeField] private bool isLeft;
        [SerializeField] private float threshold = 0.5f;
        [SerializeField] private float margin = 0.1f;
        
        private bool holding;
        private Rigidbody grabbed;
        private Transform grabbedParent;

        public bool IsBeingGripped => holding;
        public Rigidbody Grabbed => grabbed;

        private void Awake()
        {
            grabVolume.isTrigger = true;
        }

        private void Update()
        {
            var leftRightCharacteristics = (isLeft ? InputDeviceCharacteristics.Left : InputDeviceCharacteristics.Right);
            var deviceCharacteristics = leftRightCharacteristics | InputDeviceCharacteristics.Controller;
            deviceCharacteristics.GetDevice().TryGetFeatureValue(CommonUsages.grip, out var grip);

            if (holding == false && grip >= threshold + margin)
            {
                OnGrab();
                holding = true;
            }
            else if (holding == true && grip <= threshold - margin)
            {
                OnRelease();
                holding = false;
            }
        }

        private void OnGrab()
        {
            var colliderCandidates = grabVolume.Overlap();
            foreach (var collider in colliderCandidates)
            {
                var colliderRigidbody = collider.GetComponent<Rigidbody>();
                if (colliderRigidbody)
                {
                    if (colliderRigidbody.isKinematic == false)
                    {
                        grabbed = colliderRigidbody;
                        grabbed.isKinematic = true;
                        grabbedParent = grabbed.transform.parent;
                        grabbed.transform.SetParent(transform, true);
                    }
                }
            }
        }

        private void OnRelease()
        {
            if (grabbed)
            {
                grabbed.transform.SetParent(grabbedParent, true);
                grabbed.isKinematic = false;
                grabbed = null;
            }
        }
    }
}