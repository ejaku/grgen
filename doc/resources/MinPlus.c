int MinPlus(int x, int y)
{
    int min;

    if (x < y) {
        min = x;
    } else {
        min = y;
    }

    return min + 1;
}
