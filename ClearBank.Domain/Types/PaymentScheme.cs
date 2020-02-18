namespace ClearBank.DeveloperTest.Types
{
    public class PaymentScheme
    {
        public int Code { get; }
        public string Description { get; }

        public static readonly PaymentScheme FasterSchema = new PaymentScheme(1, "FasterSchema");
        public static readonly PaymentScheme BacsSchema = new PaymentScheme(2, "BacsSchema");
        public static readonly PaymentScheme ChapsSchema = new PaymentScheme(4, "ChapsSchema");
        private PaymentScheme(int schemaCode, string description)
        {
            if (string.IsNullOrEmpty(description))
            {
                throw new System.ArgumentException("Description cannot be null.", nameof(description));
            }

            Code = schemaCode;
            Description = description;
        }
        public static PaymentScheme Create(int code, string desc)
        {
            return new PaymentScheme(code, desc);
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is PaymentScheme)) return false;
            return (((PaymentScheme)obj).Code == this.Code);
        }

        public bool Has(PaymentScheme payment)
        {
            return (payment.Code & this.Code) > 0;
        }

        public override int GetHashCode()
        {
            return 1922922499 + Code.GetHashCode();
        }
    }
}
