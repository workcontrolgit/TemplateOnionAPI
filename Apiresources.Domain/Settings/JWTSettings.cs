namespace $safeprojectname$.Settings
{
    /// <summary>
    /// Represents settings for JWT (JSON Web Token) authentication.
    /// </summary>
    public class JWTSettings
    {
        /// <summary>
        /// Gets or sets the key used to sign the token.
        /// </summary>
        public string Key { get; set; }
        
        /// <summary>
        /// Gets or sets the issuer of the token (i.e., the entity that created it).
        /// </summary>
        public string Issuer { get; set; }
        
        /// <summary>
        /// Gets or sets the audience for the token (i.e., the entities that are allowed to use it).
        /// </summary>
        public string Audience { get; set; }
        
        /// <summary>
        /// Gets or sets the duration of the token in minutes.
        /// </summary>
        public double DurationInMinutes { get; set; }
    }
}