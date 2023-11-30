using GhostBank.Domain.Attributes;

namespace GhostBank.Domain.Models;

[Model]
public class PaginatedListModel<T>
{
	public int Page { get; set; }
	public int Pages { get; set; }
	public int Total { get; set; }
	public List<T> Data { get; set; } = [];
}
