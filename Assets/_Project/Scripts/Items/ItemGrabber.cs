using ElasticSea.Framework.Util.PropertyDrawers;
using Items.Ui;
using Rigs;
using UnityEngine;

namespace Items
{
    public class ItemGrabber : MonoBehaviour
    {
        [SerializeField] private Hand grabber;
        
        [CustomObjectPicker(typeof(IItemMetadataUi))]
        [SerializeField] private Component itemMetadataUi;
        
        private void Update()
        {
            var grabbedObject = grabber.Grabbed;
            if (grabbedObject && grabbedObject.GetComponent<ShoppingItem>())
            {
                ((IItemMetadataUi)itemMetadataUi).Item = grabbedObject.GetComponent<ShoppingItem>();
            }
            else
            {
                ((IItemMetadataUi)itemMetadataUi).Item = null;
            }
        }
    }
}