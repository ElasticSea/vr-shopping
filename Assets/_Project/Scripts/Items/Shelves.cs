using System.Collections.Generic;
using System.Linq;
using ElasticSea.Framework.Extensions;
using Items.Model;
using UnityEngine;

namespace Items
{
    public class Shelves : MonoBehaviour
    {
        public void Fill(IEnumerable<Item> items, Vector2 itemsOffset)
        {
            var itemPrefabs = items.Select(ItemFactory.CreateItem).ToList();

            var index = 0;
            foreach (var shelf in GetComponentsInChildren<Shelf>())
            {
                index = FillShelf(shelf, itemPrefabs, index, itemsOffset);
            }

            foreach (var itemPrefab in itemPrefabs)
            {
                Destroy(itemPrefab);
            }
        }

        private int FillShelf(Shelf shelf, List<GameObject> itemPrefabs, int index, Vector2 offset)
        {
            var x = shelf.Bounds.min.x;
            var y = shelf.Bounds.min.y;

            while (true)
            {
                var itemPrefab = itemPrefabs[index++ % itemPrefabs.Count];

                var itemBounds = itemPrefab.GetCompositeMeshBounds();

                // Item does not fit. Shelf is full
                if (x + itemBounds.size.x > shelf.Bounds.max.x)
                {
                    break;
                }

                var z = shelf.Bounds.min.z;
                while (true)
                {
                    var itemInstance = Instantiate(itemPrefab, shelf.transform, false);
                    itemInstance.transform.localPosition = new Vector3(x, y, z) - itemBounds.min;

                    SetupItem(itemInstance);

                    z += itemBounds.size.z + offset.y;

                    if (z + itemBounds.size.z > shelf.Bounds.max.z)
                    {
                        // There is no space in the back of the shelf for another copy
                        break;
                    }
                }

                x += itemBounds.size.x + offset.x;
                index++;
            }

            return index;
        }

        private void SetupItem(GameObject item)
        {
            var bounds = item.GetCompositeMeshBounds();

            var boxCollider = item.AddComponent<BoxCollider>();
            boxCollider.center = bounds.center;
            boxCollider.size = bounds.size;

            // var grabbabale = item.AddComponent<OVRGrabbable>();
            // grabbabale.snapOffset = item.transform;
            // grabbabale.grabPoints = new[] {boxCollider};

            var rigidbody = item.AddComponent<Rigidbody>();
            rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
        }
    }
}