using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OptionsAnalyzerApp.Framework;

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

        [DataType(DataType.Currency)]
        public Decimal RoundTripCommission { get; set; }

        [Display(Name = "Unit Size")]
        [DisplayFormat(DataFormatString = "{0:P0}")]
        public Decimal UnitSize { get; set; }

        [DataType(DataType.Currency)]
        public Decimal ExpectedPriceChange { get; set; }

        [Display(Name = "Risk-Free Interest Rate")]
        [DisplayFormat(DataFormatString = "{0:P4}")]
        public Decimal RiskFreeInterestRate { get; set; }

        [NotMapped]
        public Decimal TradeSize
        {
            get
            {
                return this.Balance * this.UnitSize;
            }
        }
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

        [Display(Name = "IV")]
        [DisplayFormat(DataFormatString = "{0:P2}", ApplyFormatInEditMode = true)]
        public Decimal ImpliedVolatility { get; set; }

        [DataType(DataType.Date)]
        public DateTime Expiry { get; set; }

        [DataType(DataType.Currency)]
        public Decimal UnderlyingPrice { get; set; }

        [Display(Name = "Delta P/L")]
        [DataType(DataType.Currency)]
        public Decimal DeltaPL { get; set; }

        [Display(Name = "Delta P/L %")]
        [DisplayFormat(DataFormatString = "{0:P0}", ApplyFormatInEditMode = true)]
        public Decimal DeltaPLPercent { get; set; }

        [Display(Name = "BS")]
        [DataType(DataType.Currency)]
        public Decimal BlackScholesPrice { get; set; }

        [Display(Name = "BS Tgt")]
        [DataType(DataType.Currency)]
        public Decimal BlackScholesPriceTarget { get; set; }

        [Display(Name = "BS Chg")]
        [DataType(DataType.Currency)]
        public Decimal BlackScholesPriceChange { get; set; }

        [Display(Name = "BS P/L")]
        [DataType(DataType.Currency)]
        public Decimal BlackScholesPL { get; set; }

        [Display(Name = "BS P/L %")]
        [DisplayFormat(DataFormatString = "{0:P0}", ApplyFormatInEditMode = true)]
        public Decimal BlackScholesPLPercent { get; set; }

        [NotMapped]
        [Display(Name = "Expiry")]
        public int ExpiryDays
        {
            get
            {
                return (this.Expiry - DateTime.Today).Days;
            }
        }

        [NotMapped]
        public Decimal ContractBid
        {
            get
            {
                return this.Bid * 100m;
            }
        }

        [NotMapped]
        public Decimal ContractAsk
        {
            get
            {
                return this.Ask * 100m;
            }
        }

        [NotMapped]
        [DataType(DataType.Currency)]
        public Decimal Spread
        {
            get
            {
                return this.Ask - this.Bid;
            }
        }

        public void FillCalculatedFields(TradingAccount tradingAccount)
        {
            this.Quantity = (int)Math.Ceiling(tradingAccount.TradeSize / this.ContractAsk);

            #region Delta
            this.DeltaPL = ((this.Bid + (tradingAccount.ExpectedPriceChange * this.Delta)) * this.Quantity * 100m) - (19.98m + (this.Quantity * tradingAccount.RoundTripCommission)) - (this.Quantity * this.ContractAsk);
            this.DeltaPLPercent = this.DeltaPL / (this.Quantity * this.ContractAsk);
            #endregion

            #region Black Scholes
            this.BlackScholesPrice = this.CalculateBlackScholesPrice(tradingAccount, this.UnderlyingPrice);
            this.BlackScholesPriceTarget = this.CalculateBlackScholesPrice(tradingAccount, this.UnderlyingPrice + (this.OptionType == OptionTypes.Call ? tradingAccount.ExpectedPriceChange : -tradingAccount.ExpectedPriceChange));
            this.BlackScholesPriceChange = this.BlackScholesPriceTarget - this.BlackScholesPrice;

            this.BlackScholesPL = ((this.Bid + this.BlackScholesPriceChange) * this.Quantity * 100m) - (19.98m + (this.Quantity * tradingAccount.RoundTripCommission)) - (this.Quantity * this.ContractAsk);
            this.BlackScholesPLPercent = this.BlackScholesPL / (this.Quantity * this.ContractAsk);
            #endregion
        }

        public Decimal CalculateBlackScholesPrice(TradingAccount tradingAccount, Decimal underlyingPrice)
        {
            Double S0 = (Double)underlyingPrice;
            Double X = (Double)this.Strike;
            Double sigma = (Double)this.ImpliedVolatility;
            Double r = (Double)tradingAccount.RiskFreeInterestRate;
            Double t = ((Double)this.ExpiryDays) / 365.0;

            Double d1 = (Math.Log(S0 / X) + (t * (r + (Math.Pow(sigma, 2) / 2)))) / (sigma * Math.Sqrt(t));
            Double d2 = d1 - (sigma * Math.Sqrt(t));

            if (this.OptionType == OptionTypes.Call)
            {
                return (Decimal)((S0 * StaticStuff.N(d1)) - (X * Math.Pow(Math.E, -(r * t)) * StaticStuff.N(d2)));
            }
            else
            {
                return (Decimal)((X * Math.Pow(Math.E, -(r * t)) * StaticStuff.N(-d2)) - (S0 * StaticStuff.N(-d1)));
            }
        }
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
