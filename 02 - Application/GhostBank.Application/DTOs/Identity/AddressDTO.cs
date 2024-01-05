using GhostBank.Domain.Attributes;
using GhostBank.Infrastructure.Data.Enums.Identity;

namespace GhostBank.Application.DTOs.Identity;

[DTO]
public class AddressDTO
{
	public Guid Id { get; set; }
	public string? Street { get; set; }
	public string? Number { get; set; }
	public string? District { get; set; }
	public string? City { get; set; }
	public string? ZipCode { get; set; }
	public State State { get; set; }
}
