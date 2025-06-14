using argumentParser.Classes.Utils;

namespace argumentParser.Classes.Actions;

enum DataType
{
    Integer,
    Float,
    String
}

class DataGenerator : IActionable
{
    private DataType _dataType = DataType.Integer;
    private int _count = 10;
    private Stream? _outputFileStream;
    private bool _overwriteFile = false;
    private float _min = 0.0f;
    private float _max = 1.0f;
    private int _length = 10;
    private StringType _stringType = StringType.Alphanumeric;
    private StringCase _stringCase = StringCase.Mixed;
    private bool _unique = false;

    public DataGenerator()
    {
        if (ArgumentParser.TryGet("type", out Argument? typeArg) && typeArg!.IsArgument())
        {
            if (typeArg.GetValue() == "float")
            {
                _dataType = DataType.Float;
            }
            else if (typeArg.GetValue() == "string")
            {
                _dataType = DataType.String;
            }
        }

        if (ArgumentParser.TryGet("count", out Argument? countArg) && countArg!.IsArgument())
        {
            if (int.TryParse(countArg.GetValue(), out int count) && count > 0)
            {
                _count = count;
            }
        }

        if (ArgumentParser.TryGet("overwrite", out Argument? overwriteArg))
        {
            _overwriteFile = true;
        }

        if (ArgumentParser.TryGet("output", out Argument? outputArg) && outputArg!.IsArgument())
        {
            var _outputFilePath = outputArg.GetValue();
            if (File.Exists(_outputFilePath) && !_overwriteFile)
            {
                throw new IOException($"Output file already exists and overwrite is not allowed: {_outputFilePath}");
            }
            try
            {
                _outputFileStream = File.Open(_outputFilePath, FileMode.Create, FileAccess.Write);
            }
            catch (Exception ex)
            {
                throw new IOException($"Cannot write to output file: {_outputFilePath}", ex);
            }
        }
        else
        {
            throw new ArgumentException("Output file path is required.");
        }

        if (ArgumentParser.TryGet("min", out Argument? minArg) && minArg!.IsArgument())
        {
            if (float.TryParse(minArg.GetValue(), out float min))
            {
                _min = min;
            }
            else
            {
                throw new ArgumentException($"Invalid minimum value: {minArg.GetValue()}");
            }
        }

        if (ArgumentParser.TryGet("max", out Argument? maxArg) && maxArg!.IsArgument())
        {
            if (float.TryParse(maxArg.GetValue(), out float max))
            {
                _max = max;
            }
            else
            {
                throw new ArgumentException($"Invalid maximum value: {maxArg.GetValue()}");
            }
        }

        if (_dataType != DataType.String)
        {
            if (_min >= _max)
            {
                throw new ArgumentException($"Minimum value {_min} must be less than maximum value {_max}.");
            }
        }

        if (_dataType == DataType.String)
        {
            if (ArgumentParser.TryGet("length", out Argument? lengthArg) && lengthArg!.IsArgument())
            {
                if (int.TryParse(lengthArg.GetValue(), out int len) && len > 0)
                {
                    _length = len;
                }
                else
                {
                    throw new ArgumentException($"Invalid string length: {lengthArg.GetValue()}");
                }
            }

            if (ArgumentParser.TryGet("stringType", out Argument? stringTypeArg) && stringTypeArg!.IsArgument())
            {
                if (Enum.TryParse(stringTypeArg.GetValue(), true, out StringType stringType))
                {
                    _stringType = stringType;
                }
                else
                {
                    throw new ArgumentException($"Invalid string type: {stringTypeArg.GetValue()}");
                }
            }

            if (ArgumentParser.TryGet("stringCase", out Argument? stringCaseArg) && stringCaseArg!.IsArgument())
            {
                if (Enum.TryParse(stringCaseArg.GetValue(), true, out StringCase stringCase))
                {
                    _stringCase = stringCase;
                }
                else
                {
                    throw new ArgumentException($"Invalid string case: {stringCaseArg.GetValue()}");
                }
            }
        }

        if (ArgumentParser.TryGet("unique", out Argument? uniqueArg) && uniqueArg!.IsFlag())
        {
            _unique = true;
        }
    }

    ~DataGenerator()
    {
        _outputFileStream?.Dispose();
    }

    public void Run()
    {
        if (_dataType == DataType.Integer)
            FileHandler.WriteListToFile<int>(
                _outputFileStream!,
                Generator.GenerateRandomNumbers(
                    _count,
                    (int)_min,
                    (int)_max,
                    _unique));
        else if (_dataType == DataType.Float)
            FileHandler.WriteListToFile<float>(
                _outputFileStream!,
                Generator.GetRandomFloats(
                    _count,
                    _min,
                    _max,
                    _unique));
        else if (_dataType == DataType.String)
            FileHandler.WriteListToFile<string>(
                _outputFileStream!,
                Generator.GetRandomStrings(
                    _stringType,
                    _stringCase,
                    _count,
                    _length,
                    _unique));
        // else throw new NotSupportedException($"Data type {_dataType} is not supported.");

        // It looks weird, but it works
        // FileHandler.WriteListToFile(_outputFileStream!, _dataType switch
        // {
        //     DataType.Integer => Generator.GenerateRandomNumbers(_count, (int)_min, (int)_max).Cast<object>().ToList(),
        //     DataType.Float => Generator.GetRandomFloats(_count, _min, _max).Cast<object>().ToList(),
        //     DataType.String => Generator.GetRandomStrings(_stringType, _stringCase, _count, _length, unique: true).Cast<object>().ToList(),
        //     _ => throw new NotSupportedException($"Data type {_dataType} is not supported.")
        // });
    }

    public string GetDescription()
    {
        throw new NotImplementedException();
    }

    public string GetName()
    {
        throw new NotImplementedException();
    }

}