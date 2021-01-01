using System.Linq;
using Core.Extensions;
using TMPro;
using UnityEngine;

namespace ShoppingCart
{
    public class ShoppingCartUi : MonoBehaviour
    {
        [SerializeField] private ShoppingCartContent content;
        [SerializeField] private TMP_Text list;
        [SerializeField] private TMP_Text price;

        private void Awake()
        {
            content.OnContentChanged += items =>
            {
                list.text = items.GroupBy(i => i.Name).JoinString("\n", grouping => $"{grouping.Count()}x {grouping.Key}");
                price.text = $"${items.Sum(component => component.Price):F2}";
            };
        }
    }
}