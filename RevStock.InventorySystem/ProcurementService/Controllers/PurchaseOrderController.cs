using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProcurementService.DTOs.PurchaseOrder;
using ProcurementService.Services.Interfaces;

namespace ProcurementService.Controllers
{
    [ApiController]
    [Route("revstock/api/purchaseorders")]
    public class PurchaseOrderController : ControllerBase
    {
        private readonly IPurchaseOrderService _service;

        public PurchaseOrderController(IPurchaseOrderService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Admin,ProcurementOfficer,Manager")]
        [HttpGet("getAllPurchaseOrders")]
        public async Task<IActionResult> GetAll()
            => Ok(await _service.GetAllAsync());

        [Authorize(Roles = "ProcurementOfficer")]
        [HttpPost("createPurchaseOrder")]
        public async Task<IActionResult> Create(CreatePurchaseOrderDto dto)
            => Ok(await _service.CreateAsync(dto));

        [Authorize(Roles = "ProcurementOfficer")]
        [HttpPost("submitPurchaseOrder/{id}")]
        public async Task<IActionResult> Submit(Guid id)
            => await _service.SubmitAsync(id) ? Ok() : BadRequest();

        [Authorize(Roles = "Manager")]
        [HttpPost("approvePurchaseOrder/{id}")]
        public async Task<IActionResult> Approve(Guid id, [FromQuery] Guid approverId)
            => await _service.ApproveAsync(id, approverId) ? Ok() : BadRequest();

        [Authorize(Roles = "Admin")]
        [HttpPost("cancelPurchaseOrder/{id}")]
        public async Task<IActionResult> Cancel(Guid id)
            => await _service.CancelAsync(id) ? Ok() : BadRequest();
    }
}
