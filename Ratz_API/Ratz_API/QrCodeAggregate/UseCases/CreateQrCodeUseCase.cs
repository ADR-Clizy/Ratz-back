using DatabaseConnection;
using Ratz_API.Helpers.Converter;
using STH1123.ReedSolomon;

namespace Ratz_API.QrCodeAggregate.UseCases;

public class CreateQrCodeUseCase
{
    private readonly Dictionary<char, byte> _iso18004 = new()
    {
        {'0', 0}, {'1', 1}, {'2', 2}, {'3', 3}, {'4', 4}, {'5', 5}, {'6', 6}, {'7', 7}, {'8', 8}, {'9', 9},
        {'A', 10}, {'B', 11}, {'C', 12}, {'D', 13}, {'E', 14}, {'F', 15}, {'G', 16}, {'H', 17}, {'I', 18}, {'J', 19},
        {'K', 20}, {'L', 21}, {'M', 22}, {'N', 23}, {'O', 24}, {'P', 25}, {'Q', 26}, {'R', 27}, {'S', 28}, {'T', 29},
        {'U', 30}, {'V', 31}, {'W', 32}, {'X', 33}, {'Y', 34}, {'Z', 35}, {' ', 36}, {'$', 37}, {'%', 38}, {'*', 39},
        {'+', 40}, {'-', 41}, {'.', 42}, {'/', 43}, {':', 44}
    };
    
    private readonly Dictionary<int, List<int>> _qrCodeVersionTable = new()
    {
        {
            1, new List<int>()
            {
                152, 128, 104, 72
            }
        },
        {
            2, new List<int>()
            {
                272, 224, 176, 128
            }
        },
        {
            3, new List<int>()
            {
                440, 352, 272, 208
            }
        },
        {
            4, new List<int>()
            {
                640, 512, 384, 288
            }
        },
        {
            5, new List<int>()
            {
                864, 688, 496, 368
            }
        },
        {
            6, new List<int>()
            {
                1088, 864, 608, 480
            }
        },
        {
            7, new List<int>()
            {
                1248, 992, 704, 528
            }
        },
        {
            8, new List<int>()
            {
                1552, 1232, 880, 688
            }
        },
        {
            9, new List<int>()
            {
                1856, 1456, 1056, 800
            }
        },
        {
            10, new List<int>()
            {
                2192, 1728, 1232, 976
            }
        },
        {
            11, new List<int>()
            {
                2592, 2032, 1440, 1120
            }
        },
        {
            12, new List<int>()
            {
                2960, 2320, 1648, 1264
            }
        },
        {
            13, new List<int>()
            {
                3424, 2672, 1952, 1440
            }
        },
        {
            14, new List<int>()
            {
                3688, 2920, 2088, 1576
            }
        },
        {
            15, new List<int>()
            {
                4184, 3320, 2360, 1784
            }
        },
        {
            16, new List<int>()
            {
                4712, 3624, 2600, 2024
            }
        },
        {
            17, new List<int>()
            {
                5176, 4056, 2936, 2264
            }
        },
        {
            18, new List<int>()
            {
                5768, 4504, 3176, 2504
            }
        },
        {
            19, new List<int>()
            {
                6360, 5592, 3944, 2728
            }
        },
        {
            20, new List<int>()
            {
                6888, 5352, 3880, 3080
            }
        },
        {
            21, new List<int>()
            {
                7456, 5712, 4096, 3248
            }
        },
        {
            22, new List<int>()
            {
                8048, 6256, 4544, 3536
            }
        },
        {
            23, new List<int>()
            {
                8752, 6880, 4912, 3712
            }
        },
        {
            24, new List<int>()
            {
                9392, 7312, 5312, 4112
            }
        },
        {
            25, new List<int>()
            {
                10208, 8000, 5744, 4304
            }
        },
        {
            26, new List<int>()
            {
                10960, 8496, 6032, 4768
            }
        },
        {
            27, new List<int>()
            {
                11744, 9024, 6464, 5024
            }
        },
        {
            28, new List<int>()
            {
                12248, 9544, 6968, 5288
            }
        },
        {
            29, new List<int>()
            {
                13048, 10136, 7288, 5608
            }
        },
        {
            30, new List<int>()
            {
                13880, 10984, 7880, 5960
            }
        },
        {
            31, new List<int>()
            {
                14744, 11640, 8264, 6344
            }
        },
        {
            32, new List<int>()
            {
                15640, 12328, 8920, 6760
            }
        },
        {
            33, new List<int>()
            {
                16568, 13048, 9368, 7208
            }
        },
        {
            34, new List<int>()
            {
                17528, 13800, 9848, 7688
            }
        },
        {
            35, new List<int>()
            {
                18448, 14496, 10288, 7888
            }
        },
        {
            36, new List<int>()
            {
                19472, 15312, 10832, 8432
            }
        },
        {
            37, new List<int>()
            {
                20528, 15936, 11408, 8768
            }
        },
        {
            38, new List<int>()
            {
                21616, 16816, 12016, 9136
            }
        },
        {
            39, new List<int>()
            {
                22496, 17728, 12656, 9776
            }
        },
        {
            40, new List<int>()
            {
                23648, 18672, 13328, 10208
            }
        }
    };

    private readonly Dictionary<List<int>, int> _byteToEncodeByQrCodeVersionTable = new()
    {
        {new List<int>(){1, 9}, 9},         
        {new List<int>(){10, 26}, 11},
        {new List<int>(){27, 40}, 13}
    };
    
    private readonly Dictionary<int, string> _errorCorrectionLevels = new ()
    {
        {0, "10"}, // H
        {1, "11"}, // Q
        {2, "00"}, // M
        {3, "01"}  // L
    };
    
    public readonly Dictionary<string, Dictionary<string, string>> LevelMaskPatterns = new()
    {
        {"01", new Dictionary<string, string>() {{"000", "111011111000100"}}},
        {"00", new Dictionary<string, string>() {{"000", "101010000010010"}}},
        {"11", new Dictionary<string, string>() {{"000", "011010101011111"}}},
        {"10", new Dictionary<string, string>() {{"000", "001011010001001"}}}
    };
    
    private readonly Dictionary<string, int> _dataCapacityFromQrCodeVersion = new()
    {
        {"1", 26},
        {"2", 44},
        {"3", 70},
        {"4", 100},
        {"5", 134},
        {"6", 172},
        {"7", 196},
        {"8", 242},
        {"9", 292},
        {"10", 346},
        {"11", 404},
        {"12", 466},
        {"13", 532},
        {"14", 581},
        {"15", 655},
        {"16", 733},
        {"17", 815},
        {"18", 901},
        {"19", 991},
        {"20", 1085},
        {"21", 1156},
        {"22", 1258},
        {"23", 1364},
        {"24", 1474},
        {"25", 1588},
        {"26", 1706},
        {"27", 1828},
        {"28", 1921},
        {"29", 2051},
        {"30", 2185},
        {"31", 2323},
        {"32", 2465},
        {"33", 2611},
        {"34", 2761},
        {"35", 2876},
        {"36", 3034},
        {"37", 3196},
        {"38", 3362},
        {"39", 3532},
        {"40", 3706}
    };
    
    private int _totalQrCodeCapacity;
    private const string ModeIndicator = "0010 ";
    private string _errorCodeLevel;
    const string DefaultDataMask = "000";

    const string QrCode1Mask0 =
        "100110011001100110011001011001100110011001100110" +
        "100110011001100110011001011001100110011001100110" +
        "1001100110011001100110011001011001100110" +
        "1001100110010110011001100110011001100110" +
        "10011001" +
        "100110011001100110011001";

    public QrCode Handle(string url, int userId)
    {
        int byteToEncodeStringData = 11;
        List<string> binaryData = new();
        
        List<List<byte>> byteData = ConvertStringToListOfByteList(url);
        List<int> transformedByteData = TransformByteDataToListOfInt(byteData);

        string encodedData = "";
        
        for (int i = 0; i < transformedByteData.Count; i++)
        {
            if (i == transformedByteData.Count - 1)
            {
                if (transformedByteData.Count % 2 == 0)
                {
                    byteToEncodeStringData = 6;
                }
            }

            binaryData.Add(IntToBinary(transformedByteData[i], byteToEncodeStringData));
        }
        
        List<int> qrCodeVersion = GetQrCodeVersion(binaryData);
        
        string binaryDataInformation = ModeIndicator + IntToBinary(url.Length, GetNumberOfBitsForEncoding(binaryData)) + " ";

        foreach (string byteString in binaryData)
        {
            binaryDataInformation += byteString + " ";
        }
        
        int numberOfErrorCodewords = GetNumberOfErrorCodewords(qrCodeVersion[0], qrCodeVersion[1] / 8);
        
        IEnumerable<int> dataWithErrorCode = AddByteErrorCodeToData(StringTo8BitsByte(binaryDataInformation, qrCodeVersion[1] / 8).Split(" "), numberOfErrorCodewords);

        foreach (int data in dataWithErrorCode)
        {
            encodedData += IntToBinary(data, 8) + " ";
        }

        string encodedUrl = ApplyMaskOnEncodedData(encodedData.Replace(" ", "").ToCharArray(), QrCode1Mask0);

        return new QrCode()
        {
            url = url,
            UserId = userId,
            EncodedUrl = encodedUrl,
            InformationFormat = LevelMaskPatterns[_errorCodeLevel][DefaultDataMask]
        };
    }
    
    private List<List<byte>> ConvertStringToListOfByteList(string url)
    {
        List<List<byte>> list = new();

        byte[] sentenceArray = ConvertStringToByteArray(url);

        for (int i = 1; i < sentenceArray.Length + 1; i++)
        {
            if (i % 2 == 0)
            {
                list.Add(new List<byte>() {sentenceArray[i - 2], sentenceArray[i - 1]});
            }
        }

        if (sentenceArray.Length % 2 != 0)
        {
            list.Add(new List<byte>() {sentenceArray[^1]});
        }

        return list;
    }
    
    private byte[] ConvertStringToByteArray(string sentence)
    {
        char[] sentenceArray = sentence.ToCharArray();

        return sentenceArray.Select(character => _iso18004[char.ToUpper(character)]).ToArray();
    }
    
    private List<int> TransformByteDataToListOfInt(List<List<byte>> list)
    {
        List<int> resultList = new();
        
        foreach (List<byte> bytes in list)
        {
            if (bytes.Count == 2)
            {
                resultList.Add(bytes[0] * 45 + bytes[1]);
            }
            else
            {
                resultList.Add(bytes[0]);
            }
        }

        return resultList;
    }
    
    private string IntToBinary(int number, int bits)
    {
        string binary = Convert.ToString(number, 2);
        int binaryLength = binary.Length;
        string result = "";

        if (binaryLength < bits)
        {
            for (int i = 0; i < bits - binaryLength; i++)
            {
                result += "0";
            }
        }

        result += binary;

        return result;
    }
    
    private List<int> GetQrCodeVersion(List<string> binaryStrings)
    {
        int elevenBitsCount = binaryStrings.Count(_ => _.Length == 11);
        int sixBitsCount = binaryStrings.Count(_ => _.Length == 6);
        
        int totalBits = elevenBitsCount * 11;
        totalBits += sixBitsCount * 6;
        totalBits += 4 + 9;
        
        foreach ((int key, List<int> value) in _qrCodeVersionTable)
        {
            value.Sort();
            
            foreach (int capacity in value.Where(i => totalBits <= i))
            {
                _errorCodeLevel = _errorCorrectionLevels[value.IndexOf(capacity)];
                return new List<int>() {key, capacity};
            }
        }
        
        return new List<int>() {0, 0};
    }
    
    private IEnumerable<int> AddByteErrorCodeToData(IReadOnlyList<string> stringArray, int numberOfErrorCodewords)
    {
        ReedSolomonEncoder encoder = new (GenericGF.QR_CODE_FIELD_256);
        int[] data = Enumerable.Repeat(0x00, _totalQrCodeCapacity).ToArray();
        
        for (int i = 0; i < stringArray.Count; i++)
        {
            data[i] = BinaryToHexadecimalConverter.Convert(stringArray[i]);
        }
        
        encoder.Encode(data, numberOfErrorCodewords);
        
        return data;
    }
    
    private int GetNumberOfErrorCodewords(int qrCodeVersion, int dataCapacity)
    {
        _totalQrCodeCapacity = _dataCapacityFromQrCodeVersion[qrCodeVersion.ToString()];
        return _totalQrCodeCapacity - dataCapacity;
    }
    
    private int GetNumberOfBitsForEncoding(List<string> binaryStrings)
    {
        List<int> capacity = GetQrCodeVersion(binaryStrings);
    
        foreach ((List<int>? bounds, int value) in _byteToEncodeByQrCodeVersionTable)
        {
            if(bounds[0] <= capacity[0] && capacity[0] <= bounds[1])
            {
                return value;
            }
        }
    
        return 0;
    }
    
    public string StringTo8BitsByte(string binary, int numberOfOctets)
    {
        string byteString = "";
        int binaryLength = binary.Length;
        const int bitsLength = 8;
        int count = 0;
        int moduleCount = 0;

        for (int i = 0; i < binaryLength; i++)
        {
            if (binary[i] != ' ')
            {
                byteString += binary[i];
                count++;
            }

            if (count == bitsLength)
            {
                byteString += " ";
                count = 0;
                moduleCount++;
            }

            if (i == binaryLength - 1 && count != 0)
            {
                for (int j = 0; j < bitsLength - count; j++)
                {
                    byteString += "0";
                }
            
                while (moduleCount != numberOfOctets - 1)
                {
                    byteString += " 00000000";
                    moduleCount++;
                }
            }
        }
        
        return byteString;
    }
    
    public string ApplyMaskOnEncodedData(char[] binaryString, string mask)
    {
        string result = "";
        
        for (int i = 0; i < binaryString.Length; i++)
        {   
            if(binaryString[i] == '1' && mask[i] == '1')
            {
                result += '0';
            }
           
            if(binaryString[i] == '1' && mask[i] == '0')
            {
                result += '1';
            }
            
            if(binaryString[i] == '0' && mask[i] == '1')
            {
                result += '1';
            }
            
            if(binaryString[i] == '0' && mask[i] == '0')
            {
                result += '0';
            }
        }
        return result;
    }
}