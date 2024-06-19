namespace SiGaHRMS.Data.Validations
{
    public class ValidationResult
    {
        public bool IsValid => Errors.Count < 1;

        public List<ErrorMessage> Errors { get; set; } = new List<ErrorMessage>();

        public void AddErrorMesageCode(string code, IDictionary<string, string> errors, params string[] args)
        {
            string errorMessage = string.Empty;

            if (errors.ContainsKey(code))
            {
                errorMessage = string.Format(errors[code], args);
            }

            Errors.Add(
                new ErrorMessage
                {
                    Code = code,
                    Message = errorMessage,
                });
        }
    }
}
