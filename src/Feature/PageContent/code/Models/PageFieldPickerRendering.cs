using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;

namespace SierraBadger.Feature.PageContent.Models
{
    public class PageFieldPickerRendering : RenderingModel
    {
        public string Markup { get; set; }

        public string PageField { get; set; }

        public bool Initialized => !string.IsNullOrWhiteSpace(Markup) && !string.IsNullOrWhiteSpace(PageField);

        public override void Initialize(Rendering rendering)
        {
            base.Initialize(rendering);

            Markup = GetUserFriendlyValue(Rendering.Parameters, "Markup", "Value");


            var paramPath = Rendering.Parameters["Page Field Name"];

            if (!string.IsNullOrEmpty(paramPath))
            {
                RenderingContext currentOrNull = RenderingContext.CurrentOrNull;

                Item paramItem = currentOrNull?.ContextItem.Database.GetItem(ID.Parse(paramPath));

                if (paramItem != null)
                {
                    PageField = paramItem.Name;
                }
            }
        }


        public static string GetUserFriendlyValue(RenderingParameters renderingParameters, string key, string fieldName)
        {
            var paramValue = string.Empty;

            var paramPath = renderingParameters[key];

            if (!string.IsNullOrEmpty(paramPath))
            {
                RenderingContext currentOrNull = RenderingContext.CurrentOrNull;

                Item paramItem = currentOrNull?.ContextItem.Database.GetItem(ID.Parse(paramPath));

                if (paramItem != null)
                {
                    paramValue = paramItem[fieldName];
                }
            }

            return paramValue;
        }
    }
}