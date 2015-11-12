using System.Linq;
using BringingItAllTogether.Core.Data;
using BringingItAllTogether.Interfaces;

namespace BringingItAllTogether.Interfaces
{
    public interface IPackageService
    {
        IQueryable<Package> GetPackages();
        Package GetPackage(long id);
        void InsertPackage(Package package);
        void UpdatePackage(Package package);
        void DeletePackage(Package package);
        bool DeletePackage(int packageId);
        bool UpdatePackage(int packageId, Package package);
        Package FindByTitle(string title);
    }
}
