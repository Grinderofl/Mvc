// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ModelBindingWebSite;
using ModelBindingWebSite.Models;
using Newtonsoft.Json;
using Xunit;

namespace Microsoft.AspNet.Mvc.FunctionalTests
{
    public class ModelBindingFromRouteTest : IClassFixture<MvcTestFixture<ModelBindingWebSite.Startup>>
    {
        public ModelBindingFromRouteTest(MvcTestFixture<ModelBindingWebSite.Startup> fixture)
        {
            Client = fixture.Client;
        }

        public HttpClient Client { get; }

        [Fact]
        public async Task FromRoute_CustomModelPrefix_ForParameter()
        {
            // Arrange
            // [FromRoute(Name = "customPrefix")] is used to apply a prefix
            var url = "http://localhost/FromRouteAttribute_Company/CreateEmployee/somename";

            // Act
            var response = await Client.GetAsync(url);

            // Assert
            var body = await response.Content.ReadAsStringAsync();
            var employee = JsonConvert.DeserializeObject<Employee>(body);
            Assert.Equal("somename", employee.Name);
        }

        [Fact]
        public async Task FromRoute_CustomModelPrefix_ForProperty()
        {
            // Arrange
            // [FromRoute(Name = "EmployeeId")] is used to apply a prefix
            var url = "http://localhost/FromRouteAttribute_Company/CreateEmployee/somename/1234";

            // Act
            var response = await Client.GetAsync(url);

            // Assert
            var body = await response.Content.ReadAsStringAsync();
            var employee = JsonConvert.DeserializeObject<Employee>(body);
            Assert.Equal(1234, employee.TaxId);
        }


        [Fact]
        public async Task FromRoute_NonExistingValueAddsValidationErrors_OnProperty_UsingCustomModelPrefix()
        {
            // Arrange
            // [FromRoute(Name = "TestEmployees")] is used to apply a prefix
            var url = "http://localhost/FromRouteAttribute_Company/ValidateDepartment/contoso";
            var request = new HttpRequestMessage(HttpMethod.Post, url);

            // No values.
            var nameValueCollection = new List<KeyValuePair<string, string>>();
            request.Content = new FormUrlEncodedContent(nameValueCollection);

            // Act
            var response = await Client.SendAsync(request);

            // Assert
            var body = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Result>(body);
            Assert.Null(result.Value);
            var error = Assert.Single(result.ModelStateErrors);
            Assert.Equal("TestEmployees", error);
        }
    }
}