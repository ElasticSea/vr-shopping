using System.Threading.Tasks;
using Items;
using UnityEngine;

public class Setup : MonoBehaviour
{
    [SerializeField] private Shelves shelves;
    [SerializeField] private int maxItems = 30;
    [SerializeField] private Vector2 itemsOffset = new Vector2(0.05f, 0.05f);

    private async void Start()
    {
        using (var provider = new ItemProvider("https://vr-shopping.azurewebsites.net/api/QueryItems"))
        {
            var items = await Task.Run(() => provider.ListItems(maxItems));
            shelves.Fill(items, itemsOffset);
        }
    }
}
