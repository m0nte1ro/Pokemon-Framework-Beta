using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

namespace ReadPokemonJSON
{
    class Team
    {
        [JsonProperty]
        private string _name;
        [JsonProperty]
        private List<Pokemon> _teamMembers;
        [JsonIgnore]
        public string name { get { return _name; }  }
        public Team()
        {
            _name = "";
            _teamMembers = new List<Pokemon>();
        }
        private Team(string name, List<Pokemon> teamMembers)
        {
            this._name = name;
            this._teamMembers = teamMembers;
        }
        public void Create(string name, params string[] pokemonNames)//Cria uma equipa com um nome e número indeterminado de Pokemonsnote
        {
            List<Pokemon> pokemonTeam = new List<Pokemon>();
            int contadorTentativas;
            if (!Pokedex.isStarted())
            {
                Console.WriteLine("--->Error: Pokedex wasn't initialized.");
                return;
            }
            foreach (string pokemon in pokemonNames)
            {
                contadorTentativas = 0;
                if (pokemonTeam.Count < 6)
                {
                    char[] pokemonName = pokemon.ToLower().ToCharArray(); //Cria um array de chars com todas as letras da string type
                    pokemonName[0] = char.ToUpper(pokemonName[0]); //Mete o primeiro elemento do array a maiusculo 
                    string formatedPokemonName = new string(pokemonName); //Cria uma nova string apartir do array de chars
                    foreach (Pokemon p in Pokedex.GetAllPokemons())
                    {
                        if (formatedPokemonName == p.name)
                        {
                            pokemonTeam.Add(p);
                            break;
                        }
                        else
                        {
                            contadorTentativas++;
                            if (contadorTentativas == Pokedex.GetAllPokemons().Count)
                            {
                                Console.WriteLine("--->Error: Pokemon '" + formatedPokemonName + "' not found!");
                                break;
                            }
                        }
                    }
                }
            }
            if (pokemonNames.Length > 0)
            {
                _name = name;
                _teamMembers = pokemonTeam;
            }
        }
        public void AddMembers(params string[] pokemonNames)//Adiciona Pokemons a uma equipa já existente
        {
            if (!Pokedex.isStarted())
            {
                Console.WriteLine("--->Error: Pokedex wasn't initialized.");
                return;
            }
            int contadorTentativas = 0;
            foreach (string pokemon in pokemonNames)
            {
                foreach (Pokemon p in Pokedex.GetAllPokemons())
                {
                    if (p.name.ToLower() == pokemon.ToLower())
                    {
                        contadorTentativas = 0;
                        _teamMembers.Add(p);
                    }
                    else
                    {
                        contadorTentativas++;
                        if (contadorTentativas == Pokedex.GetAllPokemons().Count - 1)
                        {
                            Console.WriteLine("\n--->Error: Pokemon '" + pokemon + "' not found!");
                        }
                    }
                }
            }
        }
        public void RemoveMembers(params string[] pokemonNames)//Remove Pokemons de uma equipa já existente
        {
            if (!Pokedex.isStarted())
            {
                Console.WriteLine("--->Error: Pokedex wasn't initialized.");
                return;
            }
            int contadorTentativas = 0;
            foreach (string pokemon in pokemonNames)
            {
                foreach (Pokemon p in Pokedex.GetAllPokemons())
                {
                    if (p.name.ToLower() == pokemon.ToLower())
                    {
                        contadorTentativas = 0;
                        _teamMembers.Remove(p);
                    }
                    else
                    {
                        contadorTentativas++;
                        if (contadorTentativas == Pokedex.GetAllPokemons().Count - 1)
                        {
                            Console.WriteLine("\n--->Error: Pokemon '" + pokemon + "' not found!");
                        }
                    }
                }
            }
        }
        public void PrintAllPokemons()//Imprime todos os Pokemons de uma equipa
        {
            if (!Pokedex.isStarted())
            {
                Console.WriteLine("--->Error: Pokedex wasn't initialized.");
                return;
            }
            Console.WriteLine("\n---> Team " + _name + ":");
            if (_teamMembers.Count > 0)
            {
                foreach (Pokemon p in _teamMembers)
                {
                    Pokedex.PrintPokemonInfo(Int32.Parse(p.id.Substring(p.id.Length - 3)));
                }
            }
            else
                Console.WriteLine("\nThe team is empty.");
        }
        public ReadOnlyCollection<Pokemon> GetAllPokemons() //Retorna uma lista com todos os Pokemons
        {
            if (!Pokedex.isStarted())
                throw new InvalidOperationException("--->Error: Pokedex wasn't initialized.");
            if (_teamMembers.Count > 0)
            {
                return _teamMembers.AsReadOnly();
            }
            else
                throw new InvalidOperationException("\nThe team is empty.");
        }
        public void PrintAllPokemons(params Team[] equipas)//Imprime todos os Pokemons de uma equipa
        {
            if (!Pokedex.isStarted())
            {
                Console.WriteLine("--->Error: Pokedex wasn't initialized.");
                return;
            }
            foreach (Team t in equipas)
            {
                Console.WriteLine("\n---> Team " + t._name + ":");
                if (t._teamMembers.Count > 0)
                {
                    foreach (Pokemon p in t._teamMembers)
                    {
                        Pokedex.PrintPokemonInfo(Int32.Parse(p.id.Substring(p.id.Length - 3)));
                    }
                }
                else
                    Console.WriteLine("\nTeam " + t._name + " is empty.");
            }
        }
        public void PrintWeaknesses() //Imprime as fraquezas da equipa
        {
            if (!Pokedex.isStarted())
            {
                Console.WriteLine("--->Error: Pokedex wasn't initialized.");
                return;
            }
            if (_teamMembers.Count > 0)
            {
                Console.WriteLine("\n---> Team " + _name + " weaknesses:");
                foreach (Pokemon p in _teamMembers)
                {
                    string[] id = p.id.Split('#');
                    Pokedex.PrintWeaknesses(Int32.Parse(id[1]));
                }
            }
            else
                Console.WriteLine("\nTeam " + _name + " is empty.");
        }
        public void PrintWeaknesses(params Team[] equipas) //Imprime as fraquezas da equipa
        {
            if (!Pokedex.isStarted())
            {
                Console.WriteLine("--->Error: Pokedex wasn't initialized.");
                return;
            }

            foreach (Team t in equipas)
            {
                if (t._teamMembers.Count > 0)
                {
                    Console.WriteLine("\n---> Team " + t._name + " weaknesses:");
                    foreach (Pokemon p in t._teamMembers)
                    {
                        string[] id = p.id.Split('#');
                        Pokedex.PrintWeaknesses(Int32.Parse(id[1]));
                    }
                }
                else
                    Console.WriteLine("\nTeam " + t._name + " is empty.");
            }
        }
        public void PrintStrengths()//Imprime os pontos fortes da equipa
        {
            if (!Pokedex.isStarted())
            {
                Console.WriteLine("--->Error: Pokedex wasn't initialized.");
                return;
            }
            if (_teamMembers.Count > 0)
            {
                Console.WriteLine("\n---> Team " + _name + " weaknesses:");
                foreach (Pokemon p in _teamMembers)
                {
                    string[] id = p.id.Split('#');
                    Pokedex.PrintStrengths(Int32.Parse(id[1]));
                }
            }
            else
                Console.WriteLine("\nTeam " + _name + " is empty.");
        }
        public void PrintStrengths(params Team[] equipas) //Imprime as fraquezas da equipa
        {
            if (!Pokedex.isStarted())
            {
                Console.WriteLine("--->Error: Pokedex wasn't initialized.");
                return;
            }
            foreach (Team t in equipas)
            {
                if (t._teamMembers.Count > 0)
                {
                    Console.WriteLine("\n---> Team " + t._name + " weaknesses:");
                    foreach (Pokemon p in t._teamMembers)
                    {
                        string[] id = p.id.Split('#');
                        Pokedex.PrintStrengths(Int32.Parse(id[1]));
                    }
                }
                else
                    Console.WriteLine("\nTeam " + t._name + " is empty.");
            }
        }
        public void ImportTeam(string filepath, bool FeedBack) //Importa a equipa presente no ficheiro indicado
        {
            if (!Pokedex.isStarted())
            {
                Console.WriteLine("--->Error: Pokedex wasn't initialized.");
                return;
            }
            string[] fileType = filepath.Split('.');
            if(fileType[fileType.Length-1].ToLower()=="json" && File.Exists(filepath)) {
                StreamReader jsonFile = File.OpenText(filepath); //Abre o ficheiro 
                string json = jsonFile.ReadToEnd(); //Lê o ficheiro até ao fim
                Team t = JsonConvert.DeserializeObject<Team>(json); //Desserializa o ficheiro para o objeto
                if (t != null) { 
                    _name = t._name;
                    _teamMembers = t._teamMembers;
                    if(FeedBack)
                        Console.WriteLine("\nFile successfully uploaded.");
                }
                else
                {
                    if (FeedBack)
                        Console.WriteLine("\nFile was not uploaded. Please check the file's integrity.");
                }
            }
            else if(FeedBack)
            {
                Console.WriteLine("\n--->Error: File not found or not valid.");
            }
            else
            {
                throw new InvalidOperationException("--->Error: File not found or not valid.");
            }
        }
    }
}
