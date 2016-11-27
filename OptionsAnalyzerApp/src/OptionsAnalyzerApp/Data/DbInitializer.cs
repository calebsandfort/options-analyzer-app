using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptionsAnalyzerApp.Data
{
    public class DbInitializer
    {
        public static void Initialize(OptionsAnalyzerContext context)
        {
            context.Database.EnsureCreated();

        }
    }
}
