// ----------------------------------------------------------------------------------------
// Copyright © 2007 - 2026 Tangible Software Solutions Inc.
// This class can be used by anyone provided that the copyright notice remains intact.
//
// This class includes methods to convert Java rectangular arrays (jagged arrays
// with inner arrays of the same length).
// ----------------------------------------------------------------------------------------
internal static class RectangularArrays
{
    public static int[][] RectangularIntArray(int size1, int size2)
    {
        int[][] newArray = new int[size1][];
        for (int array1 = 0; array1 < size1; array1++)
        {
            newArray[array1] = new int[size2];
        }

        return newArray;
    }

    public static int[][][] RectangularIntArray(int size1, int size2, int size3)
    {
        int[][][] newArray = new int[size1][][];
        for (int array1 = 0; array1 < size1; array1++)
        {
            newArray[array1] = new int[size2][];
            if (size3 > -1)
            {
                for (int array2 = 0; array2 < size2; array2++)
                {
                    newArray[array1][array2] = new int[size3];
                }
            }
        }

        return newArray;
    }

    public static short[][] RectangularShortArray(int size1, int size2)
    {
        short[][] newArray = new short[size1][];
        for (int array1 = 0; array1 < size1; array1++)
        {
            newArray[array1] = new short[size2];
        }

        return newArray;
    }
}
