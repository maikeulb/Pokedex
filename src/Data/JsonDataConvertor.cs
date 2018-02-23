using ElasticSearchWebAppTest.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Pokedex.Data
{
    public class JsonDataConvertor
    {
        public List<Pokemon.RootObject> JsonToPokemonList()
        {
            string dataPath = @"Data/pokedex.json";
            string json = File.ReadAllText(dataPath);
            var listWithPokemonRoots = JsonConvert.DeserializeObject<List<Pokemon.RootObject>>(json);

            return listWithPokemonRoots;
        }
    }
}
