public class DatatypeHelpers
{
    /// <summary>
    /// Convert an int to bool
    /// </summary>
    /// <param name="number">The int to convert</param>
    /// <returns>The bool value associated with the int</returns>
    public static bool IntToBool(int number)
    {
        if (number == 0)
        {
            return false;
        }

        return true;
    }
}
