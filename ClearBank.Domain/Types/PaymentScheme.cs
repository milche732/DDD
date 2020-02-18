namespace ClearBank.DeveloperTest.Types
{
    public class PaymentScheme
    {
        public int SchemaCode { get; }
        public string Description { get; }

        public static readonly PaymentScheme FasterSchema = new PaymentScheme(1, "FasterSchema");
        public static readonly PaymentScheme BacsSchema = new PaymentScheme(2, "BacsSchema");
        public static readonly PaymentScheme ChapsSchema = new PaymentScheme(4, "ChapsSchema");
        public PaymentScheme(int schemaCode, string description)
        {
            if (string.IsNullOrEmpty(description))
            {
                throw new System.ArgumentException("Description cannot be null.", nameof(description));
            }

            SchemaCode = schemaCode;
            Description = description;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is PaymentScheme)) return false;
            return (((PaymentScheme)obj).SchemaCode == this.SchemaCode);
        }

        public bool Has(PaymentScheme payment)
        {
            return (payment.SchemaCode & this.SchemaCode) > 0;
        }

        public override int GetHashCode()
        {
            return 1922922499 + SchemaCode.GetHashCode();
        }
    }
}
