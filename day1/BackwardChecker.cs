namespace AdventOfCode
{
	public class BackwardChecker
	{
        public static int CheckDigitBackwards(string line, int startingIndex)
        {
            var startingCharacter = line[startingIndex];
            int? digit = null;

            while (digit == null)
            {
                (digit, startingIndex) = GetDigitByCheckingIfWordOrDigit_Backward(startingCharacter, line, startingIndex);
                startingCharacter = line[startingIndex];
            }

            return digit.Value;
        }

        static (int?, int) GetDigitByCheckingIfWordOrDigit_Backward(char startingCharacter, string line, int startingIndex)
        {
            return startingCharacter switch
            {
                '1' => ((int?, int))(1, startingIndex),
                '2' => ((int?, int))(2, startingIndex),
                '3' => ((int?, int))(3, startingIndex),
                '4' => ((int?, int))(4, startingIndex),
                '5' => ((int?, int))(5, startingIndex),
                '6' => ((int?, int))(6, startingIndex),
                '7' => ((int?, int))(7, startingIndex),
                '8' => ((int?, int))(8, startingIndex),
                '9' => ((int?, int))(9, startingIndex),
                'e' => GetDigitOneThreeFiveOrNineWord_Backward(line, startingIndex),
                'o' => GetDigitTwoWord_Backward(line, startingIndex),
                'r' => GetDigitFourWord_Backward(line, startingIndex),
                'x' => GetDigitSixWord_Backward(line, startingIndex),
                'n' => GetDigitSevenWord_Backward(line, startingIndex),
                't' => GetDigitEightWord_Backward(line, startingIndex),
                _ => (null, startingIndex - 1),
            };
        }

        static (int?, int) GetDigitOneThreeFiveOrNineWord_Backward(string line, int startingIndex)
        {
            var index = startingIndex - 1;
            var secondCharacter = line[index];

            if (secondCharacter == 'n')
            {
                index--;
                var thirdCharacter = line[index];

                if (thirdCharacter == 'o') return (1, index);
                else if (thirdCharacter == 'i')
                {
                    index--;
                    var fourthCharacter = line[index];

                    if (fourthCharacter == 'n') return (9, index);
                    else return GetDigitByCheckingIfWordOrDigit_Backward(fourthCharacter, line, index);
                }
                else return GetDigitSevenWord_Backward(line, index + 1);

            }
            else if (secondCharacter == 'e')
            {
                index--;
                var thirdCharacter = line[index];

                if (thirdCharacter == 'r')
                {
                    index--;
                    var fourthCharacter = line[index];

                    if (fourthCharacter == 'h')
                    {
                        index--;
                        var fifthCharacter = line[index];

                        if (fifthCharacter == 't') return (3, index);
                        else return GetDigitByCheckingIfWordOrDigit_Backward(fifthCharacter, line, index);
                    }
                    else return GetDigitFourWord_Backward(line, index + 1);
                }
                else return GetDigitOneThreeFiveOrNineWord_Backward(line, index + 1);
            }
            else if (secondCharacter == 'v')
            {
                index--;
                var thirdCharacter = line[index];

                if (thirdCharacter == 'i')
                {
                    index--;
                    var fourthCharacter = line[index];

                    if (fourthCharacter == 'f') return (5, index);
                    else return GetDigitByCheckingIfWordOrDigit_Backward(fourthCharacter, line, index);
                }
                else return GetDigitByCheckingIfWordOrDigit_Backward(thirdCharacter, line, index);
            }
            else return GetDigitByCheckingIfWordOrDigit_Backward(secondCharacter, line, index);
        }

        static (int?, int) GetDigitTwoWord_Backward(string line, int startingIndex)
        {
            var index = startingIndex - 1;
            var secondCharacter = line[index];

            if (secondCharacter == 'w')
            {
                index--;
                var thirdCharacter = line[index];

                if (thirdCharacter == 't') return (2, index);
                else return GetDigitByCheckingIfWordOrDigit_Backward(thirdCharacter, line, index);

            }
            else return GetDigitByCheckingIfWordOrDigit_Backward(secondCharacter, line, index);
        }

        static (int?, int) GetDigitFourWord_Backward(string line, int startingIndex)
        {
            var index = startingIndex - 1;
            var secondCharacter = line[index];

            if (secondCharacter == 'u')
            {
                index--;
                var thirdCharacter = line[index];

                if (thirdCharacter == 'o')
                {
                    index--;
                    var fourthCharacter = line[index];

                    if (fourthCharacter == 'f') return (4, index);
                    else return GetDigitTwoWord_Backward(line, index + 1);
                }
                else return GetDigitByCheckingIfWordOrDigit_Backward(thirdCharacter, line, index);
            }
            else return GetDigitByCheckingIfWordOrDigit_Backward(secondCharacter, line, index);
        }

        static (int?, int) GetDigitSixWord_Backward(string line, int startingIndex)
        {
            var index = startingIndex - 1;
            var secondCharacter = line[index];

            if (secondCharacter == 'i')
            {
                index--;
                var thirdCharacter = line[index];

                if (thirdCharacter == 's') return (6, index);
                else return GetDigitByCheckingIfWordOrDigit_Backward(thirdCharacter, line, index);

            }
            else return GetDigitByCheckingIfWordOrDigit_Backward(secondCharacter, line, index);
        }

        static (int?, int) GetDigitSevenWord_Backward(string line, int startingIndex)
        {
            var index = startingIndex - 1;
            var secondCharacter = line[index];

            if (secondCharacter == 'e')
            {
                index--;
                var thirdCharacter = line[index];

                if (thirdCharacter == 'v')
                {
                    index--;
                    var fourthCharacter = line[index];

                    if (fourthCharacter == 'e')
                    {
                        index--;
                        var fifthCharacter = line[index];

                        if (fifthCharacter == 's') return (7, index);
                        else return GetDigitOneThreeFiveOrNineWord_Backward(line, index + 1);
                    }
                    else return GetDigitByCheckingIfWordOrDigit_Backward(fourthCharacter, line, index);
                }
                else return GetDigitOneThreeFiveOrNineWord_Backward(line, index + 1);
            }
            else return GetDigitByCheckingIfWordOrDigit_Backward(secondCharacter, line, index);
        }

        static (int?, int) GetDigitEightWord_Backward(string line, int startingIndex)
        {
            var index = startingIndex - 1;
            var secondCharacter = line[index];

            if (secondCharacter == 'h')
            {
                index--;
                var thirdCharacter = line[index];

                if (thirdCharacter == 'g')
                {
                    index--;
                    var fourthCharacter = line[index];

                    if (fourthCharacter == 'i')
                    {
                        index--;
                        var fifthCharacter = line[index];

                        if (fifthCharacter == 'e') return (8, index);
                        else return GetDigitByCheckingIfWordOrDigit_Backward(fifthCharacter, line, index);
                    }
                    else return GetDigitByCheckingIfWordOrDigit_Backward(fourthCharacter, line, index);
                }
                else return GetDigitByCheckingIfWordOrDigit_Backward(thirdCharacter, line, index);
            }
            else return GetDigitByCheckingIfWordOrDigit_Backward(secondCharacter, line, index);
        }
    }
}

