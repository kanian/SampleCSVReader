using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;
using System.Text;

namespace ReadCSV.Core
{
    public class CSVSchema<TModel> : ICSVSchema<TModel> where TModel:new()
    {
        public Dictionary<string, Type> Schema { get; }
        public CSVSchema()
        {
            var model = new TModel();
            Schema = new Dictionary<string,Type>();
            foreach(PropertyInfo prop in model.GetType().GetProperties())
            {
                Schema.Add(prop.Name, prop.PropertyType);
            }
        }

        //Throws ArgumentException and IndexOutOfRangeException
        public TModel Hydrate(Dictionary<string, string> csvRecord) 
        {
            var model = new TModel();

            //Get reference to the ExpandoObject as a IDictionary<string, object>
            //var value = (IDictionary<string, object>)model.Value;

            //set our model property "key" to the corresponding key of our record
            foreach (string propName in Schema.Keys)
            {
                try
                {
                    var property = typeof(TModel).GetProperty(propName);
                    var type = Schema[propName];
                    property.SetValue(model, Convert.ChangeType(csvRecord[propName], type));
                }
                catch (InvalidCastException ex)
                {
                    throw ex;
                }
                catch (ArgumentNullException ex)
                {
                    throw ex;
                }
                catch (ArgumentException ex)
                {
                    throw ex;
                }
                catch (IndexOutOfRangeException ex)
                {
                    throw ex;
                }
            }
            return model;
        }
    }
}
