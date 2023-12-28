using System.Text.RegularExpressions;

namespace GhostBank.Domain.Helpers;

public static partial class Validator
{
	public static bool IsRG(string value)
	{
		var cleanRG = new string(value.Where(char.IsDigit).ToArray());

		if (cleanRG.Length != 9)
			return false;

		int primeiroDigito = int.Parse(cleanRG[..2]);

		if (primeiroDigito < 1 || primeiroDigito > 28)
			return false;

		var regex = RGRegex();

		if (!regex.IsMatch(cleanRG.AsSpan(2)))
			return false;

		return true;
	}

	public static bool IsCPF(string value)
	{
		var cleanCPF = new string(value.Where(char.IsDigit).ToArray());

		if (cleanCPF.Length != 11)
			return false;

		if (cleanCPF.Distinct().Count() == 1)
			return false;

		int[] cpfArray = cleanCPF.Select(x => int.Parse(x.ToString())).ToArray();
		int sum1 = 0, sum2 = 0;

		for (int i = 0; i < 9; i++)
		{
			sum1 += cpfArray[i] * (10 - i);
			sum2 += cpfArray[i] * (11 - i);
		}

		int digit1 = (sum1 % 11) < 2 ? 0 : 11 - (sum1 % 11);
		int digit2 = (sum2 % 11) < 2 ? 0 : 11 - (sum2 % 11);

		return digit1 == cpfArray[9] && digit2 == cpfArray[10];
	}

	public static bool IsCNPJ(string value)
	{
		var cleanCNPJ = new string(value.Where(char.IsDigit).ToArray());

		if (cleanCNPJ.Length != 14)
			return false;

		int[] cnpjArray = cleanCNPJ.Select(x => int.Parse(x.ToString())).ToArray();
		int sum1 = 0, sum2 = 0;

		for (int i = 0; i < 12; i++)
		{
			sum1 += cnpjArray[i] * ((i % 8) + 2);
			sum2 += cnpjArray[i] * ((9 - (i % 8)) + 2);
		}

		int digit1 = (sum1 % 11) < 2 ? 0 : 11 - (sum1 % 11);
		int digit2 = (sum2 % 11) < 2 ? 0 : 11 - (sum2 % 11);

		return digit1 == cnpjArray[12] && digit2 == cnpjArray[13];
	}

	public static bool IsCellphone(string value)
	{
		var cleanCellphone = new string(value.Where(char.IsDigit).ToArray());

		if (cleanCellphone.Length != 11)
			return false;

		int areaCode = int.Parse(cleanCellphone.Substring(0, 2));

		if (areaCode < 11 || areaCode > 99)
			return false;

		Regex regex = CellphoneRegex();

		if (!regex.IsMatch(cleanCellphone.AsSpan(2)))
			return false;

		return true;
	}

	[GeneratedRegex("^[0-9]+$")]
	private static partial Regex RGRegex();
	[GeneratedRegex("^[0-9]+$")]
	private static partial Regex CellphoneRegex();
}
