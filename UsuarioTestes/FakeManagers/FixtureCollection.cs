using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UsuarioTestes.FakeManagers
{
    [CollectionDefinition("Test")]
    public class FixtureCollection : ICollectionFixture<InjectFixture>
    {
    }
}
