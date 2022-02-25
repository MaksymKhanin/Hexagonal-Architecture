// This code is under Copyright (C) 2021 of Maksym Khanin SAS all right reserved

using ArchUnitNET.Domain;
using ArchUnitNET.Fluent;
using ArchUnitNET.Loader;
using ArchUnitNET.xUnit;
using Xunit;

using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace HexagonalApi.Tests.Architecture
{
    public class ArchitectureRulesCheck
    {
        private static readonly ArchUnitNET.Domain.Architecture Architecture =
            new ArchLoader().LoadAssemblies(
                typeof(Adapters.Api.Startup).Assembly,
                typeof(Adapters.Persistence.Configuration.Dependencies).Assembly,
                typeof(Adapters.ThirdPartyService.Configuration.Dependencies).Assembly,
                typeof(HexagonalApi.ReadModel.QueryHandler).Assembly,
                typeof(Business.Class1).Assembly
            ).Build();

        private static readonly IObjectProvider<IType> ApiLayer =
            Types().That().ResideInNamespace(typeof(Adapters.Api.Program).Namespace).As("Api layer");

        private static readonly IObjectProvider<IType> Domain =
            Types().That().ResideInNamespace(typeof(Business.Class1).Namespace).As("Domain");

        private static readonly IObjectProvider<IType> Adapters =
            Types().That()
            .ResideInNamespace(nameof(HexagonalApi.Adapters.ThirdPartyService))
            .Or().ResideInNamespace(nameof(HexagonalApi.Adapters.Persistence))
            .As("Adapters");

        private static readonly IObjectProvider<IType> ReadModel =
            Types().That().ResideInNamespace(nameof(HexagonalApi.ReadModel)).As("ReadModel");

        [Fact(DisplayName = "Business forbidden dependencies")]
        public void Test01() =>
            Types().That().Are(Domain)
            .Should().NotDependOnAny(Types().That().Are(Adapters))
            .AndShould().NotDependOnAny(Types().That().Are(ApiLayer))
            .AndShould().NotDependOnAny(Types().That().Are(ReadModel))
            .Because("A domain dependency is forbidden")
            .Check(Architecture);


        [Fact(DisplayName = "ReadModel forbidden dependencies")]
        public void Test03() =>
            Types().That().Are(ReadModel)
            .Should().NotDependOnAny(Domain)
            .AndShould().NotDependOnAny(Adapters)
            .AndShould().NotDependOnAny(ApiLayer)
            .Because("A ReadModel dependency is forbidden")
            .Check(Architecture);
    }
}
