﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using CognitiveServices.Tests.Helpers;
using Microsoft.Azure.Management.CognitiveServices;
using Microsoft.Azure.Management.CognitiveServices.Models;
using Microsoft.Azure.Management.Resources;
using Microsoft.Rest.ClientRuntime.Azure.TestFramework;
using ResourceGroups.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Xunit;

namespace CognitiveServices.Tests
{
    public class CognitiveServicesAccountTests
    {
        private const string c_resourceNamespace = "Microsoft.CognitiveServices";
        private const string c_resourceType = "accounts";

        [Fact]
        public void CognitiveServicesAccountCreateTest()
        {
            var handler = new RecordedDelegatingHandler { StatusCodeToReturn = HttpStatusCode.OK };

            using (MockContext context = MockContext.Start(this.GetType().FullName))
            {
                var resourcesClient = CognitiveServicesManagementTestUtilities.GetResourceManagementClient(context, handler);
                var cognitiveServicesMgmtClient = CognitiveServicesManagementTestUtilities.GetCognitiveServicesManagementClient(context, handler);

                // Create resource group
                var rgname = CognitiveServicesManagementTestUtilities.CreateResourceGroup(resourcesClient);

                // prepare account properties
                string accountName = TestUtilities.GenerateName("csa");
                var parameters = CognitiveServicesManagementTestUtilities.GetDefaultCognitiveServicesAccountParameters();

                // Create cognitive services account
                var account = cognitiveServicesMgmtClient.CognitiveServicesAccounts.Create(rgname, accountName, parameters);
                CognitiveServicesManagementTestUtilities.VerifyAccountProperties(account, true);

                // Create same account again, make sure it doesn't fail
                account = cognitiveServicesMgmtClient.CognitiveServicesAccounts.Create(rgname, accountName, parameters);
                CognitiveServicesManagementTestUtilities.VerifyAccountProperties(account, true);

                // Create account with only required params, for each sku (but free, since we can't have two free accounts in the same subscription)
                CognitiveServicesManagementTestUtilities.CreateAndValidateAccountWithOnlyRequiredParameters(cognitiveServicesMgmtClient, rgname, SkuName.S1);
                CognitiveServicesManagementTestUtilities.CreateAndValidateAccountWithOnlyRequiredParameters(cognitiveServicesMgmtClient, rgname, SkuName.S2);
                CognitiveServicesManagementTestUtilities.CreateAndValidateAccountWithOnlyRequiredParameters(cognitiveServicesMgmtClient, rgname, SkuName.S3);
                CognitiveServicesManagementTestUtilities.CreateAndValidateAccountWithOnlyRequiredParameters(cognitiveServicesMgmtClient, rgname, SkuName.S4);
            }
        }

        [Fact]
        public void CognitiveServicesAccountCreateAllApisTest()
        {
            var handler = new RecordedDelegatingHandler { StatusCodeToReturn = HttpStatusCode.OK };

            using (MockContext context = MockContext.Start(this.GetType().FullName))
            {
                var resourcesClient = CognitiveServicesManagementTestUtilities.GetResourceManagementClient(context, handler);
                var cognitiveServicesMgmtClient = CognitiveServicesManagementTestUtilities.GetCognitiveServicesManagementClient(context, handler);

                // Create resource group
                var rgname = CognitiveServicesManagementTestUtilities.CreateResourceGroup(resourcesClient);

                CognitiveServicesManagementTestUtilities.CreateAndValidateAccountWithOnlyRequiredParameters(cognitiveServicesMgmtClient, rgname, SkuName.S0, Kind.Academic, "westus");
                CognitiveServicesManagementTestUtilities.CreateAndValidateAccountWithOnlyRequiredParameters(cognitiveServicesMgmtClient, rgname, SkuName.S1, Kind.BingAutosuggest, "global");
                CognitiveServicesManagementTestUtilities.CreateAndValidateAccountWithOnlyRequiredParameters(cognitiveServicesMgmtClient, rgname, SkuName.S1, Kind.BingSearch, "global");
                CognitiveServicesManagementTestUtilities.CreateAndValidateAccountWithOnlyRequiredParameters(cognitiveServicesMgmtClient, rgname, SkuName.S0, Kind.BingSpeech, "global");
                CognitiveServicesManagementTestUtilities.CreateAndValidateAccountWithOnlyRequiredParameters(cognitiveServicesMgmtClient, rgname, SkuName.S1, Kind.BingSpellCheck, "global");
                CognitiveServicesManagementTestUtilities.CreateAndValidateAccountWithOnlyRequiredParameters(cognitiveServicesMgmtClient, rgname, SkuName.S1, Kind.ComputerVision, "westus");
                CognitiveServicesManagementTestUtilities.CreateAndValidateAccountWithOnlyRequiredParameters(cognitiveServicesMgmtClient, rgname, SkuName.S0, Kind.ContentModerator, "westus");
                CognitiveServicesManagementTestUtilities.CreateAndValidateAccountWithOnlyRequiredParameters(cognitiveServicesMgmtClient, rgname, SkuName.S0, Kind.Emotion, "westus");
                CognitiveServicesManagementTestUtilities.CreateAndValidateAccountWithOnlyRequiredParameters(cognitiveServicesMgmtClient, rgname, SkuName.S0, Kind.Face, "westus");
                CognitiveServicesManagementTestUtilities.CreateAndValidateAccountWithOnlyRequiredParameters(cognitiveServicesMgmtClient, rgname, SkuName.S0, Kind.LUIS, "westus");
                CognitiveServicesManagementTestUtilities.CreateAndValidateAccountWithOnlyRequiredParameters(cognitiveServicesMgmtClient, rgname, SkuName.S1, Kind.Recommendations, "westus");
                CognitiveServicesManagementTestUtilities.CreateAndValidateAccountWithOnlyRequiredParameters(cognitiveServicesMgmtClient, rgname, SkuName.S0, Kind.SpeakerRecognition, "westus");
                CognitiveServicesManagementTestUtilities.CreateAndValidateAccountWithOnlyRequiredParameters(cognitiveServicesMgmtClient, rgname, SkuName.S1, Kind.SpeechTranslation, "global");
                CognitiveServicesManagementTestUtilities.CreateAndValidateAccountWithOnlyRequiredParameters(cognitiveServicesMgmtClient, rgname, SkuName.S1, Kind.TextAnalytics, "westus");
                CognitiveServicesManagementTestUtilities.CreateAndValidateAccountWithOnlyRequiredParameters(cognitiveServicesMgmtClient, rgname, SkuName.S1, Kind.TextTranslation, "global");
                CognitiveServicesManagementTestUtilities.CreateAndValidateAccountWithOnlyRequiredParameters(cognitiveServicesMgmtClient, rgname, SkuName.S0, Kind.WebLM, "westus");
            }
        }

        [Fact]
        public void CognitiveServicesAccountDeleteTest()
        {
            var handler = new RecordedDelegatingHandler { StatusCodeToReturn = HttpStatusCode.OK };

            using (MockContext context = MockContext.Start(this.GetType().FullName))
            {
                var resourcesClient = CognitiveServicesManagementTestUtilities.GetResourceManagementClient(context, handler);
                var cognitiveServicesMgmtClient = CognitiveServicesManagementTestUtilities.GetCognitiveServicesManagementClient(context, handler);

                // Create resource group
                var rgname = CognitiveServicesManagementTestUtilities.CreateResourceGroup(resourcesClient);

                // Delete an account which does not exist
                cognitiveServicesMgmtClient.CognitiveServicesAccounts.Delete(rgname, "missingaccount");

                // Create cognitive services account
                string accountName = CognitiveServicesManagementTestUtilities.CreateCognitiveServicesAccount(cognitiveServicesMgmtClient, rgname);

                // Delete an account
                cognitiveServicesMgmtClient.CognitiveServicesAccounts.Delete(rgname, accountName);

                // Delete an account which was just deleted
                cognitiveServicesMgmtClient.CognitiveServicesAccounts.Delete(rgname, accountName);
            }
        }

        [Fact]
        public void CognitiveServicesAccountCreateAndGetDifferentSkusTest()
        {
            var handler = new RecordedDelegatingHandler { StatusCodeToReturn = HttpStatusCode.OK };

            using (MockContext context = MockContext.Start(this.GetType().FullName))
            {
                var resourcesClient = CognitiveServicesManagementTestUtilities.GetResourceManagementClient(context, handler);
                var cognitiveServicesMgmtClient = CognitiveServicesManagementTestUtilities.GetCognitiveServicesManagementClient(context, handler);

                // Create resource group
                var rgname = CognitiveServicesManagementTestUtilities.CreateResourceGroup(resourcesClient);

                // Default parameters
                var f0Account = CognitiveServicesManagementTestUtilities.CreateAndValidateAccountWithOnlyRequiredParameters(cognitiveServicesMgmtClient, rgname, SkuName.F0, Kind.TextAnalytics);
                var s1Account = CognitiveServicesManagementTestUtilities.CreateAndValidateAccountWithOnlyRequiredParameters(cognitiveServicesMgmtClient, rgname, SkuName.S1, Kind.TextAnalytics);

                var f0Properties = cognitiveServicesMgmtClient.CognitiveServicesAccounts.GetProperties(rgname, f0Account.Name);
                Assert.Equal(SkuName.F0, f0Properties.Sku.Name);
                Assert.Equal(Kind.TextAnalytics.ToString(), f0Properties.Kind);


                var s1Properties = cognitiveServicesMgmtClient.CognitiveServicesAccounts.GetProperties(rgname, s1Account.Name);
                Assert.Equal(SkuName.S1, s1Properties.Sku.Name);
                Assert.Equal(Kind.TextAnalytics.ToString(), s1Properties.Kind);
            }
        }

        [Fact]
        public void CognitiveServicesAccountListByResourceGroupTest()
        {
            var handler = new RecordedDelegatingHandler { StatusCodeToReturn = HttpStatusCode.OK };

            using (MockContext context = MockContext.Start(this.GetType().FullName))
            {
                var resourcesClient = CognitiveServicesManagementTestUtilities.GetResourceManagementClient(context, handler);
                var cognitiveServicesMgmtClient = CognitiveServicesManagementTestUtilities.GetCognitiveServicesManagementClient(context, handler);

                // Create resource group
                var rgname = CognitiveServicesManagementTestUtilities.CreateResourceGroup(resourcesClient);

                var accounts = cognitiveServicesMgmtClient.Accounts.ListByResourceGroup(rgname);
                Assert.Empty(accounts);

                // Create cognitive services accounts
                string accountName1 = CognitiveServicesManagementTestUtilities.CreateCognitiveServicesAccount(cognitiveServicesMgmtClient, rgname);
                string accountName2 = CognitiveServicesManagementTestUtilities.CreateCognitiveServicesAccount(cognitiveServicesMgmtClient, rgname);

                accounts = cognitiveServicesMgmtClient.Accounts.ListByResourceGroup(rgname);
                Assert.Equal(2, accounts.Count());

                CognitiveServicesManagementTestUtilities.VerifyAccountProperties(accounts.First(), true);
                CognitiveServicesManagementTestUtilities.VerifyAccountProperties(accounts.Skip(1).First(), true);
            }
        }

        [Fact]
        public void CognitiveServicesAccountListBySubscriptionTest()
        {
            var handler = new RecordedDelegatingHandler { StatusCodeToReturn = HttpStatusCode.OK };

            using (MockContext context = MockContext.Start(this.GetType().FullName))
            {
                var resourcesClient = CognitiveServicesManagementTestUtilities.GetResourceManagementClient(context, handler);
                var cognitiveServicesMgmtClient = CognitiveServicesManagementTestUtilities.GetCognitiveServicesManagementClient(context, handler);


                // Create resource group and cognitive services account
                var rgname1 = CognitiveServicesManagementTestUtilities.CreateResourceGroup(resourcesClient);
                string accountName1 = CognitiveServicesManagementTestUtilities.CreateCognitiveServicesAccount(cognitiveServicesMgmtClient, rgname1);

                // Create different resource group and cognitive account
                var rgname2 = CognitiveServicesManagementTestUtilities.CreateResourceGroup(resourcesClient);
                string accountName2 = CognitiveServicesManagementTestUtilities.CreateCognitiveServicesAccount(cognitiveServicesMgmtClient, rgname2);

                var accounts = cognitiveServicesMgmtClient.Accounts.List();

                Assert.True(accounts.Count() >= 2);

                CognitiveServicesAccount account1 = accounts.First(
                    t => StringComparer.OrdinalIgnoreCase.Equals(t.Name, accountName1));
                CognitiveServicesManagementTestUtilities.VerifyAccountProperties(account1, true);

                CognitiveServicesAccount account2 = accounts.First(
                    t => StringComparer.OrdinalIgnoreCase.Equals(t.Name, accountName2));
                CognitiveServicesManagementTestUtilities.VerifyAccountProperties(account2, true);
            }
        }

        [Fact]
        public void CognitiveServicesAccountListKeysTest()
        {
            var handler = new RecordedDelegatingHandler { StatusCodeToReturn = HttpStatusCode.OK };

            using (MockContext context = MockContext.Start(this.GetType().FullName))
            {
                var resourcesClient = CognitiveServicesManagementTestUtilities.GetResourceManagementClient(context, handler);
                var cognitiveServicesMgmtClient = CognitiveServicesManagementTestUtilities.GetCognitiveServicesManagementClient(context, handler);

                // Create resource group
                string rgname = CognitiveServicesManagementTestUtilities.CreateResourceGroup(resourcesClient);

                // Create cognitive services account
                string accountName = CognitiveServicesManagementTestUtilities.CreateCognitiveServicesAccount(cognitiveServicesMgmtClient, rgname);

                // List keys
                var keys = cognitiveServicesMgmtClient.CognitiveServicesAccounts.ListKeys(rgname, accountName);
                Assert.NotNull(keys);

                // Validate Key1
                Assert.NotNull(keys.Key1);
                // Validate Key2
                Assert.NotNull(keys.Key2);
            }
        }

        [Fact]
        public void CognitiveServicesAccountRegenerateKeyTest()
        {
            var handler = new RecordedDelegatingHandler { StatusCodeToReturn = HttpStatusCode.OK };

            using (MockContext context = MockContext.Start(this.GetType().FullName))
            {
                var resourcesClient = CognitiveServicesManagementTestUtilities.GetResourceManagementClient(context, handler);
                var cognitiveServicesMgmtClient = CognitiveServicesManagementTestUtilities.GetCognitiveServicesManagementClient(context, handler);

                // Create resource group
                string rgname = CognitiveServicesManagementTestUtilities.CreateResourceGroup(resourcesClient);

                // Create cognitive services account
                string accountName = CognitiveServicesManagementTestUtilities.CreateCognitiveServicesAccount(cognitiveServicesMgmtClient, rgname);

                // List keys
                var keys = cognitiveServicesMgmtClient.CognitiveServicesAccounts.ListKeys(rgname, accountName);
                Assert.NotNull(keys);
                var key2 = keys.Key2;
                Assert.NotNull(key2);

                // Regenerate keys and verify that keys change
                var regenKeys = cognitiveServicesMgmtClient.CognitiveServicesAccounts.RegenerateKey(rgname, accountName, KeyName.Key2);
                var key2Regen = regenKeys.Key2;
                Assert.NotNull(key2Regen);

                // Validate key was regenerated
                Assert.NotEqual(key2, key2Regen);
            }
        }

        [Fact]
        public void CognitiveServicesAccountUpdateWithCreateTest()
        {
            var handler = new RecordedDelegatingHandler { StatusCodeToReturn = HttpStatusCode.OK };

            using (MockContext context = MockContext.Start(this.GetType().FullName))
            {
                var resourcesClient = CognitiveServicesManagementTestUtilities.GetResourceManagementClient(context, handler);
                var cognitiveServicesMgmtClient = CognitiveServicesManagementTestUtilities.GetCognitiveServicesManagementClient(context, handler);

                // Create resource group
                var rgname = CognitiveServicesManagementTestUtilities.CreateResourceGroup(resourcesClient);

                // Create cognitive services account
                var createdAccount = CognitiveServicesManagementTestUtilities.CreateAndValidateAccountWithOnlyRequiredParameters(cognitiveServicesMgmtClient, rgname, SkuName.S2, Kind.Recommendations);
                var accountName = createdAccount.Name;

                // Update SKU 
                var account = cognitiveServicesMgmtClient.CognitiveServicesAccounts.Update(rgname, accountName, new Sku { Name = SkuName.S1 });
                Assert.Equal(SkuName.S1, account.Sku.Name);

                // Validate
                var fetchedAccount = cognitiveServicesMgmtClient.CognitiveServicesAccounts.GetProperties(rgname, accountName);
                Assert.Equal(SkuName.S1, fetchedAccount.Sku.Name);

                var newTags = new Dictionary<string, string>
                {
                    {"key3","value3"},
                    {"key4","value4"},
                    {"key5","value5"}
                };

                // Update account tags
                account = cognitiveServicesMgmtClient.CognitiveServicesAccounts.Update(rgname, accountName, null, newTags);
                Assert.Equal(newTags.Count, account.Tags.Count);
                // Validate
                fetchedAccount = cognitiveServicesMgmtClient.CognitiveServicesAccounts.GetProperties(rgname, accountName);
                Assert.Equal(SkuName.S1, fetchedAccount.Sku.Name);
                Assert.Equal(newTags.Count, fetchedAccount.Tags.Count());
                Assert.Collection(fetchedAccount.Tags,
                    (keyValue) => { Assert.Equal("key3", keyValue.Key); Assert.Equal("value3", keyValue.Value); },
                    (keyValue) => { Assert.Equal("key4", keyValue.Key); Assert.Equal("value4", keyValue.Value); },
                    (keyValue) => { Assert.Equal("key5", keyValue.Key); Assert.Equal("value5", keyValue.Value); }
                );
            }
        }

        [Fact]
        public void CognitiveServicesAccountEnumerateSkusTest()
        {
            var handler = new RecordedDelegatingHandler { StatusCodeToReturn = HttpStatusCode.OK };

            using (MockContext context = MockContext.Start(this.GetType().FullName))
            {
                var resourcesClient = CognitiveServicesManagementTestUtilities.GetResourceManagementClient(context, handler);
                var cognitiveServicesMgmtClient = CognitiveServicesManagementTestUtilities.GetCognitiveServicesManagementClient(context, handler);

                // Create resource group
                var rgname = CognitiveServicesManagementTestUtilities.CreateResourceGroup(resourcesClient);

                // Create cognitive services account
                var createdAccount = CognitiveServicesManagementTestUtilities.CreateAndValidateAccountWithOnlyRequiredParameters(cognitiveServicesMgmtClient, rgname, SkuName.S3, Kind.Recommendations);
                var accountName = createdAccount.Name;

                // Enumerate SKUs

                var skuList = cognitiveServicesMgmtClient.CognitiveServicesAccounts.ListSkus(rgname, accountName);

                Assert.Equal(1, skuList.Value.Select(x => x.ResourceType).Distinct().Count());

                Assert.Equal($"{c_resourceNamespace}/{c_resourceType}", skuList.Value.Select(x => x.ResourceType).First());

                Assert.Collection(skuList.Value.Select(x => x.Sku),
                    (sku) => { Assert.Equal(SkuName.F0, sku.Name); Assert.Equal(SkuTier.Free, sku.Tier); },
                    (sku) => { Assert.Equal(SkuName.S1, sku.Name); Assert.Equal(SkuTier.Standard, sku.Tier); },
                    (sku) => { Assert.Equal(SkuName.S2, sku.Name); Assert.Equal(SkuTier.Standard, sku.Tier); },
                    (sku) => { Assert.Equal(SkuName.S3, sku.Name); Assert.Equal(SkuTier.Standard, sku.Tier); },
                    (sku) => { Assert.Equal(SkuName.S4, sku.Name); Assert.Equal(SkuTier.Standard, sku.Tier); }
                );
            }
        }

        //   Negative Scenarios Tests
        [Fact]
        public void CognitiveServicesCreateAccountErrorTest()
        {
            var handler = new RecordedDelegatingHandler { StatusCodeToReturn = HttpStatusCode.OK };

            using (MockContext context = MockContext.Start(this.GetType().FullName))
            {
                var resourcesClient = CognitiveServicesManagementTestUtilities.GetResourceManagementClient(context, handler);
                var cognitiveServicesMgmtClient = CognitiveServicesManagementTestUtilities.GetCognitiveServicesManagementClient(context, handler);

                // Create resource group
                var rgname = CognitiveServicesManagementTestUtilities.CreateResourceGroup(resourcesClient);

                var accountName = TestUtilities.GenerateName("csa");
                var parameters = new CognitiveServicesAccountCreateParameters
                {
                    Sku = new Sku { Name = SkuName.F0 },
                    Kind = Kind.ComputerVision,
                    Location = CognitiveServicesManagementTestUtilities.DefaultLocation,
                    Properties = new object(),
                };

                CognitiveServicesManagementTestUtilities.ValidateExpectedException(
                    () => cognitiveServicesMgmtClient.CognitiveServicesAccounts.Create("NotExistedRG", accountName, parameters),
                    "ResourceGroupNotFound");

                parameters.Location = "BLA";
                CognitiveServicesManagementTestUtilities.ValidateExpectedException(
                    () => cognitiveServicesMgmtClient.CognitiveServicesAccounts.Create(rgname, accountName, parameters),
                    "LocationNotAvailableForResourceType");
            }
        }

        [Fact]
        public void CognitiveServicesCreateAccountErrorTest2()
        {
            var handler = new RecordedDelegatingHandler { StatusCodeToReturn = HttpStatusCode.OK };

            using (MockContext context = MockContext.Start(this.GetType().FullName))
            {
                var resourcesClient = CognitiveServicesManagementTestUtilities.GetResourceManagementClient(context, handler);
                var cognitiveServicesMgmtClient = CognitiveServicesManagementTestUtilities.GetCognitiveServicesManagementClient(context, handler);

                // Create resource group
                var rgname = CognitiveServicesManagementTestUtilities.CreateResourceGroup(resourcesClient);

                var accountName = TestUtilities.GenerateName("csa");
                var nonExistApiPara = new CognitiveServicesAccountCreateParameters
                {
                    Sku = new Sku { Name = SkuName.F0 },
                    Kind = "NonExistAPI",
                    Location = CognitiveServicesManagementTestUtilities.DefaultLocation,
                    Properties = new object(),
                };

                var nonExistSkuPara = new CognitiveServicesAccountCreateParameters
                {
                    Sku = new Sku { Name = "N0" },
                    Kind = Kind.Academic,
                    Location = CognitiveServicesManagementTestUtilities.DefaultLocation,
                    Properties = new object(),
                };
                
                CognitiveServicesManagementTestUtilities.ValidateExpectedException(
                    () => cognitiveServicesMgmtClient.CognitiveServicesAccounts.Create(rgname, accountName, nonExistApiPara),
                    "InvalidApiSetId");

                CognitiveServicesManagementTestUtilities.ValidateExpectedException(
                    () => cognitiveServicesMgmtClient.CognitiveServicesAccounts.Create(rgname, accountName, nonExistSkuPara),
                    "InvalidSkuId");
            }
        }

        [Fact]
        public void CognitiveServicesGetAccountErrorTest()
        {
            var handler = new RecordedDelegatingHandler { StatusCodeToReturn = HttpStatusCode.OK };

            using (MockContext context = MockContext.Start(this.GetType().FullName))
            {
                var resourcesClient = CognitiveServicesManagementTestUtilities.GetResourceManagementClient(context, handler);
                var cognitiveServicesMgmtClient = CognitiveServicesManagementTestUtilities.GetCognitiveServicesManagementClient(context, handler);

                // Create resource group
                var rgname = CognitiveServicesManagementTestUtilities.CreateResourceGroup(resourcesClient);

                CognitiveServicesManagementTestUtilities.ValidateExpectedException(
                    () => cognitiveServicesMgmtClient.CognitiveServicesAccounts.GetProperties("NotExistedRG", "nonExistedAccountName"),
                    "ResourceGroupNotFound");

                CognitiveServicesManagementTestUtilities.ValidateExpectedException(
                    () => cognitiveServicesMgmtClient.CognitiveServicesAccounts.GetProperties(rgname, "nonExistedAccountName"),
                    "ResourceNotFound");

                CognitiveServicesManagementTestUtilities.ValidateExpectedException(
                    () => cognitiveServicesMgmtClient.Accounts.ListByResourceGroup("NotExistedRG"),
                    "ResourceGroupNotFound");
            }
        }

        [Fact]
        public void CognitiveServicesUpdateAccountErrorTest()
        {
            var handler = new RecordedDelegatingHandler { StatusCodeToReturn = HttpStatusCode.OK };

            using (MockContext context = MockContext.Start(this.GetType().FullName))
            {
                var resourcesClient = CognitiveServicesManagementTestUtilities.GetResourceManagementClient(context, handler);
                var cognitiveServicesMgmtClient = CognitiveServicesManagementTestUtilities.GetCognitiveServicesManagementClient(context, handler);

                // Create resource group
                var rgname = CognitiveServicesManagementTestUtilities.CreateResourceGroup(resourcesClient);

                // Create cognitive services account
                var createdAccount = CognitiveServicesManagementTestUtilities.CreateAndValidateAccountWithOnlyRequiredParameters(cognitiveServicesMgmtClient, rgname, SkuName.S2, Kind.Recommendations);
                var accountName = createdAccount.Name;

                // try to update non-existent account
                CognitiveServicesManagementTestUtilities.ValidateExpectedException(
                    () => cognitiveServicesMgmtClient.CognitiveServicesAccounts.Update("NotExistedRG", "nonExistedAccountName"),
                    "ResourceGroupNotFound");

                CognitiveServicesManagementTestUtilities.ValidateExpectedException(
                    () => cognitiveServicesMgmtClient.CognitiveServicesAccounts.Update(rgname, "nonExistedAccountName"),
                    "ResourceNotFound");

                // Update with a SKU which doesn't exist
                CognitiveServicesManagementTestUtilities.ValidateExpectedException(
                    () => cognitiveServicesMgmtClient.CognitiveServicesAccounts.Update(rgname, accountName, new Sku(SkuName.S0)),
                    "InvalidSkuId");
            }
        }

        [Fact]
        public void CognitiveServicesDeleteAccountErrorTest()
        {
            var handler = new RecordedDelegatingHandler { StatusCodeToReturn = HttpStatusCode.OK };

            using (MockContext context = MockContext.Start(this.GetType().FullName))
            {
                var resourcesClient = CognitiveServicesManagementTestUtilities.GetResourceManagementClient(context, handler);
                var cognitiveServicesMgmtClient = CognitiveServicesManagementTestUtilities.GetCognitiveServicesManagementClient(context, handler);

                // Create resource group
                var rgname = CognitiveServicesManagementTestUtilities.CreateResourceGroup(resourcesClient);

                // try to delete non-existent account
                CognitiveServicesManagementTestUtilities.ValidateExpectedException(
                    () => cognitiveServicesMgmtClient.CognitiveServicesAccounts.Delete("NotExistedRG", "nonExistedAccountName"),
                    "ResourceGroupNotFound");
            }
        }

        [Fact]
        public void CognitiveServicesAccountKeysErrorTest()
        {
            var handler = new RecordedDelegatingHandler { StatusCodeToReturn = HttpStatusCode.OK };

            using (MockContext context = MockContext.Start(this.GetType().FullName))
            {
                var resourcesClient = CognitiveServicesManagementTestUtilities.GetResourceManagementClient(context, handler);
                var cognitiveServicesMgmtClient = CognitiveServicesManagementTestUtilities.GetCognitiveServicesManagementClient(context, handler);

                // Create resource group
                var rgname = CognitiveServicesManagementTestUtilities.CreateResourceGroup(resourcesClient);

                CognitiveServicesManagementTestUtilities.ValidateExpectedException(
                    () => cognitiveServicesMgmtClient.CognitiveServicesAccounts.ListKeys("NotExistedRG", "nonExistedAccountName"),
                    "ResourceGroupNotFound");

                CognitiveServicesManagementTestUtilities.ValidateExpectedException(
                    () => cognitiveServicesMgmtClient.CognitiveServicesAccounts.ListKeys(rgname, "nonExistedAccountName"),
                    "ResourceNotFound");
            }
        }

        [Fact]
        public void CognitiveServicesEnumerateSkusErrorTest()
        {
            var handler = new RecordedDelegatingHandler { StatusCodeToReturn = HttpStatusCode.OK };

            using (MockContext context = MockContext.Start(this.GetType().FullName))
            {
                var resourcesClient = CognitiveServicesManagementTestUtilities.GetResourceManagementClient(context, handler);
                var cognitiveServicesMgmtClient = CognitiveServicesManagementTestUtilities.GetCognitiveServicesManagementClient(context, handler);

                // Create resource group
                var rgname = CognitiveServicesManagementTestUtilities.CreateResourceGroup(resourcesClient);

                CognitiveServicesManagementTestUtilities.ValidateExpectedException(
                    () => cognitiveServicesMgmtClient.CognitiveServicesAccounts.ListSkus("NotExistedRG", "nonExistedAccountName"),
                    "ResourceGroupNotFound");

                CognitiveServicesManagementTestUtilities.ValidateExpectedException(
                    () => cognitiveServicesMgmtClient.CognitiveServicesAccounts.ListSkus(rgname, "nonExistedAccountName"),
                    "ResourceNotFound");
            }
        }

        [Fact]
        public void CognitiveServicesCheckSkuAvailabilityTest()
        {
            var handler = new RecordedDelegatingHandler { StatusCodeToReturn = HttpStatusCode.OK };

            using (MockContext context = MockContext.Start(this.GetType().FullName))
            {
                var resourcesClient = CognitiveServicesManagementTestUtilities.GetResourceManagementClient(context, handler);
                var cognitiveServicesMgmtClient = CognitiveServicesManagementTestUtilities.GetCognitiveServicesManagementClient(context, handler);

                cognitiveServicesMgmtClient.Location = "westus";

                var skus = cognitiveServicesMgmtClient.CheckSkuAvailability.List(
                    skus: new List<string>() { SkuName.S0 },
                    kind: Kind.Face,
                    type: $"{c_resourceNamespace}/{c_resourceType}");

                Assert.NotNull(skus);
                Assert.NotNull(skus.Value);
                Assert.True(skus.Value.Count > 0);
            }
        }

        [Fact]
        public void CognitiveServicesAccountMinMaxNameLengthTest()
        {
            var handler = new RecordedDelegatingHandler { StatusCodeToReturn = HttpStatusCode.OK };

            using (MockContext context = MockContext.Start(this.GetType().FullName))
            {
                var resourcesClient = CognitiveServicesManagementTestUtilities.GetResourceManagementClient(context, handler);
                var cognitiveServicesMgmtClient = CognitiveServicesManagementTestUtilities.GetCognitiveServicesManagementClient(context, handler);

                // Create resource group
                var rgname = CognitiveServicesManagementTestUtilities.CreateResourceGroup(resourcesClient);

                var parameters = new CognitiveServicesAccountCreateParameters
                {
                    Sku = new Sku { Name = SkuName.S0 },
                    Kind = Kind.Academic,
                    Location = CognitiveServicesManagementTestUtilities.DefaultLocation,
                    Properties = new object(),
                };

                var minName = "zz";
                var maxName = "AcadAcadAcadAcadAcadAcadAcadAcadAcadAcadAcadAcadAcadAcadAcadAcad";

                var minAccount = cognitiveServicesMgmtClient.CognitiveServicesAccounts.Create(rgname, minName, parameters);
                var maxAccount = cognitiveServicesMgmtClient.CognitiveServicesAccounts.Create(rgname, maxName, parameters);

                Assert.Equal(minName, minAccount.Name);
                Assert.Equal(maxName, maxAccount.Name);
            }
        }
    }
}