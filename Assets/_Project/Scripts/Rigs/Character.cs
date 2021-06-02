using ElasticSea.Framework.Extensions;
using UnityEngine;
using UnityEngine.XR;

namespace Rigs
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private float speed = 1;
        
        private void Update()
        {
            var leftController = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
            leftController.GetDevice().TryGetFeatureValue(CommonUsages.primary2DAxis, out var axis2d);

            transform.position += axis2d.ToXZ() * (Time.deltaTime * speed);
        }
    }
}