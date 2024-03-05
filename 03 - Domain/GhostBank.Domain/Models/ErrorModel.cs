namespace GhostBank.Domain.Models;

public class ErrorModel
{
	public int StatusCode { get; set; }
	public string Error { get; set; } = null!;
	public string Message { get; set; } = null!;
	public string? Inner { get; set; }
}
