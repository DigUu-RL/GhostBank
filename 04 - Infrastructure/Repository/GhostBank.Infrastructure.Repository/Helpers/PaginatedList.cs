using Microsoft.EntityFrameworkCore;

namespace GhostBank.Infrastructure.Repository.Helpers;

public sealed class PaginatedList<T> : List<T>
{
	public int Page { get; }
	public int Pages { get; }
	public int Total { get; }

	private PaginatedList(IEnumerable<T> items, int page, int quantity, int total)
	{
		Page = page;
		Pages = (int) Math.Ceiling(total / (double) quantity);
		Total = total;

		AddRange(items);
	}

	public static PaginatedList<T> CreateInstante(IEnumerable<T> source, int page, int quantity)
	{
		page = page == 0 ? 1 : page;
		quantity = quantity == 0 ? 10 : quantity;

		int total = source.Count();
		IEnumerable<T> items = source.Skip((page - 1) * quantity).Take(quantity);

		return new PaginatedList<T>(items, page, quantity, total);
	}

	public static async Task<PaginatedList<T>> CreateInstanceAsync(IQueryable<T> source, int page, int quantity)
	{
		page = page == 0 ? 1 : page;
		quantity = quantity == 0 ? 10 : quantity;

		int total = await source.CountAsync();
		IQueryable<T> items = source.Skip((page - 1) * quantity).Take(quantity);

		return new PaginatedList<T>(items, page, quantity, total);
	}
}
