using System;
using ElasticSea.Framework.Extensions;
using UnityEngine;

namespace Rigs
{
    public class HandRenderer : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Hand hand;
        [SerializeField] private Renderer renderer;
        
        private Material material;

        private void Start()
        {
            material = renderer.material;
            material.SetupMaterialWithBlendMode(MaterialExtensions.Mode.Fade);
        }
        
        private void Update()
        {
            var color = hand.IsBeingGripped ? Color.red : Color.green;
            material.color = color.SetAlpha(0.3f);
        }
    }
}