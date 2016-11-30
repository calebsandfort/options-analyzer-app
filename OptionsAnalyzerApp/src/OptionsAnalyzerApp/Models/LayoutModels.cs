using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OptionsAnalyzerApp.Models
{
    public class OptionsLayout
    {
        public TradingAccount TradingAccount { get; set; }
        public List<Option> Puts { get; set; }
        public List<Option> Calls { get; set; }

        public OptionsLayout()
        {
            this.Puts = new List<Option>();
            this.Calls = new List<Option>();
        }
    }

    public class TableHeaderColumnModel
    {
        public String Header { get; set; }
        public String SortProperty { get; set; }
    }

    public class UpdateOptionsModel
    {
        [DataType(DataType.Date)]
        public DateTime Expiry { get; set; }

        [DataType(DataType.Currency)]
        public Decimal UnderlyingPrice { get; set; }
    }
}
