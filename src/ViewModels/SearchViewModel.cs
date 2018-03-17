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
        public string Selectedtype { get; set; }
        public IEnumerable<string> SelectedType { get; set; }
        public IEnumerable<SelectListItem> AvailableTypes { get; set; }

        public SearchViewModel()
        {
            Pokemons = new List<Pokemon>();
            SelectedType = new List<string>();
            AvailableTypes = new List<SelectListItem>();
        }
    }
}
