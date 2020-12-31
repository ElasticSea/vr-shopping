using UnityEngine;

namespace Items
{
    public class ItemComponent : MonoBehaviour
    {
        [SerializeField] private string name;
        [SerializeField] private string category;
        [SerializeField] private float price;
        [SerializeField] private string weight;

        public string Name
        {
            get => name;
            set => name = value;
        }

        public string Category
        {
            get => category;
            set => category = value;
        }

        public float Price
        {
            get => price;
            set => price = value;
        }

        public string Weight
        {
            get => weight;
            set => weight = value;
        }
    }
}