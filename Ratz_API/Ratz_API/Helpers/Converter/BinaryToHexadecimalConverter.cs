namespace Ratz_API.Helpers.Converter;

public static class BinaryToHexadecimalConverter
{
    public static int Convert(string strBinary)
    {
        string strHex = System.Convert.ToInt32(strBinary,2).ToString("X");
        return int.Parse(strHex, System.Globalization.NumberStyles.HexNumber);
    }
}