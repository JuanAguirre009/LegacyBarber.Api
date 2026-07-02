namespace LegacyBarber.App.Core.Model.Identity
{
    /// <summary>
    /// Authentication response including tokens and the customer's barbershops.
    /// </summary>
    public class LoginResponseModel : TokenPairModel
    {
        /// <summary>
        /// The authenticated user.
        /// </summary>
        public UsuarioModel Usuario { get; set; } = new();

        /// <summary>
        /// Barbershops the customer is associated with.
        /// Empty for new customers or users with other roles.
        /// </summary>
        public ICollection<BarberiaResumenModel> Barberias { get; set; } = new List<BarberiaResumenModel>();
    }
}
