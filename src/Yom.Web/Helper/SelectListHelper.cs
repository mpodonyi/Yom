using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SystemControlCenter.Helper
{
    public static class SelectListHelper
    {
        public static IEnumerable<SelectListItem> ToSelectListItems<T>(this IEnumerable<T> list, Func<T, object> key, Func<T, object> value, SelectListItem[] topItem = null)
        {
            if (topItem != null)
                foreach (var i in topItem)
                    yield return i;

            foreach (var i in list)
                yield return new SelectListItem()
                                                    {
                                                        Value = Convert.ToString(key(i)),
                                                        Text = Convert.ToString(value(i)),
                                                    };

        }

        //public static string GetSelectedValue(this IEnumerable<SelectListItem> list, object key)
        //{
        //    if (key == null)
        //        return null;

        //    return (from i in list
        //            where i.Value == (key == null ? "" : key.ToString())
        //            select i.Text).SingleOrDefault() ?? key.ToString();
        //}

        //public static IEnumerable<SelectListItem> GetListItemsFromEnum(Type type, object selectedValue, bool nullable = false)
        //{
        //    Type underlyingType = Nullable.GetUnderlyingType(type);

        //    if (!(type.IsEnum || (underlyingType != null && underlyingType.IsEnum)))
        //        throw new Exception("Datatype is not enum.");

        //    if (underlyingType != null)
        //    {
        //        nullable = true;
        //        type = underlyingType;
        //    }

        //    List<SelectListItem> list = new List<SelectListItem>();

        //    if (nullable)
        //    {
        //        list.Add(new SelectListItem());
        //    }


        //    Array itemValues = System.Enum.GetValues(type);
        //    string[] itemNames = System.Enum.GetNames(type);

        //    for (int i = 0; i <= itemNames.Length - 1; i++)
        //    {
        //        SelectListItem item = new SelectListItem();
        //        item.Text = itemNames[i];
        //        item.Value = itemNames[i];
        //        item.Selected = itemNames[i] == (selectedValue == null ? "" : selectedValue.ToString());

        //        list.Add(item);
        //    }

        //    return list;
        //}


        //public static IEnumerable<SelectListItem> GetListItemsFromSelectListItems(this IEnumerable<SelectListItem> items, bool nullable, object selectedValue)
        //{
        //    List<SelectListItem> listItems = new List<SelectListItem>(items);

        //    if (nullable)
        //        listItems.Insert(0, new SelectListItem());

        //    foreach (SelectListItem item in listItems)
        //    {
        //        item.Selected = item.Value == (selectedValue == null ? "" : selectedValue.ToString());
        //    }

        //    return listItems;
        //}

    }
}