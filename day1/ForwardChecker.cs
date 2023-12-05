namespace AdventOfCode
{
	public static class ForwardChecker
	{
        public static int CheckDigitForwards(string line, int startingIndex)
        {
            var startingCharacter = line[startingIndex];
            int? digit = null;

            while (digit == null)
            {
                (digit, startingIndex) = GetDigitByCheckingIfWordOrDigit_Forward(startingCharacter, line, startingIndex);
                startingCharacter = line[startingIndex];
            }

            return digit.Value;
        }

        static (int?, int) GetDigitByCheckingIfWordOrDigit_Forward(char startingCharacter, string line, int startingIndex)
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
                'o' => GetDigitOneWord_Forward(line, startingIndex),
                't' => GetDigitTwoOrThreeWord_Forward(line, startingIndex),
                'f' => GetDigitFourOrFiveWord_Forward(line, startingIndex),
                's' => GetDigitSixOrSevenWord_Forward(line, startingIndex),
                'e' => GetDigitEightWord_Forward(line, startingIndex),
                'n' => GetDigitNineWord_Forward(line, startingIndex),
                _ => (null, startingIndex + 1),
            };
        }

        static (int?, int) GetDigitOneWord_Forward(string line, int startingIndex)
        {
            var index = startingIndex + 1;
            var secondCharacter = line[index];

            if (secondCharacter == 'n')
            {
                index++;
                var thirdCharacter = line[index];

                if (thirdCharacter == 'e') return (1, index);
                else return GetDigitByCheckingIfWordOrDigit_Forward(thirdCharacter, line, index);

            }
            else return GetDigitByCheckingIfWordOrDigit_Forward(secondCharacter, line, index);
        }

        static (int?, int) GetDigitTwoOrThreeWord_Forward(string line, int startingIndex)
        {
            var index = startingIndex + 1;
            var secondCharacter = line[index];

            if (secondCharacter == 'w')
            {
                index++;
                var thirdCharacter = line[index];

                if (thirdCharacter == 'o') return (2, index);
                else return GetDigitByCheckingIfWordOrDigit_Forward(thirdCharacter, line, index);

            }
            else if (secondCharacter == 'h')
            {
                index++;
                var thirdCharacter = line[index];

                if (thirdCharacter == 'r')
                {
                    index++;
                    var fourthCharacter = line[index];
                    var fifthCharacter = line[index + 1];

                    if (fourthCharacter == 'e')
                    {
                        index++;

                        if (fifthCharacter == 'e') return (3, index);
                        else return GetDigitEightWord_Forward(line, index - 1);
                    }
                    else return GetDigitByCheckingIfWordOrDigit_Forward(fourthCharacter, line, index);
                }
                else return GetDigitByCheckingIfWordOrDigit_Forward(thirdCharacter, line, index);
            }
            else return GetDigitByCheckingIfWordOrDigit_Forward(secondCharacter, line, index);
        }

        static (int?, int) GetDigitFourOrFiveWord_Forward(string line, int startingIndex)
        {
            var index = startingIndex + 1;
            var secondCharacter = line[index];

            if (secondCharacter == 'o')
            {
                index++;
                var thirdCharacter = line[index];

                if (thirdCharacter == 'u')
                {
                    index++;
                    var fourthCharacter = line[index];

                    if (fourthCharacter == 'r') return (4, index);
                    else return GetDigitByCheckingIfWordOrDigit_Forward(fourthCharacter, line, index);
                }
                else return GetDigitOneWord_Forward(line, index - 1);
            }
            else if (secondCharacter == 'i')
            {
                index++;
                var thirdCharacter = line[index];

                if (thirdCharacter == 'v')
                {
                    index++;
                    var fourthCharacter = line[index];

                    if (fourthCharacter == 'e') return (5, index);
                    else return GetDigitByCheckingIfWordOrDigit_Forward(fourthCharacter, line, index);
                }
                else return GetDigitByCheckingIfWordOrDigit_Forward(thirdCharacter, line, index);
            }
            else return GetDigitByCheckingIfWordOrDigit_Forward(secondCharacter, line, index);
        }

        static (int?, int) GetDigitSixOrSevenWord_Forward(string line, int startingIndex)
        {
            var index = startingIndex + 1;
            var secondCharacter = line[index];

            if (secondCharacter == 'i')
            {
                index++;
                var thirdCharacter = line[index];

                if (thirdCharacter == 'x') return (6, index);
                else return GetDigitByCheckingIfWordOrDigit_Forward(thirdCharacter, line, index);

            }
            else if (secondCharacter == 'e')
            {
                index++;
                var thirdCharacter = line[index];

                if (thirdCharacter == 'v')
                {
                    index++;
                    var fourthCharacter = line[index];

                    if (fourthCharacter == 'e')
                    {
                        index++;
                        var fifthCharacter = line[index];

                        if (fifthCharacter == 'n') return (7, index);
                        else return GetDigitEightWord_Forward(line, index - 1);
                    }
                    else return GetDigitByCheckingIfWordOrDigit_Forward(fourthCharacter, line, index);
                }
                else return GetDigitEightWord_Forward(line, index - 1);
            }
            else return GetDigitByCheckingIfWordOrDigit_Forward(secondCharacter, line, index);
        }

        static (int?, int) GetDigitEightWord_Forward(string line, int startingIndex)
        {
            var index = startingIndex + 1;
            var secondCharacter = line[index];

            if (secondCharacter == 'i')
            {
                index++;
                var thirdCharacter = line[index];

                if (thirdCharacter == 'g')
                {
                    index++;
                    var fourthCharacter = line[index];

                    if (fourthCharacter == 'h')
                    {
                        index++;
                        var fifthCharacter = line[index];

                        if (fifthCharacter == 't') return (8, index);
                        else return GetDigitByCheckingIfWordOrDigit_Forward(fifthCharacter, line, index);
                    }
                    else return GetDigitByCheckingIfWordOrDigit_Forward(fourthCharacter, line, index);
                }
                else return GetDigitByCheckingIfWordOrDigit_Forward(thirdCharacter, line, index);
            }
            else return GetDigitByCheckingIfWordOrDigit_Forward(secondCharacter, line, index);
        }

        static (int?, int) GetDigitNineWord_Forward(string line, int startingIndex)
        {
            var index = startingIndex + 1;
            var secondCharacter = line[index];

            if (secondCharacter == 'i')
            {
                index++;
                var thirdCharacter = line[index];

                if (thirdCharacter == 'n')
                {
                    index++;
                    var fourthCharacter = line[index];

                    if (fourthCharacter == 'e') return (9, index);
                    else return GetDigitNineWord_Forward(line, index - 1);
                }
                else return GetDigitByCheckingIfWordOrDigit_Forward(thirdCharacter, line, index);
            }
            else return GetDigitByCheckingIfWordOrDigit_Forward(secondCharacter, line, index);
        }
    }
}

