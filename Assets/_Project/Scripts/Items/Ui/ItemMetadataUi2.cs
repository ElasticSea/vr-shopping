using Core.Extensions;
using Items;
using Items.Ui;
using TMPro;
using UnityEngine;

public class ItemMetadataUi2 : MonoBehaviour, IItemMetadataUi
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private TMP_Text infoText;
    [SerializeField] private Camera camera;
    [SerializeField] private float offset;
    
    private ItemComponent item;
    private Bounds focusedItemBounds;

    public ItemComponent Item
    {
        get => item;
        set
        {
            if (item != value)
            {
                item = value;
                if (item)
                {
                    focusedItemBounds = item.gameObject.GetLocalMeshBounds();
                    infoText.text = $"{item.Name}\n{item.Price}\n{Item.Weight}";
                    canvas.enabled = true;
                }
                else
                {
                    canvas.enabled = false;
                }

            }
        }
    }

    private void LateUpdate()
    {
        if (item)
        {
            var itemCenter = item.transform.TransformPoint(focusedItemBounds.center);
            transform.position = itemCenter - camera.transform.right * (focusedItemBounds.size.magnitude/2 + offset);
            transform.rotation = Quaternion.LookRotation(transform.position - camera.transform.position);
        }
    }
}
