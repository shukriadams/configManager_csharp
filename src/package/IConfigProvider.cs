namespace ConfigManager
{
    public interface IConfigProvider
    {

        void Update();

        string IsValid();

        /// <summary>
        /// Call to ensure basic configuration of provider is valid. Returns a null string if config valid, else
        /// string contains description of error. If not not null, config providing is not possible.
        /// </summary>
        string ValidateSetup();

        /// <summary>
        /// Tries to validate provider endpoint is reachable within given timeout. Returns empty string if reachable
        /// or description of error if not.
        /// </summary>
        /// <param name="timeout"></param>
        /// <returns></returns>
        string ValidateReachable(int timeout);
    }
}