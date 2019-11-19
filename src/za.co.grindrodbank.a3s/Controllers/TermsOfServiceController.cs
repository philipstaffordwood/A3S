using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using za.co.grindrodbank.a3s.A3SApiResources;
using za.co.grindrodbank.a3s.AbstractApiControllers;

namespace za.co.grindrodbank.a3s.Controllers
{
    public class TermsOfServiceController : TermsOfServiceApiController
    {
        public TermsOfServiceController()
        {
        }

        [Authorize(Policy = "permission:a3s.termsOfService.create")]
        public override Task<IActionResult> CreateTermsOfServiceAsync([FromBody] TermsOfServiceSubmit termsOfServiceSubmit)
        {
            throw new NotImplementedException();
        }

        [Authorize(Policy = "permission:a3s.termsOfService.delete")]
        public override Task<IActionResult> DeleteTermsOfServiceAsync([FromRoute, Required] Guid termsOfServiceId)
        {
            throw new NotImplementedException();
        }

        [Authorize(Policy = "permission:a3s.termsOfService.read")]
        public override Task<IActionResult> GetTermsOfServiceAsync([FromRoute, Required] Guid termsOfServiceId)
        {
            throw new NotImplementedException();
        }

        [Authorize(Policy = "permission:a3s.termsOfService.read")]
        public override Task<IActionResult> ListTermsOfServicesAsync([FromQuery] int page, [FromQuery, Range(1, 20)] int size, [FromQuery, StringLength(255, MinimumLength = 0)] string filterDescription, [FromQuery] List<string> orderBy)
        {
            throw new NotImplementedException();
        }
    }
}
