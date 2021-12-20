// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace CFS.IdentityServer4.Practice
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("api.WebApp", "WebApp API")
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientName = "Angular-Client",
                    ClientId = "WebApp",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,
                    RequireConsent = false,
                    RequirePkce = true,
                    AllowAccessTokensViaBrowser = true,
                    AccessTokenLifetime = 600,
                    AllowedCorsOrigins={"http://localhost:4200" },

                    // đăng nhập thành công thì redirect lại theo đường dẫn này
                    RedirectUris = { "http://localhost:4200/shop" },

                    // khi logout nó chạy cổng này và sử lý logout bên kia
                    PostLogoutRedirectUris = { "http://localhost:4200" },

                    // ở client này cho phép chuy cập đến những cái này
                    AllowedScopes = new List<string>
                    {
                        // ở đây chúng ta cho chuy cập cả thông tin user lần api
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api.WebApp"
                    }
                },
                new Client
                {
                    ClientId = "swagger",
                    ClientName = "Swagger Client",

                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,

                    RedirectUris =           { "https://localhost:5000/swagger/oauth2-redirect.html" }, // chuyển hướng
                    PostLogoutRedirectUris = { "https://localhost:5000/swagger/oauth2-redirect.html" },// chuyển hướng đăng xuất
                    AllowedCorsOrigins =     { "https://localhost:5000" }, // cho phép nguồn gốc cores

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api.WebApp"
                    }
                },
            };
    }
}