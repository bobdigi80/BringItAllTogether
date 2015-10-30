using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using BringingItAllTogether.Core.Data;
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
        public IEnumerable<Package> Get()
        {
            return _packageService.GetPackages();
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
        public void Put(int id, [FromBody]string value)
        {
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

