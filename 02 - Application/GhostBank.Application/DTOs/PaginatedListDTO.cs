using GhostBank.Domain.Attributes;

namespace GhostBank.Application.DTOs;

[DTO]
public class PaginatedListDTO<T>
{
	public int Page { get; set; }
	public int Pages { get; set; }
	public int Total { get; set; }
	public List<T> Data { get; set; } = [];
}
