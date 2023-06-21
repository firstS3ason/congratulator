
namespace WebApplication5LVL.Domain.Models
{
    /// <summary>
    /// Abstract base entity - Entity
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// Entity's identifier 
        /// </summary>
        public Guid Id { get; set; }
    }
}
