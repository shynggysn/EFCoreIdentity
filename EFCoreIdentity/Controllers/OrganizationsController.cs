using EFCoreIdentity.Dto;
using EFCoreIdentityDemo.Data;
using EFCoreIdentityDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreIdentity.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrganizationsController : ControllerBase
{
    private readonly ILogger<OrganizationsController> _logger;
    private readonly ApplicationDbContext _context;
    
    public OrganizationsController(ILogger<OrganizationsController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> PostOrganization([FromBody] CreateOrganizationDto organizationDto)
    {
        if (string.IsNullOrWhiteSpace(organizationDto.Name))
        {
            return BadRequest("Organization name is required.");
        }

        var organization = new Organization
        {
            Name = organizationDto.Name
        };

        _context.Organizations.Add(organization);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetOrganizationById), new { id = organization.Id }, organization);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrganizationById(int id)
    {
        var organization = await _context.Organizations.FindAsync(id);

        if (organization == null)
        {
            return NotFound();
        }

        return Ok(organization);
    }
}