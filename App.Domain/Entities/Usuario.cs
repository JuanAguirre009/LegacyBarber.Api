namespace LegacyBarber.App.Domain.Entities
{
    /// <summary>
    /// Represents an application user with assigned roles and refresh tokens.
    /// </summary>
    public sealed class Usuario
    {
        private readonly List<UsuarioRol> usuarioRoles = new();
        private readonly List<TokenRefresco> tokensRefresco = new();

        public long Id { get; set; }
        public long? BarberiaId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public string? Telefono { get; set; }
        public long? FotoId { get; set; }
        public bool Activo { get; set; } = true;
        public bool EmailVerificado { get; set; } = false;
        public DateTime? UltimoAcceso { get; set; }
        public DateTime CreadoEn { get; set; } = DateTime.UtcNow;
        public DateTime? ActualizadoEn { get; set; }

        public Barberia? Barberia { get; set; }
        public Archivo? Foto { get; set; }
        public Cliente? Cliente { get; set; }
        public Barbero? Barbero { get; set; }
        public ICollection<UsuarioRol> UsuarioRoles => usuarioRoles;
        public IReadOnlyCollection<string> Roles => usuarioRoles.Select(ur => ur.Rol.Nombre).ToList().AsReadOnly();
        public IReadOnlyCollection<TokenRefresco> TokensRefresco => tokensRefresco.AsReadOnly();

        public static Usuario Create(string email, string passwordHash, string nombreCompleto, string? telefono = null, long? barberiaId = null)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("The email is required.", nameof(email));
            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new ArgumentException("The password hash is required.", nameof(passwordHash));
            if (string.IsNullOrWhiteSpace(nombreCompleto))
                throw new ArgumentException("The full name is required.", nameof(nombreCompleto));

            return new Usuario
            {
                Email = email.Trim().ToLowerInvariant(),
                PasswordHash = passwordHash,
                NombreCompleto = nombreCompleto.Trim(),
                Telefono = telefono,
                BarberiaId = barberiaId
            };
        }

        public void Update(string nombreCompleto, string? telefono, bool activo)
        {
            if (string.IsNullOrWhiteSpace(nombreCompleto))
                throw new ArgumentException("The full name is required.", nameof(nombreCompleto));

            NombreCompleto = nombreCompleto.Trim();
            Telefono = telefono;
            Activo = activo;
            ActualizadoEn = DateTime.UtcNow;
        }

        public void ChangePassword(string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new ArgumentException("The password hash is required.", nameof(passwordHash));

            PasswordHash = passwordHash;
            ActualizadoEn = DateTime.UtcNow;
        }

        public void SetRoles(IEnumerable<Rol> newRoles)
        {
            ArgumentNullException.ThrowIfNull(newRoles);
            usuarioRoles.Clear();
            foreach (Rol role in newRoles.DistinctBy(r => r.Id))
                AddRole(role);
        }

        public void ReplaceRoles(IEnumerable<UsuarioRol> newRoles)
        {
            ArgumentNullException.ThrowIfNull(newRoles);
            usuarioRoles.Clear();
            foreach (UsuarioRol ur in newRoles)
            {
                ur.Usuario = this;
                usuarioRoles.Add(ur);
            }
        }

        public void AddRole(Rol role)
        {
            ArgumentNullException.ThrowIfNull(role);
            if (!usuarioRoles.Any(ur => ur.RolId == role.Id))
                usuarioRoles.Add(new UsuarioRol { Usuario = this, Rol = role, RolId = role.Id });
        }

        public bool RemoveRole(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
                return false;

            var entry = usuarioRoles.FirstOrDefault(ur => ur.Rol.Nombre == roleName.Trim());
            if (entry is null)
                return false;

            return usuarioRoles.Remove(entry);
        }

        public void AddRefreshToken(TokenRefresco tokenRefresco)
        {
            ArgumentNullException.ThrowIfNull(tokenRefresco);
            tokensRefresco.Add(tokenRefresco);
        }

        public TokenRefresco? GetActiveRefreshToken(string token)
        {
            return tokensRefresco.FirstOrDefault(rt => rt.Token == token && rt.IsActive);
        }
    }
}
