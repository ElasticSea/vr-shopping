using System.Collections.Generic;
using Core.Scripts.Extensions;
using Core.Util;
using Items.Model;
using UnityEngine;

namespace Items
{
    public class ItemFactory
    {
        public static GameObject CreateItem(Item item)
        {
            var meshBytes = item.Visual.SourceBytes;
            var mesh = MeshSerializer.DeserializeMesh(meshBytes);

            var go = new GameObject();
            go.AddComponent<MeshFilter>().mesh = mesh;

            var mats = new List<UnityEngine.Material>();
            foreach (var m in item.Visual.Materials)
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

            go.AddComponent<MeshRenderer>().materials = mats.ToArray();

            return go;
        }
    }
}