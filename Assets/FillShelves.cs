using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Extensions;
using Items;
using Items.Model;
using OculusSampleFramework;
using UnityEngine;
using Bounds = UnityEngine.Bounds;

public class FillShelves : MonoBehaviour
{
    [SerializeField] private int maxItems = 30;
    [SerializeField] private BoxCollider[] shelfs;

    private async void Start()
    {
        using (var provider = new ItemProvider("https://vr-shopping.azurewebsites.net/api/QueryItems"))
        {
            var items = await Task.Run(() => provider.ListItems(maxItems));

            var startIndex = 0;
            foreach (var shelf in shelfs)
            {
                startIndex += FillShelf(shelf, items.Skip(startIndex), new Vector2(0.05f, 0.05f));
            }
        }
    }

    private int FillShelf(BoxCollider space, IEnumerable<Item> items, Vector2 offset)
    {
        var bounds = new Bounds(space.center, space.size);
        var boundsMin = bounds.min;
        var positionX = boundsMin.x;

        var index = 0;
        foreach (var item in items)
        {
            var go = ItemFactory.CreateItem(item);
            var itemBounds = go.GetCompositeMeshBounds();

            if (positionX + itemBounds.size.x > bounds.max.x )
            {
                Destroy(go);
                break;
            }
            
            var positionZ = boundsMin.z;
            while (true)
            {
                var go2 = Instantiate(go, space.transform, false);
                go2.transform.localPosition = new Vector3(positionX, boundsMin.y, positionZ) - itemBounds.min;

                SetupItem(go2);
                
                positionZ += itemBounds.size.z + offset.y;

                if (positionZ + itemBounds.size.z > bounds.max.z )
                {
                    break;
                }
            }
            
            Destroy(go);

            positionX += itemBounds.size.x + offset.x;
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
                
        var grabbabale = item.AddComponent<OVRGrabbable>();
        grabbabale.snapOffset = item.transform;
        grabbabale.grabPoints = new[] {boxCollider};
                
        var rigidbody = item.AddComponent<Rigidbody>();
        rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
    }
}
