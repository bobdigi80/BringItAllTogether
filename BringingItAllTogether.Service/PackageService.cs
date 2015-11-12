using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using BringingItAllTogether.Core.Data;
using BringingItAllTogether.Data.UnitOfWork;
using BringingItAllTogether.Interfaces;
using Ninject;

namespace BringingItAllTogether.Service
{
    public class PackageService : IPackageService
    {
        private readonly UnitOfWork _unitOfWork;

        /// <summary>
        /// Public constructor.
        /// </summary>
        public PackageService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool DeletePackage(int packageId)
        {
            var success = false;
            if (packageId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var product = _unitOfWork.PackageRepository.GetById(packageId);
                    if (product != null)
                    {

                        _unitOfWork.PackageRepository.Delete(product);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        public bool UpdatePackage(int packageId, Package package)
        {
            var success = false;
            if (package != null)
            {
                using (var scope = new TransactionScope())
                {
                    var product = _unitOfWork.PackageRepository.GetById(packageId);
                    if (product != null)
                    {
                        product.Title = package.Title;
                        product.Description = package.Description;
                        product.Location = package.Location;
                        _unitOfWork.PackageRepository.Update(product);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }
        
        public Package FindByTitle(string title)
        {
            return _unitOfWork.PackageRepository.Table.FirstOrDefault(x => x.Title == title);
        }

        public IQueryable<Package> GetPackages()
        {
            return _unitOfWork.PackageRepository.Table;
        }

        public Package GetPackage(long id)
        {
            return _unitOfWork.PackageRepository.GetById(id);
        }

        public void InsertPackage(Package package)
        {
            //if (package.ModifiedDate < DateTime.UtcNow.Date)
            //{
            //    throw new Exception("Invalid modification date.");
            //}

            _unitOfWork.PackageRepository.Insert(package);
        }

        public void UpdatePackage(Package package)
        {
            _unitOfWork.PackageRepository.Update(package);
        }

        public void DeletePackage(Package package)
        {
            _unitOfWork.PackageRepository.Delete(package);
        }
    }
}
