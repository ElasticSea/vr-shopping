using System.Collections.Generic;
using System.Linq;
using Items;
using TMPro;
using UnityEngine;

namespace ShoppingCart
{
    public class ShoppingCartContent : MonoBehaviour
    {
        private HashSet<ItemComponent> content = new HashSet<ItemComponent>();

        [SerializeField] private TMP_Text text;
   
        private void OnTriggerEnter(Collider other)
        {
            var item = other.GetComponent<ItemComponent>();
            if (item)
            {
                content.Add(item);
                UpdateText();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var item = other.GetComponent<ItemComponent>();
            if (item)
            {
                content.Remove(item);
                UpdateText();
            }
        }

        private void UpdateText()
        {
            text.text = $"${content.Sum(component => component.Price):F2}";
        }
    }
}
