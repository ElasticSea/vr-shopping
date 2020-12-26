using UnityEngine;

namespace Items
{
    public class ItemGrabber : MonoBehaviour
    {
        [SerializeField] private OVRGrabber grabber;
        [SerializeField] private ItemMetadataUi itemMetadataUi;
        
        private void Update()
        {
            var grabbedObject = grabber.grabbedObject;
            if (grabbedObject && grabbedObject.GetComponent<ItemComponent>())
            {
                itemMetadataUi.Item = grabbedObject.GetComponent<ItemComponent>();
            }
            else
            {
                itemMetadataUi.Item = null;
            }
        }
    }
}