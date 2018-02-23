using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Nest;
using Pokedex.Models;
using Pokedex.Data;
using Pokedex.ViewModels;


namespace Pokedex.Controllers
{
    public class HomeController : Controller
    {
        private readonly Uri _node;
        private static ConnectionSettings _settings;
        private static ElasticClient _client;

        public HomeController()
        {
            _node = new Uri("http://localhost:9200");
            _settings = new ConnectionSettings(_node);
            _settings.DefaultIndex("pokemon");
            _client = new ElasticClient(_settings);
        }

        public IActionResult Index()
        {
            if (_client.IndexExists("pokemon").Exists == false)
            {

                var indexDescriptor = new CreateIndexDescriptor("pokemon")
                        .Mappings(ms => ms
                            .Map<Pokemon>(m => m.AutoMap()
                            
                            ));

                _client.CreateIndex("pokemon", i => indexDescriptor);


                var convertor = new JsonDataConvertor();
                var listWithPokemonRoots = convertor.JsonToPokemonList();

                foreach (var pokemonRoot in listWithPokemonRoots)
                {
                    var pokemon = new Pokemon()
                    {
                        Id = pokemonRoot.id,
                        Name = pokemonRoot.name,
                        Type = pokemonRoot.type,
                        Weaknesses = pokemonRoot.weaknesses,
                    };

                    var result = _client.Bulk(b => b
                        .Index<Pokemon>(i => i.Document(pokemon))
                    );
                }
            }

            var model = new SearchViewModel()
            {
            };

            return View(model);
        }

    }
}
