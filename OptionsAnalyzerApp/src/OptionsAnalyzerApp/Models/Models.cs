using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OptionsAnalyzerApp.Models
{
    #region EntityBase
    public class EntityBase
    {
        public int ID { get; set; }

        public override bool Equals(System.Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            EntityBase x = obj as EntityBase;
            if ((System.Object)x == null)
            {
                return false;
            }

            // Return true if the fields match:
            return this.ID == x.ID;
        }

        public bool Equals(EntityBase x)
        {
            // If parameter is null return false:
            if ((object)x == null)
            {
                return false;
            }

            // Return true if the fields match:
            return this.ID == x.ID;
        }

        public override int GetHashCode()
        {
            return this.ID.GetHashCode();
        }
    }
    #endregion

    #region TradingAccount
    public class TradingAccount : EntityBase
    {
        [StringLength(50)]
        public String Name { get; set; }

        [DataType(DataType.Currency)]
        public Decimal Balance { get; set; }

        [Display(Name = "Unit Size")]
        [DisplayFormat(DataFormatString = "{0:P0}", ApplyFormatInEditMode = true)]
        public Decimal UnitSize { get; set; }

        [Display(Name = "Price Change Targets")]
        public ICollection<TradingAccountPriceChangeTarget> PriceChangeTargets { get; set; }
    }
    #endregion

    #region TradingAccountPriceChangeTarget
    public class TradingAccountPriceChangeTarget : EntityBase
    {
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public Decimal PriceChangeTarget { get; set; }

        public int TradingAccountID { get; set; }
        public TradingAccount TradingAccount { get; set; }
    }
    #endregion

    #region Option
    public class Option : EntityBase
    {
        public OptionTypes OptionType { get; set; }

        [DataType(DataType.Currency)]
        public Decimal Strike { get; set; }

        [DataType(DataType.Currency)]
        public Decimal Bid { get; set; }

        [DataType(DataType.Currency)]
        public Decimal Ask { get; set; }

        public int Quantity { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public Decimal Delta { get; set; }

        public ICollection<Option> Options { get; set; }
    }
    #endregion

    #region OptionPriceChangeTarget
    public class OptionPriceChangeTarget : EntityBase
    {
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public Decimal PriceChangeTarget { get; set; }

        [DataType(DataType.Currency)]
        public Decimal PL { get; set; }

        [DisplayFormat(DataFormatString = "{0:P0}", ApplyFormatInEditMode = true)]
        public Decimal PLPercent { get; set; }

        public int OptionID { get; set; }
        public Option Option { get; set; }
    }
    #endregion

    #region CsvUpload
    public class CsvUpload
    {
        [DataType(DataType.MultilineText)]
        public String Contents { get; set; }
    }
    #endregion
}
