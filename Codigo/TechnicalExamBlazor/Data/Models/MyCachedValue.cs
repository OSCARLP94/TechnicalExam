namespace TechnicalExamBlazor.Data.Models
{
    public class MyCachedValue<T>
    {
        /// <summary>
        /// Valor a cachear
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// Tiempo de expiracion (UTC)
        /// </summary>
        public DateTime Expiry { get; set; }
    }
}
