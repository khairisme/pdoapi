using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HR.PDO.Shared.Interfaces;

namespace HR.Application.Services.PDO
{
    public class ReflectionObjectMapper : IObjectMapper
    {
        public TTarget Map<TTarget>(object source) where TTarget : new()
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            var target = new TTarget();
            var sourceProps = source.GetType().GetProperties();
            var targetProps = typeof(TTarget).GetProperties();

            foreach (var sProp in sourceProps)
            {
                if (sProp.Name == "StatusAktif")
                    continue;

                var tProp = targetProps.FirstOrDefault(p =>
                    p.Name == sProp.Name &&
                    p.PropertyType == sProp.PropertyType);

                if (tProp != null && tProp.CanWrite)
                {
                    if (sProp.GetValue(source) == null)
                    {
                        continue;
                    }
                    var value = sProp.GetValue(source);
                    tProp.SetValue(target, value);
                }
            }

            return target;
        }
    }
}

