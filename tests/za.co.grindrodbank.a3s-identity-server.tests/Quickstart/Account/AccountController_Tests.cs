/**
 * *************************************************
 * Copyright (c) 2019, Grindrod Bank Limited
 * License MIT: https://opensource.org/licenses/MIT
 * **************************************************
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.WebEncoders.Testing;
using NSubstitute;
using Xunit;
using za.co.grindrodbank.a3s.Models;
using za.co.grindrodbank.a3s.Repositories;
using za.co.grindrodbank.a3s.Services;
using za.co.grindrodbank.a3s.tests.Fakes;
using za.co.grindrodbank.a3sidentityserver.Quickstart.UI;

namespace za.co.grindrodbank.a3sidentityserver.tests.Quickstart.Account
{
    public class AccountController_Tests
    {
        private readonly CustomUserManagerFake fakeUserManager;
        private readonly CustomSignInManagerFake<UserModel> fakeSignInManager;
        private readonly IIdentityServerInteractionService mockIdentityServerInteractionService;
        private readonly IClientStore mockClientStore;
        private readonly IAuthenticationSchemeProvider mockAuthenticationSchemeProvider;
        private readonly IEventService mockEventService;
        private readonly IUserRepository mockUserRepository;
        private readonly IConfiguration mockConfiguration;
        private readonly UrlTestEncoder urlTestEncoder;
        private readonly AuthorizationRequest authorizationRequest;
        private readonly Client client;
        private readonly UserModel userModel;

        private const string RETURN_URL = "/returnUrl";

        public AccountController_Tests()
        {
            var mockOptionsAccessor = Substitute.For<IOptions<IdentityOptions>>();
            var mockPasswordHasher = Substitute.For<IPasswordHasher<UserModel>>();
            var mockUserValidators = Substitute.For<IEnumerable<IUserValidator<UserModel>>>();
            var mockPasswordValidators = Substitute.For<IEnumerable<IPasswordValidator<UserModel>>>();
            var mockKeyNormalizer = Substitute.For<ILookupNormalizer>();
            var mockErrors = Substitute.For<IdentityErrorDescriber>();
            var mockServices = Substitute.For<IServiceProvider>();
            var mockUserLogger = Substitute.For<ILogger<UserManager<UserModel>>>();
            var fakeA3SContext = new A3SContextFake(new Microsoft.EntityFrameworkCore.DbContextOptions<A3SContext>());

            mockConfiguration = Substitute.For<IConfiguration>();
            var fakesCustomUserStore = new CustomUserStoreFake(fakeA3SContext, mockConfiguration);

            fakeUserManager = new CustomUserManagerFake(fakesCustomUserStore, mockOptionsAccessor, mockPasswordHasher, mockUserValidators, mockPasswordValidators, mockKeyNormalizer,
                mockErrors, mockServices, mockUserLogger);

            var mockContextAccessor = Substitute.For<IHttpContextAccessor>();
            var mocClaimsFactory = Substitute.For<IUserClaimsPrincipalFactory<UserModel>>();
            var mockSignInLogger = Substitute.For<ILogger<SignInManager<UserModel>>>();
            mockUserRepository = Substitute.For<IUserRepository>();
            var mockLdapAuthenticationModeRepository = Substitute.For<LdapAuthenticationModeRepository>(fakeA3SContext, mockConfiguration);
            var mockLdapConnectionService = Substitute.For<LdapConnectionService>(mockLdapAuthenticationModeRepository, mockUserRepository);
            mockAuthenticationSchemeProvider = Substitute.For<IAuthenticationSchemeProvider>();

            fakeSignInManager = new CustomSignInManagerFake<UserModel>(fakeUserManager, mockContextAccessor, mocClaimsFactory, mockOptionsAccessor, mockSignInLogger, fakeA3SContext,
                mockAuthenticationSchemeProvider, mockLdapAuthenticationModeRepository, mockLdapConnectionService);

            mockIdentityServerInteractionService = Substitute.For<IIdentityServerInteractionService>();
            mockClientStore = Substitute.For<IClientStore>();
            mockEventService = Substitute.For<IEventService>();
            urlTestEncoder = new UrlTestEncoder();

            // Prepare controller contexts
            authorizationRequest = new AuthorizationRequest()
            {
                IdP = "testIdp",
                ClientId = "clientId1",
                LoginHint = "LoginHint"
            };

            var mockAuthenticationHandler = Substitute.For<IAuthenticationHandler>();
            mockAuthenticationSchemeProvider.GetAllSchemesAsync()
                .Returns(new List<AuthenticationScheme>()
                {
                    new AuthenticationScheme("testName", AccountOptions.WindowsAuthenticationSchemeName, mockAuthenticationHandler.GetType())
                });

            client = new Client()
            {
                EnableLocalLogin = true
            };

            client.IdentityProviderRestrictions.Add(AccountOptions.WindowsAuthenticationSchemeName);
            mockClientStore.FindEnabledClientByIdAsync(Arg.Any<string>()).Returns(client);

            userModel = new UserModel()
            {
                UserName = "username",
                Id = Guid.NewGuid().ToString()
            };
        }

        [Fact]
        public async Task Login_Executed_ViewResultReturned()
        {
            using var accountController = new AccountController(fakeUserManager, fakeSignInManager, mockIdentityServerInteractionService, mockClientStore, mockAuthenticationSchemeProvider,
                mockEventService, urlTestEncoder, mockConfiguration, mockUserRepository);

            // Act
            var actionResult = await accountController.Login(RETURN_URL);

            // Assert
            var viewResult = actionResult as ViewResult;
            Assert.NotNull(viewResult);
        }

        [Fact]
        public async Task Login_ExecutedWithExternalIdentityProvider_RedirectToActionResultReturned()
        {
            using var accountController = new AccountController(fakeUserManager, fakeSignInManager, mockIdentityServerInteractionService, mockClientStore, mockAuthenticationSchemeProvider,
                mockEventService, urlTestEncoder, mockConfiguration, mockUserRepository);

            mockIdentityServerInteractionService.GetAuthorizationContextAsync(Arg.Any<string>()).Returns(authorizationRequest);

            // Act
            var actionResult = await accountController.Login(RETURN_URL);

            // Assert
            var viewResult = actionResult as RedirectToActionResult;
            Assert.NotNull(viewResult);
        }

        [Fact]
        public async Task Login_ExecutedWithInternalIdentityProvider_ViewResultReturned()
        {
            using var accountController = new AccountController(fakeUserManager, fakeSignInManager, mockIdentityServerInteractionService, mockClientStore, mockAuthenticationSchemeProvider,
                mockEventService, urlTestEncoder, mockConfiguration, mockUserRepository);

            authorizationRequest.IdP = null;
            mockIdentityServerInteractionService.GetAuthorizationContextAsync(Arg.Any<string>()).Returns(authorizationRequest);

            // Act
            var actionResult = await accountController.Login(RETURN_URL);

            // Assert
            var viewResult = actionResult as ViewResult;
            Assert.NotNull(viewResult);
        }

        [Fact]
        public async Task Login_GivenLoginInputModelAndLoginButton_ViewResultReturned()
        {
            using var accountController = new AccountController(fakeUserManager, fakeSignInManager, mockIdentityServerInteractionService, mockClientStore, mockAuthenticationSchemeProvider,
                mockEventService, urlTestEncoder, mockConfiguration, mockUserRepository);

            mockIdentityServerInteractionService.GetAuthorizationContextAsync(Arg.Any<string>()).Returns(authorizationRequest);

            var inputModel = new LoginInputModel()
            {
                Username = "username",
                Password = "password",
                RememberLogin = false,
                ReturnUrl = RETURN_URL
            };

            fakeSignInManager.SetSignInSuccessful(true);
            fakeUserManager.SetUserModel(userModel);

            var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
              .AddEnvironmentVariables();

            var config = builder.Build();

            config.GetSection("TwoFactorAuthentication")["OrganizationEnforced"] = "false";
            config.GetSection("TwoFactorAuthentication")["AuthenticatorEnabled"] = "false";

            mockConfiguration.GetSection("TwoFactorAuthentication").Returns(config.GetSection("TwoFactorAuthentication"));

            // Act
            var actionResult = await accountController.Login(inputModel, "login");

            // Assert
            var viewResult = actionResult as RedirectResult;
            Assert.NotNull(viewResult);
            Assert.True(viewResult.Url == inputModel.ReturnUrl, $"ReturnUrl must be '{inputModel.ReturnUrl}'.");
        }

        [Fact]
        public async Task Login_GivenLoginInputModelAndLoginButton2FACompulsary_RedirectToActionResultReturned()
        {
            using var accountController = new AccountController(fakeUserManager, fakeSignInManager, mockIdentityServerInteractionService, mockClientStore, mockAuthenticationSchemeProvider,
                mockEventService, urlTestEncoder, mockConfiguration, mockUserRepository);

            mockIdentityServerInteractionService.GetAuthorizationContextAsync(Arg.Any<string>()).Returns(authorizationRequest);

            var inputModel = new LoginInputModel()
            {
                Username = "username",
                Password = "password",
                RememberLogin = false,
                ReturnUrl = RETURN_URL
            };

            fakeSignInManager.SetSignInSuccessful(true);
            fakeUserManager.SetUserModel(userModel);

            var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
              .AddEnvironmentVariables();

            var config = builder.Build();

            config.GetSection("TwoFactorAuthentication")["OrganizationEnforced"] = "true";
            config.GetSection("TwoFactorAuthentication")["AuthenticatorEnabled"] = "false";

            mockConfiguration.GetSection("TwoFactorAuthentication").Returns(config.GetSection("TwoFactorAuthentication"));

            // Act
            var actionResult = await accountController.Login(inputModel, "login");

            // Assert
            var viewResult = actionResult as RedirectToActionResult;
            Assert.NotNull(viewResult);
            Assert.True(viewResult.ActionName == "Register2FA", "Action Name must be 'Register2FA'.");
        }

        [Fact]
        public async Task Login_GivenLoginInputModelAndLoginButton2FAAuthenticatorEnabled_RedirectToActionResultReturned()
        {
            using var accountController = new AccountController(fakeUserManager, fakeSignInManager, mockIdentityServerInteractionService, mockClientStore, mockAuthenticationSchemeProvider,
                mockEventService, urlTestEncoder, mockConfiguration, mockUserRepository);

            mockIdentityServerInteractionService.GetAuthorizationContextAsync(Arg.Any<string>()).Returns(authorizationRequest);

            var inputModel = new LoginInputModel()
            {
                Username = "username",
                Password = "password",
                RememberLogin = false,
                ReturnUrl = RETURN_URL
            };

            fakeSignInManager.SetSignInSuccessful(true);
            fakeUserManager.SetUserModel(userModel);

            var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
              .AddEnvironmentVariables();

            var config = builder.Build();

            config.GetSection("TwoFactorAuthentication")["OrganizationEnforced"] = "false";
            config.GetSection("TwoFactorAuthentication")["AuthenticatorEnabled"] = "true";

            mockConfiguration.GetSection("TwoFactorAuthentication").Returns(config.GetSection("TwoFactorAuthentication"));

            // Act
            var actionResult = await accountController.Login(inputModel, "login");

            // Assert
            var viewResult = actionResult as RedirectToActionResult;
            Assert.NotNull(viewResult);
            Assert.True(viewResult.ActionName == "LoginSuccessful", "Action Name must be 'LoginSuccessful'.");
        }

        [Fact]
        public async Task Login_GivenLoginInputModelAndLoginButtonAndIsPkceClient_ViewResultReturned()
        {
            using var accountController = new AccountController(fakeUserManager, fakeSignInManager, mockIdentityServerInteractionService, mockClientStore, mockAuthenticationSchemeProvider,
                mockEventService, urlTestEncoder, mockConfiguration, mockUserRepository);

            mockIdentityServerInteractionService.GetAuthorizationContextAsync(Arg.Any<string>()).Returns(authorizationRequest);

            var inputModel = new LoginInputModel()
            {
                Username = "username",
                Password = "password",
                RememberLogin = false,
                ReturnUrl = RETURN_URL
            };

            fakeSignInManager.SetSignInSuccessful(true);
            fakeUserManager.SetUserModel(userModel);

            var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
              .AddEnvironmentVariables();

            var config = builder.Build();

            config.GetSection("TwoFactorAuthentication")["OrganizationEnforced"] = "false";
            config.GetSection("TwoFactorAuthentication")["AuthenticatorEnabled"] = "false";

            mockConfiguration.GetSection("TwoFactorAuthentication").Returns(config.GetSection("TwoFactorAuthentication"));
            client.RequirePkce = true;

            // Act
            var actionResult = await accountController.Login(inputModel, "login");

            // Assert
            var viewResult = actionResult as ViewResult;
            Assert.NotNull(viewResult);

            var model = viewResult.Model as RedirectViewModel;
            Assert.NotNull(model);

            Assert.True(model.RedirectUrl == inputModel.ReturnUrl, $"Model redirect must be '{inputModel.ReturnUrl}'.");
        }

        [Fact]
        public async Task Login_GivenLoginInputModelAndLoginButtonLocalUrl_ViewResultReturned()
        {
            using var accountController = new AccountController(fakeUserManager, fakeSignInManager, mockIdentityServerInteractionService, mockClientStore, mockAuthenticationSchemeProvider,
                mockEventService, urlTestEncoder, mockConfiguration, mockUserRepository);

            var urlHelper = Substitute.For<IUrlHelper>();
            urlHelper.IsLocalUrl(Arg.Any<string>()).Returns(true);
            accountController.Url = urlHelper;

            var inputModel = new LoginInputModel()
            {
                Username = "username",
                Password = "password",
                RememberLogin = false,
                ReturnUrl = RETURN_URL
            };

            fakeSignInManager.SetSignInSuccessful(true);
            fakeUserManager.SetUserModel(userModel);

            var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
              .AddEnvironmentVariables();

            var config = builder.Build();

            config.GetSection("TwoFactorAuthentication")["OrganizationEnforced"] = "false";
            config.GetSection("TwoFactorAuthentication")["AuthenticatorEnabled"] = "false";

            mockConfiguration.GetSection("TwoFactorAuthentication").Returns(config.GetSection("TwoFactorAuthentication"));

            // Act
            var actionResult = await accountController.Login(inputModel, "login");

            // Assert
            var viewResult = actionResult as RedirectResult;
            Assert.NotNull(viewResult);

            Assert.True(viewResult.Url == inputModel.ReturnUrl, $"Model redirect must be '{inputModel.ReturnUrl}'.");
        }

        [Fact]
        public async Task Login_GivenLoginInputModelAndLoginButtonNonLocalUrl_ThrowsException()
        {
            using var accountController = new AccountController(fakeUserManager, fakeSignInManager, mockIdentityServerInteractionService, mockClientStore, mockAuthenticationSchemeProvider,
                mockEventService, urlTestEncoder, mockConfiguration, mockUserRepository);

            var urlHelper = Substitute.For<IUrlHelper>();
            urlHelper.IsLocalUrl(Arg.Any<string>()).Returns(false);
            accountController.Url = urlHelper;

            var inputModel = new LoginInputModel()
            {
                Username = "username",
                Password = "password",
                RememberLogin = false,
                ReturnUrl = RETURN_URL
            };

            fakeSignInManager.SetSignInSuccessful(true);
            fakeUserManager.SetUserModel(userModel);

            var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
              .AddEnvironmentVariables();

            var config = builder.Build();

            config.GetSection("TwoFactorAuthentication")["OrganizationEnforced"] = "false";
            config.GetSection("TwoFactorAuthentication")["AuthenticatorEnabled"] = "false";

            mockConfiguration.GetSection("TwoFactorAuthentication").Returns(config.GetSection("TwoFactorAuthentication"));

            // Act
            Exception caughtEx = null;
            try
            {
                var actionResult = await accountController.Login(inputModel, "login");
            }
            catch (Exception ex)
            {
                caughtEx = ex;
            }

            // Assert
            Assert.NotNull(caughtEx);
            Assert.True(caughtEx.Message.ToLower() == "invalid return url");
        }

        [Fact]
        public async Task Login_GivenLoginInputModelAndLoginButtonEmptyUrl_ViewResultReturned()
        {
            using var accountController = new AccountController(fakeUserManager, fakeSignInManager, mockIdentityServerInteractionService, mockClientStore, mockAuthenticationSchemeProvider,
                mockEventService, urlTestEncoder, mockConfiguration, mockUserRepository);

            var urlHelper = Substitute.For<IUrlHelper>();
            urlHelper.IsLocalUrl(Arg.Any<string>()).Returns(false);
            accountController.Url = urlHelper;

            var inputModel = new LoginInputModel()
            {
                Username = "username",
                Password = "password",
                RememberLogin = false,
                ReturnUrl = string.Empty
            };

            fakeSignInManager.SetSignInSuccessful(true);
            fakeUserManager.SetUserModel(userModel);

            var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
              .AddEnvironmentVariables();

            var config = builder.Build();

            config.GetSection("TwoFactorAuthentication")["OrganizationEnforced"] = "false";
            config.GetSection("TwoFactorAuthentication")["AuthenticatorEnabled"] = "false";

            mockConfiguration.GetSection("TwoFactorAuthentication").Returns(config.GetSection("TwoFactorAuthentication"));

            // Act
            var actionResult = await accountController.Login(inputModel, "login");

            // Assert
            var viewResult = actionResult as RedirectResult;
            Assert.NotNull(viewResult);

            Assert.True(viewResult.Url == "~/", $"Model redirect must be '~/'.");
        }

        [Fact]
        public async Task Login_GivenLockedOutResult_ViewResultReturned()
        {
            using var accountController = new AccountController(fakeUserManager, fakeSignInManager, mockIdentityServerInteractionService, mockClientStore, mockAuthenticationSchemeProvider,
                mockEventService, urlTestEncoder, mockConfiguration, mockUserRepository);

            var urlHelper = Substitute.For<IUrlHelper>();
            urlHelper.IsLocalUrl(Arg.Any<string>()).Returns(false);
            accountController.Url = urlHelper;

            var inputModel = new LoginInputModel()
            {
                Username = "username",
                Password = "password",
                RememberLogin = false,
                ReturnUrl = string.Empty
            };

            fakeSignInManager.SetLockedOutState(true);
            fakeUserManager.SetUserModel(userModel);

            var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
              .AddEnvironmentVariables();

            var config = builder.Build();

            config.GetSection("TwoFactorAuthentication")["OrganizationEnforced"] = "false";
            config.GetSection("TwoFactorAuthentication")["AuthenticatorEnabled"] = "false";

            mockConfiguration.GetSection("TwoFactorAuthentication").Returns(config.GetSection("TwoFactorAuthentication"));

            // Act
            var actionResult = await accountController.Login(inputModel, "login");

            // Assert
            var viewResult = actionResult as ViewResult;
            Assert.NotNull(viewResult);

            var model = viewResult.Model as LoginViewModel;
            Assert.NotNull(model);

            Assert.True(accountController.ModelState.ErrorCount > 0, $"Modelstate must have errors.");

            bool errorFound = false;
            foreach (var modelState in accountController.ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    if (error.ErrorMessage == AccountOptions.AccountLockedOutErrorMessage)
                        errorFound = true;
                }
            }

            Assert.True(errorFound, $"Account locked out should return error message '{AccountOptions.AccountLockedOutErrorMessage}'.");
        }

        [Fact]
        public async Task Login_GivenUnsuccesfulLogin_ViewResultReturned()
        {
            using var accountController = new AccountController(fakeUserManager, fakeSignInManager, mockIdentityServerInteractionService, mockClientStore, mockAuthenticationSchemeProvider,
                mockEventService, urlTestEncoder, mockConfiguration, mockUserRepository);

            var urlHelper = Substitute.For<IUrlHelper>();
            urlHelper.IsLocalUrl(Arg.Any<string>()).Returns(false);
            accountController.Url = urlHelper;

            var inputModel = new LoginInputModel()
            {
                Username = "username",
                Password = "password",
                RememberLogin = false,
                ReturnUrl = string.Empty
            };

            fakeSignInManager.SetSignInSuccessful(false);
            fakeUserManager.SetUserModel(userModel);

            var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
              .AddEnvironmentVariables();

            var config = builder.Build();

            config.GetSection("TwoFactorAuthentication")["OrganizationEnforced"] = "false";
            config.GetSection("TwoFactorAuthentication")["AuthenticatorEnabled"] = "false";

            mockConfiguration.GetSection("TwoFactorAuthentication").Returns(config.GetSection("TwoFactorAuthentication"));

            // Act
            var actionResult = await accountController.Login(inputModel, "login");

            // Assert
            var viewResult = actionResult as ViewResult;
            Assert.NotNull(viewResult);

            var model = viewResult.Model as LoginViewModel;
            Assert.NotNull(model);

            Assert.True(accountController.ModelState.ErrorCount > 0, $"Modelstate must have errors.");

            bool errorFound = false;
            foreach (var modelState in accountController.ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    if (error.ErrorMessage == AccountOptions.InvalidCredentialsErrorMessage)
                        errorFound = true;
                }
            }

            Assert.True(errorFound, $"Account locked out should return error message '{AccountOptions.InvalidCredentialsErrorMessage}'.");
        }
    }
}
