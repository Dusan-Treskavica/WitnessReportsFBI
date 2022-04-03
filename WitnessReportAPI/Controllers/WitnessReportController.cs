using AutoMapper;
using BusinessLogic.Interface.WitnessReports;
using Common.Error;
using Common.Models.WitnessReport;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Net;
using WitnessReportAPI.ViewModel;

namespace WitnessReportAPI.Controllers
{
    [ApiController]
    [Route("api/witnessReport")]
    public class WitnessReportController : Controller
    {
        private readonly IWitnessReportService witnessReportService;
        private readonly IMapper mapper;
        private const string LOCATION_PREFIX = "api/witnessReport";

        public WitnessReportController(IWitnessReportService witnessReportService, IMapper mapper)
        {
            this.witnessReportService = witnessReportService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Gets Witness Reports by specified case name.
        /// </summary>
        /// <param name="caseName">The case name.</param>
        /// <returns></returns>
        [HttpGet("{caseName}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "List of founded Witness Reports by specified case name.", typeof(IList<WitnessReportVMResponse>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Internal Server Error.", typeof(ErrorModel))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid request options.", typeof(ErrorModel))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Business Partner not found.", typeof(ErrorModel))]
        public IActionResult GetWitnessReports(string caseName)
        {
            IList<WitnessReportVMResponse> result = this.mapper.Map<IList<WitnessReportVMResponse>>(this.witnessReportService.GetByCaseName(caseName));

            return Ok(result);
        }

        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, "List of all Witness Reports. ", typeof(IList<WitnessReportVMResponse>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Internal Server Error.", typeof(ErrorModel))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid request options.", typeof(ErrorModel))]
        public IActionResult GetAllWitnessReports()
        {
            IList<WitnessReportVMResponse> result = this.mapper.Map<IList<WitnessReportVMResponse>>(this.witnessReportService.GetAll());

            return Ok(result);
        }

        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, "Created specified Witness Report.")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Internal Server Error.", typeof(ErrorModel))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid request options.", typeof(ErrorModel))]
        public IActionResult SaveWitnessReport([FromBody] WitnessReportVMRequest witnessReportVMRequest)
        {
            WitnessReport witnessReport = this.mapper.Map<WitnessReport>(witnessReportVMRequest);
            this.witnessReportService.Save(witnessReport);

            return Created(LOCATION_PREFIX, witnessReport.Id);
        }

        
    }
}
