using Application.Account.ViewModels;
using Application.Company.Commands;
using Application.Company.Commands.ViewModel;
using Application.Company.Repository;
using Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Account
{
	public class AccountCommandService : IAccountCommandService
	{
		private const string V = "account";
		readonly UserManager<Core.Model.User> userManager;
		readonly SignInManager<Core.Model.User> signInManager;
		readonly AppSettings _appSettings;
		private readonly ICompanyRepository _companyRepository;



		public AccountCommandService(
			ICompanyRepository companyRepository,
		   UserManager<Core.Model.User> userManager,
		   SignInManager<Core.Model.User> signInManager,
		   IOptions<AppSettings> options)
		{
			_companyRepository = companyRepository;
			this.userManager = userManager;
			this.signInManager = signInManager;
			_appSettings = options.Value;
		}




		public async Task<string> GetToken(LoginModel loginModel)
		{
			var loginResult = await signInManager.PasswordSignInAsync(loginModel.Username, loginModel.Password, isPersistent: false, lockoutOnFailure: false);

			if (!loginResult.Succeeded)
			{
				throw new Exception("Failed To Login");
			}

			var user = await userManager.FindByNameAsync(loginModel.Username);

			return GetToken(user);


		}

		public async Task<string> RefreshToken(ClaimsPrincipal userClaims)
		{
			var user = await userManager.FindByNameAsync(
				userClaims.Identity.Name ??
				userClaims.Claims.Where(c => c.Properties.ContainsKey("unique_name")).Select(c => c.Value).FirstOrDefault()
				);
			return GetToken(user);

		}
		public async void Logout()
		{
			await signInManager.SignOutAsync();
		}
		public async Task<string> Register(RegisterModel registerModel)
		{
			if (await _companyRepository.IsNameDuplicate(registerModel.CompanyName)) throw new Exception($"'{registerModel.CompanyName}' already exists. Please choose a different name.");
			var company = new Core.Model.Company(registerModel.CompanyName);
			_companyRepository.Add(company);

			await _companyRepository.SaveChanges();

			var user = new Core.Model.User(company, registerModel.Email, registerModel.Username);

			var identityResult = await userManager.CreateAsync(user, registerModel.Password);
			if (identityResult.Succeeded)
			{
				await signInManager.SignInAsync(user, isPersistent: false);
				return GetToken(user);
			}
			else
			{
				_companyRepository.Remove(company);
				await _companyRepository.SaveChanges();
				throw new Exception(identityResult?.Errors?.First()?.Description);
			}
		}

		private string GetToken(Core.Model.User user)
		{
			var utcNow = DateTime.UtcNow;

			var claims = new Claim[]
			{
						new Claim(JwtRegisteredClaimNames.Sub, user.Id),
						new Claim("companyId", user.CompanyId),
						new Claim("company", user.Company.Name),
						new Claim(JwtRegisteredClaimNames.Email, user.Email),
						new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
						new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
						new Claim(JwtRegisteredClaimNames.Iat, utcNow.ToString())
			};

			var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Tokens.Key));
			var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
			var jwt = new JwtSecurityToken(
				signingCredentials: signingCredentials,
				claims: claims,
				notBefore: utcNow,
				expires: utcNow.AddSeconds(_appSettings.Tokens.Lifetime),
				audience: _appSettings.Tokens.Audience,
				issuer: _appSettings.Tokens.Issuer
				);

			return new JwtSecurityTokenHandler().WriteToken(jwt);

		}

	}

	public interface IAccountCommandService : ICommandService
	{
		Task<string> Register(RegisterModel registerModel);
		Task<string> GetToken(LoginModel loginModel);
		Task<string> RefreshToken(ClaimsPrincipal user);
		void Logout();
	}
}
