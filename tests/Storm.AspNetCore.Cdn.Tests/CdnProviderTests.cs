// <copyright file="CdnProviderTests.cs" company="Storm ID">
// Copyright (c) Storm ID. All rights reserved.
// </copyright>

namespace Storm.AspNetCore.Cdn.Tests
{
    using FluentAssertions;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class CdnProviderTests : IClassFixture<CdnClassFixture>
    {
        private const string JsonContent = "{ \"Cdn\" : " +
                                           "{ " +
                                           "\"DefaultProvider\" : \"AzureCdn\", " +
                                           "\"Providers\" : " +
                                           "{ " +
                                           "\"AzureCdn\" : { \"Host\" : \"http://mycdn.azureedge.net\" }, " +
                                           "\"Local\" : { \"Host\" : \"http://localhost:3000\" } " +
                                           "} " +
                                           "}" +
                                           "}";

        private readonly CdnClassFixture fixture;

        public CdnProviderTests(CdnClassFixture fixture)
        {
            this.fixture = fixture;
        }

        [Theory]
        [InlineData("file.jpg", "http://mycdn.azureedge.net/file.jpg")]
        [InlineData("http://mysite.local/file.jpg", "http://mysite.local/file.jpg")]
        [InlineData("file.jpg", "http://mycdn.azureedge.net/file.jpg", "AzureCdn")]
        [InlineData("file.jpg", "http://localhost:3000/file.jpg", "Local")]
        [InlineData("~/file.jpg", "~/file.jpg")]
        [InlineData("/file.jpg", "http://mycdn.azureedge.net/file.jpg")]
        [InlineData("", "")]
        [InlineData("/", "http://mycdn.azureedge.net/")]
        public void ShouldCreateCdnUriForGivenResourceUri(string resourceUri, string expectedCdnUri, string providerName = null)
        {
            var serviceProvider = this.fixture.GetServiceProvider(JsonContent);

            var cdnProvider = serviceProvider.GetService<ICdnUriProvider>();

            var cdnUri = string.IsNullOrWhiteSpace(providerName) ? cdnProvider.GetUri(resourceUri) : cdnProvider.GetUri(resourceUri, providerName);

            cdnUri.ToString().Should().Be(expectedCdnUri);
        }
    }
}