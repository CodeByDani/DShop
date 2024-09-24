using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace BuildingBlocks;

public class EnumSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type.IsEnum)
        {
            var enumType = context.Type;
            var enumNames = Enum.GetNames(enumType);
            var enumValues = Enum.GetValues(enumType);

            schema.Enum.Clear();

            foreach (var enumValue in enumValues)
            {
                var fieldInfo = enumType.GetField(enumValue.ToString()!);
                var displayAttribute = fieldInfo!.GetCustomAttribute<DisplayAttribute>();

                var name = displayAttribute?.Name ?? enumValue.ToString();

                schema.Enum.Add(new OpenApiString(name));
            }
        }
    }
}