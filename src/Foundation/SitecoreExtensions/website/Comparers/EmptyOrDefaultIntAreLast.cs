using System.Collections.Generic;

namespace LionTrust.Foundation.SitecoreExtensions.Comparers
{
    public class EmptyOrDefaultIntAreLast : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            if (x == 0 && y != 0)
            {
                return 1;
            }
            else if (x != 0 && y == 0)
            {
                return -1;
            }
            else
            {
                return x.CompareTo(y);
            }
        }
    }
}