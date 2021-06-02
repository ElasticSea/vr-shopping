using System.Collections.Generic;
using ElasticSea.Framework.Extensions;
using ElasticSea.Framework.Util;
using Items.Model;
using UnityEngine;

namespace Items
{
    public class ItemFactory
    {
        public static GameObject CreateItem(Item item)
        {
            var go = new GameObject();
            go.name = item.Name;

            foreach (var visual in item.Visuals)
            {
                var visualGo = new GameObject();
                visualGo.transform.SetParent(go.transform, false);
                
                var meshBytes = visual.SourceBytes;
                var mesh = MeshSerializer.DeserializeMesh(meshBytes);
                mesh.RecalculateBounds();

                visualGo.AddComponent<MeshFilter>().mesh = mesh;

                var mats = new List<UnityEngine.Material>();
                foreach (var m in visual.Materials)
                {
                    var mat = new UnityEngine.Material(Shader.Find("Standard"));

                    foreach (var (key, value) in m.IntProperties)
                    {
                        mat.SetInt(key, value);
                    }

                    foreach (var (key, value) in m.FloatProperties)
                    {
                        mat.SetFloat(key, value);
                    }

                    foreach (var (key, value) in m.ColorProperties)
                    {
                        mat.SetColor(key, value);
                    }

                    foreach (var (key, value) in m.TextureProperties)
                    {
                        var texbytes = value.SourceBytes;
                        var texture2D = new Texture2D(2, 2);
                        texture2D.LoadImage(texbytes);
                        mat.SetTexture(key, texture2D);
                    }

                    foreach (var (key, value) in m.BoolProperties)
                    {
                        if (value)
                        {
                            mat.EnableKeyword(key);
                        }
                        else
                        {
                            mat.DisableKeyword(key);
                        }
                    }

                    mat.renderQueue = m.RenderQueue;

                    mats.Add(mat);
                }

                visualGo.AddComponent<MeshRenderer>().materials = mats.ToArray();
            }

            var itemComponent = go.AddComponent<ShoppingItem>();
            itemComponent.Name = item.Name;
            itemComponent.Category = item.Category;
            itemComponent.Price = item.Price;
            itemComponent.Weight = item.Weight;

            return go;
        }
    }
}