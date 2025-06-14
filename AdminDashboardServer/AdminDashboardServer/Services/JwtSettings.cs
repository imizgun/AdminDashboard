namespace AdminDashboardServer;

public static class JwtSettings {
	public static string SecretKey { get; set; } = "YourSuperSecretKeyShouldBeLongEnough123!";
	public static readonly int AccessTokenLifeTimeMinutes = 5;
	public static readonly int RefreshTokenLifeTimeMinutes = 30;
	public static string Issuer { get; set; } = "Kestrel";
	public static string Audience { get; set; } = "DashboardAdminPanel";
}