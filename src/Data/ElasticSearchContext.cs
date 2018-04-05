using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using Nest;
using Pokedex.Models;

namespace Pokedex.Data
{
    public class ElasticSearchContext : IElasticSearchContext
    {
        private static Uri node;

        public ElasticClient GetClient()
        {
            node = new Uri("http://172.17.0.6:9200");
            settings.DefaultIndex("pokemon_cleansed");
            var client = new ElasticClient(settings);
            return client;
        }

        private static readonly ConnectionSettings settings = new ConnectionSettings();

        private ElasticSearchContext() { }

        public static ConnectionSettings Settings
        {
            get
            {
                return new ConnectionSettings(node);
            }
        }
    }
}
