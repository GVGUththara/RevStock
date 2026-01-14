using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesService.DTOs;
using SalesService.Services.Interfaces;
using System.Security.Claims;

namespace SalesService.Controllers
{
    [ApiController]
    [Route("revstock/api/sales")]
    public class SalesController : ControllerBase
    {
        private readonly ISalesService _service;

        public SalesController(ISalesService service)
        {
            _service = service;
        }

        [Authorize(Roles = "SalesOfficer")]
        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateSalesInvoiceDto dto)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var id = await _service.CreateInvoiceAsync(dto, userId);
            return Ok(id);
        }

        [Authorize(Roles = "SalesOfficer")]
        [HttpPost("add-item")]
        public async Task<IActionResult> AddItem(AddSalesItemDto dto)
            => await _service.AddItemAsync(dto) ? Ok() : BadRequest();

        [Authorize(Roles = "Manager")]
        [HttpPost("post/{invoiceId}")]
        public async Task<IActionResult> Post(Guid invoiceId)
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            return await _service.PostInvoiceAsync(invoiceId, token) ? Ok() : BadRequest();
        }
    }
}
