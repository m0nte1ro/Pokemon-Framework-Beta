using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ReadPokemonJSON
{
    class Pokemon
    {
        public string name { get; set; }
        public string id { get; set; }
        public string imageurl { get; set; }
        public string xdescription { get; set; }
        public string ydescription { get; set; }
        public string height { get; set; }
        public string category { get; set; }
        public string weight { get; set; }
        public IList<string> typeofpokemon { get; set; }
        public IList<string> weaknesses { get; set; }
        public IList<string> evolutions { get; set; }
        public IList<string> abilities { get; set; }
        public int hp { get; set; }
        public int attack { get; set; }
        public int defense { get; set; }
        public int special_attack { get; set; }
        public int special_defense { get; set; }
        public int speed { get; set; }
        public int total { get; set; }
        public string male_percentage { get; set; }
        public string female_percentage { get; set; }
        public int genderless { get; set; }
        public string cycles { get; set; }
        public string egg_groups { get; set; }
        public string evolvedfrom { get; set; }
        public string reason { get; set; }
        public string base_exp { get; set; }
    }
}
