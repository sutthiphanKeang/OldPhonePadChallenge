using System.Text;

public class OldPhone
{
    private static readonly Dictionary<char, string[]> keyMappings = new Dictionary<char, string[]>()
    {
        { '1', new[] { "&", "'", "(" } },
        { '2', new[] { "A", "B", "C" } },
        { '3', new[] { "D", "E", "F" } },
        { '4', new[] { "G", "H", "I" } },
        { '5', new[] { "J", "K", "L" } },
        { '6', new[] { "M", "N", "O" } },
        { '7', new[] { "P", "Q", "R", "S" } },
        { '8', new[] { "T", "U", "V" } },
        { '9', new[] { "W", "X", "Y", "Z" } },
        { '0', new[] { " " } }
    };

    public static string OldPhonePad(string input)
    {
        StringBuilder result = new StringBuilder();
        char previousChar = '\0';
        int count = 0;

        foreach (char c in input)
        {
            if (c == '#') break;
            if (c == '*')
            {
                ResetPreviousChar(ref previousChar, ref count);
                continue;
            }

            if (c == ' ')
            {
                AppendPreviousChar(result, ref previousChar, ref count);
                continue;
            }

            if (keyMappings.ContainsKey(c))
            {
                if (c == previousChar)
                {
                    count++;
                }
                else
                {
                    AppendPreviousChar(result, ref previousChar, ref count);
                    previousChar = c;
                    count = 1;
                }
            }
        }

        AppendPreviousChar(result, ref previousChar, ref count);
        return result.ToString();
    }

    private static void AppendPreviousChar(StringBuilder result, ref char previousChar, ref int count)
    {
        if (previousChar != '\0')
        {
            result.Append(keyMappings[previousChar][(count - 1) % keyMappings[previousChar].Length]);
        }
        ResetPreviousChar(ref previousChar, ref count);
    }

    private static void ResetPreviousChar(ref char previousChar, ref int count)
    {
        previousChar = '\0';
        count = 0;
    }
}

public class Program
{
    public static void Main()
    {
        TestOldPhonePad("33#", "E");
        TestOldPhonePad("227*#", "B");
        TestOldPhonePad("4433555 555666#", "HELLO");
        TestOldPhonePad("8 88777444666*664#", "TURING");
        
        TestOldPhonePad("2222#", "A");
        TestOldPhonePad("2 2#", "AA");
        TestOldPhonePad("22 2#", "BA");
        TestOldPhonePad("22222#", "B");
        TestOldPhonePad("5555555#", "J");
        TestOldPhonePad("6666666#", "M");
        TestOldPhonePad("8888888#", "T");
        TestOldPhonePad("227*66#", "BN");
        TestOldPhonePad("1111#", "&");
        TestOldPhonePad("0 0#", "  ");

        TestOldPhonePad("8442665509996668803336667770999666887770222666 6677774443 3377728444666 660557778822#", "THANK YOU FOR YOUR CONSIDERATION KRUB");
    }

    private static void TestOldPhonePad(string input, string expected)
    {
        string result = OldPhone.OldPhonePad(input);
        Console.WriteLine($"Input: {input} => Output: {result} | Expected: {expected}");
        Console.WriteLine(result == expected ? "Test Passed \n" : "Test Failed \n");
    }
}