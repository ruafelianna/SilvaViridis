using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SilvaViridis.Exe.DeviceConfiguration.Data.Models
{
    public abstract class EntityBase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; protected set; }
    }
}
