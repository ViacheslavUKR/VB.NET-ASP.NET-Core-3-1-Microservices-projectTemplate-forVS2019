﻿Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Threading.Tasks
Imports Microsoft.AspNetCore.Builder
Imports Microsoft.AspNetCore.Hosting
Imports Microsoft.AspNetCore.Mvc
Imports Microsoft.EntityFrameworkCore
Imports Microsoft.Extensions.Configuration
Imports Microsoft.Extensions.DependencyInjection
Imports Microsoft.Extensions.Logging
Imports Microsoft.Extensions.Options
Imports Microsoft.AspNetCore.Authentication
Imports Microsoft.Extensions.Hosting

Public Class Startup
    Public Sub New(ByVal configuration As IConfiguration)
        _Configuration = configuration
    End Sub

    Public ReadOnly Property _Configuration As IConfiguration

    Public Sub ConfigureServices(ByVal services As IServiceCollection)
        services.
            AddAuthentication("Bearer").
        AddIdentityServerAuthentication(Sub(options)
                                            options.Authority = "http://localhost:5000"
                                            options.RequireHttpsMetadata = False
                                            options.ApiName = "bankOfDotNetApi"
                                        End Sub)

        services.
            AddDbContext(Of BankContext)(Sub(opts) opts.UseInMemoryDatabase("BankingDb"))

        services.
            AddMvc(Sub(opt) opt.EnableEndpointRouting = False).
            SetCompatibilityVersion(CompatibilityVersion.Latest)
    End Sub


    Public Sub Configure(ByVal app As IApplicationBuilder, ByVal env As IHostEnvironment)
        If env.IsDevelopment() Then
            app.UseDeveloperExceptionPage()
        End If

        app.UseAuthentication()
        app.UseMvc()
    End Sub
End Class

