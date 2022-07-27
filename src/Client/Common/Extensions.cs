namespace MasterCraft.Client.Common
{
    public static class Extensions
    {
        public static string FormatPrice(this decimal price)
        {
            return string.Format("{0:C}", price);
        }
    }
}
