using System;
using System.Collections.Generic;
using Items;
using UnityEngine;

namespace ShoppingCart
{
    public class ShoppingCartContent : MonoBehaviour
    {
        private HashSet<ShoppingItem> content = new HashSet<ShoppingItem>();

        public event Action<ISet<ShoppingItem>> OnContentChanged = set => { };
        
        private void OnTriggerEnter(Collider other)
        {
            var item = other.GetComponent<ShoppingItem>();
            if (item)
            {
                content.Add(item);
                OnContentChanged(content);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var item = other.GetComponent<ShoppingItem>();
            if (item)
            {
                content.Remove(item);
                OnContentChanged(content);
            }
        }
    }
}
