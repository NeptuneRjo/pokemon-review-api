using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly DataContext _context;
        public PokemonRepository(DataContext context) 
        { 
            _context = context;
        }

        public Pokemon GetPokemon(int id)
        {
            // searches the Pokemon entities in the Id column for a pokemon that matches the passed id
            return _context.Pokemon.Where(p => p.Id == id).FirstOrDefault();
        }

        public Pokemon GetPokemon(string name)
        {
            // searches the Pokemon entities in the Name column for a pokemon that matches
            return _context.Pokemon.Where(p => p.Name == name).FirstOrDefault();
        }

        public decimal GetPokemonRating(int pokeId)
        {
            // searches through Reviews > Pokemon > Id; returns all of the reviews for that pokemon id
            var review = _context.Reviews.Where(r => r.Pokemon.Id == pokeId);

            if (review.Count() <= 0) return 0;

            // return the average of the ratings
            return ((decimal)review.Sum(r => r.Rating) / review.Count());
        }

        public ICollection<Pokemon> GetPokemons()
        {
            return _context.Pokemon.OrderBy(p => p.Id).ToList();
        }

        public bool PokemonExists(int pokeId)
        {
            return _context.Pokemon.Any(p => p.Id == pokeId);
        }
    }
}
