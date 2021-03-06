// Copyright © 2014-2016 Kriasoft, LLC. All rights reserved.
// This source code is licensed under the MIT license found in the
// LICENSE.txt file in the root directory of this source tree.

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Server.Models;

namespace Server.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger _logger;

        public AccountController(SignInManager<User> signInManager, ILoggerFactory loggerFactory)
        {
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<AccountController>();
        }

        [HttpGet("login/{provider}")]
        [AllowAnonymous]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        [HttpGet("auth")]
        [AllowAnonymous]
        public IActionResult ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            // TODO: Handle 3rd-party authentication callback
            return Content("Account Controller => Login Callback", "text/plain");
        }
    }
}
