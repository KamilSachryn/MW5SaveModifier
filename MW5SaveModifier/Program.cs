

using System.Globalization;

string path = @"";

Console.Write("Drag save file here: ");
path = Console.ReadLine();

int hexIn;
String hex = "";
String file = "";
int j = 0;
long fileSize = new FileInfo(path).Length;
const string startOfItem = "496e7450726f7065727479";



using (FileStream fsSource = new FileStream(path,
           FileMode.Open, FileAccess.ReadWrite))
{

    // Read the source file into a byte array.
    byte[] bytez = new byte[fsSource.Length];
    int numBytesToRead = (int)fsSource.Length;
    int numBytesRead = 0;
    while (numBytesToRead > 0)
    {
        // Read may return anything from 0 to numBytesToRead.
        int n = fsSource.Read(bytez, numBytesRead, numBytesToRead);

        // Break when the end of the file is reached.
        if (n == 0)
            break;

        numBytesRead += n;
        numBytesToRead -= n;
    }
    numBytesToRead = bytez.Length;


    file = Convert.ToHexString(bytez, 0, numBytesRead);
}

file = file.ToLower();


    List<int> indexes = AllIndexesOf(file, startOfItem);

foreach(int i in indexes)
{

    for(int k = 42; k < 46; k++)
    {
        Console.Write(file[i + k]);
    }
    Console.WriteLine();
}

char[] charFile = file.ToCharArray();
foreach (int i in indexes)
{
    charFile[i + 42] = '3';
    charFile[i + 43] = '9';
    charFile[i + 44] = '3';
    charFile[i + 45] = '0';
}

string newFile = new string(charFile);

byte[] bytes = ConvertHexToByteArray(newFile);
File.WriteAllBytes(path + ".EDITED", bytes);



// convert hex values of file back to bytes
byte[] ConvertHexToByteArray(string hexString)
{
    byte[] byteArray = new byte[hexString.Length / 2];

    for (int index = 0; index < byteArray.Length; index++)
    {
        string byteValue = hexString.Substring(index * 2, 2);
        byteArray[index] = byte.Parse(byteValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
    }

    return byteArray;
}


List<int> AllIndexesOf(string str, string value)
{
    if (String.IsNullOrEmpty(value))
        throw new ArgumentException("the string to find may not be empty", "value");
    List<int> indexes = new List<int>();
    for (int index = 0; ; index += value.Length)
    {
        index = str.IndexOf(value, index);
        if (index == -1)
            return indexes;
        indexes.Add(index);
    }
}

