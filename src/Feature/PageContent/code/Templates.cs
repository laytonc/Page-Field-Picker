using Sitecore.Data;

namespace SierraBadger.Feature.PageContent
{
    public struct Templates
    {
        public struct PageFieldPickerParameters
        {
            public static readonly ID ID =  new ID("");

            public struct Field
            {
                public static readonly ID PageFieldName = new ID("");
                public static readonly ID Markup = new ID("");
            }
        }
    }
}