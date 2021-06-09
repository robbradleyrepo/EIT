namespace LionTrust.Feature.Article.Validation
{
    using System.Linq;
    using System.Runtime.Serialization;
    using Sitecore.Data.Fields;
    using Sitecore.Data.Validators;

    public class LimitItemsValidator : StandardValidator
    {
        public override string Name
        {
            get { return "Limit Items Validator"; }
        }

        public LimitItemsValidator() { }

        public LimitItemsValidator(SerializationInfo info, StreamingContext context) : base(info, context) { }

        protected override ValidatorResult Evaluate()
        {
            var result = ValidatorResult.CriticalError;

            Field field = this.GetField();

            if (field == null || !field.HasValue)
            {
                result = ValidatorResult.Valid;
            }

            string value = this.ControlValidationValue;
            int intOutParameter;

            var max = int.TryParse(Parameters["max"], out intOutParameter) ? intOutParameter : int.MaxValue;
            var min = int.TryParse(Parameters["min"], out intOutParameter) ? intOutParameter : 0;
            
            var Ids = value.Split('|');
           
            if(Ids.Count() >= min && Ids.Count() <= max)
            {
                result = ValidatorResult.Valid;
            }

            return result;
        }

        protected override ValidatorResult GetMaxValidatorResult()
        {
            return this.GetFailedResult(ValidatorResult.Error);
        }
    }
}