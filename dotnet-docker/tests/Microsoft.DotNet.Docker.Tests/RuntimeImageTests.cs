﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.DotNet.Docker.Tests
{
    [Trait("Category", "runtime")]
    public class RuntimeImageTests : CommonRuntimeImageTests
    {
        public RuntimeImageTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        protected override DotNetImageType ImageType => DotNetImageType.Runtime;

        [DotNetTheory]
        [MemberData(nameof(GetImageData))]
        public async Task VerifyAppScenario(ProductImageData imageData)
        {
            // Skip test for Arm32 Alpine 3.13 due to https://github.com/dotnet/runtime/issues/47423
            if (imageData.OS == "alpine3.13" && imageData.Arch == Arch.Arm)
            {
                return;
            }

            ImageScenarioVerifier verifier = new ImageScenarioVerifier(imageData, DockerHelper, OutputHelper);
            await verifier.Execute();
        }

        [DotNetTheory]
        [MemberData(nameof(GetImageData))]
        public void VerifyEnvironmentVariables(ProductImageData imageData)
        {
            List<EnvironmentVariableInfo> variables = new List<EnvironmentVariableInfo>();

            if (imageData.Version.Major >= 5 || (imageData.Version.Major == 2 && DockerHelper.IsLinuxContainerModeEnabled))
            {
                variables.Add(GetRuntimeVersionVariableInfo(imageData, DockerHelper));
            }

            base.VerifyCommonEnvironmentVariables(imageData, variables);
        }

        public static EnvironmentVariableInfo GetRuntimeVersionVariableInfo(ProductImageData imageData, DockerHelper dockerHelper)
        {
            string version = imageData.GetProductVersion(DotNetImageType.Runtime, dockerHelper);
            return new EnvironmentVariableInfo("DOTNET_VERSION", version);
        }
    }
}
