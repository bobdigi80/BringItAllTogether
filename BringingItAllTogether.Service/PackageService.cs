using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BringingItAllTogether.Core.Data;
using BringingItAllTogether.Interfaces;
using Ninject;

namespace BringingItAllTogether.Service
{
    public class PackageService : IPackageService
    {
        private readonly IRepository<Package> _packageRepository;

        [Inject]
        public PackageService(IRepository<Package> packageRepository)
       {
           _packageRepository = packageRepository;
       }

        public Package FindByTitle(string title)
        {
            return _packageRepository.Table.FirstOrDefault(x => x.Title == title);
        }

        public IQueryable<Package> GetPackages()
        {
            return _packageRepository.Table;
        }

        public Package GetPackage(long id)
        {
            return _packageRepository.GetById(id);
        }

        public void InsertPackage(Package package)
        {
            //if (package.ModifiedDate < DateTime.UtcNow.Date)
            //{
            //    throw new Exception("Invalid modification date.");
            //}
            
            _packageRepository.Insert(package);
        }

        public void UpdatePackage(Package package)
        {
            _packageRepository.Update(package);
        }

        public void DeletePackage(Package package)
        {
            _packageRepository.Delete(package);
        }
    }
}
