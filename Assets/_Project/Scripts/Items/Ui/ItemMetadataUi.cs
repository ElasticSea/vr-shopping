using System.Linq;
using Core.Extensions;
using Items;
using Items.Ui;
using TMPro;
using UnityEngine;

public class ItemMetadataUi : MonoBehaviour, IItemMetadataUi
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private RectTransform infoTransform;
    [SerializeField] private TMP_Text infoText;
    [SerializeField] private RectTransform focusTransform;
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
            var worldCenter = item.transform.TransformPoint(focusedItemBounds.center);
            
            transform.position = worldCenter;
            transform.rotation = Quaternion.LookRotation(camera.transform.forward);

            var rectSize = CalculateUiRectSize();
            SetRectSize(rectSize);
        }
    }

    private void SetRectSize(Vector2 rectSize)
    {
        focusTransform.SetSize(rectSize);
        infoTransform.anchoredPosition = infoTransform.anchoredPosition.SetX(-rectSize.x / 2);
        infoTransform.SetHeight(rectSize.y);
    }

    private Vector2 CalculateUiRectSize()
    {
        var uiVertices = focusedItemBounds.GetVertices().Select(v => item.transform.TransformPoint(v, focusTransform));
        var uiRect = uiVertices.Select(p => p.FromXY()).ToRect();
        return uiRect.size + new Vector2(offset, offset);
    }
}
