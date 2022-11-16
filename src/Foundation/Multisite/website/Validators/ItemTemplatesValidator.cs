namespace LionTrust.Foundation.Multisite.Validators
{
    using System.Linq;
    using System.Runtime.Serialization;
    using Sitecore.Data;
    using Sitecore.Data.Fields;
    using Sitecore.Data.Validators;

    public class ItemTemplatesValidator : StandardValidator
    {
        private const string TemplateParameter = "Templates";

        public override string Name
        {
            get { return "Items template Validator"; }
        }

        public ItemTemplatesValidator() { }

        public ItemTemplatesValidator(SerializationInfo info, StreamingContext context) : base(info, context) { }

        protected override ValidatorResult Evaluate()
        {
            var result = ValidatorResult.CriticalError;

            var field = GetField();

            if (field == null || !field.HasValue)
            {
                result = ValidatorResult.Valid;
            }

            var value = ControlValidationValue;
            var Ids = value.Split('|');

            var templateIds = Parameters[TemplateParameter].Split('|')?.Select(x => new ID(x));

            if(Ids.All(x =>
                {
                    var item = field.Database.GetItem(x);
                    return item != null && templateIds.Any(t => item.DescendsFrom(t));
                }))
            {
                result = ValidatorResult.Valid;
            }

            return result;
        }

        protected override ValidatorResult GetMaxValidatorResult()
        {
            return GetFailedResult(ValidatorResult.FatalError);
        }
    }
}