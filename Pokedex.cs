using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Globalization;
using Newtonsoft.Json;
using System.Linq;

namespace ReadPokemonJSON
{
    public enum tipos
    {
        Normal,
        Fire,
        Water,
        Grass,
        Electric,
        Ice,
        Fighting,
        Poison,
        Ground,
        Flying,
        Psychic,
        Bug,
        Rock,
        Ghost,
        Dragon,
        Dark,
        Steel,
        Fairy
    };
    static class Pokedex
    {
        private static List<Pokemon> _ListaPokemons = new List<Pokemon>(); //Cria uma lista do tipo Pokemon
        private static string[,] TabelaFraquezas = new string[,]
        {
            {"X","Normal","Fire","Water","Grass","Electric","Ice","Fighting","Poison","Ground","Flying","Psychic","Bug","Rock","Ghost","Dragon","Dark","Steel","Fairy" },
            {"Normal","1","1","1","1","1","1","1","1","1","1","1","1","0,5","0","1","1","0,5","1" },
            {"Fire","1","0,5","0,5","2","1","2","1","1","1","1","1","2","0,5","1","0,5","1","2","1" },
            {"Water","1","2","0,5","0,5","1","1","1","1","2","1","1","1","2","1","0,5","1","1","1" },
            {"Grass","1","0,5","2","0,5","1","1","1","0,5","2","0,5","1","0,5","2","1","0,5","1","0,5","1" },
            {"Electric","1","1","2","0,5","0,5","1","1","1","0","2","1","1","1","1","0,5","1","1","1" },
            {"Ice","1","0,5","0,5","2","1","0,5","1","1","2","2","1","1","1","1","2","1","0,5","1" },
            {"Fighting","2","1","1","1","1","2","1","0,5","1","0,5","0,5","0,5","2","0","1","2","2","0,5" },
            {"Poison","1","1","1","2","1","1","1","0,5","0,5","1","1","1","0,5","0,5","1","1","0","2" },
            {"Ground","1","2","1","0,5","2","1","1","2","1","0","1","0,5","2","1","1","1","2","1" },
            {"Flying","1","1","1","2","0,5","1","2","1","1","1","1","2","0,5","1","1","1","0,5","1" },
            {"Psychic","1","1","1","1","1","1","2","2","1","1","0,5","1","1","1","1","0","0,5","1" },
            {"Bug","1","0,5","1","2","1","1","0,5","0,5","1","0,5","2","1","1","0,5","1","2","0,5","0,5" },
            {"Rock","1","2","1","1","1","2","0,5","1","0,5","2","1","2","1","1","1","1","0,5","1" },
            {"Ghost","0","1","1","1","1","1","1","1","1","1","2","1","1","2","1","0,5","1","1" },
            {"Dragon","1","1","1","1","1","1","1","1","1","1","1","1","1","1","2","1","0,5","0" },
            {"Dark","1","1","1","1","1","1","0,5","1","1","1","2","1","1","2","1","0,5","1","0,5" },
            {"Steel","1","0,5","0,5","1","0,5","2","1","1","1","1","1","1","2","1","1","1","0,5","2" },
            {"Fairy","1","0,5","1","1","1","1","2","0,5","1","1","1","1","1","1","2","2","0,5","1" }
        };
        public static void Iniciar()//Lê o ficheiro JSON no diretório do programa
        {
            string filepath = Environment.CurrentDirectory + @"\pokemons.json";
            if (File.Exists(filepath))
            {
                StreamReader jsonFile = File.OpenText(filepath); //Abre o ficheiro json
                string json = jsonFile.ReadToEnd(); //Lê o ficheiro até ao fim
                _ListaPokemons = JsonConvert.DeserializeObject<List<Pokemon>>(json); //Desserializa o ficheiro JSON, criando um objeto para cada Pokemon, adicionando-os à lista de Pokemons
                if (_ListaPokemons==null|| !isStarted())
                {
                    Console.WriteLine("\nNo Pokemon list was found, please check the file's integrity.");
                    return;
                }
            }
            else
                Console.WriteLine("\n--->Error: No .JSON file found on: " + filepath);
        }
        public static void Iniciar(string jsonFilePath)//Lê o JSON dado
        {
            string[] fileType = jsonFilePath.Split('.');
            if (File.Exists(jsonFilePath) && fileType[fileType.Length - 1].ToLower() == "json")
            {
                //---Preenche a lista com todos os Pokemons no ficheiro JSON---------------------------------
                StreamReader jsonFile = File.OpenText(jsonFilePath); //Abre o ficheiro json
                string json = jsonFile.ReadToEnd(); //Lê o ficheiro até ao fim
                _ListaPokemons = JsonConvert.DeserializeObject<List<Pokemon>>(json); //Desserializa o ficheiro JSON, criando um objeto para cada Pokemon, adicionando-os à lista de Pokemons
                if (!isStarted()|| _ListaPokemons == null)
                {
                    Console.WriteLine("\nNo Pokemon list was found, please check the file's integrity.");
                    return;
                }
            }
            else
                Console.WriteLine("\n--->Error: No .JSON file found on: " + jsonFilePath);
        }
        public static void PrintAllPokemons() //Imprime todos os Pokemon
        {
            if (!isStarted())
            {
                Console.WriteLine("--->Error: Pokedex wasn't initialized.");
                return;
            }
            foreach (Pokemon p in _ListaPokemons) //Verifica cada pokemon na lista
            {
                Console.WriteLine("Name: {0, -14} Id: {1}, Type(s): {2}", p.name + ", ", p.id, string.Join(", ", p.typeofpokemon)); //Imprime um a um
            }
        }
        public static ReadOnlyCollection<Pokemon> GetAllPokemons()//Retorna uma Lista com todos os Pokemons
        {
            if (!isStarted())
                throw new InvalidOperationException("--->Error: Pokedex wasn't initialized.");
            return _ListaPokemons.AsReadOnly();
        }
        public static void PrintPokemonInfo(int id) //Imprime a informação de um Pokemon baseado no seu ID
        {
            if (!isStarted())
            {
                Console.WriteLine("--->Error: Pokedex wasn't initialized.");
                return;
            }
            if (id >= 1 && id <= _ListaPokemons.Count)
                Console.WriteLine("Name: {0, -14} Id: {1}, Type(s): {2}", _ListaPokemons[id - 1].name + ", ", _ListaPokemons[id - 1].id, string.Join(", ", _ListaPokemons[id - 1].typeofpokemon));
            else
                Console.WriteLine("Pokemon not found!");
        }
        public static void PrintPokemonInfo(string name) //Imprime a informação de um Pokemon baseado no seu ID
        {
            if (!isStarted())
            {
                Console.WriteLine("--->Error: Pokedex wasn't initialized.");
                return;
            }
            foreach (Pokemon p in _ListaPokemons)
            {
                if (p.name.ToLower() == name.ToLower())
                {
                    Console.WriteLine("Name: {0, -14} Id: {1}, Type(s): {2}", p.name + ", ", p.id, string.Join(", ", p.typeofpokemon));
                    return;
                }
            }
            Console.WriteLine("Pokemon not found!");
        }
        public static Pokemon GetPokemon(int id) //Retorna o Pokemon com o ID
        {
            if (!isStarted())
                throw new InvalidOperationException("--->Error: Pokedex wasn't initialized.");
            if (id >= 1 && id <= _ListaPokemons.Count)
                return _ListaPokemons[id - 1];
            else
                throw new InvalidOperationException("Pokemon not found!");
        }
        public static Pokemon GetPokemon(string name) //Retorna o Pokemon com o ID
        {
            if (!isStarted())
                throw new InvalidOperationException("--->Error: Pokedex wasn't initialized.");
            foreach (Pokemon p in _ListaPokemons)
            {
                if (p.name.ToLower() == name.ToLower())
                {
                    return p;
                }
            }
            throw new InvalidOperationException("Pokemon not found!");
        }
        public static void PrintStrongestPokemon() //Imprime o Pokemon com as Base Stats mais altas
        {
            if (!isStarted())
            {
                Console.WriteLine("--->Error: Pokedex wasn't initialized.");
                return;
            }
            int highestBaseStats = 0;
            for (int j = 0; j <= _ListaPokemons.Count - 1; j++) //Percorre todos os Pokemon na lista 
            {
                if (_ListaPokemons[j].total > highestBaseStats) //Se o Pokemon em questão tiver as Base Stats mais altas que o previamente definido -
                    highestBaseStats = _ListaPokemons[j].total; //As stats em questão passam a ser a nova meta a bater
            }
            for (int i = 0; i <= _ListaPokemons.Count - 1; i++) //Percorre todos os Pokemon na lista novamente
            {
                if (_ListaPokemons[i].total == highestBaseStats) //Agora que as stats mais altas já foram determinadas é necessário procurar todos os Pokemons com o mesmo valor
                    Console.WriteLine("Name: {0, -14} Total stats: {1}", _ListaPokemons[i].name + ", ", _ListaPokemons[i].total); //Todos os Pokemons que atinjam o valor máximo são imprimidos
            }
        }
        public static List<Pokemon> GetStrongestPokemon() //Retorna o(s) Pokemon(s) com as stats mais altas
        {
            if (!isStarted())
                throw new InvalidOperationException("--->Error: Pokedex wasn't initialized.");
            int highestBaseStats = 0;
            List<Pokemon> strongestPokemons = new List<Pokemon>();
            for (int j = 0; j <= _ListaPokemons.Count - 1; j++) //Percorre todos os Pokemon na lista 
            {
                if (_ListaPokemons[j].total > highestBaseStats) //Se o Pokemon em questão tiver as Base Stats mais altas que o previamente definido -
                    highestBaseStats = _ListaPokemons[j].total; //As stats em questão passam a ser a nova meta a bater
            }
            for (int i = 0; i <= _ListaPokemons.Count - 1; i++) //Percorre todos os Pokemon na lista novamente
            {
                if (_ListaPokemons[i].total == highestBaseStats)
                { //Agora que as stats mais altas já foram determinadas é necessário procurar todos os Pokemons com o mesmo valor
                    strongestPokemons.Add(_ListaPokemons[i]);
                }
            }
            return strongestPokemons;
        }
        public static void PrintWeaknesses(int id) //Imprime as fraquezas do Pokemon com o id
        {
            if (!isStarted())
            {
                Console.WriteLine("--->Error: Pokedex wasn't initialized.");
                return;
            }
            if (id >= 1 && id <= _ListaPokemons.Count)
                Console.WriteLine("Name: {0, -14} Type: {1}; Weaknesses: {2}", _ListaPokemons[id - 1].name + "; ", string.Join(", ", _ListaPokemons[id - 1].typeofpokemon), string.Join(", ", _ListaPokemons[id - 1].weaknesses));
            else
                Console.WriteLine("Pokemon not found!");
        }
        public static List<tipos> GetWeaknesses(int id)//Retorna as fraquezas do Pokemon com o id
        {
            if (!isStarted())
                throw new InvalidOperationException("--->Error: Pokedex wasn't initialized.");
            List<tipos> fraquezas = new List<tipos>();
            if (id >= 1 && id <= _ListaPokemons.Count)
            {
                for (int i = 0; i <= _ListaPokemons[id - 1].weaknesses.Count - 1; i++)
                {
                    foreach (tipos t in Enum.GetValues(typeof(tipos)))
                    {
                        if (t.ToString() == _ListaPokemons[id - 1].weaknesses[i].ToString())
                        {
                            fraquezas.Add(t);
                        }
                    }
                }
                return fraquezas;
            }
            else
                throw new InvalidOperationException("Pokemon not found!");
        }
        public static void PrintWeaknesses(string pokemonName) //Imprime as fraquezas do Pokemon com o id
        {
            if (!isStarted())
            {
                Console.WriteLine("--->Error: Pokedex wasn't initialized.");
                return;
            }
            foreach (Pokemon p in _ListaPokemons)
            {
                if (p.name.ToLower() == pokemonName.ToLower()) { 
                    Console.WriteLine("Name: {0, -14} Type: {1}; Weaknesses: {2}", p.name + "; ", string.Join(", ", p.typeofpokemon), string.Join(", ", p.weaknesses)); return;
                    return;                
                }
            }
            Console.WriteLine("Pokemon not found!");
        }
        public static List<tipos> GetWeaknesses(string pokemonName)//Retorna as fraquezas do Pokemon com o id
        {
            if (!isStarted())
                throw new InvalidOperationException("--->Error: Pokedex wasn't initialized.");
            foreach (Pokemon p in _ListaPokemons)
            {
                if (p.name == pokemonName)
                {
                    string[] id = p.id.Split('#');
                    return GetWeaknesses(Int32.Parse(id[1]));
                }
            }
            throw new InvalidOperationException("Pokemon not found!");
        }
        public static void PrintWeaknesses(tipos tipo1) //Imprime as fraquezas do tipo de Pokemon
        {
            List<tipos> fraquezas = new List<tipos>();

            for (int i = 1; i <= TabelaFraquezas.GetLength(0) - 1; i++)
            {
                if (tipo1.ToString() == TabelaFraquezas[0, i])
                {
                    for (int j = 1; j <= TabelaFraquezas.GetLength(0) - 1; j++)
                    {
                        if (TabelaFraquezas[j, i] == "2")
                        {
                            fraquezas.Add((tipos)j - 1);
                        }
                    }
                    break;
                }
            }
            Console.WriteLine(tipo1 + " is weak against: " + string.Join(", ", fraquezas) + ".");
        }
        public static List<tipos> GetWeaknesses(tipos tipo1) //Retorna uma lista com as fraquezas do tipo de Pokemon dado
        {
            List<tipos> fraquezas = new List<tipos>();
            for (int i = 1; i <= TabelaFraquezas.GetLength(0) - 1; i++)
            {
                if (tipo1.ToString() == TabelaFraquezas[0, i])
                {
                    for (int j = 1; j <= TabelaFraquezas.GetLength(0) - 1; j++)
                    {
                        if (TabelaFraquezas[j, i] == "2")
                        {
                            fraquezas.Add((tipos)j - 1);
                        }
                    }
                    return fraquezas;
                }
            }
            throw new InvalidOperationException("The Pokedex is empty! Please make sure to initialize the Pokedex before using it.");
        }
        public static void PrintWeaknesses(tipos type1, tipos type2) //Imprime as fraquezas do Pokemon com a combinação de tipos
        {
            List<string> fraquezas = new List<string>();
            List<string> fraquezasDuplicadas = new List<string>();
            List<string> imunidades = new List<string>();
            List<float> valorFraquezas1 = new List<float>();
            List<float> valorFraquezas2 = new List<float>();

            for (int i = 1; i <= TabelaFraquezas.GetLength(0) - 1; i++)
            {
                if (type1.ToString() == TabelaFraquezas[0, i])
                {
                    for (int j = 1; j <= TabelaFraquezas.GetLength(0) - 1; j++)
                    {
                        valorFraquezas1.Add(float.Parse(TabelaFraquezas[j, i]));
                    }
                    break;
                }
                else if (i == (TabelaFraquezas.GetLength(0) - 1) && valorFraquezas1.Count == 0)
                {
                    Console.WriteLine("Type: " + type1 + " was not found");
                    return;
                }
            }
            for (int i = 1; i <= TabelaFraquezas.GetLength(0) - 1; i++)
            {
                if (type2.ToString() == TabelaFraquezas[0, i])
                {
                    for (int j = 1; j <= TabelaFraquezas.GetLength(0) - 1; j++)
                    {
                        valorFraquezas2.Add(float.Parse(TabelaFraquezas[j, i]));
                    }
                    break;
                }
                else if (i == (TabelaFraquezas.GetLength(0) - 1) && valorFraquezas2.Count == 0)
                {
                    Console.WriteLine("Type: " + type2 + " was not found");
                    return;
                }
            }
            for (int i = 0; i <= valorFraquezas1.Count - 1; i++)
            {
                if ((valorFraquezas1[i] * valorFraquezas2[i]) == 2)
                {
                    fraquezas.Add(TabelaFraquezas[i + 1, 0]);
                }
                else if ((valorFraquezas1[i] * valorFraquezas2[i]) == 4)
                {
                    fraquezasDuplicadas.Add(TabelaFraquezas[i + 1, 0]);
                }
                else if ((valorFraquezas1[i] * valorFraquezas2[i]) == 0)
                {
                    imunidades.Add(TabelaFraquezas[i + 1, 0]);
                }
            }
            Console.WriteLine("A " + type1 + " and " + type2 + " Pokemon is 2x weak to: " + string.Join(", ", fraquezas) + ((fraquezasDuplicadas.Count > 0) ? " and 4x weak to: " + string.Join(", ", fraquezasDuplicadas) : "") + ((imunidades.Count > 0) ? " and immune to: " + string.Join(", ", imunidades) + "." : "."));
        }
        public static List<tipos> GetWeaknesses(tipos type1, tipos type2) //Retorna as fraquezas do Pokemon com a combinação de tipos
        {
            List<tipos> fraquezas = new List<tipos>();
            List<float> valorFraquezas1 = new List<float>();
            List<float> valorFraquezas2 = new List<float>();

            for (int i = 1; i <= TabelaFraquezas.GetLength(0) - 1; i++)
            {
                if (type1.ToString() == TabelaFraquezas[0, i])
                {
                    for (int j = 1; j <= TabelaFraquezas.GetLength(0) - 1; j++)
                    {
                        valorFraquezas1.Add(float.Parse(TabelaFraquezas[j, i]));
                    }
                    break;
                }
            }
            for (int i = 1; i <= TabelaFraquezas.GetLength(0) - 1; i++)
            {
                if (type2.ToString() == TabelaFraquezas[0, i])
                {
                    for (int j = 1; j <= TabelaFraquezas.GetLength(0) - 1; j++)
                    {
                        valorFraquezas2.Add(float.Parse(TabelaFraquezas[j, i]));
                    }
                    break;
                }
            }
            for (int i = 0; i <= valorFraquezas1.Count - 1; i++)
            {
                if ((valorFraquezas1[i] * valorFraquezas2[i]) >= 2)
                {
                    fraquezas.Add((tipos)i);
                }
            }
            return fraquezas;
        }
        public static void PrintStrengths(int id)//Imprime os tipos cujo o tipo do Pokemon é bom contra
        {
            if (!isStarted())
            {
                Console.WriteLine("--->Error: Pokedex wasn't initialized.");
                return;
            }
            if (_ListaPokemons.Count < 1)
                throw new InvalidOperationException("--->Error: The Pokedex is empty! Please make sure to initialize the Pokedex before using it.");
            Console.WriteLine("Name: {0, -14} Type: {1}; Strengths: {2}", _ListaPokemons[id - 1].name + "; ", string.Join(", ", _ListaPokemons[id - 1].typeofpokemon), string.Join(", ", GetStrengths(id)));
        }
        public static List<tipos> GetStrengths(int id)//Retorna os tipos cujo o tipo do Pokemon é bom contra
        {
            if (!isStarted())
                throw new InvalidOperationException("--->Error: Pokedex wasn't initialized.");
            int t1 = -1, t2 = -1;
            if (id > _ListaPokemons.Count)
                throw new InvalidOperationException("--->Error: Pokemon not found!");
            if (_ListaPokemons[id - 1].typeofpokemon.Count == 2)
            {
                for (int i = 0; i <= _ListaPokemons[id - 1].typeofpokemon.Count - 1; i++)
                {
                    foreach (tipos t in Enum.GetValues(typeof(tipos)))
                    {
                        if (t.ToString() == _ListaPokemons[id - 1].typeofpokemon[i])
                        {
                            if (t1 == -1)
                                t1 = (int)t;
                            else
                                t2 = (int)t;
                        }

                    }
                }
                return GetStrengths((tipos)t1, (tipos)t2);
            }
            else if (_ListaPokemons[id - 1].typeofpokemon.Count == 1)
            {
                foreach (tipos t in Enum.GetValues(typeof(tipos)))
                {
                    if (t.ToString() == _ListaPokemons[id - 1].typeofpokemon[0])
                    {
                        t1 = (int)t;
                    }
                }
                return GetStrengths((tipos)t1);
            }
            else
                throw new InvalidOperationException("--->Error: The Pokedex is empty! Please make sure to initialize the Pokedex before using it.");
        }
        public static void PrintStrengths(string pokemonName) //Imprime os tipos cujo o tipo do Pokemon é bom contra
        {
            if (!isStarted())
            {
                Console.WriteLine("--->Error: Pokedex wasn't initialized.");
                return;
            }
            if (_ListaPokemons.Count < 1)
                throw new InvalidOperationException("--->Error: The Pokedex is empty! Please make sure to initialize the Pokedex before using it.");
            foreach (Pokemon p in _ListaPokemons)
            {
                if (p.name == pokemonName)
                {
                    Console.WriteLine("Name: {0, -14} Type: {1}; Strengths: {2}", p.name + "; ", string.Join(", ", p.typeofpokemon), string.Join(", ", GetStrengths(pokemonName)));
                }
            }
        }
        public static List<tipos> GetStrengths(string pokemonName)//Retorna os tipos cujo o tipo do Pokemon é bom contra
        {
            if (!isStarted())
                throw new InvalidOperationException("--->Error: Pokedex wasn't initialized.");
            foreach (Pokemon p in _ListaPokemons)
            {
                if (p.name == pokemonName)
                {
                    string[] id = p.id.Split('#');
                    return GetStrengths(Int32.Parse(id[1]));
                }
            }
            throw new InvalidOperationException("Pokemon not found!");
        }
        public static void PrintStrengths(tipos type1) //Imprime os tipos cujo o tipo do Pokemon é bom contra
        {
            Console.WriteLine(type1 + " is strong against: " + string.Join(", ", GetStrengths(type1)) + ".");
        }
        public static List<tipos> GetStrengths(tipos type1) //Retorna os tipos cujo o tipo do Pokemon é bom contra
        {
            List<tipos> strengths = new List<tipos>();
            for (int i = 1; i <= TabelaFraquezas.GetLength(0) - 1; i++)
            {
                if (type1.ToString() == TabelaFraquezas[i, 0])
                {
                    for (int j = 1; j <= TabelaFraquezas.GetLength(0) - 1; j++)
                    {
                        if (TabelaFraquezas[i, j] == "2")
                        {
                            strengths.Add((tipos)j - 1);
                        }
                    }
                    return strengths;
                }
            }
            throw new InvalidOperationException("The Pokedex is empty! Please make sure to initialize the Pokedex before using it.");
        }
        public static void PrintStrengths(tipos type1, tipos type2) //Imprime os tipos cujo o tipo do Pokemon é bom contra
        {
            Console.WriteLine("A " + type1 + " and " + type2 + " Pokemon is strong against: " + string.Join(", ", GetStrengths(type1, type2)) + ".");
        }
        public static List<tipos> GetStrengths(tipos type1, tipos type2) //Retorna os tipos cujo o tipo do Pokemon é bom contra
        {
            List<tipos> temp = new List<tipos>();
            List<tipos> strengths = new List<tipos>();
            List<string> valorFraquezas1 = new List<string>();
            List<string> valorFraquezas2 = new List<string>();

            for (int i = 1; i <= TabelaFraquezas.GetLength(0) - 1; i++)
            {
                if (type1.ToString() == TabelaFraquezas[i, 0])
                {
                    for (int j = 1; j <= TabelaFraquezas.GetLength(0) - 1; j++)
                    {
                        if (float.Parse(TabelaFraquezas[i, j]) == 2)
                        {
                            valorFraquezas1.Add(TabelaFraquezas[0, j]);
                        }
                    }
                    break;
                }
            }
            for (int i = 1; i <= TabelaFraquezas.GetLength(0) - 1; i++)
            {
                if (type2.ToString() == TabelaFraquezas[i, 0])
                {
                    for (int j = 1; j <= TabelaFraquezas.GetLength(0) - 1; j++)
                    {
                        if (float.Parse(TabelaFraquezas[i, j]) == 2)
                        {
                            valorFraquezas2.Add(TabelaFraquezas[0, j]);
                        }
                    }
                    break;
                }
            }
            for (int i = 0; i <= valorFraquezas1.Count - 1; i++)
            {
                foreach (tipos t in Enum.GetValues(typeof(tipos)))
                {
                    if (t.ToString() == valorFraquezas1[i])
                    {
                        temp.Add(t);
                    }
                }
            }
            for (int i = 0; i <= valorFraquezas2.Count - 1; i++)
            {
                foreach (tipos t in Enum.GetValues(typeof(tipos)))
                {
                    if (t.ToString() == valorFraquezas2[i])
                    {
                        temp.Add(t);
                    }
                }
            }

            strengths = temp.Distinct().ToList();
            return strengths;
        }
        public static void PrintAllTypes() //Imprime todos os tipos de Pokemon
        {
            List<tipos> types = new List<tipos>();
            foreach (tipos t in Enum.GetValues(typeof(tipos)))
            {
                types.Add(t);
            }
            Console.WriteLine("Pokemon types: " + string.Join(", ", types));
        }
        public static List<tipos> GetAllTypes() //Retorna todos os tipos de Pokemon
        {
            List<tipos> types = new List<tipos>();
            foreach (tipos t in Enum.GetValues(typeof(tipos)))
            {
                types.Add(t);
            }
            return types;
        }
        public static void ExportTeam(bool FeedBack, params Team[] equipas) //Exporta as equipas selecionadas para o diretório atual
        {
            if (!isStarted())
            {
                Console.WriteLine("--->Error: Pokedex wasn't initialized.");
                return;
            }
            foreach (Team t in equipas)
            {
                using (StreamWriter file = File.CreateText(Environment.CurrentDirectory + @"\" + t.name + ".json"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, t);
                    if (FeedBack)
                    {
                        Console.WriteLine("\nTeam sucessfully exported to: " + Environment.CurrentDirectory);
                    }
                }
            }
        }
        public static void ExportTeam(string filepath, bool FeedBack, params Team[] equipas) //Exporta as equipas selecionadas para o diretório escolhido
        {
            if (!isStarted())
            {
                Console.WriteLine("--->Error: Pokedex wasn't initialized.");
                return;
            }
            if (string.IsNullOrWhiteSpace(filepath) || !Directory.Exists(filepath))
            {
                Console.WriteLine("--->Error: Non-valid file path.");
                return;
            }
            foreach (Team t in equipas)
            {
                using (StreamWriter file = File.CreateText(filepath + @"\" + t.name + ".json"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, t);
                    if (FeedBack)
                    {
                        Console.WriteLine("\nTeam successfully exported to: " + filepath);
                    }
                }
            }
        }
        public static bool isStarted() //Verfica se o Pokedex foi inicializado ou não
        {
            if (_ListaPokemons.Count < 1)
                return false;
            else
                return true;
        }
    }
}
