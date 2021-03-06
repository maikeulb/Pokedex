﻿using System;
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
            _settings.DefaultIndex("pokemon_cleansed");
            _client = new ElasticClient(_settings);
        }

        public IActionResult Index()
        {
            if (_client.IndexExists("pokemon_cleansed").Exists == false)
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
                        Img = pokemonRoot.img,
                        PrevEvolution = pokemonRoot.prev_evolution,
                        NextEvolution = pokemonRoot.next_evolution,
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

        [HttpPost]
        public IActionResult Index([Bind("SearchString")] SearchViewModel model)
        {
            var result = _client.Search<Pokemon>(s => s
                .Query(q => q
                    .Bool(b => b
                        .Must(mu => mu
                            .MultiMatch(mp => mp
                                .Query(model.SearchString)
                                .Fields(f => f
                                    .Field(f1 => f1.Name, 3)
                                    .Field(f2 => f2.Type, 2)
                                    .Field(f3 => f3.PrevEvolution, 1)
                                    .Field(f4 => f4.NextEvolution, 1)
                                )
                            )
                        )
                    )
                )
                .Size(20)
            );

            model.Pokemons = result.Documents;
            model.AmountOfHits = (int)result.Total;

            return View(model);
        }

        public async Task<IActionResult> List()
        {
            var result = await _client.SearchAsync<Pokemon>(s => s
                .Query(q => q.MatchAll())
                .Size(200)
                .Sort(ss => ss
                    .Ascending(p => p.Id))
                );

            IEnumerable<Pokemon> listOfPokemon = result.Documents;

            return View(listOfPokemon);
        }
    }
}
