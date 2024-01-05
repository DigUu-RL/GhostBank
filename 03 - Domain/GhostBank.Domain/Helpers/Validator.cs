using System.Text.RegularExpressions;
using GhostBank.Domain.Helpers.Extensions;

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
		var multiplyX = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
		var multiplyY = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

		value = value.Trim();
		value = value.Remove(".").Remove("-");

		if (value.Length != 11)
			return false;

		string tempCpf = value[..9];
		int sum = 0;

		for (int i = 0; i < 9; i++)
			sum += int.Parse(tempCpf[i].ToString()) * multiplyX[i];

		int module = sum % 11;
		module = module < 2 ? 0 : 11 - module;

		string digit = module.ToString();
		tempCpf += digit;

		sum = 0;

		for (int i = 0; i < 10; i++)
			sum += int.Parse(tempCpf[i].ToString()) * multiplyY[i];

		module = sum % 11;
		module = module < 2 ? 0 : 11 - module;

		digit += module.ToString();
		return value.EndsWith(digit);
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
