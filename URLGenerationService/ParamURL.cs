using System.Collections.Generic;

namespace URLGenerationService
{
    class ParamURL
    {
        public Dictionary<string, string> ParamDictionary;

        public ParamURL()
        {
            this.ParamDictionary = new Dictionary<string, string>();
        }

        public void ResponseGroup(string responseGroup)
        {
            this.AddOrReplace("ResponseGroup", responseGroup.ToString().Replace(" ", ""));
        }
        public void BrowseNode(string browseNode)
        {
            this.AddOrReplace("BrowseNode", browseNode);
        }
        public void Operation(string operation)
        {
            this.AddOrReplace("Operation", operation);
        }
        public void ItemPage(string itemPage)
        {
            this.AddOrReplace("ItemPage", itemPage);
        }
        public void SearchIndex(string searchIndex)
        {
            this.AddOrReplace("SearchIndex", searchIndex);
        }

        public void AssociateTag(string associateTag)
        {
            this.AddOrReplace("AssociateTag", associateTag);
        }

        protected void AddOrReplace(string param, object value)
        {
            if (this.ParamDictionary.ContainsKey(param))
            {
                this.ParamDictionary[param] = value.ToString();
            }
            else
            {
                this.ParamDictionary.Add(param, value.ToString());
            }
        }
    }
}
