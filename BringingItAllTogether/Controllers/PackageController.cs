using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using AttributeRouting.Web.Http;
using BringingItAllTogether.ActionFilter;
using BringingItAllTogether.Core.Data;
using BringingItAllTogether.ErrorHelper;
using BringingItAllTogether.Filters;
using BringingItAllTogether.Interfaces;
using BringingItAllTogether.Models;
using BringingItAllTogether.Service;
using Ninject;

namespace BringingItAllTogether.Controllers
{
    public class PackageController : ApiController
    {
        [Inject]
        private readonly IPackageService _packageService;

        public PackageController(IPackageService packageService)
        {
            _packageService = packageService;
        }

       // GET: api/Package
        //public IEnumerable<Package> Get()
        //{
        //    return _packageService.GetPackages();
        //}

        [GET("allpackages")]
        [GET("all")]
        public HttpResponseMessage Get()
        {
            var packages = _packageService.GetPackages();
            if (packages.Any())
                return Request.CreateResponse(HttpStatusCode.OK, packages);
            throw new ApiDataException(1000, "Packages not found", HttpStatusCode.NotFound);
        }

        // GET: api/Package/5
        public HttpResponseMessage Get(int id)
        {
            if (id > 0)
            {
                var product = _packageService.GetPackage(id);
                if (product != null)
                    return Request.CreateResponse(HttpStatusCode.OK, product);
                throw new ApiDataException(1001, "No product found for this id.", HttpStatusCode.NotFound);
            }
            throw new ApiException() { ErrorCode = (int)HttpStatusCode.BadRequest, ErrorDescription = "Bad Request..." };
        }

        // POST: api/Package
        public HttpResponseMessage Post(Package package)
        {
            package.IP = Request.GetOwinContext().Request.RemoteIpAddress;
            _packageService.InsertPackage(package);
            var response = Request.CreateResponse(HttpStatusCode.Created, package);
            string url = Url.Link("DefaultApi", new { package.Id });
            response.Headers.Location = new Uri(url);
            return response;
        }

        // PUT: api/Package/5
        public bool Put(int id, Package packagepara)
        {
             if (id > 0)
            {
                return _packageService.UpdatePackage(id, packagepara);
            }
            return false;
        }

        // DELETE: api/Package/5
        public bool Delete(int id)
        {
            if (id > 0)
            {
                var isSuccess = _packageService.DeletePackage(id);
                if (isSuccess)
                {
                    return isSuccess;
                }
                throw new ApiDataException(1002, "Package is already deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException() { ErrorCode = (int)HttpStatusCode.BadRequest, ErrorDescription = "Bad Request..." };
        }
   }
}

