using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Core.Scripts.Extensions;
using Items.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.UnityConverters.Math;

namespace Items
{
    public class ItemProvider : IDisposable
    {
        private string endpointUrl;
        private HttpClient client;

        public ItemProvider(string endpointUrl)
        {
            this.endpointUrl = endpointUrl;
            client = new HttpClient();
            ServicePointManager.DefaultConnectionLimit = 16;
        }

        public async Task<List<Item>> ListItems(int limit)
        {
            var json = await (await client.GetAsync(endpointUrl)).Content.ReadAsStringAsync();
            var items = JsonConvert.DeserializeObject<List<Item>>(json, new ColorConverter(), new StringEnumConverter());

            var tasks = new List<Task>();
            foreach (var item in items.Take(limit))
            {
                var task = DownloadMesh(item.Visual);
                tasks.Add(task);

                foreach (var (key, texture) in item.Visual.Materials.SelectMany(m => m.TextureProperties))
                {
                    task = DownloadTexture(texture);
                    tasks.Add(task);
                }
            }

            await Task.WhenAll(tasks);
            
            return items;
        }

        private async Task DownloadMesh(Visual visual)
        {
            visual.SourceBytes = await client.GetByteArrayAsync(visual.Source);
        }

        private async Task DownloadTexture(MaterialTexture texture)
        {
            texture.SourceBytes = await client.GetByteArrayAsync(texture.Source);
        }

        public void Dispose()
        {
            client.Dispose();
        }
    }
}