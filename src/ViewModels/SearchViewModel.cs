using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pokedex.Models;

namespace Pokedex.ViewModels
{
    public class SearchViewModel
    {
        public string SearchString { get; set; }
        public IEnumerable<Pokemon> Pokemons { get; set; }
        public int AmountOfHits { get; set; }
        public string SelectedWeakness { get; set; }
        public IEnumerable<string> SelectedWeaknesses { get; set; }
        public IEnumerable<SelectListItem> AvailableWeaknesses { get; set; }

        public SearchViewModel()
        {
            Pokemons = new List<Pokemon>();
            SelectedWeaknesses = new List<string>();
            AvailableWeaknesses = new List<SelectListItem>();
        }
    }
}
