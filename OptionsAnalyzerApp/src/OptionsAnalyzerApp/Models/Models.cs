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

    #region Option
    public class Option : EntityBase
    {
        [DataType(DataType.Currency)]
        public Decimal Strike { get; set; }

        [DataType(DataType.Currency)]
        public Decimal Bid { get; set; }

        [DataType(DataType.Currency)]
        public Decimal Ask { get; set; }

        public int Contracts { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public Decimal Delta { get; set; }
    }
    #endregion
}
