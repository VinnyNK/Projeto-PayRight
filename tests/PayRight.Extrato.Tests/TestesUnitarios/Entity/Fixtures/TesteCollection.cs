using Xunit;

namespace PayRight.Extrato.Tests.TestesUnitarios.Entity.Fixtures;

[CollectionDefinition(nameof(TesteCollection))]
public class TesteCollection : ICollectionFixture<ContaCorrenteExtratoFixture>, ICollectionFixture<AtividadeFixture>
{ }