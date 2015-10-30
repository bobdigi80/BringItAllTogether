using System.Data.Entity.ModelConfiguration;
using BringingItAllTogether.Core.Data;

namespace BringingItAllTogether.Data.Mapping
{
    public class PackageMap:EntityTypeConfiguration<Package>
    {
        public PackageMap()
           {
               //key
               HasKey(t => t.Id);
               //properties
               Property(t => t.Title).IsRequired();
               Property(t => t.Description).IsRequired();
               Property(t => t.Location).IsRequired();
               Property(t => t.AddedDate).IsRequired();
               Property(t => t.ModifiedDate).IsRequired();
               Property(t => t.IP);
               //table
               ToTable("Packages");
           }
    }
}
