namespace argumentParser.Classes.Actions;

enum SortingType
{
    Ascending,
    Descending
}

enum SortingMethod
{
    BubbleSort,
    QuickSort,
    MergeSort
}

class Sorting : IActionable
{
    private SortingType _sortType = SortingType.Ascending;
    private SortingMethod _sortMethod = SortingMethod.BubbleSort;
    private Stream? _inputFileStream;
    private Stream? _outputFileStream;
    private bool _overwriteOutputFile = false;

    public Sorting()
    {
        // Checking sorting method, defaulting to BubbleSort
        if (ArgumentParser.TryGet("method", out Argument? methodArg) && methodArg!.IsArgument())
        {
            if (methodArg.GetValue() == "bubble")
            {
                _sortMethod = SortingMethod.BubbleSort;
            }
            else if (methodArg.GetValue() == "quick")
            {
                _sortMethod = SortingMethod.QuickSort;
            }
            else if (methodArg.GetValue() == "merge")
            {
                _sortMethod = SortingMethod.MergeSort;
            }
        }

        // Checking sorting order, defaulting to Ascending
        if (ArgumentParser.TryGet("order", out Argument? sortArg) && sortArg!.IsArgument())
        {
            if (sortArg.GetValue() == "descending")
            {
                _sortType = SortingType.Descending;
            }
            else if (sortArg.GetValue() == "ascending")
            {
                _sortType = SortingType.Ascending;
            }
        }

        // Checking input file access, it must exist and be readable
        if (ArgumentParser.TryGet("input", out Argument? inputArg) && inputArg!.IsArgument())
        {
            var _inputFilePath = inputArg.GetValue();
            if (!File.Exists(_inputFilePath))
            {
                throw new FileNotFoundException($"Input file not found: {_inputFilePath}");
            }
            try
            {
                _inputFileStream = File.OpenRead(_inputFilePath);
            }
            catch (Exception ex)
            {
                throw new IOException($"Cannot access input file: {_inputFilePath}", ex);
            }
        }

        // Checking if output file should be overwritten
        if (ArgumentParser.TryGet("overwrite", out Argument? overwriteArg))
        {
            _overwriteOutputFile = true;
        }

        // Checking output file access, it must be writable, if it does not exist, it will be created
        if (ArgumentParser.TryGet("output", out Argument? outputArg) && outputArg!.IsArgument())
        {
            var _outputFilePath = outputArg.GetValue();
            if (File.Exists(_outputFilePath) && !_overwriteOutputFile)
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

    }

    public void Run()
    {
        Logger.DisplayMessage($"Sorting method: {_sortMethod}, Order: {_sortType}", MesssageType.INFO);
        var inputList = ReadInputFile();
        Logger.DisplayMessage($"Input file read successfully, {inputList.Count} numbers found.", MesssageType.INFO);
        if (inputList.Count == 0)
        {
            throw new InvalidOperationException("Input file is empty or contains no valid numbers.");
        }
        // Measure the time it takes to sort the list
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        stopwatch.Start();
        switch (_sortMethod)
        {
            case SortingMethod.BubbleSort:
                BubbleSort(inputList, _sortType == SortingType.Ascending);
                break;
            case SortingMethod.QuickSort:
                QuickSort(inputList, _sortType == SortingType.Ascending);
                break;
            case SortingMethod.MergeSort:
                MergeSort(inputList, _sortType == SortingType.Ascending);
                break;
        }
        stopwatch.Stop();
        Logger.DisplayMessage($"Sorting completed in {stopwatch.ElapsedMilliseconds} ms.", MesssageType.INFO);
        Logger.DisplayMessage($"Writing sorted list to output file.", MesssageType.INFO);

        WriteOutputFile(inputList);
    }

    private List<int> ReadInputFile()
    {
        if (_inputFileStream == null)
        {
            throw new InvalidOperationException("Input file stream is not initialized.");
        }

        using var reader = new StreamReader(_inputFileStream);
        var input = new List<int>();
        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            if (int.TryParse(line, out int number))
            {
                input.Add(number);
            }
        }
        return input;
    }

    private void WriteOutputFile(List<int> sortedList)
    {
        if (_outputFileStream == null)
        {
            throw new InvalidOperationException("Output file stream is not initialized.");
        }
        using var writer = new StreamWriter(_outputFileStream);
        foreach (var number in sortedList)
        {
            writer.WriteLine(number);
        }
    }

    public static void BubbleSort(List<int> input, bool ascending = true)
    {
        bool changed;
        do
        {
            changed = false;
            for (int i = 0; i < input.Count - 1; i++)
            {
                if (ascending)
                {
                    if (input[i + 1] < input[i])
                    {
                        int temp = input[i + 1];
                        input[i + 1] = input[i];
                        input[i] = temp;
                        changed = true;
                    }
                }
                else
                {
                    if (input[i + 1] > input[i])
                    {
                        int temp = input[i + 1];
                        input[i + 1] = input[i];
                        input[i] = temp;
                        changed = true;
                    }
                }
            }
        } while (changed);
    }


    public static void QuickSort(List<int> input, bool ascending = true)
    {
        if (input.Count <= 1) return;

        int pivot = input[input.Count / 2];

        List<int> left = new List<int>();
        List<int> pivots = new List<int>();
        List<int> right = new List<int>();

        foreach (int number in input)
        {
            if (number < pivot)
            {
                left.Add(number);
            }
            else if (number > pivot)
            {
                right.Add(number);
            }
            else
            {
                pivots.Add(number);
            }
        }

        QuickSort(left, ascending);
        QuickSort(right, ascending);

        // This piece of code is used to combine the sorted lists back together
        // It never gets called in the recursive calls, so it is safe to use

        input.Clear();
        if (ascending)
        {
            input.AddRange(left);
            input.AddRange(pivots);
            input.AddRange(right);
        }
        else
        {
            input.AddRange(right);
            input.AddRange(pivots);
            input.AddRange(left);
        }
    }

    public static void MergeSort(List<int> input, bool ascending = true)
    {
        if (input.Count <= 1) return;

        int mid = input.Count / 2;
        List<int> left = input.GetRange(0, mid);
        List<int> right = input.GetRange(mid, input.Count - mid);

        MergeSort(left, ascending);
        MergeSort(right, ascending);

        int i = 0, j = 0, k = 0;

        while (i < left.Count && j < right.Count)
        {
            if (ascending ? left[i] <= right[j] : left[i] >= right[j])
            {
                input[k++] = left[i++];
            }
            else
            {
                input[k++] = right[j++];
            }
        }

        while (i < left.Count)
        {
            input[k++] = left[i++];
        }

        while (j < right.Count)
        {
            input[k++] = right[j++];
        }
    }

    public string GetDescription()
    {
        string description =
            "Sorts a list of numbers in ascending or descending order using a specified sorting method.\n" +
            "Available sorting methods: bubble, quick, merge.\n" +
            "Available sorting orders: ascending, descending.\n" +
            "Input file must exist and be readable, output file will be created if it does not exist.\n" +
            "Example usage: --method bubble --order ascending --input numbers.txt --output sorted_numbers.txt" +
            "   optional: --overwrite : to overwrite the output file if it already exists.";
        return description;
    }

    public string GetName()
    {
        return "Sorting";
    }

    ~Sorting()
    {
        if (_inputFileStream != null)
        {
            _inputFileStream.Dispose();
        }
        if (_outputFileStream != null)
        {
            _outputFileStream.Dispose();
        }
    }

}