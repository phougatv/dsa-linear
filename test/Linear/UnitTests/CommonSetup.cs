namespace Dsa.Linear.UnitTests;
internal class CommonSetup
{
	/// <summary>
	/// Converts a string of comma separated integers (Int32) to an array of Int32.
	/// </summary>
	/// <param name="ints">Comma separated Int32</param>
	/// <returns><see cref="Int32[]"/></returns>
	internal static Int32[] GetInt32Array(String ints)
		=> String.IsNullOrEmpty(ints) ? Array.Empty<Int32>() : ints.Split(',').Select(Int32.Parse).ToArray();
}
