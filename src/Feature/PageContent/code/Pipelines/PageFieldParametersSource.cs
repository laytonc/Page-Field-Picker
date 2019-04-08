using System.Linq;
using Sitecore.Pipelines.GetLookupSourceItems;
using Sitecore.Shell.Applications.ContentEditor;
using Sitecore.Text;
using Sitecore.Web;
using System;
using Sitecore.Data;
using Sitecore.Data.Fields;

namespace SierraBadger.Feature.PageContent.Pipelines
{
    public class PageFieldParametersSource
    {
        public void Process(GetLookupSourceItemsArgs args)
        {
            //should we care?
            if (HasLookup(args.Source))
            {
                var url = WebUtil.GetQueryString();

                if (string.IsNullOrWhiteSpace(url)) return;

                var fieldEditorOptions = FieldEditorOptions.Parse(new UrlString(url));
                var parameters = fieldEditorOptions.Parameters;

                var currentItemId = parameters[Constants.FieldEditor.RenderingParameters.ContentItem];

                if (string.IsNullOrEmpty(currentItemId)) return;

                // get the context item.
                var contentItemUri = new ItemUri(currentItemId);

                var contextItem = Database.GetItem(contentItemUri);


                // get all item fields, filter out standard fields and order. maybe group by section in future?
                contextItem.Fields.ReadAll();
                var itemFields = contextItem.Fields.Where(f => !f.Name.StartsWith("__")).OrderBy(f => f.DisplayName);

                foreach (Field field in itemFields)
                {
                    // check if we support the field type
                    if (IsSupportedField(field))
                    {
                        args.Result.Add(field.InnerItem);
                    }
                }


                // if no other queries to be served, no need to continue pipeline
                args.AbortPipeline();

            }
        }
        protected bool HasLookup(string dataSource)
        {
            // source needs to start with pagefields for this pipeline to execute.
            return dataSource.IndexOf(Constants.PageFieldPicker.SourcePrefix,
                StringComparison.OrdinalIgnoreCase) > -1;
        }

        protected bool IsSupportedField(Field field)
        {
            return true;
        }
    }
}