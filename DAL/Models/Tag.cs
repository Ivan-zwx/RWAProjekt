using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEng { get; set; }
        public string CreatedAt { get; set; }
        public string Type { get; set; }
        public int NumberOfApartments { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Tag tag &&
                   Id == tag.Id &&
                   Name == tag.Name &&
                   NameEng == tag.NameEng &&
                   CreatedAt == tag.CreatedAt &&
                   Type == tag.Type &&
                   NumberOfApartments == tag.NumberOfApartments;
        }

        public override int GetHashCode()
        {
            int hashCode = -1950707623;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(NameEng);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CreatedAt);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Type);
            hashCode = hashCode * -1521134295 + NumberOfApartments.GetHashCode();
            return hashCode;
        }
    }
}
