using Core.Util.PropertyDrawers;
using Items.Ui;
using UnityEngine;

namespace Items
{
    public class ItemGrabber : MonoBehaviour
    {
        [SerializeField] private OVRGrabber grabber;
        
        [CustomObjectPicker(typeof(IItemMetadataUi))]
        [SerializeField] private Component itemMetadataUi;
        
        private void Update()
        {
            var grabbedObject = grabber.grabbedObject;
            if (grabbedObject && grabbedObject.GetComponent<ItemComponent>())
            {
                ((IItemMetadataUi)itemMetadataUi).Item = grabbedObject.GetComponent<ItemComponent>();
            }
            else
            {
                ((IItemMetadataUi)itemMetadataUi).Item = null;
            }
        }
    }
}