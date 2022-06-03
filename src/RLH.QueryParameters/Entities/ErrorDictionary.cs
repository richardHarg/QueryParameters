namespace RLH.QueryParameters.Entities

{
    public sealed class ErrorDictionary
    {
        private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

        public Dictionary<string, List<string>> Errors
        {
            get { return _errors; }
        }

        /// <summary>
        /// Create a new Validation error
        /// </summary>
        /// <param name="message">Message describing the error</param>
        /// <param name="key">Property name used for key value</param>
        public void AddError(string key, string message)
        {
            if (_errors.ContainsKey(key) == true)
            {
                _errors[key].Add(message);
            }
            else
            {
                _errors.Add(key, new List<string> { message });
            }
        }

    }
}
