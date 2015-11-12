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
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Packages not found");
        }

        // GET: api/Package/5
        public Package Get(int id)
        {
            return _packageService.GetPackage(id); 
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
        public HttpResponseMessage Put(int id, Package packagepara)
        {
            Package package = _packageService.GetPackage(id);
            package.Title = packagepara.Title;
            package.Description = packagepara.Description;
            package.Location = packagepara.Location;
            try
            {
                _packageService.UpdatePackage(package);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // DELETE: api/Package/5
        public HttpResponseMessage Delete(int id)
        {
            var packageEntity = _packageService.GetPackage(id);
            _packageService.DeletePackage(packageEntity);
            var response = Request.CreateResponse(HttpStatusCode.OK, packageEntity);
            return response;
        }
   }
}

