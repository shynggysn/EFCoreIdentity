using EFCoreIdentity.Dto;
using EFCoreIdentityDemo.Data;
using EFCoreIdentityDemo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreIdentity.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly ApplicationDbContext _context;
    
    public UsersController(ILogger<UsersController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet(Name = "Get Users list")]
    public async Task<IActionResult> Get()
    {
        var usersWithOrganizations = await _context.Users
            .Select(u => new
            {
                u.Id,
                u.UserName,
                Organization =  u.Organization.Name,
                Roles = u.Roles.Select(r => r.Name).ToList()
            })
            .ToListAsync();
        
        return Ok(usersWithOrganizations);
    }
    
    [HttpPost(Name = "InsertUser")]
    public async Task<IActionResult> Insert([FromBody] CreateUserRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.Email))
        {
            return BadRequest("Username and Email are required.");
        }

        var organization = await _context.Organizations
            .FirstOrDefaultAsync(o => o.Name == request.OrganizationName);

        if (organization == null && !string.IsNullOrWhiteSpace(request.OrganizationName))
        {
            organization = new Organization { Name = request.OrganizationName };
            _context.Organizations.Add(organization);
        }

        var roles = new List<IdentityRole>();
        if (request.Roles != null && request.Roles.Any())
        {
            foreach (var roleName in request.Roles.Distinct())
            {
                var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
                if (role == null)
                {
                    role = new IdentityRole { Name = roleName };
                    _context.Roles.Add(role);
                }
                roles.Add(role);
            }
        }

        var newUser = new ApplicationUser
        {
            UserName = request.UserName,
            Email = request.Email,
            Organization = organization
        };

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        foreach (var role in roles)
        {
            _context.UserRoles.Add(new IdentityUserRole<string>
            {
                UserId = newUser.Id,
                RoleId = role.Id
            });
        }

        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser);
    }

    [HttpGet("{id}", Name = "GetUserById")]
    public async Task<IActionResult> GetUserById(string id)
    {
        var user = await _context.Users
            .Include(u => u.Organization)
            .Include(u => u.Roles)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(new
        {
            user.Id,
            user.UserName,
            user.Email,
            Organization = user.Organization?.Name,
            Roles = user.Roles.Select(r => r.Name).ToList()
        });
    }
}