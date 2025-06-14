using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace AdminDashboardServer;

public static class ServiceCollectionExtension {
	public static IServiceCollection AddSwaggerGetWithAuth(this IServiceCollection services) {
		services.AddSwaggerGen(o => {
			var secScheme = new OpenApiSecurityScheme {
				Name = "Authorization",
				Description = "JWT Authorization header using the Bearer scheme",
				In = ParameterLocation.Header,
				Type = SecuritySchemeType.Http,
				Scheme = JwtBearerDefaults.AuthenticationScheme,
				BearerFormat = "JWT",
			};
			o.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, secScheme);

			var secReq = new OpenApiSecurityRequirement {
				{
					new OpenApiSecurityScheme {
						Reference = new OpenApiReference {
							Type = ReferenceType.SecurityScheme,
							Id = JwtBearerDefaults.AuthenticationScheme
						}
					},
					[]
				}
			};
			o.AddSecurityRequirement(secReq);
		});
		return services;
	}
}